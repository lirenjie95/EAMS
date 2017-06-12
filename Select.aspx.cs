using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Select : System.Web.UI.Page
{
    string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)
        {
            GetCourseTable();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection mycon = new SqlConnection(con);
        mycon.Open();
        string sql =
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM Teacher,Course,TC" +
            " WHERE TC.Cno = Course.Cno AND Teacher.Tno = TC.Tno AND Course.Semester = '2017-1'" +
            " EXCEPT" +
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM SC,Teacher,Course,TC" +
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " TC.Semester = SC.Semester AND SC.Semester = Course.Semester AND" +
            " TC.Cno = SC.Cno AND Teacher.Tno = TC.Tno AND SC.cno = Course.Cno AND Course.Semester = '2017-1'";
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Can");
        for (int i = 0; i < GridView1.Rows.Count; i++)//根据Dataset对象中myds中表的条目来确定循环次数
        {
            bool k = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked;
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked)
            //判断当前gridview中第i行是否被选中，如果被选中则对数据库进行修改
            {
                //根据gridview主键所包含的选课编码从数据库中读取数据，并对数据进行修改
                string cno = myds.Tables["Can"].Rows[i][0].ToString();
                string sno = Session["Pubno"].ToString();
                string updatesql = 
                    " INSERT INTO SC VALUES('"+sno+"','"+cno+"','2017-1',NULL);"+
                    " UPDATE Course SET Cnow = Cnow+1 WHERE Cno = '"+cno+"';";
                SqlDataAdapter updateda = new SqlDataAdapter(updatesql, mycon);
                DataSet updateds = new DataSet();
                updateda.Fill(updateds, "Select");
                updateds.Dispose();
                updateda.Dispose();
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('选课成功')", true);
        mycon.Close();
        mycon.Dispose();
        GetCourseTable();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection mycon = new SqlConnection(con);
        mycon.Open();
        string sql =
            " SELECT SC.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM SC,Teacher,Course,TC" +
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " TC.Semester = SC.Semester AND SC.Semester = Course.Semester AND" +
            " TC.Cno = SC.Cno AND Teacher.Tno = TC.Tno AND SC.cno = Course.Cno AND Course.Semester = '2017-1'";
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Now");
        for (int i = 0; i < GridView2.Rows.Count; i++)//根据Dataset对象中myds中表的条目来确定循环次数
        {
            if (((CheckBox)GridView2.Rows[i].Cells[0].FindControl("check")).Checked)
            //判断当前gridview中第i行是否被选中，如果被选中则对数据库进行修改
            {
                //根据gridview主键所包含的选课编码从数据库中读取数据，并对数据进行修改
                string cno = myds.Tables["Now"].Rows[i][0].ToString();
                string sno = Session["Pubno"].ToString();
                string updatesql =
                    " DELETE FROM SC WHERE Sno='" + sno + "' AND Cno='" + cno + "' AND Semester = '2017-1';" +
                    " UPDATE Course SET Cnow = Cnow-1 WHERE Cno = '" + cno + "';";
                SqlDataAdapter updateda = new SqlDataAdapter(updatesql, mycon);
                DataSet updateds = new DataSet();
                updateda.Fill(updateds, "Drop");
                updateds.Dispose();
                updateda.Dispose();
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('退课成功')", true);
        mycon.Close();
        mycon.Dispose();
        GetCourseTable();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
    protected void GetCourseTable()
    {
        SqlConnection mycon = new SqlConnection(con);
        mycon.Open();
        string sql =
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM Teacher,Course,TC" +
            " WHERE TC.Cno = Course.Cno AND Teacher.Tno = TC.Tno AND Course.Semester = '2017-1'" +
            " EXCEPT" +
            " SELECT Course.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM SC,Teacher,Course,TC" +
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " TC.Semester = SC.Semester AND SC.Semester = Course.Semester AND" +
            " TC.Cno = SC.Cno AND Teacher.Tno = TC.Tno AND SC.cno = Course.Cno AND Course.Semester = '2017-1'";
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Can");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Can"];
        GridView1.DataBind();
        sql =
            " SELECT SC.Cno AS 课程编码,Course.Cname AS 课程名称,Course.Credit AS 学分,Course.Cattribute AS 课程属性," +
            " Course.Ctime AS 课程时间,Course.Cloc AS 课程地点, Course.Call AS 课程人数,Course.Cnow AS 已选人数," +
            " Teacher.Tname as 任课教师 FROM SC,Teacher,Course,TC" +
            " WHERE SC.Sno = '" + Session["Pubno"].ToString() + "' AND" +
            " TC.Semester = SC.Semester AND SC.Semester = Course.Semester AND" +
            " TC.Cno = SC.Cno AND Teacher.Tno = TC.Tno AND SC.cno = Course.Cno AND Course.Semester = '2017-1'";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Now");
        GridView2.Visible = true;
        GridView2.DataSource = myds.Tables["Now"];
        GridView2.DataBind();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
        mycon.Dispose();
    }
}