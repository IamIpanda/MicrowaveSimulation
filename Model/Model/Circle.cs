using System;

namespace Smith_Chart.Model
{
    public class Circle
    {
        public Circle(double x, double y, double r)
        {
            Center = new Point(x, y);
            Radium = r;
        }

        public Circle()
        {
            Center = new Point();
            Radium = 0;
        }
        public virtual Point Center { get; set; }
        public virtual double Radium { get; set; }

        public bool isOn(Point p)
        {
            return p.R == Radium;
        }

        public Point Rotate(Point p, double angle, Direction d)
        {
            var origin_angle = Math.Atan2(p.Y - Center.Y, p.X - Center.X);
            if (d == Direction.Clockwise || d == Direction.ToSource)
                origin_angle -= angle;
            else origin_angle += angle;
            return new Point(Center.X + Radium * Math.Cos(origin_angle), Center.Y + Radium * Math.Sin(origin_angle));
        }

        public Point RotateLength(Point p, double length, Direction d)
        {
            double angle = length*Math.PI;

            return Rotate(p, angle, d);
        }
    }

    public enum Direction
    {
        Clockwise = 0,
        AntiClockwinse = 1,
        ToSource = 0,
        ToLoad = 1
    }
}