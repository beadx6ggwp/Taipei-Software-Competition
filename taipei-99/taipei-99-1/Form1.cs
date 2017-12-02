using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_99_1
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        Label[] lab;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            lab = new Label[] { label1, label2, label3, label4 };

            reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] data = Array.ConvertAll(tb, s => double.Parse(s.Text));
            foreach (var i in data)
            {
                if (i < -20 || i > 20)
                {
                    label5.Text = "超出範圍";
                    reset();
                    return;
                }
            }

            double b = (data[1] - data[5] + data[0]) / 2;
            double a = data[1] - b;
            double c = data[0] - b;
            double d = data[4] - b;

            // 我不確定無解釋怎麼樣，但從題目帶進來，看來有小數就是無解
            if (a % 1 != 0 || b % 1 != 0 || c % 1 != 0 || c % 1 != 0)
            {
                label5.Text = "有小數，無解";
                reset();
                return;
            }

            label1.Text = a.ToString();
            label2.Text = b.ToString();
            label3.Text = c.ToString();
            label4.Text = d.ToString();
        }

        void reset()
        {
            for (int i = 0; i < lab.Length; i++)
            {
                lab[i].Text = Convert.ToChar(97 + i).ToString();
            }
        }
    }
}
