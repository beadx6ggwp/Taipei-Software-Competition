using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taipei_104_2
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        Button[] btn;
        public Form1()
        {
            InitializeComponent();

            tb = new TextBox[] { textBox1, textBox2, textBox3 };
            btn = new Button[] { button1, button2, button3 };

            button4.Click += (s, e) => Array.ForEach(tb, item => item.Clear());
            Array.ForEach(btn, item => item.Click += new EventHandler(btn_click));
        }

        public delegate int[] Calc(int[] input1, int[] input2);
        const int MAX = 40;
        const int SIZE = 10;
        bool sign = false;
        void btn_click(object sender, EventArgs e)
        {
            Calc c = (a, b) => new int[0];

            int[] in1 = getIntArr(tb[0].Text);
            int[] in2 = getIntArr(tb[1].Text);

            switch ((sender as Button).Text)
            {
                case "+":
                    c = Add;
                    break;
                case "-":
                    c = Sub;
                    break;
                case "*":
                    c = Mul;
                    break;
            }
            int[] result = c(in1, in2);

            tb[2].Text = getString(result);
        }
        int[] getIntArr(string input)
        {
            // ASCII "0" = 48
            // 要反過來存，這樣才能正確對齊
            // 123456 => 654321
            // 1234   => 4321
            int[] result = new int[MAX];

            int[] temp = Array.ConvertAll(input.ToCharArray(), s => Convert.ToInt32(s) - 48);
            Array.Reverse(temp);

            for (int i = 0; i < temp.Length; i++) result[i] = temp[i];

            return result;
        }
        string getString(int[] input)
        {
            StringBuilder sb = new StringBuilder();
            int i = MAX - 1;

            if (sign) sb.Append("-");

            while (input[i] == 0 && i > 0) i--;
            for (; i >= 0; i--)
                sb.Append(input[i].ToString());

            return sb.ToString();
        }

        int[] Add(int[] a, int[] b)
        {
            int[] result = new int[MAX];

            int carry = 0, sum = 0;
            for (int i = 0; i < MAX; i++)
            {
                sum = a[i] + b[i];
                carry = sum / 10;
                result[i] = sum % 10;
            }


            return result;
        }
        int[] Sub(int[] a, int[] b)
        {
            int[] result = new int[MAX];

            sign = false;
            // 如果b > a 就交換
            if (Compare(a, b) < 0)
            {
                int[] temp = a;
                a = b;
                b = temp;
                sign = true;
            }

            int borrow = 0, diff = 0;
            for (int i = 0; i < MAX; i++)
            {
                diff = a[i] - b[i] - borrow;
                result[i] = diff;

                borrow = diff < 0 ? 1 : 0;

                result[i] += borrow == 1 ? 10 : 0;
            }

            return result;
        }
        int[] Mul(int[] a, int[] b)
        {
            int[] result = new int[MAX];

            int carry = 0;
            for (int i = 0; i < MAX; i++)
            {
                for (int j = 0; i + j < MAX; j++)
                {
                    result[i + j] += a[i] * b[j];
                }
            }
            // 統一處理進位
            for (int i = 0; i < MAX; i++)
            {
                result[i] += carry;
                carry = result[i] / 10;
                result[i] %= 10;
            }

            return result;
        }

        int Compare(int[] a, int[] b)
        {
            int i = MAX - 1;
            while (i > 0 && a[i] == b[i]) i--;

            return a[i] - b[i];
        }
    }
}
