using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms.VisualStyles;
using Smith_Chart.Model;

namespace TransmissionLine
{
    public class In
    {
        public In()
        {
            Z0 = 100;
            Zl = new Complex(100, 20);
            W = 4;
            A1 = 20;
            A2 = 20;
            Time = 20;
        }

        public const double Vc = 10;
        public const double DB = Math.PI*2/50D;
        public double Z0 { get; set; }
        public Complex Zl;
        public double A1 { get; set; }
        public double A2 { get; set; }
        public int Time { get; set; }

        private double w;
        public double W
        {
            get { return w; }
            set
            {
                w = value;
                b = l = null;
            }
        }

        private double? b;
        public double B 
        {
            get
            {
                if (b == null) b = W/Vc;
                return b.Value;
            }
        }

        private double? l;
        public double L
        {
            get 
            { 
                if (l == null) l = Vc/W;
                return l.Value;
            }
        }

        // V = A1 * cos(w * t - b * z) + A2 * cos(w * t + b * z)
        public double this[double t, double z]
        {
            get { return A1*Math.Cos(W*t - B*z) + A2*Math.Cos(W*t + B*z); }
        }

        public double this[double t, double count, bool reflect = true]
        {
            get { return A1*Math.Cos(W*t - count*DB) + (reflect ? (A2*Math.Cos(W*t + count*DB)) : 0); }
        }
    }
}