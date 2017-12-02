using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_103_2
{
    public partial class Form1 : Form
    {
        Button[] btns = new Button[25];
        Point start = new Point(50, 50);
        int size = 50;
        int N = 5;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < btns.Length; i++)
            {
                Point pos = getPos(i);
                btns[i] = new Button()
                {
                    Name = $"btn{i}",
                    Location = new Point(size * pos.X, size * pos.Y),
                    Size = new Size(size, size),
                    BackColor = Color.White
                };
                btns[i].Click += new EventHandler(btn_Click);
            }
            this.Controls.AddRange(btns);
        }

        int getIndex(int x, int y)
        {
            return y * N + x;
        }
        Point getPos(int index)
        {
            return new Point(index % N, index / N);
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            int index = Array.IndexOf(btns, btn);
            Point pos = getPos(index);

            btn.BackColor = btn.BackColor == Color.White ? Color.Black : Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 為了不覆蓋原資料，先Copy一份
            int[] book = new int[25];
            for (int i = 0; i < btns.Length; i++)
                book[i] = btns[i].BackColor == Color.White ? 0 : 1;

            for (int i = 0; i < btns.Length; i++)
            {
                Point oldPos = getPos(i);
                Point newPos = new Point((oldPos.X + oldPos.Y) % N, (oldPos.X + 2 * oldPos.Y) % N);
                int newIndex = getIndex(newPos.X, newPos.Y);

                // 新位置的顏色=舊位置的顏色
                btns[newIndex].BackColor = book[i] == 0 ? Color.White : Color.Black;
            }
        }
    }
}
