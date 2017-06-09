<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Entry.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="foot">
    <div class="foot">
        <center>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="教师登录" />
            <asp:CheckBox ID="CheckBox2" runat="server" Text="学生登录" /><br /><br />
            <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br /><br />
            <asp:Label ID="Label2" runat="server" Text="密码"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox><br /><br />
            <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><br /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
            <br />
        </center>
    </div>
    </form>
</body>
</html>
