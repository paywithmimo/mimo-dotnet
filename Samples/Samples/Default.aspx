﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Samples.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblAccessCode" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblAccessToken" runat="server"></asp:Label>
    <br />
    <br />
    <a id="ancUser" runat="server" visible="false" href="UserProfile.aspx">User Profile</a>
    <br />
    <a id="ancMoney" runat="server" visible="false" href="MoneyTransfer.aspx">Money Transfer</a>
    <br />
    <a id="ancRefund" runat="server" visible="false" href="Refund.aspx">Refund</a>
    <br />
    <a id="ancCancel" runat="server" visible="false" href="cancelTransaction.aspx">Cancel Transaction</a>
    <br />
    <a href="Registration.aspx">New Registration</a>
    </div>
    </form>
</body>
</html>
