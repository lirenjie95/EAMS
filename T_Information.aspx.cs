using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_Information : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //根据登录的用户名，在数据库中检索该教师的个人信息，并显示在gridview中
        string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
        SqlConnection mycon = new SqlConnection(con);
        string sql =
            "SELECT Tno AS 工号,Tname AS 姓名,Tsex as 性别,Tbirth AS 出生日期,Tstage AS 职称,Ttel AS 电话,SDM.Mname AS 专业" +
            " FROM Teacher,SDM WHERE Tno = '" + Session["Pubno"].ToString() + "' AND Teacher.Major=SDM.Major";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Teacher");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Teacher"];
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