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
namespace taipei_105_1
{
    public partial class Form1 : Form
    {
        TextBox[] tb;
        public Form1()
        {
            InitializeComponent();
            tb = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = "";
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "txt|*.txt";
                ofd.InitialDirectory = Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    input = File.ReadAllText(ofd.FileName, Encoding.Default);
                }
            }

            string[] content = input.Split(new string[] { "\r\n", "、" }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(content);

            List<Fruit> list = new List<Fruit>();
            int lastIndex = 0;
            string temp = content[0];
            for (int i = 1; i < content.Length; i++)
            {
                if (temp != content[i])
                {
                    list.Add(new Fruit() { name = content[i - 1], count = i - lastIndex });
                    lastIndex = i;
                    temp = content[i];
                }
            }

            // https://msdn.microsoft.com/zh-tw/library/tfakywbh(v=vs.110).aspx
            // https://msdn.microsoft.com/zh-tw/library/w56d4y5z(v=vs.110).aspx
            // if(a - b > 0) 位置交換，小到大
            // if(b - a > 0) 位置交換，大到小
            // object.CompareTo(value) 相當於  return object - value > 0 ? 1 : -1;
            // 如果CompareTo結果是1，就交換
            list.Sort((a, b) => b.count.CompareTo(a.count));
            for (int i = 0; i < tb.Length; i++)
                tb[i].Text = list[i].name;
        }

        struct Fruit
        {
            public string name;
            public int count;
        }
    }
}
