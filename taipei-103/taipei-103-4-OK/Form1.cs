using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace taipei_103_4
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4 };

        }
        Regex regex = new Regex("[0-9]");

        private void button1_Click(object sender, EventArgs e)
        {
            double[] arr = new double[4];
            for (int i = 0; i < tb.Length; i++)
            {
                if (!double.TryParse(tb[i].Text, out arr[i]) || (arr[i] < 0 || arr[i] > 1))
                {
                    textBox5.Text = "無解";
                    return;
                }
            }

            if (sender == button1)
            {
                textBox5.Text = $"通道輸出1的機率為:{arr[1] * (1 - arr[3]) + arr[0] * arr[2]}";
            }

            if (sender == button2)
            {
                double ans1 = arr[1] * (1 - arr[3]) + arr[0] * arr[2];
                textBox5.Text = $"假設我們已經觀察到通道輸出為1，這時通道的輸入為1的機率為何:{arr[1] * (1 - arr[3]) / ans1}";
            }
        }

    }
}
