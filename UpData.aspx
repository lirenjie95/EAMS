<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpData.aspx.cs" Inherits="UpData" %>

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
            <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label2" runat="server" Text="性别"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br /><br />
            <asp:Label ID="Label3" runat="server" Text="备注"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br /><br />
            <asp:Button ID="Button3" runat="server" Text="<<返回主界面" OnClick="Button3_Click"></asp:Button>
            <asp:Button ID="Button1" runat="server" Text="载入原有信息" OnClick="Button1_Click"></asp:Button>
            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click"></asp:Button><br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
        </center>
    </div>
    </form>
</body>
</html>
