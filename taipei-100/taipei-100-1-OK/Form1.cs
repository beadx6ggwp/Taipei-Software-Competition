using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_100_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int ab = int.Parse(textBox1.Text);
            int ac = int.Parse(textBox2.Text);
            int max = int.Parse(textBox3.Text);

            Stack<Card> result = new Stack<Card>();
            for (int a = 1; a < max; a++)
            {
                int b = ab - a, c = ac - a;
                Card card = new Card(a, b, c);
                if (!card.isSame() && b < max && c < max) result.Push(card);
            }

            while (result.Count > 0)
                listBox1.Items.Add(result.Pop().toStr());

            if (listBox1.Items.Count == 0)
                listBox1.Items.Add("無解");
        }
    }

    public class Card
    {
        public int a, b, c;
        public Card(int a, int b, int c) { this.a = a; this.b = b; this.c = c; }
        public string toStr() { return $"A={a}, B={b}, C={c}"; }
        public bool isSame() { return a == b || a == c || b == c; }
    }
}
