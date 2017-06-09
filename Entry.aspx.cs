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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CheckBox1.Checked && CheckBox2.Checked)
        {
            Label3.Text = "请选择登录身份（学生或教师）";
            return;
        }
        if (!CheckBox1.Checked && !CheckBox2.Checked)
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
            string name1 = TextBox1.Text, password1 = TextBox2.Text;
            string sql = "select count(*) from User1 where username ='" + name1 + "' AND password = '" + password1 + "'";//获取符合条件（where）行的数量
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();//将数据库里的表独到Dataset对象（myds）里面
            myda.Fill(myds, "User1");
            if (Convert.ToInt16(myds.Tables["User1"].Rows[0][0]) != 0)
            {
                Session["role"] = "Student";//全局变量在浏览器关闭之前存储各个网页公用数据
                Session["Pubname"] = name1;
                Session["Pubpassword"] = password1;
                myds.Dispose();
                myda.Dispose();
                Server.Transfer("Layout.aspx");//跳转网页，有多种方法“注意整理”
            }
            else
            {
                Label3.Text = "用户名或密码错误";
                myds.Dispose();
                myda.Dispose();
            }
        }

        if (CheckBox1.Checked)//判断是否为教师登录
        {
            string sql = "select username, password from t_User";
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "t_User");
            for (int i = 0; i < myds.Tables["t_User"].Rows.Count; i++)
            {
                if ((myds.Tables["t_User"].Rows[i][0].ToString() == TextBox1.Text) && (myds.Tables["t_User"].Rows[i][1].ToString() == TextBox2.Text))
                {
                    Session["role"] = "Teacher";
                    Session["Pubname"] = TextBox1.Text;
                    Session["Pubpassword"] = TextBox2.Text;
                    myds.Dispose();
                    Server.Transfer("Layout.aspx");
                }
                else
                {
                    Label3.Text = "用户名或密码错误";
                    myds.Dispose();
                }
            }
        }

        mycon.Close();

    }
    protected void Logon_Click(object sender, EventArgs e)
    {
        Server.Transfer("Register.aspx");
    }
}