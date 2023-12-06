using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6_Csh
{
    internal class Histogram
    {

        protected Form form;
        protected LineChart lineChart;

        protected List<Dictionary<int, int>> data = new List<Dictionary<int, int>>();
        protected Dictionary<int, float> dataf = new Dictionary<int, float>();
        protected int k = 10; // numero intervalli per le P = 10*k , k = {2,..,10}


        public Histogram(Form form, LineChart lineChart, List<Dictionary<int, int>> data, int S)
        {

            this.form = form;
            this.lineChart = lineChart;

            this.data = data;

            DrawVerticalHistogram();
            lineChart.pictureBox.Invalidate();
        }

        public bool DrawVerticalHistogram()
        {
            Bitmap bitmap = lineChart.graphBitmap;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int lineChartPadding = lineChart.getPADDING();
                int width = bitmap.Width - 2 * lineChartPadding;
                int height = bitmap.Height - 2 * lineChartPadding;

                float yMin = lineChart.yMin;
                float yMax = 0;
                float xMin = lineChart.xMin;
                float xMax = lineChart.xMax;

                int[] dist = computeDist(data);

                if (k > yMax && k < yMin) return false;

                Pen pen = new Pen(Color.Violet, 2);

                // Disegna l'asse Y verticale
                g.DrawLine(pen, lineChartPadding + width, lineChartPadding + height, lineChartPadding + width, lineChartPadding);


                int numIntervallo = 0;
                // i è decide da dove partono i rettangoli
                for (float i = yMin + (yMax - yMin) / k; i < yMax; i += (yMax - yMin) / k)
                {
                    // NUMERI
                    float y = lineChartPadding + height - (i - yMin) * (height/2) / (yMax - yMin);
                    g.DrawLine(pen, lineChartPadding + width - 5, y, lineChartPadding + width + 5, y);
                    g.DrawString(i.ToString(), form.Font, Brushes.Black, lineChartPadding + width + 5, y - 5);

                    // DRAW OF K RECTANGLES

                    //float unit = height / (xMax - width / 2);
                    float rect_len = (float)dist[dist.Length - numIntervallo - 1] / (float)dist.Max() * height;
                    Debug.WriteLine("dist : " + dist[dist.Length - numIntervallo - 1]);
                    float interval_len = height/2 / k;

                    float x1 = lineChartPadding + width - rect_len;
                    float y1 = lineChartPadding + height - (i - yMin) * height/2 / (yMax - yMin);

                    numIntervallo++;

                    SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 255));

                    g.FillRectangle(brush, x1, y1, rect_len, interval_len);

                }


            }

            return true;
        }

        public int[] computeDist(List<Dictionary<int, int>> systemsFinalValues)
        {

            int[] dist = new int[9];
            int[] dist_rev = new int[9];

            int p_elem = 0;
            foreach (Dictionary<int, int> dict in systemsFinalValues)
            {
                foreach (int key_ in dict.Keys)
                {
                    if (dict[key_] == 0)
                    {
                        dist[p_elem]++;
                    }
                }
                p_elem++;
            }

            return dist;
        }

    }
}
