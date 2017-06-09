<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_ChangeMark.aspx.cs" Inherits="T_ChangeMark" %>

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
            <asp:Label ID="Label1" runat="server" Text="选择课程信息："></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Width="100" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="true">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
            </asp:DropDownList>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="选择学生信息："></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server" Width="100" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <br /><br />
            <asp:Label ID="Label3" runat="server" Text="成绩："></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br /><br /><br />
            <asp:Label ID="Label4" runat="server" Text="学生成绩信息" Visible="false"></asp:Label>
            <br />
            <asp:GridView ID="GridView1" runat="server" Visible="false"></asp:GridView>
            <br /><br />
            <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
            <asp:Button ID="Button2" runat="server" Text="保存" Visible="false" OnClick="Button2_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
