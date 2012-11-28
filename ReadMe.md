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
2.	 Define the 'ClientID','ClientSecret' attributes in the `Web.config` file with your Mimo application key and secret respectively.
2.   Also set key "ReturnURL" to your path like "http://your domain here/Default.aspx" in Web.config file.
3.	 Also take changes in web.config file keys if required, those keys are: "NetworkCredential_Username", "NetworkCredential_Password" and "BaseURL" 
4.   Click on 'Build > Rebuild Solution'.


Usage
-----

First of all get Access Code from Mimo site which is done by
	MimoOAuth.GetAccessCode();

After getting Access Code from query string of your return URL, also set session["Mimo_Client_AccessCode"] in the return url page from query string like below:
 
if (Request.QueryString["code"] != "" && Request.QueryString["code"] != null)
{
	Session["Mimo_Client_AccessCode"] = Convert.ToString(Request.QueryString["code"]);
}

After complete this task get Access Token from mimo site which is done by 
	string AccessToken = MimoOAuth.GetAccessToken();
	
Both Access code and Access Token you have taken from Mimo site.

Now you can get user profile like :
	string UserProfile = MimoOAuth.GetUserProfile("username=le");

and perform money transaction like :
	string MoneyTransfer = MimoOAuth.GetUserProfile("&notes=buyKindle&amount=100");

## Methods
Authantication :

- GetAccessCode() ==> perform to get access code 
- GetAccessToken() ==> perform to get access token 

User Profile :

- GetGetUserProfile(string sSearchParaMeter) ==> get user detail from with different type e.g. by username, by email-id, by phone-no, by account no

Money Transfer :

- MoneyTransfer(string sTransferParaMeter) ==> transfer amount from account

## Credits
MIMO Payment Services

## Support
Developer Support <developers@mimo.ng>
MIMO API <api@mimo.ng>

## References / Documentation
https://www.mimo.com.ng/developer

## License
The MIT License (MIT)
Copyright (c) 2012 MIMO Payment Services
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.




An implementation of the Mimo REST API using OAuth 2.0 in C#.



