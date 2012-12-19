#Dotnet-sdk-oauth2: Dotnet SDK for Mimo's API
=================================================================================

## Version
1.0

## Requirements
- [Microsoft Visual Studio](http://www.microsoft.com/visualstudio/eng/downloads)
- [Mimo Application](https://staging.mimo.com.ng)

## Installation
- Microsoft Visual Studio

## Examples / Quickstart
This API includes various usage examples, including:

* Authenticating with OAuth [Default.aspx]
* Fetching account information [UserProfile.aspx]
* Transfer money [MoneyTransfer.aspx]

	Setup Project in Visual Studio
	------------------------------------------
	1.   Open the 'MimoAPI.sln' with Visual Studio.
	2.	 Click on 'Build > Rebuild Solution'.
	3.	 The .dll is copied to '..\Bin'.

	Samples
	--------
	1.	 Open the 'Samples.sln' file in Visual Studio.
	2.	 Define the 'apiKey','apiSecret' attributes in the `Web.config` file with your Mimo application key and secret respectively.
	2.   Set key "redirectUri" to your path like "http://your domain here/Default.aspx" in Web.config file.
	3.	 Change the values of following key in web.config if required
		 "NetworkCredential_Username", "NetworkCredential_Password" and "BaseURL" 
	4.   Click on 'Build > Rebuild Solution'.


	Usage
	-----
	First of all get Access Code from Mimo site which is done by
	
		MimoRestClient.GetAccessCode();

	Once you will get the Access Code from query string of your return URL, also set session["Mimo_Client_AccessCode"] in the return url page from query string like below:
	
 
	if (Request.QueryString["code"] != "" && Request.QueryString["code"] != null)
	{
		Session["Mimo_Client_AccessCode"] = Convert.ToString(Request.QueryString["code"]);
	}

	Next task is to get the Access Token from mimo site which is done by 
	
		string AccessToken = MimoRestClient.requestToken();
	
	After geting the Access Code and Access Token , now you can get the user profile as shown:
	
    	string UserProfile = MimoRestClient.getUser(string sSearchField, string sValue); ==> Get user details based on search criteria

	You can perform money transaction as shown :
	
		string MoneyTransfer = MimoRestClient.transaction(string amount, string note);    ==> transaction details
		
	You can perform money refund as shown :
	
		string MoneyRefund = MimoRestClient.Refund(string amount, string note, string transaction_id);    ==> refund details

## Methods
Authantication :

- GetAccessCode() ==> perform to get access code 
- requestToken() ==> perform to get access token 

User Profile :

- getUser(string sSearchField, string sValue) ==> get user detail from with different type e.g. by username, by email-id, by phone-no, by account no

Money Transfer :

- transaction(string amount, string note) ==> transfer amount from account

Money Refund :

- Refund(string amount, string note, string transaction_id) ==> refund amount from account with given transaction id.

## Credits
MIMO Payment Services

## Support
-Developer Support <developers@mimo.ng>
-MIMO API <api@mimo.ng>

## References / Documentation
https://www.mimo.com.ng/developer

## License
The MIT License (MIT)
Copyright (c) 2012 MIMO Payment Services
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
