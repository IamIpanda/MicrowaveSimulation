using System;
using Smith_Chart.Model;

namespace Smith_Chart.Help
{
    public static class Core
    {
        static public Point RotateWithCircle(Point p, Circle c, double angle, Direction d)
        {
            return c.Rotate(p, angle, d);
        }
        static public Point RotateWithCircleInLength(Point p, Circle c, double length, Direction d)
        {
            return c.RotateLength(p, length, d);
        }

        static public Point SearchPointViaReflectionCoefficient(Complex c)
        {
            return SearchPointViaReflectionCoefficient(c.Real, c.Imaginary);
        }
        static public Point SearchPointViaReflectionCoefficient(double Resistance, double Reactance)
        {
            return new Point(Resistance, Reactance);
        }

        static public Point SearchPointViaImpedance(Complex c)
        {
            Complex reflection_coefficient = (c - 1)/(c + 1);
            return SearchPointViaReflectionCoefficient(reflection_coefficient);
        }
        static public Point SearchPointViaImpedance(double real, double imaginary)
        {
            return SearchPointViaImpedance(new Complex(real, imaginary));
        }

        static public ConstImpedanceCircle SearchConstImpedanceCircle(Point p)
        {
            return SearchConstImpedanceCircle(p.E);
        }
        static public ConstImpedanceCircle SearchConstImpedanceCircle(Complex c)
        {
            return SearchConstImpedanceCircle(c.Modulus);
        }
        static public ConstImpedanceCircle SearchConstImpedanceCircle(double e)
        {
            return new ConstImpedanceCircle(e);
        }
        static public ConstReactanceCircle SearchConstReactanceCircle(Point p)
        {
            return SearchConstReactanceCircle(p.E);
        }
        static public ConstReactanceCircle SearchConstReactanceCircle(Complex c)
        {
            return SearchConstReactanceCircle(c.Imaginary);
        }
        static public ConstReactanceCircle SearchConstReactanceCircle(double d)
        {
            return new ConstReactanceCircle(d);
        }

        static public ConstResistanceCircle SearchConstResistanceCircle(Point p)
        {
            return SearchConstResistanceCircle(p.E);
        }
        static public ConstResistanceCircle SearchConstResistanceCircle(Complex c)
        {
            return SearchConstResistanceCircle(c.Real);
        }
        static public ConstResistanceCircle SearchConstResistanceCircle(double d)
        {
            return new ConstResistanceCircle(d);
        }
        static public Circle SearchCircle(double x, double y, double r)
        {
            return new Circle(x, y, r);
        }

        static public Point[] CrossCircle(Circle c1, Circle c2)
        {
            double x1 = c1.Center.X, x2 = c2.Center.X, y1 = c1.Center.Y, y2 = c2.Center.Y;
            double r1 = c1.Radium, r2 = c2.Radium;
            double a = 2*r1*(x1 - x2), 
                b = 2*r1*(y1 - y2), 
                c = r2 * r2 - r1 * r1 - Sqr(x1 - x2) - Sqr(y1 - y2);
            double p = a*a + b*b, q = -2*a*c, r = c*c - b*b;
            var d = Distance(x1 - x2, y1 - y2);
            var dr = Math.Abs(r1 - r2);
            if (d > r1 + r2 || d < dr) return new Point[] {};
            else if (d == r1 + r2 || d == dr)
            {
                var cos_value = -q/p/2.0;
                var sin_value = Math.Sqrt(1 - cos_value*cos_value);
                var p1x = r1*cos_value + x1;
                var p1y = r1*sin_value + y1;
                if (Distance(p1x - x2, p1y - y2) != Sqr(r2))
                    p1y = y1 - r1*sin_value;
                return new[] {new Point(p1x, p1y)};
            }
            else
            {
                double cos_value_0 = (Math.Sqrt(q*q - 4.0*p*r) - q)/p/2.0,
                    cos_value_1 = (-Math.Sqrt(q*q - 4.0*p*r) - q)/p/2.0,
                    sin_value_0 = Math.Sqrt(1 - cos_value_0*cos_value_0),
                    sin_value_1 = Math.Sqrt(1 - cos_value_1*cos_value_1);
                double p0x = r1*cos_value_0 + x1,
                    p1x = r1*cos_value_1 + x1,
                    p0y = r1*sin_value_0 + y1,
                    p1y = r1*sin_value_1 + y1;
                if (Distance(p0x - x2, p0y - y2) != Sqr(r2))
                    p0y = y1 - r1*sin_value_0;
                if (Distance(p1x - x2, p1y - y2) != Sqr(r2))
                    p1y = y1 - r1*sin_value_1;
                return new[] {new Point(p0x, p0y), new Point(p1x, p1y)};
            }
        }

        static double Sqr(double d)
        {
            return d * d;
        }
        static double Distance(double x, double y)
        {
            return Math.Sqrt(Sqr(x) + Sqr(y));
        }

        static public ConstImpedanceCircle UnitCircle = new ConstImpedanceCircle(1);
    }
}