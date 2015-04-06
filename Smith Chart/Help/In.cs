using System.Windows.Forms;

namespace Smith_Chart.Help
{
    public class In : DefaultDictionary<string, object>
    {
        public static In Instance { get; set; }

        static In()
        {
            Instance = new In();
        }

        private In() {}
    }
}