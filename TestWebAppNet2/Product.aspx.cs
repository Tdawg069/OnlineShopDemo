using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebAppNet2.Data;

namespace TestWebAppNet2
{
    public partial class Product : Page
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
                }
            }
            try
            {
                DataTable allProducts = ProductController.GetAllProductsAsDataTable();
                gvProducts.DataSource = allProducts;
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                errDiv.InnerHtml = ex.Message;
            }
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            string selectedValue = (sender as LinkButton).CommandArgument.ToString();
        }

        protected void lbAddToCart_Click(object sender, EventArgs e)
        {
            errDiv.InnerText = "";
            string[] cArgs = (sender as LinkButton).CommandArgument.ToString().Split(',');
            //string cArg = (sender as LinkButton).CommandArgument.ToString();
            string selectedProductId = cArgs[0];//cArg.Substring(0, cArg.IndexOf(','));
            string selectedProductTitle = cArgs[1];//cArg.Substring(cArg.IndexOf(','));
            string userId = hfUserId?.Value ?? Session["user_id"]?.ToString();
            int quantity = 1;
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(selectedProductId))
            {
                int uid = int.Parse(userId);
                int pid = int.Parse(selectedProductId);
                if (UserBasketController.ProductExistsForUser(uid, pid))
                {
                    UserBasketController.UpdateBasket(uid, pid, quantity);
                }
                else
                {
                    UserBasketController.AddNewProductForUser(uid, pid, quantity);
                }
                errDiv.InnerText = selectedProductTitle + " added to Cart.";
            }
            else
            {
                errDiv.InnerText = "Please login first!";
            }
        }
    }
}