using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace Smith_Chart.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Complex
    {
        private double _real;
        private double _imaginary;

        [DebuggerStepThrough]
        public Complex(double real = 0, double imaginary = 0)
        {
            _real = real;
            _imaginary = imaginary;
        }

        public double Real
        {
            get { return _real; }
            set { _real = value; }
        }

        public double Imaginary
        {
            get { return _imaginary; }
            set { _imaginary = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Complex Conjugate
        {
            [DebuggerStepThrough] get { return new Complex(_real, -_imaginary); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double Modulus
        {
            [DebuggerStepThrough] get { return Math.Sqrt((_real*_real) + (_imaginary*_imaginary)); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double Argument
        {
            [DebuggerStepThrough] get { return Math.Atan2(_imaginary, _real); }
            set
            {
                double modulus = Modulus;
                _real = Math.Cos(value)*modulus;
                _imaginary = Math.Sin(value)*modulus;
            }
        }

        public static Complex operator +(Complex z1, Complex z2)
        {
            return new Complex(z1._real + z2._real, z1._imaginary + z2._imaginary);
        }

        public static Complex operator +(Complex c)
        {
            return c;
        }

        public static Complex operator -(Complex c)
        {
            return new Complex(-c.Real, -c.Imaginary);
        }

        public static Complex operator -(Complex z1, Complex z2)
        {
            return new Complex(z1._real - z2._real, z1._imaginary - z2._imaginary);
        }

        public static Complex operator *(Complex z1, Complex z2)
        {
            return new Complex((z1._real*z2._real) - (z1._imaginary*z2._imaginary),
                (z1._real*z2._imaginary) + (z1._imaginary*z2._real));
        }

        public static Complex operator *(double d1, Complex z2)
        {
            return new Complex(d1*z2._real, d1*z2._imaginary);
        }

        public static Complex operator *(Complex z1, double d2)
        {
            return d2*z1;
        }

        public static Complex operator /(Complex z1, Complex z2)
        {
            double num = (z2._real*z2._real) + (z2._imaginary*z2._imaginary);
            return new Complex(((z1._real*z2._real) + (z1._imaginary*z2._imaginary))/num,
                ((z1._imaginary*z2._real) - (z1._real*z2._imaginary))/num);
        }

        public static bool operator ==(Complex z1, Complex z2)
        {
            return ((z1._real == z2._real) && (z1._imaginary == z2._imaginary));
        }

        public static bool operator !=(Complex z1, Complex z2)
        {
            if (z1._real == z2._real)
            {
                return (z1._imaginary != z2._imaginary);
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (_real.GetHashCode() ^ _imaginary.GetHashCode());
        }

        public override string ToString()
        {
            if (_imaginary < 0.0)
            {
                return (_real + " - j" + -_imaginary);
            }
            else if (_imaginary == 0.0)
                return _real.ToString();
            return (_real + " + j" + _imaginary);
        }

        public string ToString(string p)
        {
            if (_imaginary < 0.0)
            {
                return (_real.ToString(p) + " - j" + (-_imaginary).ToString(p));
            }
            else if (_imaginary == 0.0)
                return _real.ToString(p);
            return (_real.ToString(p) + " + j" + _imaginary.ToString(p));
        }



        public static Complex operator +(Complex c, double d)
        {
            return new Complex(c.Real + d, c.Imaginary);
        }

        public static Complex operator +(double d, Complex c)
        {
            return new Complex(c.Real + d, c.Imaginary);
        }

        public static Complex operator -(Complex c, double d)
        {
            return new Complex(c.Real - d, c.Imaginary);
        }

        public static Complex operator -(double d, Complex c)
        {
            return new Complex(-c.Real + d, -c.Imaginary);
        }

        public static Complex operator /(Complex c, double d)
        {
            return new Complex(c.Real/d, c.Imaginary/d);
        }

        public static Complex operator /(double d, Complex c)
        {
            return new Complex(d, 0)/c;
        }

        static public Complex J = new Complex(0, 1);
    }
}