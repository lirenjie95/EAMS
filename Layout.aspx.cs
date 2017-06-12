using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Layout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Pubno"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        Label2.Text = DateTime.Now.ToString("yyyy/MM/dd");//获取登录时间
        if (Session["role"].ToString() == "Student")
        {
            Label3.Text = Session["Pubname"].ToString() + " 同学，欢迎您！";
        }
        else
        {
            Label3.Text = Session["Pubname"].ToString() + " 老师，欢迎您！";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Pno"] = null;
        Session["Pubname"] = null;
        Session["Pubpasswd"] = null;
        Server.Transfer("Entry.aspx");
    }
}