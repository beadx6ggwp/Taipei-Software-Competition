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

namespace taipei_102_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[][] D;
        int[,] S;
        int sum = 0;
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                    textBox1.Text = File.ReadAllText(ofd.FileName, Encoding.Default);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 初始化資料
            string[] input = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            D = new int[input.Length][];
            for (int i = 0; i < D.Length; i++)
            {
                string[] arr = input[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                D[i] = Array.ConvertAll(arr, int.Parse);
            }

            S = new int[D.Length + 1, D.Length + 1]; // 注意! S從1,1開始

            // 計算、顯示總和面積表

            for (int r = 0; r < D.Length; r++)
            {
                for (int c = 0; c < D[0].Length; c++)
                {
                    S[r + 1, c + 1] = D[r][c] + S[r + 1, c] + S[r, c + 1] - S[r, c];
                    textBox2.Text += $"\t{S[r + 1, c + 1]}";
                }
                textBox2.Text += "\r\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int xL = int.Parse(textBox3.Text); int yT = int.Parse(textBox4.Text);
            int xR = int.Parse(textBox5.Text); int yB = int.Parse(textBox6.Text);

            sum = S[yB, xR] - S[yT - 1, xR] - S[yB, xL - 1] + S[yT - 1, xL - 1];
            count = (xR - xL) * (yB + 1 - yT + 1);
            textBox7.Text = $"{sum}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox8.Text = $"{sum / (float)count}";
        }
    }
}
