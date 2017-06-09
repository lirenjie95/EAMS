using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_ChangeMark : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)//判断页面是否为第一次刷新，如果是则执行if里面的代码，功能是为dropdownlist绑定数据
        {
            //根据用户名从数据库中获取该教师所开的课程信息
            string sql = "select Class.PK, Class.classname from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK)";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "ClassInformation");

            //在myds中的ClassInfromation表添加一列（C_Infor）
            DataTable dt = myds.Tables["ClassInformation"];
            dt.Columns.Add("C_Infor", System.Type.GetType("System.String"), "PK + '，' + classname");

            //对myds中的表ClassInformation按字段PK从小到大的顺序进行排序
            DataView dv = dt.DefaultView;
            dv.Sort = "PK ASC";
            dt = dv.ToTable();

            //为dropdownlist绑定数据源
            DropDownList1.DataSource = myds.Tables["ClassInformation"];
            DropDownList1.DataTextField = "C_Infor";
            DropDownList1.DataValueField = "PK";
            DropDownList1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DropDownList1.SelectedValue) == 0)//判断是否选择了课程信息
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择课程信息')", true);
            return;
        }
        if (Convert.ToInt32(DropDownList2.SelectedValue) == 0)//判断是否选择了学生信息
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择学生信息')", true);
        }
        if (TextBox1.Text != "")//判断TextBox1是否为空，如果不为空则执行if里面的代码，功能为判断输入的数据是否能转为int型
        {
            try//判断输入的数据是否能转为int型，如果不能则执行catch模块跳出函数
            {
                Convert.ToInt16(TextBox1.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请输入数字')", true);
                return;
            } 
        }

        //根据droplist中所选择的内容来获取学生的成绩信息
        string sql = "select * from Mark where c_PK = " + Convert.ToInt32(DropDownList1.SelectedValue) + " AND s_PK = " + Convert.ToInt32(DropDownList2.SelectedValue) + "";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Mark");

        //对数据库进行修改
        DataTable dt = myds.Tables["Mark"];
        DataRow dr = dt.Rows[0];
        dr.BeginEdit();
        if (TextBox1.Text != "")//判断TextBox1是否为空，如果不为空则mark字段等于TextBox1中的内容
        {
            dr["mark"] = Convert.ToInt16(TextBox1.Text);
        }
        else//如果TextBox1为空，则将mark字段设置城空
        {
            dr["mark"] = DBNull.Value;
        }
        dr.EndEdit();
        SqlCommandBuilder mycom = new SqlCommandBuilder(myda);//更新数据库
        myda.Update(myds, "Mark");
        dt.Dispose();        
        myds.Dispose();
        myda.Dispose();
        mycom.Dispose();
        mycon.Close();
        DropDownList2_SelectedIndexChanged(sender, e);//刷新gridview中的内容
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DropDownList1.SelectedValue) == 0)//判断是否选择了课程信息，如果没有选择，则执行if里面的语句，将gridview1设为不可见
        {
            GridView1.Visible = false;
        }
        int k = Convert.ToInt16(DropDownList1.SelectedValue);//将以选择的条目的‘PK’信息转为int型
        //根据所选的课程序号从SelectedClass表中选取选课信息
        string sql = "select Student.PK, Student.name from Student join SelectedClass on(SelectedClass.C_PK = " + k + " AND Selected = 1 AND Student.PK = S_PK)";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "StudentInformation");

        //增加一列，该列的值为‘PK’和‘name’的和
        DataTable dt = myds.Tables["StudentInformation"];
        dt.Columns.Add("S_Infor", System.Type.GetType("System.String"), "PK + '，' + name");
        
        //为StudentInfromation表插入一行
        DataRow dr = dt.NewRow();
        dr["PK"] = 0;
        dr["name"] = "Please Select";
        dt.Rows.Add(dr);
        
        //对StudentInformation表按PK的升序排序
        DataView dv = dt.DefaultView;
        dv.Sort = "PK ASC";

        //根据选择的课程信息为dropdownlist2绑定数据源
        DropDownList2.DataSource = myds.Tables["StudentInformation"];
        DropDownList2.DataTextField = "S_Infor";
        DropDownList2.DataValueField = "PK";
        DropDownList2.DataBind();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
        if (DropDownList2.Items.Count == 1)
        {
            GridView1.Visible = false;
            Button2.Visible = false;
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DropDownList2.SelectedValue) == 0)//判断选择的学生信息的有效值是否为0，如果为0则执行if里面的代码隐藏gridview
        {
            GridView1.Visible = false;
        }
        //刷新gridview中的信息
        string sql = "select Student.PK as 学号, Student.name as 学生姓名, Class.classname as 课程名称, Mark.mark as 成绩 from Mark join Student on(Student.PK = " + Convert.ToInt32(DropDownList2.SelectedValue) + " AND Mark.s_PK = " + Convert.ToInt32(DropDownList2.SelectedValue) + ") join Class on(Class.PK = " + Convert.ToInt32(DropDownList1.SelectedValue) + " AND Mark.c_PK = " + Convert.ToInt32(DropDownList1.SelectedValue) + ")";
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
        Button2.Visible = true;
    }
}