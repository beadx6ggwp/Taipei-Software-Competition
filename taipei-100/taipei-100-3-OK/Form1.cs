using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_100_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double min = double.Parse(textBox1.Text);
            double max = double.Parse(textBox2.Text);

            double mu = double.Parse(textBox3.Text);
            double sigma2 = double.Parse(textBox4.Text);

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.White);

            int tx = 20, ty = -20;

            g.TranslateTransform(bmp.Width / 2 + tx, bmp.Height + ty);

            double sx = (bmp.Width - tx / 2) / (max - min), sy = -(bmp.Height - ty / 2);


            for (int x = (int)(min); x <= max; x++)
            {
                g.DrawString(x.ToString(), new Font("consolas", 8), Brushes.Black, (float)(x * sx) - 4, 10);
                g.FillRectangle(Brushes.Black, (float)(x * sx), 5, 1, 3);
            }
            g.DrawLine(Pens.Black, -bmp.Width / 2, 5, bmp.Width / 2, 5);
            for (int y = 0; y <= 10; y++)
            {
                g.DrawString((y / 10.0).ToString(), new Font("consolas", 8), Brushes.Black, -bmp.Width / 2 - 20, (float)(y * sy / 10));
            }
            g.DrawLine(Pens.Black, -bmp.Width / 2, 0, -bmp.Width / 2, -bmp.Height);

            List<PointF> pts = new List<PointF>();
            for (double x = min; x <= max; x += 0.1)
            {
                double y = Calc(x, mu, sigma2);
                pts.Add(new PointF((float)(x * sx), (float)(y * sy)));
            }

            g.DrawLines(Pens.Blue, pts.ToArray());
            pictureBox1.Image = bmp;
        }

        double Calc(double x, double mu, double sigma2)
        {
            double sigma = Math.Sqrt(sigma2);
            double PI = Math.PI;
            double left = 1.0 / (Math.Sqrt(2 * PI) * sigma);
            double right = Math.Exp(-(Math.Pow(x - mu, 2) / (2 * sigma2)));

            return left * right;
        }
    }
}
