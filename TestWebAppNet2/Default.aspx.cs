using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebAppNet2;

namespace TestWebAppNet2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBConnection.TestDB();
            if (Request.QueryString.HasKeys())
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["uid"]))
                {
                    hfUserId.Value = Request.QueryString["uid"].ToString();
                    Session["UserId"] = hfUserId.Value;
                }
                if (!string.IsNullOrWhiteSpace(Request.QueryString["checkout"]))
                {
                    feedback.InnerText = "And just like that, your money is gone.......";
                }
                else if (!string.IsNullOrWhiteSpace(Request.QueryString["login"]))
                {
                    feedback.InnerText = "You've just logged in...";
                }
            }
        }
    }
}