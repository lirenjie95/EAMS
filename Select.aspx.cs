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
    static string con = "Data Source = localhost; Initial Catalog = test; User ID = sa; password = 551188";
    static string sql = "";
    SqlConnection mycon = new SqlConnection(con);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)//判断界面是否第一次刷新，如果是则执行if里面的代码，功能为将选课信息加载东gridview控件中显示
        {
            Label1.Text = "课程信息";
            sql = "select SelectPK, Class.PK, classname, tname, Class.memo, Selected from Class join SelectedClass on(Class.PK = C_PK AND signed = 1) join Student on(s_username = '"+Session["Pubname"].ToString()+"' AND S_PK = Student.PK) ORDER BY SelectPK";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "Class");
            GridView1.DataSource = myds.Tables["Class"];
            GridView1.DataBind();
            for (int i = 0; i < myds.Tables["Class"].Rows.Count; i++)
            {
                if (myds.Tables["Class"].Rows[i][5].ToString() == "True")
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked = true;
                }
            }
            myda.Dispose();
            myds.Dispose();
            mycon.Close();
        }
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string name = Session["Pubname"].ToString();
    //    sql = "select SelectPK, Class.PK as 课程代码, classname as 课程名称, tname as 老师, Selected from SelectedClass join Class on(Class.PK = C_PK AND signed = 1) join Student on(s_username = '"+name+"' AND S_PK = Student.PK)";
    //    mycon.Open();
    //    SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
    //    DataSet myds = new DataSet();
    //    myda.Fill(myds, "Class");
    //    DataTable dt = myds.Tables[0];
    //    dt.Columns.Add("课程信息", System.Type.GetType("System.String"), "课程代码+','+课程名称+','+老师");
    //    this.CheckBoxList1.DataSource = myds.Tables[0];
    //    CheckBoxList1.DataTextField = "课程信息";
    //    CheckBoxList1.DataValueField = "SelectPK";
    //    CheckBoxList1.DataBind();
    //    for (int i = 0; i < this.CheckBoxList1.Items.Count; i++ )
    //    {
    //        if(myds.Tables[0].Rows[i][4].ToString() == "True")
    //        {
    //            this.CheckBoxList1.Items[i].Selected = true;
    //        }
    //    }
    //    Label1.Text = "已选课程信息";
    //    sql = "select Class.PK as 课程代号, classname as 课程名称, tname as 任课教师, Class.memo as 备注 from Class join SelectedClass on(Selected = 1 AND Class.PK = C_PK AND signed = 1) join Student on(s_username = '" + name + "' AND S_PK = Student.PK)";
    //    myda = new SqlDataAdapter(sql, mycon);
    //    myda.Fill(myds, "SC_Class");
    //    GridView1.DataSource = myds.Tables["SC_Class"];
    //    GridView1.DataBind();
    //    myds.Dispose();
    //    mycon.Close();
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        //从数据库中获取与当前gridview中相同的数据，即Page_load（）函数中加载的数据
        string name = Session["Pubname"].ToString();
        sql = "select SelectPK, Class.PK, classname, Student.PK, Selected from Class join SelectedClass on(Class.PK = C_PK AND signed = 1) join Student on(s_username = '" + name + "' AND S_PK = Student.PK) ORDER BY SelectPK";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Class");

        //对数据库中的表进行修改
        //for (int i = 0; i < this.CheckBoxList1.Items.Count; i++ )
        for (int i = 0; i < myds.Tables["Class"].Rows.Count; i++ )//根据Dataset对象中myds中表的条目来确定循环次数
        {
            //if(this.CheckBoxList1.Items[i].Selected.ToString() != myds.Tables[0].Rows[i][4].ToString())
            if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked.ToString() != myds.Tables["Class"].Rows[i][4].ToString())//判断当前gridview中的选择像与数据库中的是否不相同，如果不相同则执行if里面的代码对数据库进行修改
            {
                //int a_Mark = Convert.ToInt32(this.CheckBoxList1.Items[i].Value);
                int a_Mark = Convert.ToInt32(GridView1.DataKeys[i].Value);//获取gridview主键的值
                int S_PK = Convert.ToInt32(myds.Tables[0].Rows[i][3]);//获取学号
                int C_PK = Convert.ToInt32(myds.Tables[0].Rows[i][1]);//获取选课序号
                //if (this.CheckBoxList1.Items[i].Selected.ToString() == "True")
                if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked)//判断当前gridview中第i行是否被选中，如果被选中则执行if里面的代码，对数据库进行修改
                {
                    //sql = "UpDate SelectedClass set Selected = 1 where SelectPK = " + a_Mark + "";
                    //myda = new SqlDataAdapter(sql, mycon);
                    //myda.Fill(myds, "ChangeSelectedClass");
                    //sql = "insert into Mark(PK, s_PK, c_PK, mark) values(" + a_Mark + ", " + S_PK + ", " + C_PK + ", null)";
                    //myda = new SqlDataAdapter(sql, mycon);
                    //myda.Fill(myds, "UpdateMark");

                    //根据gridview主键所包含的选课编码从数据库中读取数据，并对数据进行修改
                    sql = "select * from SelectedClass where SelectPK = " + a_Mark + "";
                    myda = new SqlDataAdapter(sql, mycon);
                    myda.Fill(myds, "SelectedClass");
                    DataTable dt = myds.Tables["SelectedClass"];
                    DataRow dr = dt.Rows[0];
                    dr.BeginEdit();
                    dr[3] = "True";
                    dr.EndEdit();
                    SqlCommandBuilder mycom = new SqlCommandBuilder(myda);
                    myda.Update(myds, "SelectedClass");

                    //根据选课表的选课情况对成绩表进行修改
                    sql = "select * from Mark";
                    myda = new SqlDataAdapter(sql, mycon);
                    myda.Fill(myds, "Mark");
                    DataTable dt1 = myds.Tables["Mark"];
                    DataRow dr1 = dt1.NewRow();
                    dr1["PK"] = dr["SelectPK"];
                    dr1["s_PK"] = dr["S_PK"];
                    dr1["c_PK"] = dr["C_PK"];
                    dr1["mark"] = DBNull.Value;
                    dt1.Rows.Add(dr1);
                    mycom = new SqlCommandBuilder(myda);
                    myda.Update(myds, "Mark");
                    dt.Clear();
                    dt1.Clear();
                }
                else//当前行被取消选中，则执行else里面的代码对数据库进行修改
                {
                    //sql = "UpDate SelectedClass set Selected = 0 where SelectPK = " + a_Mark + "";
                    //myda = new SqlDataAdapter(sql, mycon);
                    //myda.Fill(myds, "ChangeSelectedClass");
                    //sql = "delete from Mark where Mark.PK = "+a_Mark+"";
                    //myda = new SqlDataAdapter(sql, mycon);
                    //myda.Fill(myds, "UpdateMark");

                    //根据gridview主键所包含的选课编码从数据库中读取数据，并对数据进行修改
                    sql = "select * from SelectedClass where SelectPK = " + a_Mark + "";
                    myda = new SqlDataAdapter(sql, mycon);
                    myda.Fill(myds, "SelectedClass");
                    DataTable dt = myds.Tables["SelectedClass"];
                    DataRow dr = dt.Rows[0];
                    dr.BeginEdit();
                    dr[3] = "False";
                    dr.EndEdit();
                    SqlCommandBuilder mycom = new SqlCommandBuilder(myda);
                    myda.Update(myds, "SelectedClass");
                    dt.Clear();

                    //根据选课表的选课情况对成绩表进行修改
                    sql = "select * from Mark";
                    myda = new SqlDataAdapter(sql, mycon);
                    myda.Fill(myds, "Mark");
                    DataTable dt1 = myds.Tables["Mark"];
                    DataColumn[] cols = new DataColumn[] { dt1.Columns["PK"] };
                    dt1.PrimaryKey = cols;
                    DataRow dr1 = dt1.Rows.Find(a_Mark);
                    dr1.Delete();
                    mycom = new SqlCommandBuilder(myda);
                    myda.Update(myds, "Mark");
                    dt1.Clear();
                }
            }
        }
        //Label1.Text = "已选课程信息";
        //sql = "select Class.PK as 课程代号, classname as 课程名称, tname as 任课教师, Class.memo as 备注 from Class join SelectedClass on(Selected = 1 AND Class.PK = C_PK AND signed = 1) join Student on(s_username = '"+name+"' AND S_PK = Student.PK)";
        //myda = new SqlDataAdapter(sql, mycon);
        //myda.Fill(myds, "SC_Class");
        //GridView1.DataSource = myds.Tables["SC_Class"];
        //GridView1.DataBind();

        //对gridview所显示的内容进行刷新
        sql = "select SelectPK, Class.PK, classname, tname, Class.memo, Selected from Class join SelectedClass on(Class.PK = C_PK AND signed = 1) join Student on(s_username = '"+Session["Pubname"].ToString()+"' AND S_PK = Student.PK) ORDER BY SelectPK";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Refurbish");
        GridView1.DataSource = myds.Tables["Refurbish"];
        GridView1.DataBind();
        for (int j = 0; j < myds.Tables["Refurbish"].Rows.Count; j++)
        {
            if (myds.Tables["Refurbish"].Rows[j][5].ToString() == "True")
            {
                ((CheckBox)GridView1.Rows[j].Cells[j].FindControl("check")).Checked = true;
            }
        }

        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if(e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        CheckBoxList CheckSelect = (CheckBoxList)e.Row.FindControl("CheckBoxList1");
    //        if(CheckSelect != null)
    //        {
    //            BindCheckBoxList(CheckSelect);
    //        }
    //    }
    //}
    //protected void BindCheckBoxList(CheckBoxList cbl)
    //{
    //    sql = "select Class.PK as 课程代码, classname as 课程名称, tname as 老师, Selected as 选择 from Class join SelectedClass on(Class.PK = C_PK) join Student on(s_username = '2' AND S_PK = Student.PK)";
    //    mycon.Open();
    //    SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
    //    DataSet myds = new DataSet();
    //    myda.Fill(myds, "Class");
    //    CheckBoxList1.DataSource = myds.Tables[0];
    //    //CheckBoxList1.DataTextField = "课程名称";
    //    CheckBoxList1.DataValueField = "课程代码";
    //    CheckBoxList1.DataBind();
    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
}