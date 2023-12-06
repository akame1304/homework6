using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework6_Csh
{
    internal class LineChart
    {

        // mouse
        Point mouseDownLocation;
        bool isDragging = false;

        public PictureBox pictureBox { get; set; }
        public Bitmap graphBitmap { get; set; }

        // IMAGE SETTINGS
        int PADDING = 50;
        // cartesian values
        public float xMin = 0;
        public float xMax = 0;
        public float yMin = 0;
        public float yMax = 0;

        public LineChart(Form form, PictureBox pictureBox)
        {

            this.pictureBox = pictureBox;

            mouseSettings();

            graphBitmap = new Bitmap(this.pictureBox.Width, this.pictureBox.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            this.pictureBox.Image = graphBitmap;

        }


        public void DrawChart(Form form, List<List<int>> data)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {

                int width = graphBitmap.Width - 2 * PADDING;
                int height = graphBitmap.Height - 2 * PADDING;
                this.xMin = 0;  // minx fixed to 0
                this.xMax = data.Max(list => list.Count);

                // DIMESIONAMENTO YMAX E MIN RISPETTO AI VALORI
                this.yMax = data.Max(list => list.Count);
                //this.yMax = data.Max(list => list.Count(x => x == 1));
                this.yMin = -yMax;

                Pen axisPen = new Pen(Color.Black, 1);
                // Brush pointBrush = new SolidBrush(Color.Red);

                // x axis
                g.DrawLine(axisPen, PADDING, PADDING + height / 2, PADDING + width, PADDING + height / 2);
                // y axis
                g.DrawLine(axisPen, PADDING, PADDING + height, PADDING, PADDING);

                for (int i = (int)xMin; i <= xMax; i++)
                {
                    int x = (int)(PADDING + (i - xMin) * width / (xMax - xMin));
                    g.DrawLine(axisPen, x, PADDING + height / 2 - 5, x, PADDING + height / 2 + 5);
                    g.DrawString(i.ToString(), form.Font, new SolidBrush(Color.Black), x - 10, PADDING + height / 2 + 10);
                }

                for (int i = (int)yMin; i <= yMax; i++)
                {
                    int y = (int)(PADDING + height - (i - yMin) * height / (yMax - yMin));
                    g.DrawLine(axisPen, PADDING - 5, y, PADDING + 5, y);
                    g.DrawString(i.ToString(), form.Font, new SolidBrush(Color.Black), PADDING - 30, y - 10);
                }

                // Draw trajectories of the input data
                Pen[] pens = { Pens.Blue, Pens.Green, Pens.Red, Pens.Orange, Pens.Purple, Pens.Brown, Pens.Magenta, Pens.Cyan };
                for (int i = 0; i < data.Count; i++)
                {
                    List<int> dataList = data[i];
                    if (dataList.Count < 2) continue;

                    int x1 = PADDING;
                    int y1 = PADDING + height / 2;
                    int x2 = (int)(PADDING + (1 - xMin) * width / (xMax - xMin));
                    // Interval i of y -> i * height / (yMax - yMin) 
                    int y2 = (int)(PADDING + height / 2 - dataList[0] * height / (yMax - yMin));

                    Pen pen = pens[i % pens.Length];
                    g.DrawLine(pen, x1, y1, x2, y2);

                    for (int j = 1; j < dataList.Count; j++)
                    {
                        x1 = x2;
                        y1 = y2;
                        x2 = (int)(PADDING + (j + 1 - xMin) * width / (xMax - xMin));
                        y2 += (int)(-dataList[j] * height / (yMax - yMin));

                        // Use different colors and styles for each trajectory
                        pen = pens[i % pens.Length];
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }

                }
            }
        }

        public void DrawChart(Form form, List<List<float>> data)
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {

                float width = graphBitmap.Width - 2 * PADDING;
                float height = graphBitmap.Height - 2 * PADDING;

                this.xMin = 0;  // minx fisso a 0
                this.xMax = data.Max(list => list.Count());

                this.yMin = 0;
                this.yMax = data.SelectMany(list => list).Max();


                Pen axisPen = new Pen(Color.Black, 2);
                // Brush pointBrush = new SolidBrush(Color.Red);

                g.DrawLine(axisPen, PADDING, PADDING + height, (PADDING + width), (PADDING + height));
                g.DrawLine(axisPen, PADDING, PADDING + height, PADDING, PADDING);

                for (float i = xMin; i <= xMax; i++)
                {
                    float x = PADDING + (i - xMin) * width / (xMax - xMin);
                    g.DrawLine(axisPen, x, (PADDING + height - 5), x, (PADDING + height + 5));
                    g.DrawString(i.ToString(), form.Font, new SolidBrush(Color.Black), x - 10, PADDING + height + 10);
                }

                for (float i = yMin; i <= yMax; i++)
                {
                    float y = PADDING + height - (i - yMin) * height / (yMax - yMin);
                    g.DrawLine(axisPen, PADDING - 5, y, (PADDING + 5), y);
                    g.DrawString(i.ToString(), form.Font, new SolidBrush(Color.Black), (PADDING - 30), (y - 10));
                }

                // Disegna le traiettorie dei dati in input
                Pen[] pens = { Pens.Blue, Pens.Green, Pens.Red, Pens.Orange, Pens.Purple, Pens.Brown, Pens.Magenta, Pens.Cyan };


                for (int i = 0; i < data.Count; i++)
                {
                    List<float> dataList = data[i];
                    if (dataList.Count < 2) continue;

                    for (int j = 1; j < dataList.Count; j++)
                    {
                        double x1 = PADDING + (j - 1 - xMin) * width / (xMax - xMin);
                        double y1 = PADDING + height - (dataList[j - 1] - yMin) * height / (yMax - yMin);
                        double x2 = PADDING + (j - xMin) * width / (xMax - xMin);
                        double y2 = PADDING + height - (dataList[j] - yMin) * height / (yMax - yMin);

                        // Usa colori e stili diversi per ogni traiettoria
                        Pen pen = pens[i % pens.Length];
                        g.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
                    }
                }
            }
        }

        public int getPADDING()
        {
            return PADDING;
        }

        public void setPadding(int padding)
        {
            PADDING = padding;
        }

        private void mouseSettings()
        {

            // move Box
            // Register mouse events
            this.pictureBox.MouseUp += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (null == c) return;
                isDragging = false;
            };

            this.pictureBox.MouseDown += (sender, args) =>
            {
                if (args.Button != MouseButtons.Left) return;
                isDragging = true;
                mouseDownLocation.X = args.X;
                mouseDownLocation.Y = args.Y;
            };

            this.pictureBox.MouseMove += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (!isDragging || null == c) return;
                c.Top = args.Y + c.Top - mouseDownLocation.Y;
                c.Left = args.X + c.Left - mouseDownLocation.X;
            };
        }
    }
}
