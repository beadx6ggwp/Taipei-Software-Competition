using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_102_2
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        double[] len = new double[3];
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vec p1 = new Vec(tb[0].Text, tb[1].Text);
            Vec p2 = new Vec(tb[2].Text, tb[3].Text);
            Vec p3 = new Vec(tb[4].Text, tb[5].Text);

            label8.Text = getType(p1, p2, p3);

            label7.Text = $"座標1~座標2 邊長= {len[0]}\n" +
                $"座標2~座標3 邊長= {len[1]}\n" +
                $"座標3~座標1 邊長= {len[2]}\n";
        }
        string getType(Vec p1, Vec p2, Vec p3)
        {
            Vec[] v = new Vec[] { p2 - p1, p3 - p2, p1 - p3 }; // 取三邊
            for (int i = 0; i < 3; i++) len[i] = Math.Round(v[i].getLength(), 3);

            Array.Sort(v, (a, b) => b.getLength().CompareTo(a.getLength()));// 大->小
            double l = Math.Round(v[0].getLength(), 3);// 最大邊
            double s1 = Math.Round(v[1].getLength(), 3);
            double s2 = Math.Round(v[2].getLength(), 3);// 兩小邊

            if (p1.ToString() == p2.ToString() || p2.ToString() == p3.ToString())
                return "有相同座標";

            if (v[0].getM() == v[1].getM() && v[1].getM() == v[2].getM())
                return "三點共線";

            double check = Math.Round(s1 * s1 + s2 * s2 - l * l);

            if (check > 0)
            {
                // 銳角
                if (s1 == s2 && s2 == l)
                    return "正三角形";
                if (s1 == s2 || s2 == l)
                    return "銳角等腰三角形";

                return "銳角三角形";

            }
            if (check < 0)
            {
                //鈍角
                if (s1 == s2)
                    return "鈍角等腰三角形";

                return "鈍角三角形";
            }

            // 直角
            if (s1 == s2)
                return "等腰直角三角形";

            return "直角三角形";
        }
    }
    public class Vec
    {
        public double x, y;
        public Vec(double x, double y)
        {
            this.x = x; this.y = y;
        }
        public Vec(string x, string y)
        {
            this.x = double.Parse(x); this.y = double.Parse(y);
        }
        public static Vec operator -(Vec v1, Vec v2)
        {
            return new Vec(v1.x - v2.x, v1.y - v2.y);
        }
        public double getLength()
        {
            return Math.Sqrt(x * x + y * y);
        }
        public double getM()
        {
            return y / x;
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
