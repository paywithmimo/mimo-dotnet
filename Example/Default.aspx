﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MimoWebApp.Default" %>

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
    <asp:Button ID="btn" runat="server" Visible="false" onclick="btn_Click" Text="Get Access Token" />
    </div>
    </form>
</body>
</html>
