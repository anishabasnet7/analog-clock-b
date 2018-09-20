using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

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
            g.SmoothingMode = SmoothingMode.AntiAlias;
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
            //Hour Indicators
            int r1 = r;
            int r2 = r -70;
            double thetaInterval = DegToRad(30);
            DrawIndicators(g, new Pen(Color.Black,4) ,r1, r2, thetaInterval,xc,yc);
            //Minute Indicators
            r1 = r;
            r2 = (int)(0.92*r);
            thetaInterval = DegToRad(6);
            DrawIndicators(g, new Pen(Color.Black, 2), r1, r2, thetaInterval, xc, yc);
            //
            int sec = DateTime.Now.Second;
            int min=DateTime.Now.Minute;
            int hour=DateTime.Now.Hour;
            DrawSecondHand(g,sec,r-80,xc,yc);
            DrawMinuteHand(g, min, r-100,xc, yc,sec);
            DrawHourHand(g, hour, r - 140, xc, yc, min,sec);
     
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
            int x =xc+(int)(r * Math.Cos(theta));
            int y= yc-(int)(r * Math.Sin(theta));
            g.DrawLine(new Pen(Color.Red,1), xc, yc, x, y);

        }
        private void DrawMinuteHand(Graphics g, int minute, int r, int xc, int yc,int second)
        {
            double theta = 90 - (6 * minute) - (0.1 * second);
            theta = DegToRad(theta);
            int x = xc + (int)(r * Math.Cos(theta));
            int y = yc - (int)(r * Math.Sin(theta));
            g.DrawLine(new Pen(Color.Black,3), xc, yc, x, y);

        }
        private void DrawHourHand(Graphics g, int hour, int r, int xc, int yc, int minute,int second)
        {
            double theta = 90 - (30 * hour) - (0.5 * minute)-(1/120*second);
            theta = DegToRad(theta);
            int x = xc + (int)(r * Math.Cos(theta));
            int y = yc - (int)(r * Math.Sin(theta));
            g.DrawLine(new Pen(Color.Black,4), xc, yc, x, y);

        }
        private void DrawIndicator(Graphics g, Pen pen,int r1,int r2,double theta,int xc,int yc)
       {
          
           int x1 = xc + (int)(r1 * Math.Cos(theta));
           int y1 = yc - (int)(r1 * Math.Sin(theta));
           int x2 = xc + (int)(r2 * Math.Cos(theta));
           int y2 = yc - (int)(r2 * Math.Sin(theta));
           g.DrawLine(pen, x1, y1, x2, y2);
            
       }
        private void DrawIndicators(Graphics g, Pen pen, int r1, int r2, double thetaInterval, int xc, int yc)
        {
          for(double theta=0;theta<2*Math.PI;theta+=thetaInterval)
          {
              DrawIndicator(g, pen, r1, r2, theta, xc, yc);
          }
        }

        private double DegToRad(double theta)
        {
            return ( theta * (Math.PI / 180));
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
