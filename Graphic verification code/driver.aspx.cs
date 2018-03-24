using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Graphic_verification_code
{
    public partial class driver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
             protected void Button1_Click1(object sender, EventArgs e)
        {
            if (TextBox1.Text.ToString() == Session["numbers"].ToString())
            {
                Response.Write("成功！");
                TextBox1.Text = "";
            }
            else
            {
                Response.Write("失败!");
                TextBox1.Text = "";
            }

        }

             protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
             {
                 ImageButton1.ImageUrl="image.aspx";
             }      
    }
}