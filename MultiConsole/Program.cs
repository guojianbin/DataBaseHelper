﻿using DbHelper;
using DbHelper.HelperMini;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace MultiConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //OracleHelperMini Ohelper = new OracleHelperMini(HelperConfigType.appSettings);
            //var surl = Ohelper.RunToList<YewuKdhm>("select t.*, t.rowid from YEWU_KDHM t");
            //SQLiteHelperMini hel = new SQLiteHelperMini(HelperConfigType.appSettings);
            //var su = hel.RunToList<testUser>("select * from testuser ");

            //Console.Read();

            SqlHelperMini helper = new SqlHelperMini(HelperConfigType.appSettings);
            helper.Run("insert into [ChenxyDataBase].[dbo].[ChenxyCode](ID,CodeName,CodeNumber) values('2','1','2')");

            ////测试增加
            //Console.WriteLine("开始执行添加操作");
            //MySqlParameter[] par = {
            //                        new MySqlParameter("Name",MySqlDbType.VarChar,50)
            //                       };
            //par[0].Value = "wode";
            //int addResult = helper.Run("insert into dictionary_data VALUES('a','0',@Name,'a','0','0','asdf',12,'1111','1111')", CommandType.Text, par);
            //Console.WriteLine(string.Format("添加操作执行结果：{0}", addResult == 1 ? "成功" : "失败"));

            ////测试修改
            //Console.WriteLine("开始执行修改操作");
            //int updateResult = helper.Run("update dictionary_data set dict_name = 'a1' where dict_code = 'a'");
            //Console.WriteLine(string.Format("修改操作执行结果：{0}", updateResult == 1 ? "成功" : "失败"));
            //Console.WriteLine();

            ////测试删除
            //Console.WriteLine("开始执行删除操作");
            //int delResult = helper.Run("delete from dictionary_data where dict_code = 'a'");
            //Console.WriteLine(string.Format("删除操作执行结果：{0}", delResult == 1 ? "成功" : "失败"));
            //Console.WriteLine();

            ////测试查询返回DataSet
            //Console.WriteLine("开始执行查询返回DataSet操作");
            //DataSet ds = helper.RunToDataSet("select * from dictionary_data");
            //Console.WriteLine(string.Format("执行结果：{0}。返回条数：{1}", ds.Tables[0].Rows.Count > 0 ? "成功" : "失败", ds.Tables[0].Rows.Count));
            //Console.WriteLine();

            ////测试查询返回List泛型
            //Console.WriteLine("开始执行查询返回List泛型操作");
            //List<DictionaryData> list = helper.RunToList<DictionaryData>("select * from dictionary_data");
            //Console.WriteLine(string.Format("执行结果：{0}。返回条数：{1}", list.Count > 0 ? "成功" : "失败", list.Count));
            //Console.WriteLine();

            ////测试事务管理
            //Console.WriteLine("开始执行事务测试操作");
            //int trResult = helper.Run("insert into dictionary_data VALUES('a','0','a','a','0','0','asdf',12,'1111','1111');insert into dictionary_data VALUES('a','0','a','a','0','0','asdf',12,'1111','1111')");
            //Console.WriteLine("事务测试执行结果：{0}", trResult == -1 ? "成功" : "失败");
            //Console.WriteLine();

            ////测试存储过程，带返回值，带参数的存储过程
            //Console.WriteLine("开始测试带返回值的存储过程");
            //MySqlParameter[] part = {
            //                          new MySqlParameter("DictName",MySqlDbType.VarChar,50)
            //                        };
            //part[0].Value = "测试名称";
            //int proResult = helper.Run("AddDict", part, CommandType.StoredProcedure);
            //Console.WriteLine("带返回值的存储过程执行结果：{0}", proResult == 1 ? "成功" : "失败");
            //Console.WriteLine();

            ////测试存储过程，返回List
            //Console.WriteLine("开始测试存储过程返回List");
            //List<DictionaryData> prlist = helper.RunToList<DictionaryData>("GetDict", null, CommandType.StoredProcedure);
            //Console.WriteLine("存储过程返回List执行结果：{0}。返回条数：{1}", prlist.Count > 0 ? "成功" : "失败", prlist.Count);
            //Console.WriteLine();

            Console.ReadLine();
        }
    }
}