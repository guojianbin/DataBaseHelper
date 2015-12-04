using System;

namespace MultiWeb
{
    public partial class MongoDBTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Test();
        }

        public async void Test()
        {
            MongoDBHelperMini<Chenxy> helper = new MongoDBHelperMini<Chenxy>("mongodb://127.0.0.1:27017", "local");
            //await helper.Add(new Chenxy() { Id = 3, Name = "晨星宇测试3" }, "Chenxy");

            //var list = await helper.GetList("Chenxy");

            //await helper.Update("Chenxy");

            // await helper.Delete("Chenxy");
        }
    }

    public class Chenxy
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}