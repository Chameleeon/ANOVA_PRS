using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PerformanceTest
{

    public partial class ResultsWindow : Window
    {
        private DataGrid _dataGrid;
        public ObservableCollection<DataItem> Summaries { get; set; }

        public ResultsWindow(AnovaSummary summary)
        {
            InitializeComponent();
            Summaries = new ObservableCollection<DataItem>();
            DataItem item = new DataItem("Sum of squares", summary.SumOfSquares[0], summary.SumOfSquares[1], summary.SumOfSquares[2]);
            Summaries.Add(item);
            item = new DataItem("Degrees of freedom", summary.DegsOfFreedom[0], summary.DegsOfFreedom[1], summary.DegsOfFreedom[2]);
            Summaries.Add(item);
            item = new DataItem("Mean squares", summary.MeanSquares[0], summary.MeanSquares[1]);
            Summaries.Add(item);
            item = new DataItem("Computed F", summary.ComputedF);
            Summaries.Add(item);
            item = new DataItem("Tabulated F", summary.TabulatedF);
            Summaries.Add(item);

            // PopulateDataGrid(summary);
            var backgroundImage = this.FindControl<Image>("Background");
            var wrenchImage = this.FindControl<Image>("Wrench");

            backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
            wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _dataGrid = this.FindControl<DataGrid>("DataGrid");
        }

        // private void PopulateDataGrid(AnovaSummary summary)
        // {
        //     // Create a list to hold the data
        //     var data = new List<DataItem>();
        //
        //     // Add rows to the list
        //     data.Add(new DataItem("Sum of squares", summary.SumOfSquares[0], summary.SumOfSquares[1], summary.SumOfSquares[2]));
        //     data.Add(new DataItem("Degrees of freedom", summary.DegsOfFreedom[0], summary.DegsOfFreedom[1], summary.DegsOfFreedom[2]));
        //     data.Add(new DataItem("Mean squares", summary.MeanSquares[0], summary.MeanSquares[1]));
        //     data.Add(new DataItem("Computed F", summary.ComputedF));
        //     data.Add(new DataItem("Tabulated F", summary.TabulatedF));
        //
        //     // Set the data to the DataGrid
        //     _dataGrid.DataContext = data;
        // }

    }
}
