using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClock
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int xc = ClientSize.Width / 2;
            int yc = ClientSize.Height / 2;
           // g.DrawLine(Pens.Black, 0, yc, ClientSize.Width, yc);
           // g.DrawLine(Pens.Black, xc, 0, xc, ClientSize.Height);
            int r = Math.Min(ClientSize.Height, ClientSize.Width) / 2 - 10;
            int w = 2 * r;
            int h = w;
            int x = xc - r;
            int y = yc - r;
            g.DrawEllipse(Pens.Blue, x, y, w, h);
            int sec = DateTime.Now.Second;
            int min=DateTime.Now.Minute;
            int hour=DateTime.Now.Hour;
            DrawSecondHand(g,sec,r-10,xc,yc);
            DrawMinuteHand(g, min, r-20,xc, yc,sec);
            DrawHourHand(g, hour, r - 35, xc, yc, min,sec);
     
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();

        }
        private void DrawSecondHand(Graphics g, int second, int r, int xc, int yc)
        {
            double theta = 90 - (6 * second);
            theta = DegToRad(theta);
            int x = xc + (int)(r * Math.Cos(theta));
            int y = yc - (int)(r * Math.Sin(theta));
        
            g.DrawLine(Pens.Red, xc, yc, x, y);

        }
        private void DrawMinuteHand(Graphics g, int minute, int r, int xc, int yc,int second)
        {
            double theta = 90 - (6 * minute) - (0.1 * second);
            theta = DegToRad(theta);
            int x = xc + (int)(r * Math.Cos(theta));
            int y = yc - (int)(r * Math.Sin(theta));
            g.DrawLine(Pens.MediumTurquoise, xc, yc, x, y);

        }
        private void DrawHourHand(Graphics g, int hour, int r, int xc, int yc, int minute,int second)
        {
            double theta = 90 - (30 * hour) - (0.5 * minute)-(1/120*second);
            theta = DegToRad(theta);
            int x = xc + (int)(r * Math.Cos(theta));
            int y = yc - (int)(r * Math.Sin(theta));
            g.DrawLine(Pens.SandyBrown, xc, yc, x, y);

        }
        
        private double DegToRad(double theta)
        {
            return (theta * (Math.PI / 180));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
