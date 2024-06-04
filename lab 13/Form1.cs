using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab_13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double u = 0;
        const double sd = 0.015;
        const double k = u - 0.5 * sd * sd;

        Random rnd = new Random();

        double VEuro, VDollar, NumDay, BoxG;

        private void btStart_Click(object sender, EventArgs e)
        {
            VEuro = (double)edEuro.Value;
            VDollar = (double)edDollar.Value;
            NumDay = 1;

            //Начало в точках (0, VDollar) и (0, VEuro)
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, VDollar);
            chart1.Series[1].Points.Clear();
            chart1.Series[1].Points.AddXY(0, VEuro);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Maximum = Math.Ceiling(NumDay);

            BoxG = BoxGenerator();

            VEuro = VEuro * Math.Exp(k + sd * BoxG);
            VDollar = VDollar * Math.Exp(k + sd * BoxG);

            chart1.Series[0].Points.AddXY(NumDay, VDollar);
            chart1.Series[1].Points.AddXY(NumDay, VEuro);

            NumDay++;
        }

        private double BoxGenerator()
        {
            Random rand = new Random();
            var a1 = rand.NextDouble();
            var a2 = rand.NextDouble();

            return Math.Sqrt(-2.0 * Math.Log(a1)) * Math.Cos(2.0 * Math.PI * a2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}