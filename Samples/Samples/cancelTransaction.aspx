<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cancelTransaction.aspx.cs" Inherits="Samples.cancelTransaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Cancel Transaction</h1>
    <br />
    Transaction id : <asp:TextBox ID="txtTransactionId" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator id="vldTransactionIDReq" runat="server" ControlToValidate="txtTransactionId" Display="Dynamic" ErrorMessage="Please enter Transaction ID"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btnAmount" runat="server" CausesValidation="true" Text="Cancel Transaction" onclick="btnAmount_Click" />
    <br />
    <asp:Label ID="lblmessage" runat="server"></asp:Label><br />
    <br />
    <a href="Default.aspx">Home</a>
    <br />
    <a href="UserProfile.aspx">User Profile</a>
    <br />
    <a href="MoneyTransfer.aspx">Money Transfer</a>
    <br />
    <a href="Refund.aspx">Refund</a>
    <br />
    <a href="Registration.aspx">New Registration</a>
    </div>
    </form>
</body>
</html>
