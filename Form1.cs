using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПР12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private PointF tek_pt;
        private PointF start_pos = new Point(200, 50);
        private int tek_ind;
        private double v = 1;
        private float rad = 20f;
        List<PointF> points = new List<PointF>();

        private void button1_Click(object sender, EventArgs e)
        {
            points.Clear();

            int count = (int)numericUpDown1.Value;
            int length = 50;//размер

            double R = length / (2 * Math.Sin(Math.PI / count));
            for (double angle = 0.0; angle <= 2 * Math.PI; angle += 2 * Math.PI / count)
            {
                int x = (int)(R * Math.Cos(angle));
                int y = (int)(R * Math.Sin(angle));
                points.Add(new PointF((int)R + x + start_pos.X, (int)R + y + start_pos.Y));
            }
            Invalidate();
            tek_pt = new PointF(points[0].X, points[0].Y);
            tek_ind = 1;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.FillEllipse(Brushes.Red, tek_pt.X - (rad / 2.0f), tek_pt.Y - (rad / 2.0f), rad, rad);
            if (points.Count >= 3)
                e.Graphics.DrawPolygon(Pens.Blue, points.ToArray());
            DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tek_ind != points.Count)
            {
                PointF A = tek_pt;
                PointF B = points[tek_ind];
                if (Math.Sqrt(((B.X - A.X) * (B.X - A.X)) + ((B.Y - A.Y) * (B.Y - A.Y))) <= v)
                {
                    tek_pt = points[tek_ind];
                    B = points[tek_ind];
                    tek_ind += 1;
                }
                double k1 = B.X - A.X;
                double k2 = B.Y - A.Y;
                double h = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y)) / v;
                double x = A.X + (k1 / h);
                double y = A.Y + (k2 / h);
                tek_pt = new PointF((float)x, (float)y);
                Invalidate();
            }
            else
            {
                tek_ind = 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
