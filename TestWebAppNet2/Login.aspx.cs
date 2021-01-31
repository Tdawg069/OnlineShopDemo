using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebAppNet2.Data;

namespace TestWebAppNet2
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errDiv.InnerText = "";
            if (Request.QueryString.HasKeys())//!IsPostBack && 
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["uid"]))
                {
                    hfUserId.Value = Request.QueryString["uid"].ToString();
                    Session["UserId"] = hfUserId.Value;
                    Users user = UserController.GetByKey(int.Parse(hfUserId.Value));
                    errDiv.InnerText = user.FirstName + " " + user.SurName + " is currently logged in.";
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            errDiv.InnerText = "";
            Users user = UserController.GetByUserName(tbxLogin.Text);
            if (user.UserId > 0)
            {
                Session["UserId"] = user.UserId.ToString();
                Response.Redirect("~/?login=true&uid=" + user.UserId.ToString(), false);
            }
            else
            {
                errDiv.InnerText = "User Not Found!";
            }
        }
    }
}