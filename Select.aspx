<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Select.aspx.cs" Inherits="Select" %>

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
<%--            <asp:CheckBoxList ID="CheckBoxList1" runat="server" TextAlign="Left">
            </asp:CheckBoxList>
            <br /><br />--%>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="SelectPK" AutoGenerateColumns="False"><%--将gridview设置城不自动匹配列，并将SelectPK列设置为主键--%>
                <Columns>
                    <asp:TemplateField HeaderText="选择"><%--添加选择的checkbox列--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SelectPK" Visible="false"><%--添加选择的SelectPK列，并与‘SelectPK’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="SelectPK" runat="server" Text='<%#Eval("SelectPK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码"><%--添加选择的‘课程编码’列，并与‘PK’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="PK" runat="server" Text='<%#Eval("PK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称"><%--添加选择的‘课程名称’列，并与‘classname’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="classname" runat="server" Text='<%#Eval("classname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任课教师"><%--添加选择的‘任课教师’列，并与‘tname’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="tname" runat="server" Text='<%#Eval("tname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注"><%--添加选择的‘备注’列，并与‘memo’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="memo" runat="server" Text='<%#Eval("memo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selected" Visible="false"><%--添加选择的Selected列，并与‘Selected’绑定--%>
                        <ItemTemplate>
                            <asp:Label ID="Selected" runat="server" Text='<%#Eval("Selected") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br /><br />
            <asp:Button ID="Button3" runat="server" Text="<<返回主界面" OnClick="Button3_Click"></asp:Button>
            <%--<asp:Button ID="Button1" runat="server" Text="加载课程信息" OnClick="Button1_Click"></asp:Button>--%>
            <asp:Button ID="Button2" runat="server" Text="保存" OnClick="Button2_Click"></asp:Button>
        </center>
    </div>
    </form>
</body>
</html>
