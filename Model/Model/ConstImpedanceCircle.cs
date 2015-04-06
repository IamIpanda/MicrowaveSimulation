namespace Smith_Chart.Model
{
    public class ConstImpedanceCircle : Circle
    {
        private static readonly Point CenterPoint = new Point(0, 0);
        public override Point Center
        {
            get { return CenterPoint; }
            set { base.Center = value; }
        }

        public double E
        {
            get { return Radium; }
        }

        public ConstImpedanceCircle(double eta)
        {
            Radium = eta;
        }
    }
}