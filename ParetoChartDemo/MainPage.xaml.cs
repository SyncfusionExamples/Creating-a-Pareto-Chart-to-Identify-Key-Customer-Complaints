namespace ParetoChartDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NumericalAxis_LabelCreated(object sender, Syncfusion.Maui.Charts.ChartAxisLabelEventArgs e)
        {
            var num = e.Position;
            if (num >= 100000000)
            {
                e.Label = (num / 1000000D).ToString("0.#M");
            }
            if (num >= 1000000)
            {
                e.Label = (num / 1000000D).ToString("0.##M");
            }
            if (num >= 100000)
            {
                e.Label = (num / 1000D).ToString("0.#k");
            }
            if (num >= 10000)
            {
                e.Label = (num / 1000D).ToString("0.##k");
            }
        }

        private void CategoryAxis_LabelCreated(object sender, Syncfusion.Maui.Charts.ChartAxisLabelEventArgs e)
        {
#if ANDROID || IOS
            char letter = Convert.ToChar('A' + (int)e.Position);
            e.Label = letter.ToString();
#endif
        }
    }
}