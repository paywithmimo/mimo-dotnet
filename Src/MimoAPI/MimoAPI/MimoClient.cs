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
        public static string apiKey = "";       // Client ID provided by the mimo site
        public static string apiSecret = "";    // Client Secret provided by the mimo site
        public static string redirectUri = "";  // Url where the user will be redirected after he accepts or denied the T&S
        public static string mode = "";         // Transaction mode. Can be 'live' or 'test'        
        public static string sApiUrl = "";      // Url for api server
        public static string sUserApiUrl = "";  // Url for user api server

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

        public static string stransaction_id = "";

        public static void SetAPIURL()
        {
            try
            {
                mode = ConfigurationManager.AppSettings["mode"].ToString();
                if (mode.ToLower() == "test")
                {
                    sApiUrl = ConfigurationManager.AppSettings["STAGE_API_SERVER"].ToString();
                    sUserApiUrl = ConfigurationManager.AppSettings["STAGE_USER_API_SERVER"].ToString();
                }
                else
                {
                    sApiUrl = ConfigurationManager.AppSettings["LIVE_API_SERVER"].ToString();
                    sUserApiUrl = ConfigurationManager.AppSettings["LIVE_USER_API_SERVER"].ToString();
                }
            }
            catch (Exception ex)
            {
                sApiUrl = "";
                sUserApiUrl = "";
            }            
        }

        /// <summary>
        /// Get the Access Code from MIMO site for Current Client ID
        /// </summary>
        public static void GetAccessCode()
        {
            try
            {
                SetAPIURL();
                apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
                apiSecret = ConfigurationManager.AppSettings["apiSecret"].ToString();
                redirectUri = ConfigurationManager.AppSettings["redirectUri"].ToString();
                HttpContext.Current.Response.Redirect(sApiUrl + "authenticate?client_id=" + apiKey + "&url=" + redirectUri + "&response_type=code");
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
            SetAPIURL();
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
                    webRequest = (HttpWebRequest)WebRequest.Create(sApiUrl + "token?client_id=" + apiKey + "&client_secret=" + apiSecret + "&url=" + redirectUri + "&code=" + Convert.ToString(HttpContext.Current.Session["Mimo_Client_AccessCode"]) + "&grant_type=authorization_code");
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
                    SetAPIURL();
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(sUserApiUrl + "user/card_id?" + sSearchField + "=" + sValue + "&access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString());
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
        /// <param name="note">Note to which information is pulled</param>
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
                    SetAPIURL();
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(sUserApiUrl + "transfers?access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString() + "&amount=" + amount + "&notes=" + note);
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

        /// <summary>
        /// Grab information for the given transaction ID
        /// </summary>
        /// <param name="amount">Amount to which information is pulled</param>
        /// <param name="note">Note to which information is pulled</param>
        /// <param name="transaction_id">transaction_id to which information is pulled</param>
        /// <returns>Refund information</returns>
        public static string Refund(string amount, string note, string transaction_id)
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
                    SetAPIURL();
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(sUserApiUrl + "refunds?access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString() + "&amount=" + amount + "&notes=" + note + "&transaction_id=" + transaction_id);
                    webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                    webRequest.Method = "POST";
                    var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    sReturnJson = Convert.ToString(streamReader.ReadToEnd());
                    if (sReturnJson != "" || sReturnJson != null)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        Refund RF = serializer.Deserialize<Refund>(sReturnJson);
                        sMessage = RF.message;
                        stransaction_id = RF.transaction_id;
                        sMsg = sReturnJson;
                    }
                }
            }
            catch (Exception ex)
            {
                sMsg = ex.ToString();
            }
            return sMsg;
        }

        /// <summary>
        /// Grab information for the given transaction ID
        /// </summary>
        /// <param name="transaction_id">transaction_id to which information is pulled</param>
        /// <returns>Transaction void information</returns>
        public static string cancelTransaction(string transaction_id)
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
                    if (transaction_id == "" || transaction_id == null)
                    {
                        return sMsg = "Please enter Transaction ID.";
                    }
                    SetAPIURL();
                    string sReturnJson = "";
                    HttpWebRequest webRequest;
                    webRequest = (HttpWebRequest)WebRequest.Create(sUserApiUrl + "transfers/void?access_token=" + HttpContext.Current.Session["Mimo_Client_AccessToken"].ToString() + "&transaction_id=" + transaction_id);
                    webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                    webRequest.Method = "POST";
                    //webRequest.KeepAlive = false;
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

        /// <summary>
        /// Register the new user with MIMO
        /// </summary>
        /// <param name="username"></param>
        /// <param name="account_type"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="pin"></param>
        /// <param name="first_name"></param>
        /// <param name="middle_name"></param>
        /// <param name="surname"></param>
        /// <param name="dob"></param>
        /// <param name="gender"></param>
        /// <param name="about"></param>
        /// <param name="address_type"></param>
        /// <param name="address"></param>
        /// <param name="address_2"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zip"></param>
        /// <param name="website"></param>
        /// <param name="facebook"></param>
        /// <param name="twitter"></param>
        /// <param name="challenge_question"></param>
        /// <param name="challenge_answer"></param>
        /// <param name="terms_and_conditions"></param>
        /// <param name="company_name"></param>
        /// <param name="company_id_number"></param>
        /// <param name="rc_incorporation_year"></param>
        /// <returns></returns>
        public static string newRegistration(string username, string account_type, string email, string password, string pin, string first_name, string middle_name, string surname, string dob, string gender, string about, string address_type, string address, string address_2, string city, string state, string country, string zip,string Phone, string website, string facebook, string twitter, string challenge_question, string challenge_answer, string terms_and_conditions, string company_name, string company_id_number, string rc_incorporation_year)
        {
            apiKey = ConfigurationManager.AppSettings["apiKey"].ToString();
            apiSecret = ConfigurationManager.AppSettings["apiSecret"].ToString();
            string sMsg = "";
            try
            {
                if (email == "" || email == null)
                {
                    return sMsg = "Please enter Email address.";
                }
                if (password == "" || password == null)
                {
                    return sMsg = "Please enter password.";
                }
                SetAPIURL();
                string sReturnJson = "";
                HttpWebRequest webRequest;
                webRequest = (HttpWebRequest)WebRequest.Create(sUserApiUrl + "registration?client_id=" + apiKey + "&client_secret=" + apiSecret + "&username=" + username + "&account_type=" + account_type + "&email=" + email + "&password=" + password + "&pin=" + pin + "&first_name=" + first_name + "&middle_name=" + middle_name + "&surname=" + surname + "&dob=" + dob + "&gender=" + gender + "&about=" + about + "&address_type=" + address_type + "&address=" + address + "&address_2=" + address_2 + "&city=" + city + "&state=" + state + "&country=" + country + "&zip=" + zip + "&mobile_phone=" + Phone + "&website=" + website + "&facebook=" + facebook + "&twitter=" + twitter + "&challenge_question=" + challenge_question + "&challenge_answer=" + challenge_answer + "&terms_and_conditions=" + terms_and_conditions + "&company_name=" + company_name + "&company_id_number=" + company_id_number + "&rc_incorporation_year=" + rc_incorporation_year);
                webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_Username"].ToString(), ConfigurationManager.AppSettings["NetworkCredential_Password"].ToString());
                webRequest.Method = "POST";
                var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                sReturnJson = Convert.ToString(streamReader.ReadToEnd());
                if (sReturnJson != "" || sReturnJson != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Register R = serializer.Deserialize<Register>(sReturnJson);
                    sMessage = R.message;
                    sAccessToken = R.access_token;
                    sExpiresIn = R.access_token_expires_in;
                    sMsg = sReturnJson;
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