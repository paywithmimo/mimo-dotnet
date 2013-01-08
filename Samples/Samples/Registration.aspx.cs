using System;
using MimoAPI;

namespace Samples
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spMerchant.Visible = false;
            }
        }

        /// <summary>
        /// Take registration for given user details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string terms = "";
            if (chkTerms.Checked)
            {
                terms = "1";
            }
            else
            {
                terms = "0";
            }
            
            lblmessage.Text = MimoRestClient.newRegistration(txtUserName.Text.Trim(),drpAccountType.SelectedValue, txtEmail.Text.Trim(),txtPassword.Text.Trim(),txtPin.Text.Trim(),txtFirstName.Text.Trim(),txtMiddleName.Text.Trim(),txtSurname.Text.Trim(),txtDOB.Text.Trim(),drpGender.SelectedValue,txtAboutMe.Text.Trim(),drpAddressType.SelectedValue,txtAddress.Text.Trim(),txtAddress2.Text.Trim(),txtCity.Text.Trim(),txtState.Text.Trim(),txtCountry.Text.Trim(),txtPostalCode.Text.Trim(),txtwebsite.Text.Trim(),txtFacebook.Text.Trim(),txtTwitter.Text.Trim(),txtQuestion.Text.Trim(),txtSecurityAnswer.Text.Trim(),terms,txtCompanyName.Text.Trim(),txtRCNumber.Text.Trim(),txtYear.Text.Trim());
        }

        protected void drpAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpAccountType.SelectedValue == "merchant")
            {
                spMerchant.Visible = true;
            }
        }
    }
}