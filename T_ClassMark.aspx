<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_ClassMark.aspx.cs" Inherits="T_ClassMark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="学号" OnRowDataBound="GridView1_RowDataBound">
                <HeaderStyle BackColor="#EDEDED" />
                <Columns>
                    <asp:TemplateField HeaderText="课程编号"><%--添加一个‘课程编号’列，并与‘课程编号’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="课程编号" runat="server" Text='<%#Eval("课程编号") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学期"><%--添加一个‘学期’列，并与‘学期’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="学期" runat="server" Text='<%#Eval("学期") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称"><%--添加一个‘课程名称’列，并与‘课程名称’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="课程名称" runat="server" Text='<%#Eval("课程名称") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学号"><%--添加一个‘学号’列，并与‘学号’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="学号" runat="server" Text='<%#Eval("学号") %>' ReadOnly="True" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学生姓名" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="学生姓名" runat="server" Text='<%#Eval("学生姓名") %>' ReadOnly="true" BorderStyle="None" BackColor="#66FFFF"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成绩"><%--添加一个‘成绩’列，并与‘成绩’绑定--%>
                        <ItemTemplate>
                            <asp:TextBox ID="成绩" runat="server" Text='<%#Eval("成绩") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView><br /><br />
        <asp:Button ID="Button1" runat="server" Text="按成绩排序" OnClick="Button1_Click"></asp:Button>
        <asp:Button ID="Button2" runat="server" Text="<<返回主界面" OnClick="Button2_Click"></asp:Button><br /><br />
        <asp:Button ID="Button4" runat="server" Text="保存" OnClick="Button4_Click"></asp:Button>
        <asp:Button ID="Button3" runat="server" Text="导出到Excel中" OnClick="Button3_Click"></asp:Button>
    </div>
    </form>
</body>
</html>
