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

            List<Obj> objs = new List<Obj>();
            for (int i = 0; i < 6; i++)
            {
                string name = tb[0][i].Text;
                int price = int.Parse(tb[1][i].Text);
                int weight = int.Parse(tb[2][i].Text);
                objs.Add(new Obj(name, price, weight));
            }


            Queue<Node> queue = new Queue<Node>();
            SortedList<string, bool> book = new SortedList<string, bool>();// 檢查重複

            // 根節點從甚麼都沒有開始往下分支
            Node start = new Node(new List<Obj>());

            queue.Enqueue(start);
            book.Add(start.GetString(), true);

            int maxPrice = 0;
            Node super = null;
            while (queue.Count > 0)
            {
                Node now = queue.Dequeue();

                int nowPrice = now.GetPrice();

                // 篩選出最高價格，且不超出重量的組合
                if (nowPrice > maxPrice && now.GetWeight() <= maxWeight)
                {
                    super = now;
                    maxPrice = nowPrice;
                }

                List<Node> next = getNext(now, objs);// 取得所有可能的節點分支
                foreach (var n in next)
                {
                    string sign = n.GetString();
                    if (!book.Keys.Contains(sign))// 只要這個分支沒有重複，就加入
                    {
                        queue.Enqueue(n);
                        book.Add(sign, true);
                    }
                }
            }

            label4.Text = super.GetString();
            label5.Text = maxPrice.ToString();
        }

        List<Node> getNext(Node now, List<Obj> list)// 傳入當前節點，跟所有商品
        {
            // List的Clone方法:https://stackoverflow.com/questions/9622211/how-to-make-correct-clone-of-the-listmyobject
            List<Node> next = new List<Node>();
            foreach (var i in list)
            {
                var new1 = new List<Obj>(now.status);// 複製一份list
                if (new1.IndexOf(i) == -1)// 如果當前商品不再組合內，就加進去
                {
                    new1.Add(i);
                    next.Add(new Node(new1));// 產生新的子節點
                }
            }

            // 最後now可分支的所有節點
            return next;
        }
    }
    public class Obj
    {
        public string name;
        public int price;
        public int weight;
        public Obj(string name, int price, int weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
        }
    }
    public class Node
    {
        public List<Obj> status;// 每個節點用List儲存當前物品組合
        public Node(List<Obj> status)
        {
            this.status = status;
        }
        public string GetString()
        {
            var query = status.Select(item => item.name);

            return string.Join(",", query.ToArray());
        }
        public int GetPrice()
        {
            var query = status.Select(item => item.price);

            return query.Sum();
        }
        public int GetWeight()
        {
            var query = status.Select(item => item.weight);

            return query.Sum();
        }
    }
}

