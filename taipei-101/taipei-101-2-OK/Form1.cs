using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace taipei_101_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int w = pictureBox1.Image.Width;
            int h = pictureBox1.Image.Height;

            Point[] p = selectBmp(new Bitmap(pictureBox1.Image), 0, 0, w, h, Color.FromArgb(255, 255, 255));

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawRectangle(Pens.Red, p[0].X, p[0].Y, p[1].X - p[0].X, p[1].Y - p[0].Y);

            pictureBox1.Image = bmp;
        }


        Point[] selectBmp(Bitmap bmp, int sx, int sy, int ex, int ey, Color backColor)
        {
            Point[] pts = new Point[2];

            int xL = 0, xR = 0, yT = 0, yB = 0;

            // 垂直掃描線
            bool isTouch = false; // 紀錄現在是要找起始邊界還是結束邊界 F:起始, T:結束
            for (int x = sx; x < ex; x++)
            {
                bool isCheckColor = false;
                // 檢查垂直掃描線上是否有物件
                for (int y = sy; y < ey; y++)
                {
                    if (bmp.GetPixel(x, y).R != backColor.R)
                    {
                        isCheckColor = true;
                        break;
                    }
                }

                if (isCheckColor && !isTouch)
                {
                    // 如果抓取到物件，而且目前是起使邊
                    // 紀錄x為起始位置,並告至現在要搜尋結束邊界
                    xL = x;
                    isTouch = true;
                }
                else if (!isCheckColor && isTouch)
                {
                    xR = x;
                    break;
                }
            }

            // 水平掃描線
            isTouch = false;
            for (int y = sy; y < ey; y++)
            {
                bool isCheckColor = false;
                for (int x = xL; x <= xR; x++)
                {
                    // 如果顏色與背景色不同時，代表碰到物體
                    if (bmp.GetPixel(x, y).R != backColor.R)
                    {
                        isCheckColor = true;
                        break;
                    }
                }
                if (isCheckColor && !isTouch)
                {
                    yT = y;
                    isTouch = true;
                }
                else if (!isCheckColor && isTouch)
                {
                    yB = y;
                    break;
                }
            }
            pts[0] = new Point(xL, yT); // 左上
            pts[1] = new Point(xR, yB); // 右下
            return pts;
        }
    }
}
