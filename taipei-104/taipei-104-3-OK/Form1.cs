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
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        Color checkColor = Color.White;// 侵蝕:白, 膨脹:黑
        private void button2_Click(object sender, EventArgs e)
        {
            // 選擇模式
            if ((sender as Button) == button2)
                checkColor = Color.White;
            else
                checkColor = Color.Black;

            Bitmap old = new Bitmap(pictureBox1.Image);
            Bitmap result = new Bitmap(pictureBox1.Image);

            for (int y = 0; y < old.Height; y++)
            {
                for (int x = 0; x < old.Width; x++)
                {
                    if (Check(x, y, old))
                        result.SetPixel(x, y, checkColor);// 255侵蝕<=>0膨脹
                }
            }
            pictureBox1.Image = result;
        }

        bool Check(int x, int y, Bitmap bmp)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int tx = x + dx;
                    int ty = y + dy;
                    if (dy == 0 && dx == 0) continue;
                    if (tx < 0 || tx > bmp.Width - 1 || ty < 0 || ty > bmp.Height - 1) continue;

                    Color c = bmp.GetPixel(x, y), tc = bmp.GetPixel(tx, ty);
                    int grayNow = (int)((c.R + c.G + c.B) / 3.0);// 當前像素顏色
                    int grayTemp = (int)((tc.R + tc.G + tc.B) / 3.0);// 周圍像素顏色
                    int checkValue = (int)((checkColor.R + checkColor.G + checkColor.B) / 3.0);// 比對的顏色

                    // 以侵蝕為例，如果當前像素是黑點，且該點周圍有白點，那就將這個點設為白色
                    // 以膨脹為例，如果當前像素是白點，且該點周圍有黑點，那就將這個點設為黑色
                    if (grayNow != checkValue && grayTemp == checkValue)// 255侵蝕<=>0膨脹
                        return true;
                }
            }
            return false;
        }
    }
}
