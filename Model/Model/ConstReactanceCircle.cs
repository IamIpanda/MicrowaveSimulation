using System;

namespace Smith_Chart.Model
{
    public class ConstReactanceCircle : Circle
    {
        public override Point Center
        {
            get { return new Point(1, 1/X); }
            set { base.Center = value; }
        }

        public double X { get; private set; }

        public ConstReactanceCircle(double X)
        {
            this.X = X;
            Radium = 1/Math.Abs(X);
        }
    }
}