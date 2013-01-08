using System;
using System.Web;
using MimoAPI;
using System.Configuration;
using System.Threading;

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
                        //set access code session
                        Session["Mimo_Client_AccessCode"] = Convert.ToString(Request.QueryString["code"]);
                        //print access code
                        lblAccessCode.Text = "Your current Access Code for mimo is : " + Convert.ToString(Request.QueryString["code"]);
                        
                        //Exchange the temporary code given to us in the querystring, for a never-expiring OAuth access token
                        MimoRestClient.requestToken();

                        //print access token
                        lblAccessToken.Text = "Your current Access Token for mimo is : " + MimoRestClient.requestToken();
                        
                        ancUser.Visible = true;
                        ancMoney.Visible = true;
                        ancRefund.Visible = true;
                        ancCancel.Visible = true;
                    }
                    else
                    {
                        //Create an authentication URL that the user will be redirected to for get access code
                        MimoRestClient.GetAccessCode();
                    }
                }
            }
            catch (ThreadAbortException th) { }
            catch (Exception ex) { }
        }
    }
}