using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6_Csh
{
    public partial class Form1 : Form
    {
        private Point mouseDownLocation;
        private bool isDragging = false;


        private Bitmap graphBitmap;

        public Form1()
        {
            InitializeComponent();

            // generate attacks
            Adversary adv = new Adversary(100, 70, 0.5f);
            adv.generateAttacks();
            // retrieve data
            List<List<int>> data1 = adv.GetLineChart1AttackList();

            // generate LineCharts
            LineChart chart1 = new LineChart(this, pictureBox1);
            chart1.DrawChart(this, data1);
            
            LineChart chart2 = new LineChart(this, pictureBox2);
            chart2.DrawChart(this, data1);
            
            LineChart chart3 = new LineChart(this, pictureBox3);
            chart3.DrawChart(this, data1);

            int S1 = 20;
            int S2 = 60;
            int S3 = 100;

            // retrieve histogram data
            List<Dictionary<int, int>> histogramDistChart1 = adv.createCompleteHistogramData(data1, S1);
            List<Dictionary<int, int>> histogramDistChart2 = adv.createCompleteHistogramData(data1, S2);
            List<Dictionary<int, int>> histogramDistChart3 = adv.createCompleteHistogramData(data1, S3);

            // generate Histograms
            Histogram histogram1 = new Histogram(this, chart1, histogramDistChart1, S1);
            Histogram histogram2 = new Histogram(this, chart2, histogramDistChart2, S2);
            Histogram histogram3 = new Histogram(this, chart3, histogramDistChart3, S3);

        }
    }
}
