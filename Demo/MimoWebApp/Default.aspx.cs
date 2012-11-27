using System;
using System.Web;
using MimoWrapperClass;
using System.Configuration;

namespace MimoWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                    btn.Visible = true;
                }
                else
                {
                    MimoClient.GetAccessCode();
                }
                                
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            MimoClient.GetAccessToken();
            lblAccessToken.Text = "Your current Access Token for mimo is : " + MimoClient.GetAccessToken();
        }
    }
}