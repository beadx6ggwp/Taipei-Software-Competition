using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_103_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // in2 = xi, in1 = yi
            int[] in1 = Array.ConvertAll(textBox1.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int[] in2 = Array.ConvertAll(textBox2.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            int[,] arr = new int[in2.Length + 1, in1.Length + 1];
            for (int i = 1; i < in2.Length + 1; i++) // xi
            {
                for (int j = 1; j < in1.Length + 1; j++) // yi
                {
                    // 因為arr是從1開始，in1、in2是從0開始，所以i-1, j-1
                    if (i == 1 && j == 1)
                        arr[i, j] = Math.Abs(in2[i - 1] - in1[j - 1]);
                    if (i == 1 && j > 1)
                        arr[i, j] = Math.Abs(in2[i - 1] - in1[j - 1]) + arr[1, j - 1];
                    if (i > 1 && j == 1)
                        arr[i, j] = Math.Abs(in2[i - 1] - in1[j - 1]) + arr[i - 1, 1];
                    if (i > 1 && j > 1)
                        arr[i, j] = Math.Abs(in2[i - 1] - in1[j - 1]) + getMin3(arr[i - 1, j], arr[i - 1, j - 1], arr[i, j - 1]);
                }
            }

            // 將arr上面那條0，和左邊那條0，放置xi與yi資料
            for (int i = 1; i < in2.Length + 1; i++) arr[i, 0] = in2[i - 1];
            for (int i = 1; i < in1.Length + 1; i++) arr[0, i] = in1[i - 1];

            for (int i = 0; i < in2.Length + 1; i++)
            {
                for (int j = 0; j < in1.Length + 1; j++)
                {
                    textBox3.Text += arr[i, j] + "\t";
                }
                textBox3.Text += "\r\n";
            }
        }

        int getMin3(int n1, int n2, int n3)
        {
            return Math.Min(Math.Min(n1, n2), n3);
        }
    }
}
