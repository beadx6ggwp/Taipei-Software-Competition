using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_101_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] n = new double[]
            {
                0,
                double.Parse(textBox1.Text),
                double.Parse(textBox2.Text),
                double.Parse(textBox3.Text),
                double.Parse(textBox4.Text)
            };

            double p1 = n[2] + n[3] - n[1];
            double p2 = n[1] - n[2] - n[4];

            double y = (p2 * n[1] + p1 * n[1]) / p1;
            double x = y * p1 / p2;

            string result = "";

            double d = y % 1;
            if (n[1] % 1 != 0 || n[2] % 1 != 0 || n[3] % 1 != 0 || n[4] % 1 != 0 ||
                p1 % 1 != 0 || p2 % 1 != 0 || y % 1 != 0 || x % 1 != 0)
                result = "無解";
            else
                result = $"X:{x}, Y:{y}";

            label10.Text = result;
        }
    }
}
