using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_101_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(" ", "");
            double[] data = Array.ConvertAll(textBox1.Text.Split(','), double.Parse);
            Array.Sort(data);

            double Q1 = GetQ(data, 25);
            double Q2 = GetQ(data, 50);
            double Q3 = GetQ(data, 75);

            double IQR = Q3 - Q1;

            double min = data[0];
            double max = data[data.Length - 1];

            pictureBox1.Image = Boxplot(300, 300, Q1, Q2, Q3, min, max);
        }

        double GetQ(double[] arr, int p)
        {
            int len = arr.Length;
            double i = (len * p / 100.0) - 1;// 陣列從0開始

            if (i % 1 != 0)
            {
                int index = (int)i + 1;

                return arr[index];
            }
            else
            {
                int index = (int)i;
                int next = Math.Min(index + 1, len - 1);

                return (arr[index] + arr[next]) / 2.0;
            }
        }

        Bitmap Boxplot(int width, int height, double Q1, double Q2, double Q3, double min, double max)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            g.TranslateTransform(width / 2, height);

            int maxX = width, maxY = height - 20;
            int sx = 1, sy = -1;

            float minY = 20;

            float y = minY;
            float gap = (float)((maxY - minY) / (max - min));
            for (int startCount = (int)min; startCount <= max; startCount += 1)
            {
                g.FillRectangle(Brushes.Black, -maxX / 2, y * sy, 5, 1);
                if (startCount % 2 == 0)
                    g.DrawString($"{startCount}", new Font("consolas", 8), Brushes.Red, -maxX / 2 * sx + 10, y * sy - 5);
                y += gap;
            }
            g.DrawLine(Pens.Black, -maxX / 2, -maxY, -maxX / 2, -minY);

            g.FillRectangle(Brushes.Black, -5, (minY + (float)(max - min) * gap) * sy, 10, 1);
            g.FillRectangle(Brushes.Black, -5, (minY + (float)(min - min) * gap) * sy, 10, 1);

            g.DrawRectangle(Pens.Blue, -10, (minY + (float)(Q3 - min) * gap) * sy, 20, (float)((Q3 - Q1) * gap));

            g.FillRectangle(Brushes.Red, -10, (minY + (float)(Q2 - min) * gap) * sy, 20, 1);

            Pen p = new Pen(Brushes.Black, 1);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(p, 0, (minY + (float)(max - min) * gap) * sy, 0, (minY + (float)(Q3 - min) * gap) * sy);
            g.DrawLine(p, 0, (minY + (float)(Q1 - min) * gap) * sy, 0, (minY + (float)(min - min) * gap) * sy);

            return bmp;
        }

    }
}
