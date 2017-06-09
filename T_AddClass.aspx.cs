using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_AddClass : System.Web.UI.Page
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
        //获取已开的课程信息
        sql = "select Class.PK as 课程编码, Class.classname as 课程名称, Class.tname as 任课教师, Class.memo as 备注 from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK AND Class.signed = 1)";
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
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if ((TextBox1.Text == "") || (TextBox2.Text == ""))//判断TextBox1或TextBox2是否为空，如果为空则跳出函数
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请输入课程信息');", true);
            return;
        }

        //获取教师的个人信息（Teacher表）
        sql = "select * from Teacher where t_username = '"+Session["Pubname"].ToString()+"'";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Teacher");

        //获取当前课程编码的最大编码
        sql = "select MAX(PK) from Class";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "MaxClass");

        //读入课程信息表
        sql = "select * from Class";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Class");

        //在课程信息表内插入新的课程
        DataTable dt1 = myds.Tables["Class"];
        DataRow dr1 = dt1.NewRow();
        dr1["PK"] = Convert.ToInt32(myds.Tables["MaxClass"].Rows[0][0]) + 1;
        dr1["classname"] = TextBox1.Text;
        dr1["tname"] = TextBox2.Text;
        if (TextBox3.Text == "")
        {
            dr1["memo"] = DBNull.Value;
        }
        else
        {
            dr1["memo"] = TextBox3.Text;
        }
        dr1["t_PK"] = myds.Tables["Teacher"].Rows[0][0];
        dr1["signed"] = "True";
        dt1.Rows.Add(dr1);
        SqlCommandBuilder mycom = new SqlCommandBuilder(myda);//更新数据库中的Class表
        myda.Update(myds, "Class");

        //读入学生信息表
        sql = "select * from Student";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Student");

        //获取选课表中当前选课编码的最大值
        sql = "select MAX(SelectPK) from SelectedClass";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "MaxSelectedClass");

        //读入选课信息表
        sql = "select * from SelectedClass";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "SelectedClass");

        //在选课信息表中插入新的课程条目
        DataTable dt2 = myds.Tables["SelectedClass"];
        DataRow dr2;
        for (int i = 0; i < myds.Tables["Student"].Rows.Count; i++)//根据学生信息表中所包含的学生人数判断循环次数
        {
            dr2 = dt2.NewRow();
            dr2["SelectPK"] = Convert.ToInt32(myds.Tables["MaxSelectedClass"].Rows[0][0]) + 1 + i;
            dr2["S_PK"] = myds.Tables["Student"].Rows[i][0];
            dr2["C_PK"] = myds.Tables["Class"].Rows.Count;
            dr2["Selected"] = "False";
            dt2.Rows.Add(dr2);
        }
        mycom = new SqlCommandBuilder(myda);//更新数据库中的SelectedClass表
        myda.Update(myds, "SelectedClass");
        dt1.Clear();
        dt2.Clear();
        myds.Dispose();
        mycon.Close();
        Page_Load(sender, e);//刷新gridview中的内容
    }
}