<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Layout.aspx.cs" Inherits="Layout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script lang="jv">
        function Stu_Information()//javascript函数，功能为在iframe中载入S_Information.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Student")//判断登录成员是否为学生
            {
                document.getElementById("right_iframe").src = "S_Information.aspx";
            }
            else
            {
                document.getElementById("S_Information").style.display = "none";
            }
        }
        function Stu_Change()//javascript函数，功能为在iframe中载入Updata.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Student")//判断登录成员是否为学生
            {
                document.getElementById("right_iframe").src = "Updata.aspx";
            }
            else
            {
                document.getElementById("S_Change").style.display = "none";
            }
        }
        function Stu_Class()//javascript函数，功能为在iframe中载入S_Class.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Student")//判断登录成员是否为学生
            {
                document.getElementById("right_iframe").src = "S_Class.aspx";
            }
            else
            {
                document.getElementById("S_Class").style.display = "none";
            }
        }
        function Stu_Mark()//javascript函数，功能为在iframe中载入S_Mark.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Student")//判断登录成员是否为学生
            {
                document.getElementById("right_iframe").src = "S_Mark.aspx";
            }
            else
            {
                document.getElementById("S_Mark").style.display = "none";
            }
        }
        function Stu_Select()//javascript函数，功能为在iframe中载入Select.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Student")//判断登录成员是否为学生
            {
                document.getElementById("right_iframe").src = "Select.aspx";
            }
            else {
                document.getElementById("Select").style.display = "none";
            }
        }
        function Tea_Information()//javascript函数，功能为在iframe中载入T_Information.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Teacher")//判断登录成员是否为教师
            {
                document.getElementById("right_iframe").src = "T_Information.aspx";
            }
            else
            {
                document.getElementById("T_Information").style.display = "none";
            }
        }
        function Tea_Class()//javascript函数，功能为在iframe中载入T_Class.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Teacher")//判断登录成员是否为教师
            {
                document.getElementById("right_iframe").src = "T_Class.aspx";
            }
            else
            {
                document.getElementById("T_Class").style.display = "none";
            }
        }
        function Tea_ClassMark()//javascript函数，功能为在iframe中载入T_ClassMark.aspx页面
        {
            if ("<%=Session["role"].ToString()%>" == "Teacher")//判断登录成员是否为教师
            {
                document.getElementById("right_iframe").src = "T_ClassMark.aspx";
            }
            else
            {
                document.getElementById("T_ClassMark").style.display = "none";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="top" style="position:absolute; width:100%; height:50px; background-color:pink; border:solid; top:0px">
        <div style="position:absolute; left:5px; top:10px">
            <asp:Button ID="Button1" runat="server" Text="退出登录" OnClick="Button1_Click" />
        </div>
        <center>
            <asp:Label ID="Label1" runat="server" Text="某旦的教务系统(仿)" Font-Size="XX-Large"></asp:Label>
        </center>
        <div style="position:absolute; right:10px; top:0px">
            <asp:Label ID="Label2" runat="server"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text="欢迎光临某旦的教务系统(仿)"></asp:Label>
        </div>
    </div>
    <div id="left" style="position:absolute; height:93%; width:200px; top:55px; background-color:gray; border:solid">
        <div id="Student" style="position:absolute; height:415px; width:200px; background-color:purple; align-items:center">
            <asp:Label ID="Label4" runat="server" Text="学生" Font-Size="XX-Large"></asp:Label><br />
            <input type="button" id="S_Information" value="学生个人信息" onclick="Stu_Information()" /><br />
            <input type="button" id="S_Change" value="修改个人信息" onclick="Stu_Change()" /><br />
            <input type="button" id="S_Class" value="查询已选课程及教材" onclick="Stu_Class()" /><br />
            <input type="button" id="S_Mark" value="查询修读情况" onclick="Stu_Mark()" /><br />
            <input type="button" id="Select" value="学生选课" onclick="Stu_Select()" /><br />
        </div>
        <div id="Teacher" style="position:absolute; height:400px; width:200px; background-color:brown; top:415px">
            <asp:Label ID="Label5" runat="server" Text="教师" Font-Size="XX-Large"></asp:Label><br />
            <input type="button" id="T_Information" value="教师个人信息" onclick="Tea_Information()" /><br />
            <input type="button" id="T_Class" value="查询已开课程" onclick="Tea_Class()" /><br />
            <input type="button" id="T_ClassMark" value="查看学生成绩" onclick="Tea_ClassMark()" /><br />
        </div>
    </div>
    <iframe id="right_iframe" style="position:absolute; left:214px; top:55px; height:815px; width:1473px; overflow:auto; background-color: #66FFFF;" src="mainpage.aspx"></iframe>
    </form>
</body>
</html>
