/**
 * MIMO REST API Library for C#
 *
 * MIT LICENSE
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * @package   MIMO
 * @copyright Copyright (c) 2012 Mimo Inc. (http://www.mimo.com.ng)
 * @license   http://opensource.org/licenses/MIT MIT
 * @version   1.0.0
 * @link      http://www.mimo.com.ng
 */

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;
using System.Configuration;

/**
 * MIMO API Library for C#
 *
 * @package   MIMO
 * @copyright Copyright (c) 2012 Mimo Inc. (http://www.mimo.com.ng)
 * @license   http://opensource.org/licenses/MIT MIT
 */
namespace MimoAPI
{
    public class MimoRestClient
    {
        public static string apiKey = "";      // Client ID provided by the mimo site
        public static string apiSecret = "";  // Client Secret provided by the mimo site
        public static string redirectUri = "";     // Url where the user will be redirected after he accepts or denied the T&S
        public static string mode = "";          // Transaction mode. Can be 'live' or 'test'        

        public static string sAccessToken = "";  // oauth token
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
                apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
                apiSecret = ConfigurationManager.AppSettings["apiSecret"].ToString();
                redirectUri = ConfigurationManager.AppSettings["redirectUri"].ToString();
                HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/oauth/v2/authenticate?client_id=" + apiKey + "&url=" + redirectUri + "&response_type=code");
            }
            catch (ThreadAbortException th) { }
            catch (Exception ex) { }
        }
        
        /// <summary>
        /// Get the Access Token from MIMO site for Current Client ID
        /// </summary>
        /// <returns>AccessToken</returns>
        public static string requestToken()
        {
            apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
            apiSecret = ConfigurationManager.AppSettings["apiSecret"].ToString();
            redirectUri = ConfigurationManager.AppSettings["redirectUri"].ToString();
            string AccessToken = "";
            string sReturnJson = "";
            try
            {
                HttpWebRequest webRequest;
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessCode"] != null)
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/oauth/v2/token?client_id=" + apiKey + "&client_secret=" + apiSecret + "&url=" + redirectUri + "&code=" + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) + "&grant_type=authorization_code");
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
        /// Grabs the basic account information for the provided Mimo user search parameter
        /// </summary>
        /// <param name="sSearchField">Mimo Account Field like username, email, phone, account_number</param>
        /// <param name="sValue">Mimo Account value of username, email, phone, account_number you wanted to get searched</param>
        /// <returns>Basic user information</returns>
        public static string getUser(string sSearchField, string sValue)
        {
            string UserProfile = "";
            try
            {
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] == null)
                {
                    requestToken();
                    UserProfile = "";
                }
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] != null)
                {
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/partner/user/card_id?" + sSearchField + "=" + sValue + "&access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString());
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
                if (ex.ToString().Contains("404"))
                {
                    UserProfile = "User account information not found.";
                }
                else
                {
                    UserProfile = ex.ToString();
                }
            }
            return UserProfile;
        }
        
        /// <summary>
        /// Grab information for the given transaction ID
        /// </summary>
        /// <param name="amount">Amount to which information is pulled</param>
        /// <param name="note">Amount to which information is pulled</param>
        /// <returns>Transaction information</returns>
        public static string transaction(string amount, string note)
        {
            string sMsg = "";
            try
            {
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) == "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] == null)
                {
                    requestToken();
                    sMsg = "";
                }
                if (Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessToken"]) != "" || HttpContext.Current.Session["Mimo_Client_AccessToken"] != null)
                {
                    if (amount == "" || amount == null)
                    {
                        return sMsg = "Please enter amount.";
                    }
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BaseURL"].ToString() + "/partner/transfers?access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString() + "&amount=" + amount + "&notes=" + note);
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
