using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace taipei_101_4
{
    public partial class Form1 : Form
    {
        Button[] btns;
        WebBrowser wb = new WebBrowser();
        public Form1()
        {
            InitializeComponent();
            btns = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
                button11, button12, button13, button14, button15, button16, button17 };

            foreach (var item in btns) item.Click += (s, e) => { textBox1.Text += (s as Button).Text; };

            wb.DocumentText = "<script>function calc(e){return eval(e);}</script>";// 記得要在執行前先給定內容
        }
        private void button18_Click(object sender, EventArgs e)
        {
            var obj = wb.Document.InvokeScript("calc", new object[] { textBox1.Text });
            if (obj != null)
            {
                string result = $"{textBox1.Text} = {obj}";
                string path = $@"{Directory.GetCurrentDirectory() }\b.txt";
                File.WriteAllText(path, result);
            }
            else
                MessageBox.Show("輸入錯誤");
        }
    }
}
