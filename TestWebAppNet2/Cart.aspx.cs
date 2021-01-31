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
    public partial class Cart : Page
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
            string userId = hfUserId?.Value ?? Session["user_id"]?.ToString();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                try
                {
                    int uid = int.Parse(userId);
                    DataTable allProducts = UserBasketController.GetAllDetailsByUserIdAsDataTable(uid);
                    gvProducts.DataSource = allProducts;
                    gvProducts.DataBind();

                    hfTotal.Value = UserBasketController.GetTotalForUser(uid).ToString();
                    divTotalCost.InnerText = "Total Cost: R " + hfTotal.Value;
                }
                catch (Exception ex)
                {
                    //errDiv.InnerHtml = ex.Message;
                }
            }
            else
            {
                errDiv.InnerHtml = "Please login first!";
            }
        }

        protected void lbRemoveFromCart_Click(object sender, EventArgs e)
        {
            errDiv.InnerHtml = "";
            try
            {
                string selectedProductId = (sender as LinkButton).CommandArgument.ToString();
                string userId = hfUserId?.Value ?? Session["user_id"]?.ToString();
                int pid = int.Parse(selectedProductId);
                int uid = int.Parse(userId);
                UserBasketController.UpdateBasket(uid, pid, -1);

                DataTable allProducts = UserBasketController.GetAllDetailsByUserIdAsDataTable(uid);
                gvProducts.DataSource = allProducts;
                gvProducts.DataBind();

                hfTotal.Value = UserBasketController.GetTotalForUser(uid).ToString();
                divTotalCost.InnerText = "Total Cost: R " + hfTotal.Value;

                errDiv.InnerHtml = "Item removed.";
            }
            catch (Exception ex)
            {
                errDiv.InnerHtml = ex.Message;
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            errDiv.InnerHtml = "";
            string userId = hfUserId?.Value ?? Session["user_id"]?.ToString();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                UserBasketController.DeleteAllForUser(int.Parse(userId));
                Response.Redirect("~/?checkout=true&uid=" + userId, true);
            }
        }

        protected void lbView_Click(object sender, EventArgs e)
        {

        }
    }
}