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

namespace taipei_102_4
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
            Bitmap source = new Bitmap(pictureBox1.Image);
            Bitmap result = new Bitmap(pictureBox1.Image);

            for (int r = 0; r < source.Height; r++)
            {
                for (int c = 0; c < source.Width; c++)
                {
                    if (c - 1 < 0 || c + 1 > source.Width - 1 || r - 1 < 0 || r + 1 > source.Height - 1) continue;

                    int p = result.GetPixel(c, r).R;
                    int err = 0;
                    if (p > 128)
                    {
                        err = p - 255;
                        result.SetPixel(c, r, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        err = p;
                        result.SetPixel(c, r, Color.FromArgb(0, 0, 0));
                    }

                    int R = getRange((err * 7) / 16 + result.GetPixel(c + 1, r).R);
                    int RB = getRange((err * 1) / 16 + result.GetPixel(c + 1, r + 1).R);
                    int B = getRange((err * 5) / 16 + result.GetPixel(c, r + 1).R);
                    int LB = getRange((err * 3) / 16 + result.GetPixel(c - 1, r + 1).R);

                    result.SetPixel(c + 1, r, Color.FromArgb(R, R, R));
                    result.SetPixel(c + 1, r + 1, Color.FromArgb(RB, RB, RB));
                    result.SetPixel(c, r + 1, Color.FromArgb(B, B, B));
                    result.SetPixel(c - 1, r + 1, Color.FromArgb(LB, LB, LB));
                }
            }

            pictureBox2.Image = result;
        }

        int getRange(int val)
        {
            if (val > 255) return 255;
            if (val < 0) return 0;
            return val;
        }
    }
}
