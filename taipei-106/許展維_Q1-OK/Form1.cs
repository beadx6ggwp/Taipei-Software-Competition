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

namespace 許展維_Q1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string input;
        string[] data;
        List<Obj> objs = new List<Obj>();
        string filename = "";

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.SafeFileName;
                input = File.ReadAllText(ofd.FileName, Encoding.Default);
            }

            data = input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            objs.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                string[] temp = data[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                objs.Add(new Obj(temp[0], temp[1]));
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int border = int.Parse(textBox1.Text);
            int cellspacing = int.Parse(textBox2.Text);
            int colMax = int.Parse(textBox3.Text);



            List<string> html = new List<string>();
            html.Add(@"<!DOCTYPE html>");
            html.Add(@"<html>");
            html.Add(@"<head><title>顯示各類食材熱量</title>");
            html.Add(@"<style>img {width:100%;height:auto;}</style>");
            html.Add(@"</head>");
            html.Add(@"<body>");

            //
            html.Add($@"<table cellspacing = {cellspacing} border = {border} >");

            int row = (int)Math.Ceiling(objs.Count / (double)colMax);
            int dataCount = 0;

            bool flag = false;
            for (int i = 0; i < row; i++)
            {
                html.Add(@"<tr>");
                int start = dataCount;
                for (int j = 0; j < colMax; j++)
                {
                    string str1 = objs[dataCount].title;
                    string str2 = objs[dataCount].after;
                    html.Add($@"<td style='background - color:lightgray'>{str1}<br>{str2}</td>");
                    dataCount++;
                    if (dataCount > objs.Count - 1)
                    {
                        flag = true;
                        break;
                    }
                }
                html.Add(@"</tr>");

                html.Add(@"<tr>");
                dataCount = start;
                for (int j = 0; j < colMax; j++)
                {
                    string str1 = objs[dataCount].title;
                    html.Add($@"<td><img src='{str1}.jpg'</td>");
                    dataCount++;
                    if (dataCount > objs.Count - 1)
                    {
                        flag = true;
                        break;
                    }
                }
                html.Add(@"</tr>");

                if (flag) break;
            }

            //
            html.Add(@"</body></html>");


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Filter = "html|*.html";
            sfd.FileName = $"{filename.Split('.')[0]}.html";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, string.Join("\r\n", html.ToArray()), Encoding.Unicode);
            }
        }
    }
    public class Obj
    {
        public string title, after;
        public Obj(string t, string a) { title = t; after = a; }
    }
}
