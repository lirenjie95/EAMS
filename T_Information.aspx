﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_Information.aspx.cs" Inherits="T_Information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
    </div>
    </form>
</body>
</html>
