using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class S_Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //连接数据库读取学生信息表，并将其显示在gridview控件中
        string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
        SqlConnection mycon = new SqlConnection(con);
        string sql = 
            "SELECT Sno AS 学号,Sname AS 姓名,Ssex as 性别,Sbirth AS 出生日期,Sgrade AS 年级,Stel AS 电话,SDM.Mname AS 专业"+
            " FROM Student,SDM WHERE Sno = '"+Session["Pubno"].ToString()+"' AND Student.Major=SDM.Major";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, con);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Student");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Student"];
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