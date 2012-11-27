﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyTransfer.aspx.cs" Inherits="MimoWebApp.MoneyTransfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Money Transfer</h1>
    <br />
    note : <asp:TextBox ID="txtnote" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="VldNoteReq" runat="server" ControlToValidate="txtnote" Display="Dynamic" ErrorMessage="Please enter Note"></asp:RequiredFieldValidator>
    <br />
    <br />
    amount : <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="vldAmountReq" runat="server" ControlToValidate="txtAmount" Display="Dynamic" ErrorMessage="Please enter Amount"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btnAmount" runat="server" CausesValidation="true" Text="Money Transfer" onclick="btnAmount_Click" />
    <br />
    <asp:Label ID="lblmessage" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
