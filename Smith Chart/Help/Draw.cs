using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Smith_Chart.Model;
using Point = System.Drawing.Point;

namespace Smith_Chart.Help
{
    public static class Draw
    {
        static Control target;
        static float half_width = 0, half_height = 0, zoom = 1;

        static public Control Target
        {
            get { return target; }
            set
            {
                if (target != null) target.SizeChanged -= TargetOnSizeChanged;
                target = value;
                target.SizeChanged += TargetOnSizeChanged;
                TargetOnSizeChanged(null, new EventArgs());
            }
        }

        private static void TargetOnSizeChanged(object sender, EventArgs eventArgs)
        {
            half_width = target.ClientRectangle.Width / 2;
            half_height = target.ClientRectangle.Height / 2;
            zoom = Math.Min(half_width, half_height) / 1.1F;
        }

        static Draw()
        {
            BackGrounds.Add(Core.SearchConstImpedanceCircle(1));
            BackGrounds.Add(Core.SearchConstImpedanceCircle(1.02));
            BackGrounds.Add(Core.SearchConstResistanceCircle(0.3));
            BackGrounds.Add(Core.SearchConstResistanceCircle(0.6));
            BackGrounds.Add(Core.SearchConstResistanceCircle(1));
            BackGrounds.Add(Core.SearchConstResistanceCircle(3));
            BackGrounds.Add(Core.SearchConstResistanceCircle(5));
            BackGrounds.Add(Core.SearchConstReactanceCircle(0.3));
            BackGrounds.Add(Core.SearchConstReactanceCircle(0.6));
            BackGrounds.Add(Core.SearchConstReactanceCircle(1));
            BackGrounds.Add(Core.SearchConstReactanceCircle(3));
            BackGrounds.Add(Core.SearchConstReactanceCircle(5));
            BackGrounds.Add(Core.SearchConstReactanceCircle(-0.3));
            BackGrounds.Add(Core.SearchConstReactanceCircle(-0.6));
            BackGrounds.Add(Core.SearchConstReactanceCircle(-1));
            BackGrounds.Add(Core.SearchConstReactanceCircle(-3));
            BackGrounds.Add(Core.SearchConstReactanceCircle(-5));
        }

        static public Graphics G { get; set; }

        static List<object> Objects = new List<object>();
        static List<object> BackGrounds = new List<object>(); 

        static PointF Transform(Model.Point p)
        {
            return new PointF(Convert.ToSingle(p.X * zoom + half_width), Convert.ToSingle(- p.Y * zoom + half_height));
        }

        static void DrawPoint(Model.Point point)
        {
            if (G == null) return;
            var p = Transform(point);
            G.FillEllipse(Colors.PointBrush, p.X - 3, p.Y - 3, 6, 6);
        }

        private static void DrawCircle(Model.Circle circle, bool back = false)
        {
            if (G == null) return;
            var center = Transform(circle.Center);
            var r = Convert.ToSingle((circle.Radium*zoom));
            var b = back ? Colors.BackGroundPen : Colors.LineBrush;
            var rect = new RectangleF(center.X - r, center.Y - r, 2*r, 2*r);
            if (circle is ConstReactanceCircle)
            {
                var cir = circle as ConstReactanceCircle;
                var cross = Core.CrossCircle(circle, Core.UnitCircle);
                var t = cross[0];
                if (t.X == 1 && t.Y == 0) t = cross[1];
                float theta = Convert.ToSingle(Math.Atan2(t.Y - cir.Center.Y, t.X - cir.Center.X)*180/Math.PI);
                DrawPoint(t);
                G.DrawArc(b,rect,-180,-90);
                //G.DrawArc(back ? Colors.BackGroundPen : Colors.LineBrush, center.X - r, center.Y - r, 2*r, 2*r,
                //Convert.ToSingle(Math.PI/2), Convert.ToSingle(theta));
            }
            else
                G.DrawEllipse(b, rect);
        }

        static public void RedrawAll()
        {
            G.Clear(Color.White);
            foreach (var backGround in BackGrounds)
                DrawObj(backGround, true);
            foreach (var o in Objects)
                DrawObj(o);
        }

        static void DrawObj(object obj, bool back = false)
        {
            if (obj is Model.Point)
                DrawPoint(obj as Model.Point);
            else if (obj is Model.Circle)
                DrawCircle(obj as Model.Circle, back);
        }

        static public void Erase()
        {
            G.Clear(Color.White);
            foreach (var backGround in BackGrounds)
                DrawObj(backGround, true);
            Objects.Clear();
        }

        public static void Add(object obj)
        {
            if (!(Objects.Contains(obj)))
                Objects.Add(obj);
            DrawObj(obj);
        }

        static public class Colors
        {
            static Colors()
            {
                LineBrushes = new List<Pen>();
                Count = 0;
                PointBrush = new SolidBrush(Color.Red);
                LineBrushes.Add(new Pen(Color.DarkOrange));
                LineBrushes.Add(new Pen(Color.Yellow));
                LineBrushes.Add(new Pen(Color.DarkGreen));
                LineBrushes.Add(new Pen(Color.DodgerBlue));
                LineBrushes.Add(new Pen(Color.DarkBlue));
                LineBrushes.Add(new Pen(Color.Purple));
                BackGroundPen = new Pen(Color.Black);
            }

            public static List<Pen> LineBrushes { get; set; }
            public static Brush PointBrush { get; set; }

            public static int Count { get; set; }
            public static Pen BackGroundPen { get; set; }

            public static Pen LineBrush
            {
                get
                {
                    var answer = LineBrushes[Count];
                    Count = (++Count)%LineBrushes.Count;
                    return answer;
                }
            }
        }
    }
}