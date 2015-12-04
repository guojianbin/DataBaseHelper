using PetaPoco;
using System;
using System.Collections.Generic;

namespace MultiWeb
{
    public class PetaPcoHelperMini<T> where T : class
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private Database db { get; set; }

        /// <summary>
        /// PetaPcoHelper帮助类
        /// 构造函数会初始化连接
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <param name="providerName">数据库引擎</param>
        public PetaPcoHelperMini(string conn, string providerName)
        {
            db = new Database(conn, providerName);
        }

        #region 数据库帮助类调用方法

        /// <summary>
        /// 查询结果集
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">查询SQL</param>
        /// <returns>实体结果集</returns>
        public List<T> GetList<T>(string strSql)
        {
            List<T> list = db.Fetch<T>(strSql);
            return list;
        }

        /// <summary>
        /// 查询结果集，迭代方式
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="strSql">查询SQL</param>
        /// <returns>可迭代的结果集</returns>
        public IEnumerable<T> GetEnum<T>(string strSql)
        {
            IEnumerable<T> list = db.Query<T>(strSql);
            return list;
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="strSql">查询SQL</param>
        /// <returns>数量</returns>
        public long GetLong(string strSql)
        {
            return db.ExecuteScalar<long>(strSql);
        }

        /// <summary>
        /// 查询一条结果
        /// </summary>
        /// <param name="strSql">查询SQL</param>
        /// <returns>实体结果集</returns>
        public T GetDefault(string strSql)
        {
            return db.SingleOrDefault<T>(strSql);
        }

        /// <summary>
        /// 自动分页查询
        /// </summary>
        /// <param name="strSql">查询SQL</param>
        /// <param name="page">当前页</param>
        /// <param name="size">显示页数</param>
        /// <returns>分页后的结果集</returns>
        public List<T> GetList<T>(string strSql, int page, int size)
        {
            return db.Page<T>(page, size, strSql).Items;
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="strSql">查询SQL</param>
        /// <returns>受影响行数</returns>
        public int Execute(string strSql)
        {
            return db.Execute(strSql);
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKey">主键名</param>
        /// <returns>返回添加主键</returns>
        public object Add(T model, string tableName, string primaryKey)
        {
            return db.Insert(tableName, primaryKey, model);
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>返回添加主键</returns>
        public object Add(T model)
        {
            object result = "";
            using (var scope = db.GetTransaction())
            {
                try
                {
                    result = db.Insert(model);
                    scope.Complete();
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>返回修改主键</returns>
        public object Update(T model)
        {
            return db.Update(model);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>返回删除主键</returns>
        public object Delete(T model)
        {
            return db.Delete(model);
        }

        /// <summary>
        /// 是否为新增数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>是否新增</returns>
        public bool IsNew(T model)
        {
            return db.IsNew(model);
        }

        #endregion 数据库帮助类调用方法
    }
}