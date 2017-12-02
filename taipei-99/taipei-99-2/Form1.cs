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
using System.Windows.Forms.DataVisualization.Charting;

namespace taipei_99_2
{
    public partial class Form1 : Form
    {
        // 這題就慢慢畫吧，沒什麼技巧
        // 只是要記得每次輸入都要留著
        public Form1()
        {
            InitializeComponent();

            chart1.Series.Clear();
            chart1.Series.Add("type0");
            chart1.Series.Add("type1");
        }
        List<P> list = new List<P>();

        private void button1_Click(object sender, EventArgs e)
        {
            string input = readFile();
            int[] inarr = Array.ConvertAll(input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);


            int maxX = 0, maxY = 0;
            for (int i = 2; i < inarr.Length; i += 3)
            {
                list.Add(new P(inarr[i], inarr[i + 1], inarr[i + 2]));
                if (inarr[i] > maxX) maxX = inarr[i];
                if (inarr[i + 1] > maxY) maxY = inarr[i + 1];
            }

            chart1.Series[0].ChartType = SeriesChartType.Point;
            chart1.Series[0].MarkerStyle = MarkerStyle.Cross;
            chart1.Series[0].MarkerSize = 10;
            chart1.Series[0].MarkerColor = Color.FromArgb(200, 0, 0, 255);

            chart1.Series[1].ChartType = SeriesChartType.Point;
            chart1.Series[1].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[1].MarkerSize = 10;
            chart1.Series[1].MarkerColor = Color.FromArgb(200, 255, 0, 0);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = maxX;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = maxY;

            chart1.ChartAreas[0].AxisX.Interval = 50;
            chart1.ChartAreas[0].AxisY.Interval = 20;

            foreach (var p in list)
            {
                if (p.color == 0)
                    chart1.Series[0].Points.AddXY(p.x, p.y);
                else
                    chart1.Series[1].Points.AddXY(p.x, p.y);
            }
        }

        string readFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                return File.ReadAllText(ofd.FileName);

            return "";
        }
    }
    class P
    {
        public int x, y, color;
        public P(int x, int y, int color) { this.x = x; this.y = y; this.color = color; }
    }
}
