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

namespace 許展維_Q3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            chart1.Series.Clear();
            checkBox1.Checked = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = checkBox1.Checked;
        }
        string[] input;
        List<PointF> list = new List<PointF>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (ofd.ShowDialog() == DialogResult.OK)
                input = File.ReadAllLines(ofd.FileName);

            list.Clear();
            string[] temp = input[0].Split(' ');
            int count = int.Parse(temp[0]);
            for (int i = 1; i < count + 1; i++)
            {
                temp = input[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                list.Add(new PointF(float.Parse(temp[0]), float.Parse(temp[1])));
            }

            chart1.Series.Clear();
            chart1.Series.Add("Point");
            chart1.Series[0].ChartType = SeriesChartType.Point;
            for (int i = 0; i < list.Count; i++)
            {
                PointF p = list[i];
                chart1.Series[0].Points.AddXY(p.X, p.Y);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<PointF> result = new List<PointF>();

            float min = list.Min(s => s.X);
            float max = list.Max(s => s.X);
            for (float i = min; i <= max; i += 0.1f)
            {
                result.Add(new PointF(i, calc(i)));
            }

            // draw
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;


            chart1.Series.Add("Line");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            for (int i = 0; i < result.Count; i++)
            {
                PointF p = result[i];
                chart1.Series[0].Points.AddXY(p.X, p.Y);
            }

            chart1.Series.Add("Point");
            chart1.Series[1].ChartType = SeriesChartType.Point;
            for (int i = 0; i < list.Count; i++)
            {
                PointF p = list[i];
                chart1.Series[1].Points.AddXY(p.X, p.Y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            list.Clear();
        }

        float calc(float x)
        {
            float result = 0;

            // 按照公式輸出
            for (int i = 0; i < list.Count; i++)
            {
                PointF p = list[i];
                result += p.Y * GetL(i, x);
            }

            return result;
        }

        float GetL(int j, float x)
        {
            float up = 1;
            float down = 1;

            for (int m = 0; m < list.Count; m++)
            {
                if (j == m) continue;

                up *= (x - list[m].X);
                down *= (list[j].X - list[m].X);
            }

            return up / down;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = checkBox1.Checked;
        }
    }
}
