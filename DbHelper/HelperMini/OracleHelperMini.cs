//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :OracleHelperMini
// created by 陈星宇
// at 2015/09/18 11:59:19
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;


namespace DbHelper.HelperMini
{
    /// <summary>
    /// Oracle数据库操作帮助类。
    /// 精简版，仅保存增删改查，存储过程，添加事务管理。
    /// 默认数据库连接字符串名称：OracleConfig。
    /// </summary>
    public class OracleHelperMini
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string conn = "";

        /// <summary>
        /// 从Config里获取连接字符串
        /// 字符串示例：DATA SOURCE=*.*.*.*;USER ID=***;PASSWORD=***;
        /// </summary>
        /// <param name="type">读取配置文件根目录</param>
        /// <param name="strConfig">连接字符串名称。默认“OracleConfig”</param>
        public OracleHelperMini(HelperConfigType type, string strConfig = "OracleConfig")
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
                connection = new OracleConnection(conn);
                connection.Open();
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
        private static OracleConnection connection;
        public static OracleConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new OracleConnection(conn);
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
                return OracleHelperMini.connection;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void CloseConnection()
        {
            Connection.Close();
        }
        #endregion

        #region 数据库命令类模块功能
        /// <summary>
        /// 生成命令类
        /// </summary>
        /// <returns></returns>
        private OracleCommand CommandMethod(string strText, CommandType type, OracleParameter[] pars = null)
        {
            OracleCommand command = new OracleCommand();
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
        private OracleCommand GetCommand(string strSql)
        {
            return CommandMethod(strSql, CommandType.Text);
        }

        /// <summary>
        /// 获取命令类，执行存储过程。不带参数
        /// </summary>
        /// <param name="proName">存储过程名称</param>
        /// <param name="type">命令类类型。输入CommandType.StoredProcedure</param>
        /// <returns>命令类对象</returns>
        private OracleCommand GetCommand(string proName, CommandType type)
        {
            return CommandMethod(proName, type);
        }

        /// <summary>
        /// 获取命令类，执行存储过程。带参数
        /// </summary>
        /// <param name="proName">存储过程名字</param>
        /// <param name="pars">参数</param>
        /// <returns>命令类对象</returns>
        private OracleCommand GetCommand(string proName, OracleParameter[] pars)
        {
            return CommandMethod(proName, CommandType.StoredProcedure, pars);
        }
        #endregion

        #region 数据库帮助类调用方法
        /// <summary>
        /// 执行增、删、改操作。无需返回集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>执行结果</returns>
        public int Run(string strSql)
        {
            OracleTransaction tran = Connection.BeginTransaction();
            OracleCommand sqlCommand = GetCommand(strSql);
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
        /// <param name="strProName">存储过程名称</param>
        /// <param name="pars">存储过程参数</param>
        /// <returns>执行结果</returns>
        public int Run(string strProName, OracleParameter[] pars)
        {
            OracleTransaction tran = Connection.BeginTransaction();
            OracleCommand sqlCommand = GetCommand(strProName, pars);
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
            OracleCommand cmd = GetCommand(strSql);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            CloseConnection();
            return ds;
        }
        /// <summary>
        /// 执行存储过程，返回DataSet对象
        /// </summary>
        /// <param name="strProName">存储过程</param>
        /// <param name="pars">参数</param>
        /// <returns>DataSet集合</returns>
        public DataSet RunToDataSet(string strProName, OracleParameter[] pars)
        {
            OracleCommand cmd = GetCommand(strProName, pars);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
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
        public List<T> RunToList<T>(string strSql) where T : class,new()
        {
            OracleCommand cmd = GetCommand(strSql);
            OracleDataReader re = cmd.ExecuteReader();
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
                                else {
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
        /// <param name="strProName">存储过程名称</param>
        /// <returns></returns>
        public List<T> RunToList<T>(string strProName, OracleParameter[] pars) where T : class,new()
        {
            OracleCommand cmd = GetCommand(strProName, pars);
            OracleDataReader re = cmd.ExecuteReader();
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
        #endregion
    }
}
