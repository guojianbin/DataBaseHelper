using System;

namespace MultiWeb
{
    public partial class RedisTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RedisHelperMini helper = new RedisHelperMini();
            var a = helper.Get<string>("test");
            helper.Set("fdsa", "1234");
            var b = helper.Get<string>("fdsa");
        }
    }
}