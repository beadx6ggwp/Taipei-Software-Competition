using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_104_1
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        Button[] btns;
        int size = 3;
        int btnSize = 50;
        bool turn = false;// !F:A, F:B
        int turnCount = 0;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2 };

            btns = new Button[size * size];
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i] = new Button
                {
                    Name = $"btn{i}",
                    Size = new Size(btnSize, btnSize),
                    Location = new Point(i % size * btnSize, i / size * btnSize)
                };

                btns[i].Click += (s, e) =>
                {
                    // 驗證輸入
                    foreach (var t in tb)
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(t.Text, "[A-Za-z]"))
                        {
                            MessageBox.Show("請輸入A~Z or a~z");
                            return;
                        }
                    }
                    if (tb[0] == tb[1])
                    {
                        MessageBox.Show("符號不可相同");
                        return;
                    }

                    // 判斷輸贏
                    Button btn = (Button)s;
                    if (btn.Text != "")
                    {
                        MessageBox.Show("該點以下過");
                        return;
                    }

                    btn.Text = !turn ? tb[0].Text : tb[1].Text;

                    turn = !turn;
                    turnCount++;

                    string check = "";
                    for (int k = 0; k < size; k++) check += btn.Text;

                    bool iswin = false;

                    string rs = "", ls = "";// 斜線
                    for (int k = 0; k < size && !iswin; k++)
                    {
                        string rowText = "", colText = "";// 直線, 橫線
                        for (int r = 0; r < size; r++) rowText += btns[getIndex(k, r)].Text;
                        for (int c = 0; c < size; c++) colText += btns[getIndex(c, k)].Text;
                        rs += btns[getIndex(k, k)].Text;
                        ls += btns[getIndex(k, size - 1 - k)].Text;

                        if (rowText == check || colText == check || rs == check || ls == check)
                            iswin = true;
                    }

                    if (iswin)
                    {
                        MessageBox.Show($"{btn.Text} win");
                        Array.ForEach(btns, item => { if (item.Text != btn.Text) item.Enabled = false; });
                    }
                    else if (turnCount == size * size) MessageBox.Show("平局");
                };
            }
            this.Controls.AddRange(btns);
        }
        int getIndex(int x, int y)
        {
            return y * size + x;
        }
    }
}
