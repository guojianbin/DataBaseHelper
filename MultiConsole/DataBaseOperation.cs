using DbHelper;
//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :DataBaseOperation
// created by 陈星宇
// at 2015/07/10 9:51:27
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiConsole
{
    /// <summary>
    /// 抽象数据库操作
    /// 封装增删改查，存储过程，事务
    /// add by chenxy 2015/7/10
    /// </summary>
    public abstract class DataBaseOperation
    {
        //字符串
        private static string _SqlConnect;

        /// <summary>
        /// 获取字符串
        /// </summary>
        public string StrSqlConnect 
        {
            get {
                return _SqlConnect;
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="name">获取字符串名称</param>
        /// <returns>成功 0，失败 1 + 失败原因</returns>
        public string GetConnect(string name)
        {
            _SqlConnect = SqlHelper.GetConnSting(name);
            if (string.Equals(_SqlConnect,null))
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public abstract int Insert();

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public abstract int Delete();

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public abstract int Update();

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public abstract DataSet GetAll();

        /// <summary>
        /// 新增存储过程
        /// </summary>
        /// <returns></returns>
        public abstract int InsertProcedure();

        /// <summary>
        /// 查询存储过程
        /// </summary>
        /// <returns></returns>
        public abstract DataSet GetProcedure();
    }
}
