using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_103_1
{
    public partial class Form1 : Form
    {
        Graphics g;
        int size = 400;
        int sx;
        int sy;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox1.Text);
            int a = int.Parse(textBox2.Text);
            int m = int.Parse(textBox3.Text);
            int b = int.Parse(textBox4.Text);

            draw1(getPts1(x, a, m, b));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox1.Text);
            int a = int.Parse(textBox5.Text);
            int b = int.Parse(textBox6.Text);
            int c = int.Parse(textBox7.Text);
            int d = int.Parse(textBox8.Text);

            draw1(getPts2(x, a, b, c, d));
        }

        void draw1(PointF[] pts)
        {
            g.Clear(BackColor);
            g.TranslateTransform(50, 50);

            g.DrawLine(Pens.Gray, 0, size, size, size);
            for (int x = 0; x < pts.Length; x++)
            {
                g.FillRectangle(Brushes.Gray, x * sx, size - 0, 1, 4);
                g.DrawString(x.ToString(), new Font("consolas", 8), Brushes.Red, x * sx - 4, size - 0);
            }

            g.DrawLine(Pens.Gray, 0, 0, 0, size);
            for (int y = 1; y <= 10; y++)
            {
                g.FillRectangle(Brushes.Gray, 0, size - (float)y * 400 / 10, 4, 1);
                g.DrawString($"{(float)y / 10}", new Font("consolas", 8), Brushes.Red, -30, size - (float)y * 400 / 10);
            }

            g.DrawLines(new Pen(Color.Black, 2), pts);

            g.ResetTransform();
        }

        PointF[] getPts1(int mx, double a, double m, double b)
        {
            sx = size / mx;
            sy = size;

            List<PointF> pts = new List<PointF>();

            for (int x = 0; x <= mx; x++)
            {
                double y = 0;
                if (x <= a)
                {
                    y = 0;
                }
                else if (a < x && x <= m)
                {
                    y = (x - a) / (m - a);
                }
                else if (m < x && x < b)
                {
                    y = (b - x) / (b - m);
                }
                else if (b <= x)
                {
                    y = 0;
                }

                pts.Add(new PointF(x * sx, size - (float)y * sy));
            }
            return pts.ToArray();
        }
        PointF[] getPts2(int mx, double a, double b, double c, double d)
        {
            sx = size / mx;
            sy = size;

            List<PointF> pts = new List<PointF>();

            for (int x = 0; x <= mx; x++)
            {
                double y = 0;
                if (x < a || x > d)
                {
                    y = 0;
                }
                else if (a <= x && x <= b)
                {
                    y = (x - a) / (b - a);
                }
                else if (b <= x && x < c)
                {
                    y = 1;
                }
                else if (c <= x && x <= d)
                {
                    y = 1 - (x - c) / (d - c);
                }

                pts.Add(new PointF(x * sx, size - (float)y * sy));
            }
            return pts.ToArray();
        }
    }
}
