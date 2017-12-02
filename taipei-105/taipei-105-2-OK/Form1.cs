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

namespace taipei_105_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[][] map;
        private void button1_Click(object sender, EventArgs e)
        {
            string input = "";
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    input = File.ReadAllText(ofd.FileName);
                }
            }

            textBox1.Text = input;

            string[] temp = input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            map = new string[temp.Length][];
            for (int i = 0; i < temp.Length; i++)
                map[i] = temp[i].Split('\t');


            int count = 0;
            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[0].Length; c++)
                {
                    if (map[r][c] == "255")
                    {
                        count++;
                        dfs(c, r, count.ToString());
                    }
                }
            }


            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[0].Length; c++)
                {
                    textBox2.Text += map[r][c] + "\t";
                }
                textBox2.Text += "\r\n";
            }

            label1.Text = $"連通數:{count}";
        }

        void dfs(int x, int y, string text)
        {
            int tx, ty;
            map[y][x] = text;
            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    tx = x + c;
                    ty = y + r;
                    if (r == 0 && c == 0) continue;
                    if (tx < 0 || tx > map[0].Length - 1 ||
                        ty < 0 || ty > map.Length - 1) continue;

                    if (map[ty][tx] == "255")
                        dfs(tx, ty, text);
                }
            }


            return;
        }
    }
}
