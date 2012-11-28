using System;
using System.Web;
using MimoAPI;
using System.Configuration;

namespace Samples
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["response_type"] != "" && Request.QueryString["response_type"] != null)
                    {
                        if (!Convert.ToString(Request.QueryString["response_type"]).Contains("code"))
                        {
                            lblAccessCode.Text = "Access Denied";
                            return;
                        }
                    }
                    if (Request.QueryString["code"] != "" && Request.QueryString["code"] != null)
                    {
                        Session["Mimo_Client_AccessCode"] = Convert.ToString(Request.QueryString["code"]);
                        lblAccessCode.Text = "Your current Access Code for mimo is : " + Convert.ToString(Request.QueryString["code"]);
                        MimoOAuth.GetAccessToken();
                        lblAccessToken.Text = "Your current Access Token for mimo is : " + MimoOAuth.GetAccessToken();
                    }
                    else
                    {
                        MimoOAuth.GetAccessCode();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}