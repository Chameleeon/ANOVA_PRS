using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PerformanceTest
{

    public partial class ResultsWindow : Window
    {
        private string _path;
        private AnovaSummary _summary;
        private DataGrid _dataGrid;
        private double[][] _measurements;
        public ObservableCollection<DataItem> Summaries { get; set; }

        public void InitializeWindow()
        {
            InitializeComponent();
            _dataGrid = this.FindControl<DataGrid>("DataGrid");
            var dataItems = new List<DataItem>();
            DataItem item = new DataItem("Sum of squares", _summary.SumOfSquares[0].ToString("F4"), _summary.SumOfSquares[1].ToString("F4"), _summary.SumOfSquares[2].ToString("F4"));
            dataItems.Add(item);
            DataItem item2 = new DataItem("Degrees of freedom", _summary.DegsOfFreedom[0].ToString("F0"), _summary.DegsOfFreedom[1].ToString("F0"), _summary.DegsOfFreedom[2].ToString("F0"));
            dataItems.Add(item2);
            DataItem item3 = new DataItem("Mean squares", _summary.MeanSquares[0].ToString("F4"), _summary.MeanSquares[1].ToString("F4"));
            dataItems.Add(item3);
            DataItem item4 = new DataItem("Computed F", _summary.ComputedF.ToString("F4"));
            dataItems.Add(item4);
            DataItem item5 = new DataItem("Tabulated F", _summary.TabulatedF.ToString("F4"));
            dataItems.Add(item5);
            Summaries = new ObservableCollection<DataItem>(dataItems);
            _dataGrid.DataContext = Summaries;

            var backgroundImage = this.FindControl<Image>("Background");
            var wrenchImage = this.FindControl<Image>("Wrench");
            var contrastImage = this.FindControl<Image>("Contrast");
            var quitButtonImage = this.FindControl<Image>("Quit");

            backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
            wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
            contrastImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/contrast.png");
            quitButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/quit.png");

        }
        public ResultsWindow(AnovaSummary summary, string path)
        {
            _path = path;
            _summary = summary;
            InitializeWindow();
        }

        public ResultsWindow(AnovaSummary summary, double[][] measurements)
        {
            _measurements = measurements;
            _summary = summary;
            InitializeWindow();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ContrastButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_path != null)
            {
                var contrastWindow = new ContrastWindow(_summary, _path);
                contrastWindow.Show();
            }
            else if (_measurements != null)
            {
                var contrastWindow = new ContrastWindow(_summary, _measurements);
                contrastWindow.Show();
            }
            this.Close();
        }

        private void QuitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
