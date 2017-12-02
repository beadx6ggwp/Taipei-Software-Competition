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
namespace taipei_105_4
{
    public partial class Form1 : Form
    {
        Bitmap grayBmp, IxBmp, IyBmp, IxIxBmp, IyIyBmp, IxIyBmp;


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
            grayBmp = getGrayBmp(new Bitmap(pictureBox1.Image));
            pictureBox2.Image = grayBmp;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            IxBmp = getIxBmp(grayBmp);
            pictureBox2.Image = IxBmp;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            IyBmp = getIyBmp(grayBmp);
            pictureBox2.Image = IyBmp;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            IxIxBmp = getIxIxBmp(grayBmp);
            pictureBox2.Image = IxIxBmp;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            IyIyBmp = getIyIyBmp(grayBmp);
            pictureBox2.Image = IyIyBmp;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            IxIyBmp = getIxIyBmp(grayBmp);
            pictureBox2.Image = IxIyBmp;
        }

        Bitmap getGrayBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);

            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    Color currP = source.GetPixel(w, h);
                    int grayVal = (int)(0.2125 * currP.R + 0.7154 * currP.G + 0.0721 * currP.B);
                    resultBmp.SetPixel(w, h, Color.FromArgb(grayVal, grayVal, grayVal));
                }
            }
            return resultBmp;
        }
        Bitmap getIxBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);

            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    if (w - 1 < 0 || w + 1 > source.Width - 1 || h - 1 < 0 || h + 1 > source.Height - 1) continue;
                    double temp = (source.GetPixel(w - 1, h - 1).R +
                                 source.GetPixel(w, h - 1).R +
                                 source.GetPixel(w + 1, h - 1).R -
                                 source.GetPixel(w - 1, h + 1).R -
                                 source.GetPixel(w, h + 1).R -
                                 source.GetPixel(w + 1, h + 1).R) * 0.166666667;
                    int val = Math.Abs((int)temp);
                    resultBmp.SetPixel(w, h, Color.FromArgb(val, val, val));
                }
            }
            return resultBmp;
        }
        Bitmap getIyBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);

            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    if (w - 1 < 0 || w + 1 > source.Width - 1 || h - 1 < 0 || h + 1 > source.Height - 1) continue;
                    double temp = (source.GetPixel(w - 1, h - 1).R +
                                 source.GetPixel(w - 1, h).R +
                                 source.GetPixel(w - 1, h + 1).R -
                                 source.GetPixel(w + 1, h - 1).R -
                                 source.GetPixel(w + 1, h).R -
                                 source.GetPixel(w + 1, h + 1).R) * 0.166666667;
                    int val = Math.Abs((int)temp);
                    resultBmp.SetPixel(w, h, Color.FromArgb(val, val, val));
                }
            }
            return resultBmp;
        }

        Bitmap getIxIxBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);
            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    int temp = IxBmp.GetPixel(w, h).R;
                    int val = (int)Math.Sqrt(temp * temp);
                    resultBmp.SetPixel(w, h, Color.FromArgb(val, val, val));
                }
            }

            return resultBmp;
        }
        Bitmap getIyIyBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);
            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    int temp = IyBmp.GetPixel(w, h).R;
                    int val = (int)Math.Sqrt(temp * temp);
                    resultBmp.SetPixel(w, h, Color.FromArgb(val, val, val));
                }
            }
            return resultBmp;
        }
        Bitmap getIxIyBmp(Bitmap source)
        {
            Bitmap resultBmp = new Bitmap(source);
            for (int h = 0; h < source.Height; h++)
            {
                for (int w = 0; w < source.Width; w++)
                {
                    int val = (int)Math.Sqrt(IxBmp.GetPixel(w, h).R * IyBmp.GetPixel(w, h).R);
                    resultBmp.SetPixel(w, h, Color.FromArgb(val, val, val));
                }
            }
            return resultBmp;
        }
    }
}
