using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
namespace Graphic_verification_code
{
    public partial class image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                createimage(display());//getnumbers(4),getletters(4),getmixture(4);
            }
        }
        //获取随机中文函数
         private static object[] getchinesecode(int n)
        {
            String[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            object[] bytes = new object[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                int r1 = rnd.Next(11, 14);//B0-F7,汉字都从第16区B0开始，并且从区位D7开始以后的汉字都是和很难见到的繁杂汉字
                string str_r1 = rBase[r1].Trim();
                int r2;
                if(r1==13)//如果第1位是D的话，第2位区位码就不能是7以后的十六进制数
                {
                    r2 = rnd.Next(0, 8);
                }
                else
                {
                     r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();
                int r3 = rnd.Next(10, 16);//A1-FE
                string str_r3 = rBase[r3].Trim();
                int r4;
                if(r3==10)
                {
                     r4 = rnd.Next(1, 16);//每区的第一个位置，没有汉字，因此随机生成的区位码第3位如果是A的话，第4位就不能是0
                }
                else if(r3==15)
                {
                     r4 = rnd.Next(0, 15);//最后一个位置都是空的,没有汉字,第3位如果是F的话，第4位就不能是F
                }
                else
                {
                     r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();
                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };
                bytes.SetValue(str_r, i);
            }
            return bytes;
        }
        public String display()
        {
            Encoding gb = Encoding.GetEncoding("gb2312");//引用System.Text这个命名空间，获取GB2编码表
            object[] bytes = getchinesecode(4);
            string s = String.Empty;
            foreach (object byt in bytes)
            {
                String str1 = gb.GetString((byte[])Convert.ChangeType(byt, typeof(byte[])));
                s = s + str1;
            }
            Session["chinese"] = s;
            return s;
        }

    

        //获取随机数字函数
        private String getletters(int n)
        {
            String s = "";
            int cal = 0;
            String[] num = new String[52];//必须要初始化，在你初始化的时候要指定长度
            for (int i = 65; i <= 122; i++)
            {
                if (i > 90 && i < 97)
                    continue;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] btNumber = new byte[] { (byte)i };
                num[cal] = asciiEncoding.GetString(btNumber);
                cal += 1;
            }
            Random ran = new Random();
            for (int i = 0; i < n; i++)
            {
                int t = ran.Next(num.Length);//产生一个小于num.Length的数字
                s += num[t];
            }
            Session["letters"] = s.ToLower();
            return s;
        }
        //获取随机数字
         public String getnumbers(int n)
        {
            String s="";
            String vail = "0,1,2,3,4,5,6,7,8,9";
            String[] num = vail.Split(',');
            //String[] num={"0","1","2","3","4","5","6","7","8","9"};这样写也可以，但是看书上跟别人的代码都用了上面的方法
            Random ran = new Random();
            for(int i=0;i<n;i++)
            {
                int t = ran.Next(num.Length);//产生一个小于num.Length的数字
                s += num[t];
            }
            Session["numbers"] = s;
            return s;
        }
        //获取数字英文混合
         private String getmixture(int n)
         {
             String s = "";
             int cal = 0;
             String[] num = new String[62];

             for (int i = 65; i <= 122; i++)
             {
                 if (i > 90 && i < 97)
                     continue;
                 System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                 byte[] btNumber = new byte[] { (byte)i };
                 num[cal] = asciiEncoding.GetString(btNumber);
                 cal += 1;
             }
             for (int j = 0; j <= 9; j++)
             {
                 num[cal] = Convert.ToString(j);
                 cal++;
             }
             Random ran = new Random();
             for (int i = 0; i < n; i++)
             {
                 int t = ran.Next(num.Length);//产生一个小于num.Length的数字
                 s += num[t];
             }
             Session["mixture"] = s.ToLower();
             return s;
         }
        //创建图片
         private void createimage(String  vaild)//参数为生成的随机数字之类的字符串
        {
            if (vaild == null || vaild.Trim() == String.Empty)
                return;
            //源图像  
            //System.Drawing.Bitmap oldBmp = new System.Drawing.Bitmap(imgUrl);

            //新图像,并设置新图像的宽高  
            Bitmap image = new Bitmap(vaild.Length*20,30);//图像,并设置图像的宽高 
            Graphics g = Graphics.FromImage(image);////从图像获取对应的Graphics  
            try
            {
                Random random = new Random();
                g.Clear(Color.White);//清空图片背景色
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Coral), x1, y1, x2, y2);//使用Coral颜色在点 (x1, y1) 和 (x2, y2) 之间画一条线  
                }
                Font font = new Font("黑体", 13, FontStyle.Italic);//设置字体;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);//设置渐变画刷 
                g.DrawString(vaild, font, brush, 2, 2);//在指定位置并且用指定的 Brush 和 Font 对象绘制指定的文本字符串。
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}