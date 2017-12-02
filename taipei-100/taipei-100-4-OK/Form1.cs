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

namespace taipei_100_4
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
            Point[] range = selectBmp(bmp, 0, 0, bmp.Width, bmp.Height, Color.White);

            double size = (range[1].X - range[0].X) * (range[1].Y - range[0].Y);

            int blackCount = 0;
            for (int r = range[0].Y; r < range[1].Y; r++)
            {
                for (int c = range[0].X; c < range[1].X; c++)
                {
                    Color color = bmp.GetPixel(c, r);
                    if ((color.R + color.G + color.B) / 3 < 127)
                        blackCount++;
                }
            }
            double val = (double)blackCount / size * 100;

            label1.Text = GetNumber(val).ToString();
        }

        int GetNumber(double val)
        {
            double[] arr = new double[] { 38.125, 28.125, 32.5, 28.75, 32.9545, 33.3333, 35, 24.375, 43.125, 35.625 };

            for (int i = 0; i < arr.Length; i++)
            {
                double err = arr[i] * 0.0005;
                if (arr[i] - err < val && val < arr[i] + err)
                    return i;
            }
            return -1;
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
