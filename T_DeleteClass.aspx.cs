using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class T_DeleteClass : System.Web.UI.Page
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
        if (!IsPostBack)//判断页面是否为第一次刷新，如果是则执行if里面的代码，功能是为gridview1绑定数据源，并为checkbox复选框赋初值
        {
            //根据登录的用户名，为gridview1绑定数据源
            sql = "select Class.PK, classname, tname, Class.memo, t_PK, signed from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND t_PK = Teacher.PK) ORDER BY Class.PK ASC";
            mycon.Open();
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
            DataSet myds = new DataSet();
            myda.Fill(myds, "Class");
            GridView1.Visible = true;
            GridView1.DataSource = myds.Tables["Class"];
            GridView1.DataBind();

            //根据Class表中的signed字段的值为checkbox赋值
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (myds.Tables["Class"].Rows[i][5].ToString() == "True")
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked = true;
                }
            }

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
        //从数据库中获取与当前页面gridview中的数据相同的数据信息
        sql = "select Class.PK, classname, tname, Class.memo, t_PK, signed from Class join Teacher on(t_username = '"+Session["Pubname"].ToString()+"' AND t_PK = Teacher.PK) ORDER BY Class.PK ASC";
        mycon.Open();
        SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);
        DataSet myds = new DataSet();
        myda.Fill(myds, "Class");

        //比较gridview中的checkbox的值与Class表中的signed的值得相同与否对数据库进行修改
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            bool check = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("check")).Checked;//获取gridview中的checkbox的值
            if (check.ToString() != myds.Tables["Class"].Rows[i][5].ToString())//如果checkbox的值与Class中signed的值不想等，则执行if里面的代码，功能为根据checkbox中的值对Class中的signed字段进行修改
            {
                //根据gridview1的主键的值来从数据库中读取数据
                sql = "select * from Class where PK = "+ Convert.ToInt32(GridView1.DataKeys[i].Value)+"";
                myda = new SqlDataAdapter(sql, mycon);
                myda.Fill(myds, "NewClass");

                //对NewClass表进行修改
                DataTable dt = myds.Tables["NewClass"];
                DataRow dr;
                if (check)//如果checkbox中的值为true，则将NewClass的signed字段的值改为true
                {

                    dr = dt.Rows[0];
                    dr.BeginEdit();
                    dr["signed"] = "True";
                    dr.EndEdit();
                }
                else//如果checkbox中的值为false，则将NewClass的signed字段的值改为false
                {
                    dr = dt.Rows[0];
                    dr.BeginEdit();
                    dr["signed"] = "False";
                    dr.EndEdit();
                }
                SqlCommandBuilder mycom = new SqlCommandBuilder(myda);//更新数据库
                myda.Update(myds, "NewClass");
                dt.Clear();
                dt.Dispose();
            }
        }

        //刷新gridview1中的数据
        sql = "select Class.PK, classname, tname, Class.memo, t_PK, signed from Class join Teacher on(t_username = "+Session["Pubname"].ToString()+" AND t_PK = Teacher.PK) ORDER BY Class.PK ASC";
        myda = new SqlDataAdapter(sql, mycon);
        myda.Fill(myds, "Refurbish");
        GridView1.Visible = true;
        GridView1.DataSource = myds.Tables["Refurbish"];
        GridView1.DataBind();
        for(int j = 0; j < myds.Tables["Refurbish"].Rows.Count; j++)
        {
            if (myds.Tables["Refurbish"].Rows[j][5].ToString() == "True")
            {
                ((CheckBox)GridView1.Rows[j].Cells[0].FindControl("check")).Checked = true;
            }
        }

        myds.Dispose();
        mycon.Close();
    }
}