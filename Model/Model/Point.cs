using System;
using System.Text.RegularExpressions;

namespace Smith_Chart.Model
{
    public partial class Point
    {
        private double x, y;
        private double? r, t;

        public Point(double x = 1, double y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                r = t = null;
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                r = t = null;
            }
        }

        public double R
        {
            get { return r ?? CalculateR(); }
        }

        public double T
        {
            get { return t ?? CalculateT(); }
        }

        double CalculateR()
        {
            Calculate();
            return r ?? 0;
        }

        double CalculateT()
        {
            Calculate();
            return t ?? 0;
        }

        void Calculate()
        {
            r = Math.Sqrt(x*x + y*y);
            t = Math.Atan2(y, x);
        }
    }


    public partial class Point
    {
        private Complex? reflection_coefficient = null;
        private Complex? impedance = null;
        void CalculateParameters()
        {
            reflection_coefficient = new Complex(x, y);
            impedance = (1 + reflection_coefficient.Value)/(1 - reflection_coefficient);
        }

        Complex CalculateE()
        {
            CalculateParameters();
            return reflection_coefficient ?? default(Complex);
        }

        Complex CalculateZ()
        {
            CalculateParameters();
            return reflection_coefficient ?? default(Complex);
        }

        
        public Complex ReflectionCoefficient
        {
            get { return reflection_coefficient ?? CalculateE(); }
        }

        public Complex Impedance
        {
            get { return impedance ?? CalculateZ(); }
        }
        
        /// <summary>
        /// 反射系数
        /// </summary>
        public Complex E
        {
            get { return ReflectionCoefficient; }
        }

        /// <summary>
        /// 阻抗
        /// </summary>
        public Complex Z
        {
            get { return Impedance; }
        }


    }
}