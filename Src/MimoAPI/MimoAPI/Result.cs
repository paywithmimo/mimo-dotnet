
namespace MimoAPI
{
    public class Result
    {
    }

    /// <summary>
    /// Use for the Set the value of Access Token response
    /// </summary>
    public class AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }

    /// <summary>
    /// Use for set the value of User Profile response
    /// </summary>
    public class UserProfile
    {
        public string account_number { get; set; }
        public string account_type { get; set; }
        public string company_name { get; set; }
        public string first_name { get; set; }
        public string id { get; set; }
        public string middle_name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string photo_url { get; set; }
        public string email { get; set; }
        public string level { get; set; }
    }

    /// <summary>
    /// Use for set the value of Money Transfer response
    /// </summary>
    public class Transfer
    {
        public string message { get; set; }
    }

    /// <summary>
    /// Use for set the value of Refund response
    /// </summary>
    public class Refund
    {
        public string message { get; set; }
        public string transaction_id { get; set; }
    }

    /// <summary>
    /// Use for set the value of newRegistration response
    /// </summary>
    public class Register
    {
        public string message { get; set; }
        public string access_token { get; set; }
        public string access_token_expires_in { get; set; }
    }
}
