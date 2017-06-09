<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_ClassMark.aspx.cs" Inherits="T_ClassMark" %>

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
            <asp:Label ID="Label1" runat="server" Text="查询指定课程成绩："></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Width="100px"></asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="查询" OnClick="Button1_Click"></asp:Button>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="查询指定学生成绩："></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="学号"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="姓名"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="查询" OnClick="Button2_Click"></asp:Button>
            <br /><br /><br />
            <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label><br />
            <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView><br /><br />
            <asp:Button ID="Button3" runat="server" Text="按课程成绩排序" Visible="false" OnClick="Button3_Click"></asp:Button>
            <asp:Button ID="Button4" runat="server" Text="<<返回主界面" OnClick="Button4_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
