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
            int ab = int.Parse(textBox1.Text);
            int ac = int.Parse(textBox2.Text);
            int max = int.Parse(textBox3.Text);

            Queue<Card> queue = new Queue<Card>();
            SortedList<int, bool> book = new SortedList<int, bool>();

            Card start = new Card(1, 1, 1);
            queue.Enqueue(start);
            book.Add(start.toInt(), true);

            while (queue.Count > 0)
            {
                Card now = queue.Dequeue();
                if (!now.isSame() && now.a + now.b == ab && now.a + now.c == ac)
                {
                    listBox1.Items.Add($"A={now.a}, B={now.b}, C={now.c}");
                }

                List<Card> next = getNextCard(now, max);
                foreach (var i in next)
                {
                    int sign = i.toInt();
                    // 檢查這個組合是否重複了
                    if (!book.Keys.Contains(sign))
                    {
                        queue.Enqueue(i);
                        book.Add(sign, true);
                    }
                }
            }
            if (listBox1.Items.Count == 0)
                listBox1.Items.Add("無解");
        }

        List<Card> getNextCard(Card now, int max)// 因為不能有一張牌超過max
        {
            List<Card> next = new List<Card>();
            max--;// 題目的要求是小於，像是輸入10，那條件要列if(val < 9) val+1，這樣最多只會到9

            // 反正就根據條件，暴力列出所有接下來的可能狀態就夠了，題目也不會給太靠杯的範圍
            if (now.a < max) next.Add(new Card(now.a + 1, now.b, now.c));
            if (now.b < max) next.Add(new Card(now.a, now.b + 1, now.c));
            if (now.c < max) next.Add(new Card(now.a, now.b, now.c + 1));
            if (now.a < max && now.b < max) next.Add(new Card(now.a + 1, now.b + 1, now.c));
            if (now.a < max && now.c < max) next.Add(new Card(now.a + 1, now.b, now.c + 1));
            if (now.b < max && now.c < max) next.Add(new Card(now.a, now.b + 1, now.c + 1));

            return next;
        }
    }

    public class Card
    {
        public int a, b, c;
        public Card(int a, int b, int c) { this.a = a; this.b = b; this.c = c; }
        public int toInt() { return a * 100 * 100 + b * 100 + c; }
        public bool isSame() { return a == b || a == c || b == c; }
    }
}
