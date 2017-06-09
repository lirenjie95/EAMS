<%@ Page Language="C#" AutoEventWireup="true" CodeFile="T_DeleteClass.aspx.cs" Inherits="T_DeleteClass" %>

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
            <asp:Label ID="Label1" runat="server" Text="课程信息"></asp:Label><br />
            <asp:GridView ID="GridView1" runat="server" Visible="false" DataKeyNames="PK" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="选择"><%--添加一个checkbox复选框列--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码"><%--添加一个‘课程编码’列，并与‘PK’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="PK" runat="server" Text='<%#Eval("PK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称"><%--添加一个‘课程名称’列，并与‘classname’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="classname" runat="server" Text='<%#Eval("classname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任课教师"><%--添加一个‘任课教师’列，并与‘tname’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="tname" runat="server" Text='<%#Eval("tname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注"><%--添加一个‘备注’列，并与‘memo’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="memo" runat="server" Text='<%#Eval("memo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教师编号"><%--添加一个‘教师编号’列，并与‘t_PK’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="t_PK" runat="server" Text='<%#Eval("t_PK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="signed" Visible="false"><%--添加一个‘signed’列，并与‘signed’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="signed" runat="server" Text='<%#Eval("signed") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView><br /><br />
            <%--<asp:Label ID="Label2" runat="server" Text="标记课程是否可选"></asp:Label><br />--%>
            <%--<asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList><br /><br />--%>
            <asp:Button ID="Button1" runat="server" Text="<<返回主界面" OnClick="Button1_Click"></asp:Button>
<%--            <asp:Button ID="Button3" runat="server" Text="加载标记信息" OnClick="Button3_Click"></asp:Button>--%>
            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
