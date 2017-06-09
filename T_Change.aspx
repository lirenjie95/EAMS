<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_Change.aspx.cs" Inherits="T_Change" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Label ID="Label5" runat="server" Text="*号为必填" ForeColor="Red"></asp:Label><br /><br /><br />
            <asp:Label ID="Label1" runat="server" Text="*姓名："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label2" runat="server" Text="*性别："></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label3" runat="server" Text="*课程："></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label4" runat="server" Text="备注："></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
            <asp:Button ID="Button3" runat="server" Text="加载个人信息" OnClick="Button3_Click"></asp:Button>
            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
