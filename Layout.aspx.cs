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
        if (Session["Pubname"] == null)//判断登录用户名是否为空，如果为空，则跳转到登录界面
        {
            Server.Transfer("Entry.aspx");
        }
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "clossbutton", true);
        Label2.Text = DateTime.Now.ToString("yy/mm/dd");//获取登录时间
        Label3.Text = Session["Pubname"].ToString() + "，" + "欢迎您！";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Pubname"] = null;
        Session["Pubpassword"] = null;
        Server.Transfer("Entry.aspx");
    }
}