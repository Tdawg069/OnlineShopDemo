using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebAppNet2.Data;

namespace TestWebAppNet2
{
    public partial class User : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            subheading.InnerText = "Create New User";
            if (Request.QueryString.HasKeys())
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["uid"]))
                {
                    hfUserId.Value = Request.QueryString["uid"].ToString();
                    Session["UserId"] = hfUserId.Value;
                    BindControls(hfUserId.Value);
                    subheading.InnerText = "View/Edit User";
                }
            }
        }

        private void BindControls(Users user)
        {
            hfUserId.Value = user.UserId.ToString();
            tbxUserId.Text = user.UserId.ToString();
            tbxUserName.Text = user.UserName;
            tbxFirstName.Text = user.FirstName;
            tbxSurName.Text = user.SurName;
            ddlAddressType.SelectedValue = user.AddressType.ToString();
            tbxStreetAddress.Text = user.StreetAddress;
            tbxSuburb.Text = user.Suburb;
            tbxCity.Text = user.City;
            tbxPostalCode.Text = user.PostalCode;
        }

        private void BindControls(string userId)
        {
            Users user = UserController.GetByKey(int.Parse(userId));
            BindControls(user);
        }

        private Users GetFormData()
        {
            Users user = new Users();

            user.UserId = int.Parse(tbxUserId.Text);
            user.UserName = tbxUserName.Text;
            user.FirstName = tbxFirstName.Text;
            user.SurName = tbxSurName.Text;
            user.AddressType = ddlAddressType.SelectedValue.ElementAt(0);
            user.StreetAddress = tbxStreetAddress.Text;
            user.Suburb = tbxSuburb.Text;
            user.City = tbxCity.Text;
            user.PostalCode = tbxPostalCode.Text;

            return user;
        }

        private void ClearFormData()
        {
            errDiv.InnerText = "";
            tbxUserId.Text = "";
            tbxUserName.Text = "";
            tbxFirstName.Text = "";
            tbxSurName.Text = "";
            ddlAddressType.SelectedValue = "";
            tbxStreetAddress.Text = "";
            tbxSuburb.Text = "";
            tbxCity.Text = "";
            tbxPostalCode.Text = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearFormData();
            hfFunction.Value = "ADD";
            subheading.InnerText = "Create New User";
            int NewUserId = UserController.GetNewUserId();
            hfUserId.Value = NewUserId.ToString();
            tbxUserId.Text = NewUserId.ToString();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ClearFormData();
            hfFunction.Value = "EDIT";
            subheading.InnerText = "View/Edit User";
            Users user = UserController.GetByUserName(tbxSearch.Text);
            if (user.UserId != 0)
            {
                BindControls(user);
            }
            else
            {
                errDiv.InnerText = "User Not Found!";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            errDiv.InnerText = "";
            Users user = GetFormData();
            if (string.IsNullOrWhiteSpace(tbxUserName.Text) ||
                string.IsNullOrWhiteSpace(tbxFirstName.Text) ||
                string.IsNullOrWhiteSpace(tbxSurName.Text))
            {
                errDiv.InnerText = "Please fill in required fields.";
                return;
            }

            if ("ADD".Equals(hfFunction.Value))
            {
                UserController.SaveNewUser(user);
            }
            else if ("EDIT".Equals(hfFunction.Value))
            {
                UserController.UpdateUser(user);
            }
            errDiv.InnerText = "User Data Saved.";
        }
    }
}