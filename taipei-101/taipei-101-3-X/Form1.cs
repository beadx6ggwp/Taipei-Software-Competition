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
            double[] data = Array.ConvertAll(textBox1.Text.Split(','), double.Parse);
            Array.Sort(data);

            double min = data[0];
            double max = data[data.Length - 1];
            double Q1 = getP_Index(data, 25);
            double Q2 = getP_Index(data, 50);
            double Q3 = getP_Index(data, 75);

            int size = 200;
            int sx = 2, sy = 300 / (int)(max);
            int width = 20;
            Graphics g = panel1.CreateGraphics();
            g.TranslateTransform(150, 300);


            g.DrawLine(Pens.Black, -30, 0, -30, -300);
            for (int i = (int)min; i <= max; i += 2)
            {
                g.DrawString($"{i}", new Font("consolas", 8), Brushes.Black, -50, i * -sy - 8);
                g.FillRectangle(Brushes.Black, -30, i * -sy, 3, 3);
            }

            Pen p = new Pen(Brushes.Black, 2);
            g.DrawLine(p, -width / 2, (float)max * -sy, width / 2, (float)max * -sy);// max
            g.DrawLine(p, -width / 2, (float)min * -sy, width / 2, (float)min * -sy);// min

            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(p, 0, (float)max * -sy, 0, (float)Q3 * -sy);
            g.DrawLine(p, 0, (float)Q1 * -sy, 0, (float)min * -sy);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            p.Color = Color.Blue;
            g.DrawRectangle(p, -width / 2, (float)Q3 * -sy, width, (float)(Q3 - Q1) * sy);
            p.Color = Color.Red;
            g.DrawLine(p, -width / 2, (float)Q2 * -sy, width / 2, (float)Q2 * -sy);// min
        }

        double getP_Index(double[] arr, double p)
        {
            double i = arr.Length * p / 100.0;

            if (i % 1 != 0)
            {
                int index = (int)i + 1;

                return arr[index - 1];// 因為陣列從0開始
            }
            else
            {
                int index = (int)i + 1;
                int next = Math.Min(index + 1, arr.Length - 1);

                return (arr[(int)i] + arr[(int)i + 1]) / 2.0;
            }
        }
    }
}
