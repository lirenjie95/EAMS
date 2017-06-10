using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class T_ClassMark : System.Web.UI.Page
{
    static string con = "server=10.153.170.140;uid=sa;pwd=lrj@130279;database=HW;Trusted_Connection=no";
    SqlConnection mycon = new SqlConnection(con);
    string sql = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        if (!IsPostBack)
        {
            //根据登录的用户名从Class表中获取该教师所开的课程
            sql =
                " SELECT Course.Cno AS 课程编号,Course.Semester AS 学期,Course.Cname AS 课程名称," +
                " SC.Sno AS 学号,Student.Sname AS 学生姓名,SC.Score AS 成绩" +
                " FROM Course,TC,SC,Student" +
                " WHERE TC.Tno = '" + Session["Pubno"].ToString() + "' AND TC.Cno = Course.Cno AND TC.Semester = Course.Semester AND" +
                " SC.Cno = Course.Cno AND SC.Semester = Course.Semester AND Student.Sno = SC.Sno";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "Score");

            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["Score"];
            GridView1.DataBind();
            myds.Dispose();
            myda.Dispose();
            mycon.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sql =
            " SELECT Course.Cno AS 课程编号,Course.Semester AS 学期,Course.Cname AS 课程名称," +
            " SC.Sno AS 学号,Student.Sname AS 学生姓名,SC.Score AS 成绩" +
            " FROM Course,TC,SC,Student" +
            " WHERE TC.Tno = '" + Session["Pubno"].ToString() + "' AND TC.Cno = Course.Cno AND TC.Semester = Course.Semester AND" +
            " SC.Cno = Course.Cno AND SC.Semester = Course.Semester AND Student.Sno = SC.Sno"+
            " ORDER BY 成绩";
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Score");
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainpage.aspx");
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        DisableControls(GridView1);//调用DisableControls()函数，将gridview1中的TextBox控件替换成liveral控件
        string style = @"<style>.text{mso-number-format:\@;}</script>";
        //用style字符串来存放gridview中列的样式，用response.write()方法来将样式写入到输出流中，最后就是将样式添加到“学号列”，可以在gridview控件的RowDataBound时间中完成
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        sql =
            " SELECT Course.Cno AS 课程编号,Course.Semester AS 学期,Course.Cname AS 课程名称," +
            " SC.Sno AS 学号,Student.Sname AS 学生姓名,SC.Score AS 成绩" +
            " FROM Course,TC,SC,Student" +
            " WHERE TC.Tno = '" + Session["Pubno"].ToString() + "' AND TC.Cno = Course.Cno AND TC.Semester = Course.Semester AND" +
            " SC.Cno = Course.Cno AND SC.Semester = Course.Semester AND Student.Sno = SC.Sno";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Score");
        //对数据库中的SC表进行修改
        string sno, cno, semester;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string c_mark = ((TextBox)GridView1.Rows[i].Cells[5].FindControl("成绩")).Text;
            //获取当前gridview中的成绩
            sno = myds.Tables["Score"].Rows[i][3].ToString();
            cno = myds.Tables["Score"].Rows[i][0].ToString();
            semester = myds.Tables["Score"].Rows[i][1].ToString();
            if (c_mark == "")//判断当前gridview中的成绩是否为空
            {
                c_mark = "NULL";
            }
            else
            {
                c_mark = "'" + c_mark + " '";
            }
            string updatesql =
                " UPDATE SC SET Score = " + c_mark +
                " WHERE Cno = '" + cno + "' AND Semester = '" + semester + "' AND Sno = '" + sno + "'";
            SqlDataAdapter updateda = new SqlDataAdapter(updatesql, mycon);//更行数据库
            //根据当前gridview中的成绩对数据库中的SC表进行修改
            DataSet updateds = new DataSet();
            updateda.Fill(updateds, "update");
            updateds.Dispose();
            updateda.Dispose();
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('保存成功')", true);
        myda = new SqlDataAdapter(sql, mycon);
        myds = new DataSet();
        myda.Fill(myds, "Score");
        myds.Dispose();
        myda.Dispose();
        mycon.Close();
    }
    public override void VerifyRenderingInServerForm(Control control) { }
    //重载该函数才不会出现“處理 GridView的控制項。 'GridView' 必須置於有 runat=server 的表單標記之中”错误
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("class", "text");
        }
    }
}