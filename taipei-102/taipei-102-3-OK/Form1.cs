using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_102_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            int n = int.Parse(textBox1.Text);
            int num = n * n;

            if (n % 2 != 1) { MessageBox.Show("請輸入正確數字"); return; }

            int[,] map = new int[n, n];

            int r = 0, c = n / 2;
            for (int i = 1; i <= num; i++)
            {
                map[r, c] = i;
                int tc = c - 1, tr = r - 1;

                if (tc < 0) tc = n - 1;
                if (tr < 0) tr = n - 1;

                if (map[tr, tc] != 0)
                {
                    tc = c;
                    tr = r + 1;
                }

                c = tc;
                r = tr;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    textBox2.Text += map[i, j] + "\t";
                }
                textBox2.Text += "\r\n\r\n";
            }
        }
    }
}
