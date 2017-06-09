using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class T_InputMark : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)//判断界面是否为第一次刷新，如果是第一次刷新，则执行if里面的代码，功能是为dropdownlist绑定数据源
        {
            //根据登录用户名来检索该教师所开的课程信息，并按课程序号的升序进行排序
            string sql = "select Class.PK, classname from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND Class.t_PK = Teacher.PK) ORDER BY Class.PK ASC";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "Class");

            //添加一列（C_Infor）
            DataTable dt = myds.Tables["Class"];
            dt.Columns.Add("C_Infor", System.Type.GetType("System.String"), "PK + '，' + classname");

            //为dropdownlist1绑定数据源
            DropDownList1.DataSource = myds.Tables["Class"];
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
        //应用for循环来判断所输入的学生成绩是否为int型数据
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((TextBox)GridView1.Rows[i].Cells[3].FindControl("mark")).Text != "")
            {
                try
                {
                    Convert.ToInt16(((TextBox)GridView1.Rows[i].Cells[3].FindControl("mark")).Text);
                }
                catch
                {
                    int signPK = Convert.ToInt32(((TextBox)GridView1.Rows[i].Cells[0].FindControl("StudentNum")).Text);
                    string signname = ((TextBox)GridView1.Rows[i].Cells[1].FindControl("name")).Text;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('学号为："+signPK+"，姓名为："+signname+"的学生成绩录入错误')", true);
                    return;
                } 
            }
        }

        //从数据库中获取与当前gridview中显示的数据相匹配的数据
        bool success = false;
        string sql = "select Mark.PK, s_PK, c_PK, mark from Mark join Student on(Student.PK = Mark.s_PK) join Class on(Mark.c_PK = Class.PK AND Class.PK = "+Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)+") ORDER BY Student.PK ASC";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Mark");

        //对数据库中的Mark表进行修改
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string c_mark = ((TextBox)GridView1.Rows[i].Cells[3].FindControl("mark")).Text;//获取当前gridview中的成绩
            if (myds.Tables["Mark"].Rows[i][3].ToString() != c_mark)//将gridview中的成绩与数据Mark表中相应测条目成绩比较是否相等，如果不相等，则执行if里面的代码，功能为根据当前gridview中的成绩对数据库中的Mark表进行修改
            {
                int g_PK = Convert.ToInt32(GridView1.DataKeys[i].Value);
                string updatesql = "";
                if (c_mark != "")//判断当前gridview中的成绩是否为空
                {
                    updatesql = "update Mark set mark = " + Convert.ToInt16(c_mark) + " where PK = " + g_PK + "";
                }
                else
                {
                    updatesql = "update Mark set mark = Null where PK = "+g_PK+"";
                }
                SqlDataAdapter updatemyda = new SqlDataAdapter(updatesql, mycon);//更行数据库
                DataSet updatemyds = new DataSet();
                updatemyda.Fill(updatemyds, "Mark");
                updatemyds.Dispose();
                updatemyda.Dispose();
                success = true;
            }
        }
        if (success)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('保存成功')", true);
        }
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //根据dropdownlist中选择情况，根性gridview中的数据
        string sql = "select Mark.PK as markNum, Student.PK as StudentNum, Student.name, Class.classname, Mark.mark from Mark join Student on(Student.PK = Mark.s_PK) join Class on(Mark.c_PK = Class.PK AND Class.PK = " + Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value) + ") ORDER BY StudentNum ASC";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "StudentMark");
        GridView1.DataSource = myds.Tables["StudentMark"];
        GridView1.DataBind();
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DisableControls(GridView1);//调用DisableControls()函数，将gridview1中的TextBox控件替换成liveral控件
        string style = @"<style>.text{mso-number-format:\@;}</script>";//用style字符串来存放gridview中列的样式，用response.write()方法来将样式写入到输出流中，最后就是将样式添加到“学好列”，可以在gridview控件的RowDataBound时间中完成
        Response.ClearContent();//清除缓冲区流中的所有内容输出
        Response.AddHeader("content-disposition", "attachment; filename = MyExcelFile.xls");//将HTTP的头添加到输出流中
        Response.Charset = "gb2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
        Response.ContentType = "application/ms-excel";//设置输出文件为excel文件
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)//重载该函数才不会出现“處理 GridView的控制項。 'GridView' 必須置於有 runat=server 的表單標記之中”错误
    {
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("class", "text");
        }
    }
    private void DisableControls(Control gv)//将gv中的TextBox控件替换城liberal控件
    {
        TextBox tb = new TextBox();
        Literal liter = new Literal();
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(TextBox))
            {
                liter.Text = (gv.Controls[i] as TextBox).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, liter);
            }
            if (gv.Controls[i].HasControls())
            {
                DisableControls(gv.Controls[i]);
            }
        }
    }
}