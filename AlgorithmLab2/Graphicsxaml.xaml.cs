using System.Collections.ObjectModel;
using System.Windows.Controls;
using LiveCharts.Defaults;
using LiveCharts;
using System;
using System.ComponentModel;

namespace AlgorithmLab2
{
    public partial class Graphicsxaml : Page
    {
        public ChartValues<ObservableValue> Values { get; set; }
        public Func<double, string> LabelFormatter => value => (value + 1).ToString();

        public Graphicsxaml()
        {
            InitializeComponent();
            Values = new ChartValues<ObservableValue>();
            MyChart.DataContext = this;
        }

        private void FunctionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFunction = (sender as ComboBox).SelectedItem as ComboBoxItem;

            if (selectedFunction != null && int.TryParse(DepthInput.Text, out int n))
            {
                Values.Clear();
                switch (selectedFunction.Content.ToString())
                {
                    case "Hanoi towers recursively":
                        PlotFunction(TimeCounter.MeasureTime(n, new HanoiTowerRecursive()));
                        MyChart.AxisX[0].Title = "Rings, n";
                        break;
                    case "Hanoi towers with stack":
                        PlotFunction(TimeCounter.MeasureTime(n, new HanoiTowerStack()));
                        MyChart.AxisX[0].Title = "Rings, n";
                        break;
                    case "T-square recursively":
                        PlotFunction(TimeCounter.MeasureTime(n, new TSquareRecursive()));
                        MyChart.AxisX[0].Title = "Depth, n";
                        break;
                    case "T-square with stack":
                        PlotFunction(TimeCounter.MeasureTime(n, new TSquareStack()));
                        MyChart.AxisX[0].Title = "Depth, n";
                        break;
                }
                (sender as ComboBox).SelectedItem = null;

            }
        }

        private void PlotFunction(double[] n)
        {
            for (int x = 0; x < n.Length; x ++)
            {
                Values.Add(new ObservableValue(n[x]));
            }
        }


    }
}
