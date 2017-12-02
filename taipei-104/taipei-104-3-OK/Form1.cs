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

namespace taipei_104_3
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
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap old = new Bitmap(pictureBox1.Image);
            Bitmap result = new Bitmap(pictureBox1.Image);

            for (int y = 0; y < old.Height; y++)
            {
                for (int x = 0; x < old.Width; x++)
                {
                    if (NeedWhite(x, y, old))
                        result.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }

            pictureBox1.Image = result;
        }

        bool NeedWhite(int x, int y, Bitmap bmp)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int tx = x + dx;
                    int ty = y + dy;
                    if (dy == 0 && dx == 0) continue;
                    if (tx < 0 || tx > bmp.Width - 1 || ty < 0 || ty > bmp.Height - 1) continue;

                    Color c = bmp.GetPixel(x, y), tc = bmp.GetPixel(tx,ty);
                    int grayNow = (int)((c.R + c.G + c.B) / 3.0);
                    int grayTemp = (int)((tc.R + tc.G + tc.B) / 3.0);

                    // 如果當前像素是黑點，且該點周圍有白點，那就將這個點設為白色
                    if (grayNow != 255 && grayTemp == 255)
                        return true;
                }
            }
            return false;
        }
    }
}
