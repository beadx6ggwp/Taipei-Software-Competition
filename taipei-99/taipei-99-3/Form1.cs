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

namespace taipei_99_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(ofd.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            // 最右下角是記錄到，物體邊界外，會多1，所以要-1
            Point[] pts = getSize(bmp, Color.White);
            // 記得他的輸出Y軸是要正的
            Point p1 = new Point(pts[0].X, bmp.Height - pts[0].Y);
            Point p2 = new Point(pts[1].X - 1, bmp.Height - pts[1].Y - 1);

            double m = (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);

            label1.Text = $"線段左邊座標:{p1.X}, {p1.Y}";
            label2.Text = $"線段右邊座標:{p2.X},{p2.Y}";
            label3.Text = $"線段斜率:{m:0.##}";
        }

        Point[] getSize(Bitmap bmp, Color backColor)
        {
            Point[] pts = new Point[2];

            int lx = 0, ty = 0, rx = 0, by = 0;

            bool isTouch = false;
            for (int x = 0; x < bmp.Width; x++)
            {
                bool hasObj = false;
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (bmp.GetPixel(x, y).R != 255)
                    {
                        hasObj = true;
                        break;
                    }
                }
                if (hasObj && !isTouch)
                {
                    lx = x;
                    isTouch = true;
                }
                else if (!hasObj && isTouch)
                {
                    rx = x;
                    break;
                }
            }

            isTouch = false;
            for (int y = 0; y < bmp.Height; y++)
            {
                bool hasObj = false;
                for (int x = lx; x < rx; x++)
                {
                    if (bmp.GetPixel(x, y).R != 255)
                    {
                        hasObj = true;
                        break;
                    }
                }
                if (hasObj && !isTouch)
                {
                    ty = y;
                    isTouch = true;
                }
                else if (!hasObj && isTouch)
                {
                    by = y;
                    break;
                }
            }
            // 如果左上角是黑色，代表他是從左上到右下的斜線
            if (bmp.GetPixel(lx, ty).R != 255)
            {
                pts[0] = new Point(lx, ty);
                pts[1] = new Point(rx, by);
            }
            else// 否則代表他是從左下到右上的斜線
            {
                pts[0] = new Point(lx, by);
                pts[1] = new Point(rx, ty);
            }
            return pts;
        }
    }
}
