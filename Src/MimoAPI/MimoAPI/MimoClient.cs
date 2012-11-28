using System;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;
using System.Configuration;

namespace MimoAPI
{
    public class MimoOAuth
    {
        public static string ClientID = "";      // Client ID provided by the mimo site
        public static string ClientSecret = "";  // Client Secret provided by the mimo site
        public static string ReturnURL = "";     // Url where the user will be redirected after he accepts or denied the T&S

        public static string sAccessToken = "";
        public static string sExpiresIn = "";

        public static string sAccountnumber = "";
        public static string sAccountType = "";
        public static string sCompanyName = "";
        public static string sFirstName = "";
        public static string sid = "";
        public static string sMiddleName = "";
        public static string sSurname = "";
        public static string sUsername = "";
        public static string sPhotoUrl = "";
        public static string sEmail = "";
        public static string sLevel = "";

        public static string sMessage = "";

        /// <summary>
        /// Get the Access Code from MIMO site for Current Client ID
        /// </summary>
        public static void GetAccessCode()
        {
            try
            {
                ClientID = ConfigurationManager.AppSettings["ClientID"].ToString();
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
                ReturnURL = ConfigurationManager.AppSettings["ReturnURL"].ToString();
                HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/oauth/v2/authenticate?client_id=" + ClientID + "&url=" + ReturnURL + "&response_type=code");                
            }
            catch (ThreadAbortException th) { }
            catch (Exception ex) { }
        }
        
        /// <summary>
        /// Get the Access Token from MIMO site for Current Client ID
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            ClientID = ConfigurationManager.AppSettings["ClientID"].ToString();
            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"].ToString();
            ReturnURL = ConfigurationManager.AppSettings["ReturnURL"].ToString();
            string AccessToken = "";
            string sReturnJson = "";
            try
            {
                HttpWebRequest webRequest;
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessCode"] != null)
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/oauth/v2/token?client_id=" + ClientID + "&client_secret=" + ClientSecret + "&url=" + ReturnURL + "&code=" + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) + "&grant_type=authorization_code");
                    webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                    webRequest.Method = "POST";
                    var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    sReturnJson = Convert.ToString(streamReader.ReadToEnd());
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
            return AccessToken;
        }

        /// <summary>
        /// Get the User Profile from MIMO site for current user
        /// </summary>
        /// <param name="sSearchParaMeter"></param>
        /// <returns></returns>
        public static string GetUserProfile(string sSearchParaMeter)
        {
            string UserProfile = "";
            try
            {
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] == null)
                {
                    GetAccessToken();
                    UserProfile = "";
                }
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] != null)
                {
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/partner/user/card_id?" + sSearchParaMeter + "&access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString());
                    webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                    webRequest.Method = "GET";
                    var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    sReturnJson = Convert.ToString(streamReader.ReadToEnd());
                    UserProfile = sReturnJson;
                    if (sReturnJson != "" || sReturnJson != null)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        UserProfile UP = serializer.Deserialize<UserProfile>(sReturnJson);
                        sAccountnumber = UP.account_number;
                        sAccountType = UP.account_type;
                        sCompanyName = UP.company_name;
                        sFirstName = UP.first_name;
                        sid = UP.id;
                        sMiddleName = UP.middle_name;
                        sSurname = UP.surname;
                        sUsername = UP.username;
                        sPhotoUrl = UP.photo_url;
                        sEmail = UP.email;
                        sLevel = UP.level;
                    }
                }
            }
            catch (Exception ex)
            {
                UserProfile = ex.ToString();
            }
            return UserProfile;
        }
        
        /// <summary>
        /// Take money transfer for Current user
        /// </summary>
        /// <param name="sTransferParaMeter"></param>
        /// <returns></returns>
        public static string MoneyTransfer(string sTransferParaMeter)
        {
            string sMsg = "";
            try
            {
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] == null)
                {
                    GetAccessToken();
                    sMsg = "";
                }
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] != null)
                {
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/partner/transfers?access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString() + sTransferParaMeter);
                    webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                    webRequest.Method = "POST";
                    var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    sReturnJson = Convert.ToString(streamReader.ReadToEnd());
                    if (sReturnJson != "" || sReturnJson != null)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        Transfer TF = serializer.Deserialize<Transfer>(sReturnJson);
                        sMessage = TF.message;
                        sMsg = sMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                sMsg = ex.ToString();                
            }
            return sMsg;
        }
    }
}
