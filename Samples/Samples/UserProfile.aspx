<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Samples.UserProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>User Profile</h1>
    <br />
    <asp:RadioButtonList ID="rdblSearchParameter" runat="server">
        <asp:ListItem Selected="True" Text="User Name" Value="username"></asp:ListItem>
        <asp:ListItem Text="Email" Value="email"></asp:ListItem>
        <asp:ListItem Text="Phone" Value="phone"></asp:ListItem>
        <asp:ListItem Text="Account Number" Value="account_number"></asp:ListItem>
    </asp:RadioButtonList>
    <br />
    Enter search value :<asp:TextBox id="txtValue" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldValueReq" runat="server" ControlToValidate="txtValue" Display="Dynamic" ErrorMessage="Please enter serach parameter value"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text ="Get User Profile" onclick="btn_Click" CausesValidation="true"/>
    <br />
    <br />
    <asp:Label ID="lblUserProfile" runat="server"></asp:Label>
    <br />
    <br />
    <a href="Default.aspx">Home</a>
    <br />
    <a href="MoneyTransfer.aspx">Money Transfer</a>
    </div>
    </form>
</body>
</html>
