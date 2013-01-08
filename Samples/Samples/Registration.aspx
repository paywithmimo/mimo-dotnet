<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Samples.Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Registration</h1>
    <br />
    Account Type : <asp:DropDownList ID="drpAccountType" runat="server" 
            AutoPostBack="true" 
            onselectedindexchanged="drpAccountType_SelectedIndexChanged">
        <asp:ListItem Selected="True" Text="Personal" Value="personal"></asp:ListItem>
        <asp:ListItem Text="Merchant" Value="merchant"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <span id="spMerchant" runat="server">
    Company Name : <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldCompanyNameReq" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" ErrorMessage="Please enter Company Name"></asp:RequiredFieldValidator>
    <br />
    <br />
    RC Number : <asp:TextBox ID="txtRCNumber" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldRCNumberReq" runat="server" ControlToValidate="txtRCNumber" Display="Dynamic" ErrorMessage="Please enter RC Number"></asp:RequiredFieldValidator>
    <br />
    <br />
    Year of Incorporation : <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldYearOfIncorporationReq" runat="server" ControlToValidate="txtYear" Display="Dynamic" ErrorMessage="Please enter Year of Incorporation"></asp:RequiredFieldValidator>
    <br />
    <br />
    </span>
    User Name : <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldUserNameReq" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Please enter User Name"></asp:RequiredFieldValidator>
    <br />
    <br />
    Pin : <asp:TextBox ID="txtPin" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="vldPinReq" runat="server" ControlToValidate="txtPin" Display="Dynamic" ErrorMessage="Please enter Pin"></asp:RequiredFieldValidator>
    <br />
    <br />
    Email : <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldEmailReq" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please enter Email"></asp:RequiredFieldValidator>
    <br />
    <br />
    Date of Bitrh : <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldDOBReq" runat="server" ControlToValidate="txtDOB" Display="Dynamic" ErrorMessage="Please enter Date Of Birth"></asp:RequiredFieldValidator>
    <br />
    <br />
    Password : <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldPasswordReq" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Please enter Password"></asp:RequiredFieldValidator>
    <br />
    <br />
    Security Question : <asp:TextBox ID="txtQuestion" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldQuestionReq" runat="server" ControlToValidate="txtQuestion" Display="Dynamic" ErrorMessage="Please enter Security Question"></asp:RequiredFieldValidator>
    <br />
    <br />
    Security Answer : <asp:TextBox ID="txtSecurityAnswer" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldSecurityAnswerReq" runat="server" ControlToValidate="txtSecurityAnswer" Display="Dynamic" ErrorMessage="Please enter Security Answer"></asp:RequiredFieldValidator>
    <br />
    <br />
    First Name : <asp:TextBox ID="txtFirstName" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldFirstNameReq" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="Please enter First Name"></asp:RequiredFieldValidator>
    <br />
    <br />
    Middle Name : <asp:TextBox ID="txtMiddleName" runat="server" ></asp:TextBox>
    <br />
    <br />
    Surname : <asp:TextBox ID="txtSurname" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldSurnameReq" runat="server" ControlToValidate="txtSurname" Display="Dynamic" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
    <br />
    <br />
    Gender : <asp:DropDownList ID="drpGender" runat="server">
        <asp:ListItem Selected="True" Text="Male" Value="Male"></asp:ListItem>
        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <span style="float:left;">About Me : </span><asp:TextBox ID="txtAboutMe" runat="server" MaxLength="250" TextMode="MultiLine" Rows="5" Wrap="true"></asp:TextBox>
    <br />
    <br />
    Website : <asp:TextBox ID="txtwebsite" runat="server" ></asp:TextBox>
    <br />
    <br />
    Facebook : <asp:TextBox ID="txtFacebook" runat="server" ></asp:TextBox>
    <br />
    <br />
    Twitter : <asp:TextBox ID="txtTwitter" runat="server" ></asp:TextBox>
    <br />
    <br />
    Address : <asp:TextBox ID="txtAddress" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldAddressReq" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Please enter Address"></asp:RequiredFieldValidator>
    <br />
    <br />
    Address 2 : <asp:TextBox ID="txtAddress2" runat="server" ></asp:TextBox>
    <br />
    <br />
    city : <asp:TextBox ID="txtCity" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldCityReq" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="Please enter City"></asp:RequiredFieldValidator>
    <br />
    <br />
    State / Province : <asp:TextBox ID="txtState" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldStateReq" runat="server" ControlToValidate="txtState" Display="Dynamic" ErrorMessage="Please enter State"></asp:RequiredFieldValidator>
    <br />
    <br />
    Postal Code : <asp:TextBox ID="txtPostalCode" runat="server" ></asp:TextBox>
    <br />
    <br />
    Country : <asp:TextBox ID="txtCountry" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator id="VldCountryReq" runat="server" ControlToValidate="txtCountry" Display="Dynamic" ErrorMessage="Please enter Country"></asp:RequiredFieldValidator>
    <br />
    <br />
    Address Type : <asp:DropDownList ID="drpAddressType" runat="server">
        <asp:ListItem Selected="True" Text="home" Value="home"></asp:ListItem>
        <asp:ListItem Text="business" Value="business"></asp:ListItem>
        <asp:ListItem Text="mailing" Value="mailing"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:CheckBox ID="chkTerms" runat="server" Text="Terms And Conditions" />
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" Text="Register" 
            onclick="btnSubmit_Click" />
    <br />
    <asp:Label ID="lblmessage" runat="server"></asp:Label><br />
    <br />
    <a href="Default.aspx">Home</a>
    <br />
    <a href="MoneyTransfer.aspx">Money Transfer</a>
    <br />
    <a href="UserProfile.aspx">User Profile</a>
    <br />
    <a href="Refund.aspx">Refund</a>
    <br />
    <a href="cancelTransaction.aspx">Cancel Transaction</a>
    </div>
    </form>
</body>
</html>
