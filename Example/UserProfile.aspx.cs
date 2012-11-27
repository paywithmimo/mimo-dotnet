using System;
using MimoWrapperClass;

namespace MimoWebApp
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Get User Profile based on search criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Click(object sender, EventArgs e)
        {
            string sSearchParaMeter;
            sSearchParaMeter = rdblSearchParameter.SelectedValue;
            switch (sSearchParaMeter)
            {
                case "username":
                    lblUserProfile.Text = MimoClient.GetUserProfile("username=" + txtValue.Text.Trim());
                    break;
                case "email":
                    lblUserProfile.Text = MimoClient.GetUserProfile("email=" + txtValue.Text.Trim());
                    break;
                case "phone":
                    lblUserProfile.Text = MimoClient.GetUserProfile("phone=" + txtValue.Text.Trim());
                    break;
                case "account_number":
                    lblUserProfile.Text = MimoClient.GetUserProfile("account_number=" + txtValue.Text.Trim());
                    break;
            }
        }
    }
}