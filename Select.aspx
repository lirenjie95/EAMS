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
        <asp:Label ID="Label1" runat="server" Text="当前学期为2017-1。可选课程如下："></asp:Label>
        <asp:GridView ID="GridView1" runat="server" DataKeyNames="课程编码" AutoGenerateColumns="False">
            <%--将gridview设置城不自动匹配列，并将课程编码设置为主键--%>
                <Columns>
                    <asp:TemplateField HeaderText="选择"><%--添加选择的checkbox列--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码">
                        <ItemTemplate>
                            <asp:Label ID="课程编码" runat="server" Text='<%#Eval("课程编码") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称">
                        <ItemTemplate>
                            <asp:Label ID="课程名称" runat="server" Text='<%#Eval("课程名称") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学分">
                        <ItemTemplate>
                            <asp:Label ID="学分" runat="server" Text='<%#Eval("学分") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程属性">
                        <ItemTemplate>
                            <asp:Label ID="课程属性" runat="server" Text='<%#Eval("课程属性") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程时间">
                        <ItemTemplate>
                            <asp:Label ID="课程时间" runat="server" Text='<%#Eval("课程时间") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程地点">
                        <ItemTemplate>
                            <asp:Label ID="课程地点" runat="server" Text='<%#Eval("课程地点") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程人数">
                        <ItemTemplate>
                            <asp:Label ID="课程人数" runat="server" Text='<%#Eval("课程人数") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="已选人数">
                        <ItemTemplate>
                            <asp:Label ID="已选人数" runat="server" Text='<%#Eval("已选人数") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任课教师">
                        <ItemTemplate>
                            <asp:Label ID="任课教师" runat="server" Text='<%#Eval("任课教师") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="选课" OnClick="Button1_Click"></asp:Button><br /><br />
        <asp:Label ID="Label2" runat="server" Text="已选课程如下："></asp:Label>
        <asp:GridView ID="GridView2" runat="server" DataKeyNames="课程编码" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="选择"><%--添加选择的checkbox列--%>
                        <ItemTemplate>
                            <asp:CheckBox ID="check" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程编码">
                        <ItemTemplate>
                            <asp:Label ID="课程编码" runat="server" Text='<%#Eval("课程编码") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称">
                        <ItemTemplate>
                            <asp:Label ID="课程名称" runat="server" Text='<%#Eval("课程名称") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学分">
                        <ItemTemplate>
                            <asp:Label ID="学分" runat="server" Text='<%#Eval("学分") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程属性">
                        <ItemTemplate>
                            <asp:Label ID="课程属性" runat="server" Text='<%#Eval("课程属性") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程时间">
                        <ItemTemplate>
                            <asp:Label ID="课程时间" runat="server" Text='<%#Eval("课程时间") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程地点">
                        <ItemTemplate>
                            <asp:Label ID="课程地点" runat="server" Text='<%#Eval("课程地点") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程人数">
                        <ItemTemplate>
                            <asp:Label ID="课程人数" runat="server" Text='<%#Eval("课程人数") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="已选人数">
                        <ItemTemplate>
                            <asp:Label ID="已选人数" runat="server" Text='<%#Eval("已选人数") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="任课教师">
                        <ItemTemplate>
                            <asp:Label ID="任课教师" runat="server" Text='<%#Eval("任课教师") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        <asp:Button ID="Button2" runat="server" Text="退课" OnClick="Button2_Click"></asp:Button>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="<<返回主界面" OnClick="Button3_Click"></asp:Button>
    </div>
    </form>
</body>
</html>
