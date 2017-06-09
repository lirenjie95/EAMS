using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_Class : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //根据登录的用户名，来获取该教师的开课信息
        string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
        SqlConnection mycon = new SqlConnection(con);
        string sql = "select Class.PK as 课程编码, Class.classname as 课程名称, Class.tname as 任课教师, Class.memo as 备注 from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK AND Class.signed = 1)";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Class");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Class"];
        GridView1.DataBind();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
        mycon.Dispose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
}