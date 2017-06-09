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
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //根据登录的用户名，将该学生的原有信息加载到TextBox中
        string name = Session["Pubname"].ToString();
        string sql = "select name, sex, memo from Student where s_username = '"+name+"'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Student");
        TextBox1.Text = myds.Tables["Student"].Rows[0][0].ToString();
        TextBox2.Text = myds.Tables["Student"].Rows[0][1].ToString();
        TextBox3.Text = myds.Tables["Student"].Rows[0][2].ToString();
        mycon.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //根据TextBox中的内容，对数据库中的Student表的相应条目进行修改
        string name = Session["Pubname"].ToString();
        string sql = "update Student set name = '" + TextBox1.Text + "', sex = '" + TextBox2.Text + "', memo = '" + TextBox3.Text + "' where s_username = '" + name + "'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Student");
        myds.Clear();

        //显示修改后的信息
        sql = "select PK as 学号, name as 姓名, sex as 性别, s_username as 用户名, memo as 备注 from Student where s_username = '"+name+"'";
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