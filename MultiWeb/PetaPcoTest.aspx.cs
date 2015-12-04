using PetaPoco;
using System;

namespace MultiWeb
{
    public partial class PetaPcoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Test();
        }

        private static Database db;

        public void Test()
        {
            PetaPcoHelperMini<Chenxy_Users> helper = new PetaPcoHelperMini<Chenxy_Users>("server=192.168.66.25;user id=root;password=root123;database=chenxytest", "MySql.Data.MySqlClient");

            // var listTest = helper.GetList<Chenxy_Users>("select * from chenxy_users");
            // var enumTest = helper.GetEnum<Chenxy_Users>("select * from chenxy_users");
            // var longTest = helper.GetLong("select count(*) from chenxy_users");
            // var defaulTest = helper.GetDefault("select * from chenxy_users where id = 2");
            // var listTestFy = helper.GetList<Chenxy_Users>("select * from chenxy_users", 2, 2);
            var addTest = helper.Add(new Chenxy_Users() { Name = "晨星宇测试3" });
            Chenxy_Users model = helper.GetDefault("select * from chenxy_users where id = " + addTest);
            model.Name = "晨星宇测试3";
            // var updateTest = helper.Update(model);
            // var deleteTest = helper.Delete(model);
            // var isNewTest = helper.IsNew(model);
        }
    }

    [PetaPoco.TableName("chenxy_users")]
    [PetaPoco.PrimaryKey("id")]
    public class Chenxy_Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}