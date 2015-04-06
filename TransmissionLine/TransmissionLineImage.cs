using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TransmissionLine
{
    public class TransmissionLineImage : UserControl
    {
        public TransmissionLineImage()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);
            transmissionPointer.DashStyle = DashStyle.Dot;
            Limit = 350;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 预置
            height_percent = e.ClipRectangle.Height / 40F;
            width_percent = e.ClipRectangle.Width / 40F;
            // 背景
            e.Graphics.Clear(Color.White);
            // 边框
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Outset);
            base.OnPaint(e);
            // 传输线图像
            DrawTransmissionLine(e.Graphics);
            // 波形
            DrawPoints(e.Graphics);
            // 焦点
            DrawSelected(e.Graphics);
        }

        public float height_percent = 0, width_percent = 0;
        private readonly Pen transmissionBrush = new Pen(Color.Black);
        private readonly Pen transmissionPointer = new Pen(Color.Black);
        private readonly Pen wavePen = new Pen(Color.DarkGreen);
        readonly Pen FocusPen = new Pen(Color.OrangeRed, 1.4F);

        void DrawTransmissionLine(Graphics g)
        {
            g.DrawLine(transmissionBrush, width_percent * 5, height_percent * 10, width_percent * 18, height_percent * 10);
            g.DrawLine(transmissionBrush, width_percent * 22, height_percent * 10, width_percent * 35, height_percent * 10);
            g.DrawRectangle(transmissionBrush, width_percent * 18, height_percent * 9, width_percent * 4, height_percent * 2);
            g.DrawLine(transmissionBrush, width_percent * 5, height_percent * 30, width_percent * 35, height_percent * 30);
            g.DrawLine(transmissionBrush, width_percent * 35, height_percent * 10, width_percent * 35, height_percent * 15);
            g.DrawLine(transmissionBrush, width_percent * 35, height_percent * 30, width_percent * 35, height_percent * 25);
            g.DrawRectangle(transmissionBrush, width_percent * 34, height_percent * 15, width_percent * 2, height_percent * 10);
            g.DrawEllipse(transmissionBrush, width_percent * 5 - height_percent * 2, height_percent * 9, height_percent * 2, height_percent * 2);
            g.DrawEllipse(transmissionBrush, width_percent * 5 - height_percent * 2, height_percent * 29, height_percent * 2, height_percent * 2);
            g.DrawLine(transmissionPointer, width_percent * 5, height_percent * 20, width_percent * 33, height_percent * 20);
        }

        void DrawPoints(Graphics g)
        {
            float siz = width_percent/12.5F, width_standard = width_percent * 5, height_standard = height_percent * 20;
            var last_x = width_standard;
            var last_y = height_standard + (float)points[0];
            for (var i = 1; i < 350 && i <= Limit; i++)
            {
                var x = width_standard + siz*i;
                var y = height_standard + (float) points[i];
                g.DrawLine(wavePen, last_x, last_y, x, y);
                last_x = x;
                last_y = y;
            }
        }

        void DrawSelected(Graphics g)
        {
            if (SelectedIndex < 0) return;
            var i = SelectedIndex;
            var x = (float)(width_percent * 5 + width_percent / 12.5 * i);
            var y = height_percent * 20 + (float)points[i];
            g.DrawEllipse(FocusPen, x - 3, y - 3, 6F, 6F);
        }

        public double Limit { get; set; }
        public double[] points = new double[350];

        public int SelectedIndex { get; set; }
    }
}