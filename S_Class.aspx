<%@ Page Language="C#" AutoEventWireup="true" CodeFile="S_Class.aspx.cs" Inherits="S_Class" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <asp:Label ID="Label1" runat="server" Text="课程书籍"></asp:Label>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
    </div>
    </form>
</body>
</html>
