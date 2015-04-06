namespace Smith_Chart.Help
{
    public class Out
    {
        public static Out Instance { get; set; }

        static Out()
        {
            Instance = new Out();
        }

        private Out() {} 
    }
}