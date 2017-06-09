<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainpage.aspx.cs" Inherits="mainpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background:url(main.jpg) repeat-y center top">
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="Calendar1" runat="server" SelectedDate="06/09/2017 12:47:44">
            <DayHeaderStyle BackColor="#996633" />
            <SelectedDayStyle BackColor="#993333" />
            <SelectorStyle BackColor="#FFFF66" />
            <TitleStyle BackColor="#9900FF" />
            <TodayDayStyle BackColor="#FF33CC" />
            <WeekendDayStyle BackColor="#FF9933" ForeColor="Blue" />
        </asp:Calendar>
    </div>
    </form>
</body>
</html>
