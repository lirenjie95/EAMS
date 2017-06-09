<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_AddClass.aspx.cs" Inherits="T_AddClass" %>

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
            <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView><br /><br />
            <asp:Label ID="Label4" runat="server" Text="*为必填项" ForeColor="Red"></asp:Label><br /><br />
            <asp:Label ID="Label1" runat="server" Text="*课程名称："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label2" runat="server" Text="*任课教师："></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label3" runat="server" Text="备注信息："></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br /><br /><br />
            <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
