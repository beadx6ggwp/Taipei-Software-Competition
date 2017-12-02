using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace taipei_100_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double min = double.Parse(textBox1.Text);
            double max = double.Parse(textBox2.Text);

            double mu = double.Parse(textBox3.Text);
            double sigma2 = double.Parse(textBox4.Text);

            chart1.Series.Add("Data");
            chart1.Series[0].ChartType = SeriesChartType.Line;

            // 設定X軸最大最小
            chart1.ChartAreas[0].AxisX.Minimum = min;
            chart1.ChartAreas[0].AxisX.Maximum = max;
            // 不設定，讓Y軸為自動調整
            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
            // 設定X、Y軸的間距
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY.Interval = 0.1;
            // 不顯示軸線條
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            for (double x = min; x < max; x += 0.1)
            {
                chart1.Series[0].Points.AddXY(x, Calc(x, mu, sigma2));
            }
        }


        double Calc(double x, double mu, double sigma2)
        {
            double sigma = Math.Sqrt(sigma2);
            double PI = Math.PI;
            double left = 1.0 / (Math.Sqrt(2 * PI) * sigma);
            double right = Math.Exp(-(Math.Pow(x - mu, 2) / (2 * sigma2)));

            return left * right;
        }
    }
}
