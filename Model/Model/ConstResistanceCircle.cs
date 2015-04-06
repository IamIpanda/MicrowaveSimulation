namespace Smith_Chart.Model
{
    public class ConstResistanceCircle : Circle
    {
        public override Point Center
        {
            get { return new Point(1 - Radium, 0); }
            set { base.Center = value; }
        }

        public double r
        {
            get { return 1 / Radium - 1; }
        }

        public ConstResistanceCircle(double resistance)
        {
            Radium = 1/(1 + resistance);
        }
    }
}