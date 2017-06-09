using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class S_Class : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //连接数据库读取课程信息表，并将其显示到gridview控件中
        string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
        SqlConnection mycon = new SqlConnection(con);
        string sql = 
            " SELECT DISTINCT SC.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数,"+
            " SC.Semester AS 学期,Teacher.Tname as 任课教师 FROM SC,Teacher,Course,TC"+
            " WHERE SC.Sno = '"+Session["Pubno"].ToString()+"' AND"+
            " TC.Cno = SC.Cno AND Teacher.Tno = TC.Tno AND SC.cno = Course.Cno"+
            " ORDER BY SC.Semester";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds,"Course");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Course"];
        GridView1.DataBind();
        sql =
            " SELECT DISTINCT SC.Cno AS 课程编码,Course.Cname AS 课程名称,Book.Bno AS ISBN编码,Book.Bname AS 书名," +
            " Book.Bauthor AS 作者,Book.Bpublish AS 出版社, Book.Bvalue AS 价格" +
            " FROM SC,TC,Book,Course"+
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " TC.Cno = SC.Cno AND SC.cno = Course.Cno AND TC.Bno = Book.Bno";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Book");
        GridView2.Visible = true;
        GridView2.DataSource = myds.Tables["Book"];
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