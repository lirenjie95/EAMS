using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_ClassMark : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)//判断界面是否为第一次刷新，如果是则执行if里面的代码，功能是为dropdownlist绑定数据源
        {
            //根据登录的用户名从Class表中获取该教师所开的课程
            sql = "select Class.PK, classname from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND t_PK = Teacher.PK)";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "Class");

            //为myds中的Class表添加一列
            DataTable dt = myds.Tables["Class"];
            DataRow dr = dt.NewRow();
            dr["PK"] = 0;
            dr["classname"] = "*";
            dt.Rows.Add(dr);

            //将myds中的Class表按PK的升序进行排序
            DataView dv = dt.DefaultView;
            dv.Sort = "PK Asc";
            dt = dv.ToTable();

            //为dropdownlist绑定数据源
            DropDownList1.DataSource = myds.Tables["Class"];
            DropDownList1.DataTextField = "classname";
            DropDownList1.DataValueField = "PK";
            DropDownList1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();  
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label5.Visible = true;
        int sign = 0;
        
        //利用for循环来获取dropdownlist当前所选取条目的课程编号PK的值，整个for循环可以用Convert.ToInt16(DropDownList1.SelectedValue)来代替
        for (int i = 1; i < DropDownList1.Items.Count; i++)
        {
            if (DropDownList1.Items[i].Selected)
            {
                sign = Convert.ToInt16(DropDownList1.Items[i].Value);
                Label5.Text = DropDownList1.Items[i].Text + "成绩";
                break;
            }
        }
        if (sign != 0)//如果选取的课程编号PK不为0，则执行if里面的代码，功能为显示选择该课程的所有学生的课程成绩
        {
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Mark.mark as 成绩 from Mark join Student on(Mark.c_PK = " + sign + " AND Student.PK = Mark.s_PK) ORDER BY Student.PK ASC";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "StudentMark");
            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["StudentMark"];
            GridView1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
        else//如果课程编码为0（*），则显示该选择教师所开课程的所有学生的课程成绩
        {
            Label5.Text = "全部课程成绩";
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.PK as 课程编码, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.PK = Mark.s_PK) join Class on(Mark.c_PK = Class.PK) join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK) ORDER BY Student.PK, Class.PK ASC";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "StudentMark");
            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["StudentMark"];
            GridView1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
        Button3.Visible = true;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button3.Visible = false;
        if ((TextBox1.Text == "") && (TextBox2.Text == ""))//如果TextBox1和TextBox2都为空则执行if里面的代码跳出函数
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请输入学号或姓名')", true);
            return;
        }
        if ((TextBox1.Text != "") && (TextBox2.Text != ""))//如果TextBox1和TextBox2都不为空则执行if里面的代码，功能为判断TextBox1中的数据是否为int型，如果不为int型则执行catch跳出函数，如果是int型则为sql赋值
        {
            try
            {
                int a = Convert.ToInt32(TextBox1.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('学号输入错误')", true);
                return;
            }
            //根据TextBox1和TextBox2中的信息编写对数据库进行查询的SQL语句
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.PK = "+Convert.ToInt32(TextBox1.Text)+" AND Student.name = '"+TextBox2.Text+"' AND Mark.s_PK = Student.PK) join Class on(Mark.c_PK = Class.PK) join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK)";
        }
        if ((TextBox1.Text != "") && (TextBox2.Text == ""))//如果TextBox1不为空，TextBox2为空则执行if里面的代码，功能为先判断TextBox1是否为int型，如果不是则执行catch模块跳出函数，如果是则为sql赋值
        {
            try
            {
                int a = Convert.ToInt32(TextBox1.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('学号输入错误')", true);
                return;
            }
            //根据TextBox1中的信息编写对数据库进行查询的SQL语句
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.PK = " + Convert.ToInt32(TextBox1.Text) + " AND Mark.s_PK = Student.PK) join Class on(Mark.c_PK = Class.PK) join Teacher on(t_username = '" + Session["Pubname"].ToString() + "' AND Class.t_PK = Teacher.PK)"; 
        }
        if ((TextBox1.Text == "") && (TextBox2.Text != ""))//如果TextBox1为空，TextBox2不为空则执行if里面的代码，功能为sql进行赋值
        {
            //根据TextBox2中的内容编写对数据库进行查询的SQL语句
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.name = '" + TextBox2.Text + "' AND Mark.s_PK = Student.PK) join Class on(Mark.c_PK = Class.PK) join Teacher on(t_username = '" + Session["Pubname"].ToString() + "' AND Class.t_PK = Teacher.PK)";
        }

        //根据sql字符串中的内容对数据库进行查询
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "StudentMark");
        if (myds.Tables["StudentMark"].Rows.Count == 0)//如果检索条目为0，则跳出函数
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('该学生未选您的课程')", true);
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
            return;
        }
        Label5.Visible = true;
        Label5.Text = myds.Tables["StudentMark"].Rows[0][1].ToString() + "的课程成绩";
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["StudentMark"];
        GridView1.DataBind();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label5.Visible = true;
        int sign = 0;
        //利用for循环来获取dropdownlist当前所选取条目的课程编号PK的值，整个for循环可以用Convert.ToInt16(DropDownList1.SelectedValue)来代替
        for (int i = 1; i < DropDownList1.Items.Count; i++)
        {
            if (DropDownList1.Items[i].Selected)
            {
                sign = Convert.ToInt16(DropDownList1.Items[i].Value);
                Label5.Text = DropDownList1.Items[i].Text;
                break;
            }
        }
        if (sign != 0)//如果课程编码不等于0，则执行if里面的代码，动能为根据所选择的课程信息以mark的升序进行排序
        {
            sql = "select Student.PK as 学号, Student.name as 学生新明, Mark.mark as 成绩 from Mark join Student on(Student.PK = Mark.s_PK AND Mark.c_PK = " + sign + ") ORDER BY Mark.mark";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "StudentMark");
            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["StudentMark"];
            GridView1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
        else//如果课程编码为0（*），则执行else里面的代码，对所有课程以mark和Class.PK排序
        {
            Label5.Text = "全部课程成绩";
            sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.PK = Mark.s_PK) join Class on(Mark.c_PK = Class.PK) join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK) ORDER BY Mark.mark, Class.PK";
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "StudentMark");
            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["StudentMark"];
            GridView1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
}