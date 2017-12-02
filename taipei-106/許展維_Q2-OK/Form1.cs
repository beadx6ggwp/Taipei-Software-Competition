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

namespace 許展維_Q2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var c = getRandomCard();

            imgA = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };
            imgB = new PictureBox[] { pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
        }
        Random ran = new Random();
        string[] CARD = new string[] { "C", "D", "H", "S" };
        string[] input1;

        PictureBox[] imgA, imgB;

        BigCard A, B;

        private void button2_Click(object sender, EventArgs e)
        {
            B = new BigCard(getRandomCard());
            var img2 = B.getCardImage();
            for (int i = 0; i < 5; i++)
            {
                imgB[i].Image = img2[i];
            }

            this.Text = $"{A.point},{B.point}";

            textBox1.Clear();
            textBox1.Text += "甲方:" + A.getPointType() + "\r\n";
            textBox1.Text += "乙方:" + B.getPointType() + "\r\n";
            if (A.point > B.point)
                textBox1.Text += "A贏了" + "\r\n";
            else if (B.point > A.point)
                textBox1.Text += "B贏了" + "\r\n";
            else
                textBox1.Text += "平手" + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                input1 = File.ReadAllText(ofd.FileName).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            // card
            A = new BigCard(input1);
            var img1 = A.getCardImage();
            for (int i = 0; i < 5; i++)
            {
                imgA[i].Image = img1[i];
            }
        }

        string[] getRandomCard()
        {
            string[] card = new string[5];
            string s = "";

            for (int i = 0; i < 5; i++)
            {
                var c = new Card(CARD[ran.Next(0, 4)], ran.Next(1, 11));
                if (s.Contains(c.ToString()))
                {
                    i--;
                }
                else
                {
                    card[i] = c.ToString();
                    s += c.ToString();
                }
            }


            return card;
        }
    }

    public class BigCard
    {
        public Card[] arr;
        public int point;
        public BigCard(string[] arr)
        {
            this.arr = new Card[5];
            for (int i = 0; i < 5; i++)
            {
                this.arr[i] = new Card(arr[i]);
            }

            int[] c = getSameCount();
            int[] ct = getSameCard();

            if (FindCount(4))
            {
                point = 10;
                return;
            }

            if (FindCount(3))
            {
                for (int i = 1; i < c.Length; i++)
                {
                    if (c[i] == 2)
                    {
                        point = 7;
                        return;
                    }
                }
                point = 5;
                return;
            }

            int r2 = 0;
            for (int i = 1; i < c.Length; i++)
            {
                if (c[i] == 2)
                    r2++;
            }
            if (r2 == 2)//兩組一樣
            {
                point = 4;
                return;
            }
            if (r2 == 1)
            {
                point = 2;
                return;
            }

            int cou = 0;
            for (int i = 0; i < ct.Length; i++)
            {
                if (ct[i] == 1) cou++;
            }
            if (cou == 5)//花色同
            {
                point = 1;
                return;
            }

            point = 0;
        }

        public Image[] getCardImage()
        {
            Image[] imgs = new Bitmap[5];
            for (int i = 0; i < 5; i++)
            {
                imgs[i] = Image.FromFile(@"cards\" + arr[i].ToString() + ".bmp");
            }
            return imgs;
        }

        public int[] getSameCount()
        {
            int[] c = new int[14];
            for (int i = 0; i < 5; i++)
            {
                c[arr[i].n] += 1;
            }
            return c;
        }
        public int[] getSameCard()
        {
            int[] c = new int[4];
            for (int i = 0; i < 5; i++)
            {
                switch (arr[i].t)
                {
                    case "C":
                        c[0]++;
                        break;
                    case "D":
                        c[1]++;
                        break;
                    case "H":
                        c[2]++;
                        break;
                    case "S":
                        c[3]++;
                        break;
                }
            }
            return c;
        }
        public bool FindCount(int num)
        {
            int[] c = getSameCount();
            for (int i = 1; i < c.Length; i++)
            {
                if (c[i] == num) return true;
            }
            return false;
        }

        public string getPointType()
        {
            string str = "";
            switch (point)
            {
                case 10:
                    str = "四張相同";
                    break;
                case 7:
                    str = "3+2";
                    break;
                case 5:
                    str = "3張相同";
                    break;
                case 4:
                    str = "2張數字相同+2張數字相同";
                    break;
                case 2:
                    str = "2張數字相同";
                    break;
                case 1:
                    str = "花色相同";
                    break;
                case 0:
                    str = "其他";
                    break;
            }
            return str;
        }
    }
    public class Card
    {
        public string t;
        public int n;
        public Card(string t, int n)
        {
            this.t = t;
            this.n = n;
        }
        public Card(string t)
        {
            this.t = t.Substring(0, 1);
            this.n = int.Parse(t.Substring(1, t.Length - 1));
        }
        public override string ToString()
        {
            return $"{t}{n}";
        }
    }
}
