<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="driver.aspx.cs" Inherits="Graphic_verification_code.driver" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p>
            验证码：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        
        <%--<a href="javascript:void(0)" onclick="getletters(4)" >看不清，换一张</a> --%>
        <%--  <!-- -->， html comment会包含在最终生成的html文件中
           现在使用的注释，aspx comment 不会包含在最终生成的html文件中 --%>       
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image.aspx" OnClick="ImageButton1_Click" />
        <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click1"  />
    
    </div>
    </form>
</body>
</html>
