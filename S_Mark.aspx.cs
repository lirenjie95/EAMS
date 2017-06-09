using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class S_Mark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //连接数据库读取学生成绩表，并将其显示在gridview控件中
        string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
        SqlConnection mycon = new SqlConnection(con);
        string sql =
            " SELECT DISTINCT Course.Cno AS 课程编码,Course.Cname AS 课程名称,SC.Semester AS 学期," +
            " Course.Credit AS 学分,SC.Score AS 成绩,Score_To_GPA.GPA AS 绩点" +
            " FROM Course,SC,Score_to_GPA" +
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND SC.Cno = Course.Cno AND SC.Score = Score_To_GPA.Score";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Score");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Score"];
        GridView1.DataBind();
        Double SumGPA = 0, Credit = 0;
        foreach (DataRow row in myds.Tables["Score"].Rows)
        {
            if (row["成绩"].ToString() != "P ")//因为成绩是CHAR(2)
            {
                SumGPA += Convert.ToDouble(row["学分"]) * Convert.ToDouble(row["绩点"]);
                Credit += Convert.ToDouble(row["学分"]);
            }
        }
        Label1.Text = "您的目前GPA为" + (SumGPA/Credit).ToString("0.00");
        sql =
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称"+
            " FROM Course,RC,Student" +
            " WHERE Student.Sno = '" + Session["Pubno"].ToString() + "' AND"+
            " RC.Cno = Course.Cno AND RC.Major = Student.Major AND RC.Sgrade = Student.Sgrade"+
            " EXCEPT"+
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称"+
            " FROM Course,SC"+
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " Course.Cno = SC.Cno";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "RC");
        GridView2.Visible = true;
        GridView2.DataSource = myds.Tables["RC"];
        GridView2.DataBind();
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