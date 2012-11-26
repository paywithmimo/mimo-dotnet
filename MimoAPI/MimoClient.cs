using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MimoWrapperClass;
using System.Net;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;

namespace MimoWrapperClass
{
    public class MimoClient
    {
        public static string ClientID = "";      // Client ID provided by the mimo site
        public static string ClientSecret = "";  // Client Secret provided by the mimo site
        public static string ReturnURL = "";     // Url where the user will be redirected after he accepts or denied the T&S

        public static string sAccessToken = "";
        public static string sExpiresIn = "";

        /// <summary>
        /// Get the Access Code from MIMO site for this Client ID
        /// </summary>
        public static void GetAccessCode()
        {
            try
            {
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessCode"] != null)
                {
                    //HttpContext.Current.Response.Write(HttpContext.Current.Session["Mimo_Client_AccessCode"]);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("https://staging.mimo.com.ng/oauth/v2/authenticate?client_id=" + ClientID + "&url=" + ReturnURL + "&response_type=code");
                }
            }
            catch (ThreadAbortException th) { }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Get the Access Token from MIMO site for Current Client ID
        /// </summary>
        public static string GetAccessToken()
        {
            string AccessToken = "";
            if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] != null)
            {
                AccessToken = HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString();
            }
            else
            {
                string sReturnJson = "";
                try
                {
                    HttpWebRequest webRequest;
                    if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessCode"] != null)
                    {
                        webRequest = (HttpWebRequest)WebRequest.Create("https://staging.mimo.com.ng/oauth/v2/token?client_id=" + ClientID + "&client_secret=" + ClientSecret + "&url=" + ReturnURL + "&code=" + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) + "&grant_type=authorization_code");
                        webRequest.Credentials = new NetworkCredential("mimo", "mimo");
                        webRequest.Method = "POST";
                        var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                        var streamReader = new StreamReader(httpResponse.GetResponseStream());
                        sReturnJson = streamReader.ReadToEnd().ToString();
                        if (sReturnJson != "" || sReturnJson != null)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            AccessToken AT = serializer.Deserialize<AccessToken>(sReturnJson);
                            sAccessToken = AT.access_token;
                            sExpiresIn = AT.expires_in;
                            HttpContext.Current.Session["Mimo_Client_AccessToken"] = AT.access_token;
                            AccessToken = AT.access_token;
                        }
                        else
                        {
                            AccessToken = "";
                        }
                    }
                    else
                    {
                        GetAccessCode();
                        AccessToken = "";
                    }
                }
                catch (Exception ex)
                {
                    AccessToken = ex.ToString();
                }                
            }
            return AccessToken;
        }
    }
}
