# DataBaseHelper
###　　　　　　　　　　　　Author:Chenxy
###　　　　　　　　　 E-mail:1025395601@qq.com
## 多数据库帮助类使用简介
* [程序结构](#jiegou)
* [功能点](#gongneng)
* [调用方法](#diaoyong)
* [完善计划](#jihua)

###<a name="jiegou"/>程序结构
* Helper
 * MySqlHelper
 * SqlHelper
 * OracleHelper
 * AccessHelper
 * SQLiteHelper
* HelperMini
 * MySqlHelperMini
 * SqlHelperMini
 * OracleHelperMini
 * AccessHelperMini
 * SQLiteHelperMini
* 调用程序控制台

###<a name="gongneng"/>功能点
Helper，为完整版数据库操作（尚未完成）。
HelperMini，为精简版数据库操作。
 包含功能点有：
* 增删改
* 查询返回DataSet
* 查询返回List泛型
* 存储过程调用
* 增删改,事务监听
* 参数化查询

###<a name="diaoyong"/>调用方法
需要先配置访问数据库连接，并在初始化中，提供配置文件根目录和连接名称。
不填连接名称，默认找对应帮助类的数据库类型+Config。例如：MySql数据库，默认配置文件为：MySqlConfig
引用DbHelper.HelperMini命名空间，随之调用其方法即可
```Java
初始化：MySqlHelperMini helper = new MySqlHelperMini(HelperConfigType.appSettings,"MySqlConfig");
执行增删改操作：helper.Run(Sql语句);
返回DataSet：DataSet ds = helper.RunToDataSet(Sql语句);
返回List：List<Model> list = helper.RunToList<Model>(Sql语句);
存储过程：MySqlParameter[] part = {
                                      new MySqlParameter("变量名称",MySqlDbType.VarChar,50) 
                                    };
          part[0].Value = "变量赋值";
          int proResult = helper.Run("存储过程名称",part);
 参数化查询：MySqlParameter[] par = {
                                    new MySqlParameter("参数名称",MySqlDbType.VarChar,50)
                                   };
            par[0].Value = "变量赋值";
            int addResult = helper.Run(Sql语句, CommandType.Text, par);
```
 返回List泛型，必须保证实体字段名称、类型与数据库相同。名称大小写均可。
 
 
###数据库连接示例：
    MySql
    <add name="MySqlConfig" providerName="MySql.Data.MySqlClient" connectionString="server=*.*.*.*;user id=***;password=***;database=***" />
    SqlServer
    <add name="SqlConfig" providerName="System.Data.SqlClient" connectionString="*,*,*,*;database=***;user id=***;password=***" />
    Oracle 
    <add name="ORAConnStr"  connectionString="data spurce=***;user id=***;Password=***;"/>
 	SQLite
    <add name="SQLiteConfig" providerName="System.Data.SQLite" connectionString="Data Source=Db\SQLiteDataBase.mdb" />
    Access
    <add name="AccessConfig" providerName="Microsoft.Jet.OLEDB.4.0" connectionString="Data Source=Db\AccessDataBase.mdb" />

[Oracle数据库配置教程](http://www.cnblogs.com/shengtianlong/archive/2010/07/03/1770447.html "Oracle配置教程")

###<a name="jihua"/>完善计划
* 对象关系型数据库
 * PostgreSQL（安装有点问题，先滞后）
* 非关系型数据库
 * Redis
 * MongoDB
