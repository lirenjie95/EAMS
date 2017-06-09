using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_Change : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    static string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if ((TextBox1.Text == "") || (TextBox2.Text == "") || (TextBox3.Text == ""))//判断TextBox1或TextBox2或TextBox3是否为空，如果为空则跳出函数
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('个人信息不完整');", true);
            return;
        }
        //根据用户名从Teacher表中获取教师的个人信息
        sql = "select * from Teacher where t_username = '"+Session["Pubname"].ToString()+"'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Teacher");

        //对教师跟人信息进行修改
        DataTable dt = myds.Tables["Teacher"];
        DataRow dr = dt.Rows[0];
        dr.BeginEdit();
        dr["name"] = TextBox1.Text;
        dr["sex"] = TextBox2.Text;
        dr["class"] = TextBox3.Text;
        dr["memo"] = TextBox4.Text;
        dr.EndEdit();
        SqlCommandBuilder mycom = new SqlCommandBuilder(myda);//更新数据库中的Teacher表
        myda.Update(myds, "Teacher");
        dt.Clear();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //根据用户名读取教师的个人信息
        sql = "select name, sex, class, memo from Teacher where t_username = '" + Session["Pubname"].ToString() + "'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Teacher");
        TextBox1.Text = myds.Tables["Teacher"].Rows[0][0].ToString();
        TextBox2.Text = myds.Tables["Teacher"].Rows[0][1].ToString();
        TextBox3.Text = myds.Tables["Teacher"].Rows[0][2].ToString();
        TextBox4.Text = myds.Tables["Teacher"].Rows[0][3].ToString();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
}