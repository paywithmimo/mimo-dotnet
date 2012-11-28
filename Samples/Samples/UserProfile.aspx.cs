using System;
using MimoAPI;

namespace Samples
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
                    lblUserProfile.Text = MimoOAuth.GetUserProfile("username=" + txtValue.Text.Trim());
                    break;
                case "email":
                    lblUserProfile.Text = MimoOAuth.GetUserProfile("email=" + txtValue.Text.Trim());
                    break;
                case "phone":
                    lblUserProfile.Text = MimoOAuth.GetUserProfile("phone=" + txtValue.Text.Trim());
                    break;
                case "account_number":
                    lblUserProfile.Text = MimoOAuth.GetUserProfile("account_number=" + txtValue.Text.Trim());
                    break;
            }
        }
    }
}