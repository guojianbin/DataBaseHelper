//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :MySqlHelperMini
// created by 陈星宇
// at 2015/09/18 10:49:56
//--------------------------------------------
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace DbHelper.HelperMini
{
    /// <summary>
    /// MySql数据库操作帮助类。
    /// 精简版，仅保存增删改查，存储过程，添加事务管理。
    /// 默认数据库连接字符串名称：MySqlConfig。
    /// </summary>
    public sealed class MySqlHelperMini
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string conn = "";

        /// <summary>
        /// 从Config里获取连接字符串。
        /// 字符串示例：name="MySqlConfig" providerName="MySql.Data.MySqlClient" connectionString="server=*.*.*.*;user id=***;password=***;database=***"
        /// </summary>
        /// <param name="type">读取配置文件根目录</param>
        /// <param name="strConfig">连接字符串名称。默认“MySqlConfig”</param>
        public MySqlHelperMini(HelperConfigType type, string strConfig = "MySqlConfig")
        {
            try
            {
                switch (type)
                {
                    case HelperConfigType.appSettings:
                        conn = ConfigurationManager.AppSettings[strConfig].ToString();
                        break;

                    case HelperConfigType.connectionStrings:
                        conn = ConfigurationManager.ConnectionStrings[strConfig].ConnectionString;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("未找到指定字符串。请检查字符串名称；" + ex);
            }
        }

        #region 数据库连接类模块功能

        /// <summary>
        /// 数据连接对象
        /// </summary>
        private static MySqlConnection connection;

        public static MySqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(conn);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                return MySqlHelperMini.connection;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void CloseConnection()
        {
            Connection.Close();
        }

        #endregion 数据库连接类模块功能

        #region 数据库命令类模块功能

        /// <summary>
        /// 生成命令类
        /// </summary>
        /// <returns></returns>
        private MySqlCommand CommandMethod(string strText, CommandType type, MySqlParameter[] pars = null)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = Connection;
            command.CommandText = strText;
            command.CommandType = type;
            if (pars != null)
                command.Parameters.AddRange(pars);
            return command;
        }

        /// <summary>
        /// 获取命令类，执行SQL
        /// </summary>
        /// <param name="strSql">执行SQL</param>
        /// <returns>命令类对象</returns>
        private MySqlCommand GetCommand(string strSql)
        {
            return CommandMethod(strSql, CommandType.Text);
        }

        /// <summary>
        /// 获取命令类，执行存储过程。不带参数
        /// </summary>
        /// <param name="proName">存储过程名称</param>
        /// <param name="type">命令类类型。输入CommandType.StoredProcedure</param>
        /// <returns>命令类对象</returns>
        private MySqlCommand GetCommand(string proName, CommandType type)
        {
            return CommandMethod(proName, type);
        }

        /// <summary>
        /// 获取命令类，执行存储过程。带参数
        /// </summary>
        /// <param name="proName">存储过程名字</param>
        /// <param name="pars">参数</param>
        /// <returns>命令类对象</returns>
        private MySqlCommand GetCommand(string proName, MySqlParameter[] pars, CommandType type)
        {
            return CommandMethod(proName, type, pars);
        }

        #endregion 数据库命令类模块功能

        #region 数据库帮助类调用方法

        /// <summary>
        /// 执行增、删、改操作。无需返回集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>执行结果</returns>
        public int Run(string strSql)
        {
            MySqlTransaction tran = Connection.BeginTransaction();
            MySqlCommand sqlCommand = GetCommand(strSql);
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
                tran.Commit();
                return result;
            }
            catch
            {
                tran.Rollback();
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行增、删、改操作。无需返回集合
        /// </summary>
        /// <param name="strProName">存储过程名称或Sql语句</param>
        /// <param name="type">类型</param>
        /// <param name="pars">可选参数，填写此参数。可以进行参数化Sql查询</param>
        /// <returns>执行结果</returns>
        public int Run(string strProName, CommandType type, MySqlParameter[] pars = null)
        {
            MySqlTransaction tran = Connection.BeginTransaction();
            MySqlCommand sqlCommand = null;
            if (pars == null)
                sqlCommand = GetCommand(strProName, type);
            else
                sqlCommand = GetCommand(strProName, pars, type);
            try
            {
                int result = sqlCommand.ExecuteNonQuery();
                tran.Commit();
                return result;
            }
            catch
            {
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// 执行命令，返回DataSet对象
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>DataSet集合</returns>
        public DataSet RunToDataSet(string strSql)
        {
            MySqlCommand cmd = GetCommand(strSql);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }

        /// <summary>
        /// 执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="strProName">存储过程</param>
        /// <param name="type">类型</param>
        /// <param name="pars">可选参数，填写此参数。可以进行参数化Sql查询</param>
        /// <returns>DataSet集合</returns>
        public DataSet RunToDataSet(string strProName, CommandType type, MySqlParameter[] pars = null)
        {
            MySqlCommand cmd = null;
            if (pars == null)
                cmd = GetCommand(strProName, type);
            else
                cmd = GetCommand(strProName, pars, type);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }

        /// <summary>
        /// 执行命令，返回List泛型对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="strSql">Sql语句</param>
        /// <returns>返回结果</returns>
        public List<T> RunToList<T>(string strSql) where T : class, new()
        {
            MySqlCommand cmd = GetCommand(strSql);
            MySqlDataReader re = cmd.ExecuteReader();
            var list = new List<T>();
            while (re.Read())
            {
                T ob = Activator.CreateInstance<T>();
                for (int i = 0; i < re.FieldCount; i++)
                {
                    PropertyInfo[] propertyInfo = ob.GetType().GetProperties();
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        if (info.Name.ToUpper().Equals(re.GetName(i).ToUpper()))
                        {
                            if (re[i] != DBNull.Value)
                            {
                                if (info.PropertyType.FullName == re[i].GetType().FullName)
                                {
                                    info.SetValue(ob, re[i], null);
                                }
                                else
                                {
                                    var resultObj = Convert.ChangeType(re[i], info.PropertyType);
                                    info.SetValue(ob, resultObj, null);
                                }
                            }
                            else
                            {
                                info.SetValue(ob, null, null);
                            }
                        }
                    }
                }
                list.Add(ob);
            }
            CloseConnection();
            return list;
        }

        /// <summary>
        /// 执行存储过程，返回List泛型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="strProName">存储过程名称或Sql语句</param>
        /// <param name="type">类型</param>
        /// <param name="pars">可选参数，填写此参数。可以进行参数化Sql查询</param>
        /// <returns>List泛型集合</returns>
        public List<T> RunToList<T>(string strProName, CommandType type, MySqlParameter[] pars = null) where T : class, new()
        {
            MySqlCommand cmd = null;
            if (pars == null)
                cmd = GetCommand(strProName, type);
            else
                cmd = GetCommand(strProName, pars, type);
            MySqlDataReader re = cmd.ExecuteReader();
            var list = new List<T>();
            while (re.Read())
            {
                T ob = Activator.CreateInstance<T>();
                for (int i = 0; i < re.FieldCount; i++)
                {
                    PropertyInfo[] propertyInfo = ob.GetType().GetProperties();
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        if (info.Name.ToUpper().Equals(re.GetName(i).ToUpper()))
                        {
                            if (re[i] != DBNull.Value)
                            {
                                if (info.PropertyType.FullName == re[i].GetType().FullName)
                                {
                                    info.SetValue(ob, re[i], null);
                                }
                                else
                                {
                                    var resultObj = Convert.ChangeType(re[i], info.PropertyType);
                                    info.SetValue(ob, resultObj, null);
                                }
                            }
                            else
                            {
                                info.SetValue(ob, null, null);
                            }
                        }
                    }
                }
                list.Add(ob);
            }
            CloseConnection();
            return list;
        }

        #endregion 数据库帮助类调用方法
    }
}