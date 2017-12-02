using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_99_4
{
    public partial class Form1 : Form
    {
        // 可以從這裡驗證自己的過河順序有沒有錯 https://wenku.baidu.com/view/4790c54f767f5acfa1c7cd8d.html
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int[] input = Array.ConvertAll(textBox1.Text.Split(','), int.Parse);

            Queue<Node> queue = new Queue<Node>();
            SortedList<int, bool> book = new SortedList<int, bool>();

            Node end = new Node(new int[] { 0, 0, 0, 3, 3, 1 }, null);
            int endStatus = end.Sequence();

            Node start = new Node(input, null);
            if (!start.IsCorrect())
            {
                MessageBox.Show("違反題意");
                return;
            }

            queue.Enqueue(start);
            book.Add(start.Sequence(), true);

            while (queue.Count > 0)
            {
                Node now = queue.Dequeue();

                if (now.Sequence() == endStatus)
                {
                    MessageBox.Show("成功過河");
                    Stack<Node> path = new Stack<Node>();
                    while (now.father != null)
                    {
                        path.Push(now);
                        now = now.father;
                    }

                    while (path.Count > 0)
                        listBox1.Items.Add(path.Pop().ToString());
                }


                List<Node> nextPath = GetNext(now);
                foreach (var next in nextPath)
                {
                    // 把錯誤跟重複的組合篩選掉
                    if (next == null) continue;
                    int sign = next.Sequence();
                    if (!book.Keys.Contains(sign))
                    {
                        queue.Enqueue(next);
                        book.Add(sign, true);
                    }
                }
            }
        }

        List<Node> GetNext(Node now)
        {
            List<Node> next = new List<Node>();
            // 列出所有過河可能
            if (now.BootInLeft())
            {
                next.Add(now.MoveLtoR(0, 1));
                next.Add(now.MoveLtoR(0, 2));
                next.Add(now.MoveLtoR(1, 0));
                next.Add(now.MoveLtoR(2, 0));
                next.Add(now.MoveLtoR(1, 1));
            }
            else
            {
                next.Add(now.MoveRtoL(0, 1));
                next.Add(now.MoveRtoL(0, 2));
                next.Add(now.MoveRtoL(1, 0));
                next.Add(now.MoveRtoL(2, 0));
                next.Add(now.MoveRtoL(1, 1));
            }


            return next;
        }
    }

    public class Node
    {
        public int[] status;//左岸 野人,傳教,船, 右岸 野人,傳教,船
        public Node father;
        public Node(int[] status, Node father)
        {
            this.status = status;
            this.father = father;
        }
        public Node MoveLtoR(int m, int p)
        {
            int[] arr = (int[])status.Clone();
            arr[0] -= m;
            arr[1] -= p;
            arr[2] = 0;
            arr[3] += m;
            arr[4] += p;
            arr[5] = 1;
            Node next = new Node(arr, this);
            if (next.IsCorrect())
                return next;
            else
                return null;
        }
        public Node MoveRtoL(int m, int p)
        {
            int[] arr = (int[])status.Clone();
            arr[0] += m;
            arr[1] += p;
            arr[2] = 1;
            arr[3] -= m;
            arr[4] -= p;
            arr[5] = 0;
            Node next = new Node(arr, this);
            if (next.IsCorrect())
                return next;
            else
                return null;
        }

        public bool BootInLeft() => status[2] == 1;

        public bool IsCorrect()
        {
            // 檢查組合是否符合條件
            for (int i = 0; i < status.Length; i++)
                if (status[i] < 0) return false;

            // 野人比傳教士多會發狂，但條件是至少要有一個傳教士在岸上
            if ((status[0] > status[1] && status[1] > 0) || (status[3] > status[4] && status[4] > 0))
                return false;

            return true;
        }

        public int Sequence()
        {
            // 轉序列，一樣搭配SortedList檢查重複組合
            int n = 0;
            for (int i = 0; i < status.Length; i++)
                n = n * 10 + status[i];

            return n;
        }
        public override string ToString()
        {
            string str = "";
            // 因為只有存移動後的結果，所以要過程的話要在反推一下
            if (!BootInLeft())// 船在右邊，代表這趟是左到右
            {
                // 以左岸為基準來看，因為左岸到右岸會少人，所以是father - now
                str = $"左岸->右岸,  傳教士過去{father.status[1] - status[1]},  野人過去{father.status[0] - status[0]}";
            }
            else
            {
                // 以左岸為基準來看，因為右岸回左岸會多人，所以是now - father
                str = $"右岸->左岸,  傳教士過去{status[1] - father.status[1]},  野人過去{status[0] - father.status[0]}";
            }
            return $"{str}\t左岸->野:{status[0]} 傳:{status[1]} 船:{status[2]} , 右岸->野:{status[3]} 傳:{status[4]} 船:{status[5]}";
        }
    }
}
