using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class UpData : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //根据登录的用户名，将该学生的原有信息加载到TextBox中
        string sql = "SELECT Spasswd,Sbirth,Stel from Student WHERE Sno = '" + Session["Pubno"].ToString() + "'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Student");
        TextBox1.Text = myds.Tables["Student"].Rows[0]["Spasswd"].ToString();
        TextBox2.Text = myds.Tables["Student"].Rows[0]["Sbirth"].ToString();
        TextBox3.Text = myds.Tables["Student"].Rows[0]["Stel"].ToString();
        mycon.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //根据TextBox中的内容，对数据库中的Student表的相应条目进行修改
        string sql = "UPDATE Student SET Spasswd = '"+ TextBox1.Text + "',Sbirth= '" + TextBox2.Text +
            "',Stel= '" + TextBox3.Text + "' WHERE Sno = '" + Session["Pubno"].ToString() + "'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Student");
        myds.Clear();

        //显示修改后的信息
        sql = "SELECT Sno AS 学号,Sname AS 姓名,Sbirth AS 生日, Stel AS 电话,Spasswd AS 新密码"+
            " FROM Student WHERE Sno= '" + Session["Pubno"].ToString() + "'";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Student");
        GridView1.DataSource = myds.Tables["Student"];
        GridView1.DataBind();
        mycon.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
}