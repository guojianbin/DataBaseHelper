using DbHelper;
//--------------------------------------------
// Copyright (C) 北京日升天信科技股份有限公司
// filename :SqlServerDataBase
// created by 陈星宇
// at 2015/07/10 9:59:44
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiConsole
{
    public class SqlServerDataBase : DataBaseOperation
    {
        public SqlServerDataBase() {
            GetConnect("SqlCon");
        }

        public override int Insert()
        {
            throw new NotImplementedException();
        }

        public override int Delete()
        {
            throw new NotImplementedException();
        }

        public override int Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取记录
        /// </summary>
        /// <returns></returns>
        public override DataSet GetAll()
        {
            try
            {
                return SqlHelper.ExecuteDataset(StrSqlConnect, CommandType.Text, "select * from dbo.EmployType");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override int InsertProcedure()
        {
            throw new NotImplementedException();
        }

        public override DataSet GetProcedure()
        {
            throw new NotImplementedException();
        }
    }
}
