using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_100_2
{
    public partial class Form1 : Form
    {
        GroupBox[] gb;
        TextBox[][] tb;

        // 預設輸入
        string[] str = new string[] { "商品一", "商品二", "商品三", "商品四", "商品五", "商品六" };
        int[] pri = new int[] { 20, 100, 250, 350, 499, 555 };
        int[] wei = new int[] { 1, 2, 3, 4, 5, 6 };
        public Form1()
        {
            InitializeComponent();

            // 生成表單
            gb = new GroupBox[] { groupBox1, groupBox2, groupBox3 };
            tb = new TextBox[3][];
            for (int i = 0; i < 3; i++)
            {
                tb[i] = new TextBox[6];
                for (int j = 0; j < 6; j++)
                {
                    tb[i][j] = new TextBox()
                    {
                        Location = new Point(80 * j + 20, 30),
                        Size = new Size(60, 20)
                    };
                }
                gb[i].Controls.AddRange(tb[i]);
            }

            // 預設輸入
            for (int i = 0; i < 6; i++)
            {
                tb[0][i].Text = str[i];
                tb[1][i].Text = pri[i].ToString();
                tb[2][i].Text = wei[i].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxWeight = int.Parse(textBox1.Text);
            int nowWeight = maxWeight;

            List<Obj> objs = new List<Obj>();
            for (int i = 0; i < 6; i++)
            {
                string name = tb[0][i].Text;
                int price = int.Parse(tb[1][i].Text);
                int weight = int.Parse(tb[2][i].Text);
                objs.Add(new Obj(name, price, weight));
            }

            // 按照CP值排序，大->小
            objs.Sort((a, b) => b.cp.CompareTo(a.cp));

            // 開始挑選
            List<Obj> result = new List<Obj>();
            for (int i = 0; i < 6; i++)
            {
                if (nowWeight >= objs[i].weight)
                {
                    nowWeight -= objs[i].weight;
                    result.Add(objs[i]);
                }
            }

            label4.Text = string.Join("+", Array.ConvertAll(result.ToArray(), s => s.name));
            label5.Text = result.Sum(s => s.price).ToString();
        }
    }
    public class Obj
    {
        public string name;
        public int price;
        public int weight;
        public double cp;
        public Obj(string name, int price, int weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
            cp = (double)price / (double)weight;
        }
    }
}

