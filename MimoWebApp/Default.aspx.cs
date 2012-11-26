using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MimoWrapperClass;
using System.Configuration;

namespace MimoWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["code"] != "" && Request.QueryString["code"] != null)
            {
                Session["Mimo_Client_AccessCode"] = Convert.ToString(Request.QueryString["code"]);
                lblAccessCode.Text = "Your current Access Code for mimo is : " + Convert.ToString(Request.QueryString["code"]);
            }
            if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessCode"] == null)
            {
                MimoClient.ClientID = ConfigurationManager.AppSettings["ClientID"].ToString();
                MimoClient.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
                MimoClient.ReturnURL = ConfigurationManager.AppSettings["ReturnURL"].ToString();
                MimoClient.GetAccessCode();
            }
            else
            {
                lblAccessCode.Text = "Your current Access Code for mimo is : " + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]);
            }
            if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] == null)
            {

                MimoClient.ClientID = ConfigurationManager.AppSettings["ClientID"].ToString();
                MimoClient.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
                MimoClient.ReturnURL = ConfigurationManager.AppSettings["ReturnURL"].ToString();
                MimoClient.GetAccessToken();
                lblAccessToken.Text = "Your current Access Token for mimo is : " + MimoClient.GetAccessToken();
            }
            else
            {
                lblAccessToken.Text = "Your current Access Token for mimo is : " + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]);
            }
        }
    }
}