using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{//连接数据库
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    protected void Page_Load(object sender, EventArgs e){}
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql,role;
        if ((CheckBox1.Checked && CheckBox2.Checked)||(!CheckBox1.Checked && !CheckBox2.Checked))
        {
            Label3.Text = "请选择登录身份（学生或教师）";
            return;
        }
        if((TextBox1.Text == "")||(TextBox2.Text == ""))
        {
            Label3.Text = "请输入用户名和密码";
            return;
        }
        mycon.Open();//开启数据库

        if (CheckBox2.Checked)//判断是否为学生登录
        {
            sql = "SELECT Sname FROM Student WHERE Sno ='" + TextBox1.Text + "' AND Spasswd = '" + TextBox2.Text + "'";
            role = "Student";
        }
        else{//否则为教师登录
            sql = "SELECT Tname FROM Teacher WHERE Tno ='" + TextBox1.Text + "' AND Tpasswd = '" + TextBox2.Text + "'";
            role = "Teacher";
        }
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();//将数据库里的表独到Dataset对象（myds）里面
        myda.Fill(myds,role);
        if (myds.Tables[0].Rows.Count != 0)
        {
            Session["role"] = role;//全局变量在浏览器关闭之前存储各个网页公用数据
            Session["Pubno"] = TextBox1.Text;
            Session["Pubpasswd"] = TextBox2.Text;
            Session["Pubname"] = myds.Tables[role].Rows[0][0].ToString();
            Server.Transfer("Layout.aspx");//跳转网页
        }
        else
        {
            Label3.Text = "用户名或密码错误";
        }
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
}