# DataBaseHelper
###多数据库帮助类使用简介
数据库帮助类格式如下：
*Helper
  *MySqlHelper
  *SqlHelper
  *OracleHelper
*HelperMini
  *MySqlHelperMini
  *SqlHelperMini
  *OracleHelperMini
不同数据库帮助类格式，均为数据库名称+Helper。例如：OracleHelper。
HelperMini，功能如下：
*增删改，存储过程
*查询返回DataSet
*查询返回List泛型
其中增删改，包含事务回滚功能。如有异常，则会回滚添加的数据。
