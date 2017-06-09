<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_InputMark.aspx.cs" Inherits="T_InputMark" %>--%>
<%@Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="~/T_InputMark.aspx.cs" Inherits="T_InputMark" %>
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
            <asp:Label ID="Label1" runat="server" Text="选择课程："></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Width="100" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
            </asp:DropDownList><br /><br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="markNum" OnRowDataBound="GridView1_RowDataBound">
                <HeaderStyle BackColor="#EDEDED" />
                <Columns>
                    <asp:TemplateField HeaderText="学号"><%--添加一个‘学号’列，并与‘StudentNum’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="StudentNum" runat="server" Text='<%#Eval("StudentNum") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学生姓名"><%--添加一个‘学生姓名’列，并与‘name’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="name" runat="server" Text='<%#Eval("name") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称"><%--添加一个‘课程名称’列，并与‘classname’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="classname" runat="server" Text='<%#Eval("classname") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="markNum" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="markNum" runat="server" Text='<%#Eval("markNum") %>' ReadOnly="true"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成绩"><%--添加一个‘成绩’列，并与‘mark’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="mark" runat="server" Text='<%#Eval("mark") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView><br /><br />
            <asp:Button ID="Button2" runat="server" Text="<<返回主界面" OnClick="Button2_Click"></asp:Button>
            <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click"></asp:Button><br /><br />
            <asp:Button ID="Button3" runat="server" Text="导出到Excel中" OnClick="Button3_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
