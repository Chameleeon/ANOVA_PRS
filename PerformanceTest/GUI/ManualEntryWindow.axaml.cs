using System;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using Avalonia.Styling;
using Avalonia.Media;

namespace PerformanceTest;

public partial class ManualEntryWindow : Window
{
    // private TextBox _textBox;
    private TextBox[,] _textBoxes;
    private int _measurements;
    private int _alternatives;
    private TextBlock _textBlock;
    public ManualEntryWindow(int alternatives, int measurements)
    {
        _alternatives = alternatives;
        _measurements = measurements;
        InitializeComponent();
        var backgroundImage = this.FindControl<Image>("Background");
        var wrenchImage = this.FindControl<Image>("Wrench"); var calculateButtonImage = this.FindControl<Image>("Calc"); var quitButtonImage = this.FindControl<Image>("Quit");

        backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
        wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
        calculateButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/calculate.png");
        quitButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/quit.png");
        _textBlock = new TextBlock
        {
            Text = "Invalid input!",
            IsVisible = false,
            FontSize = 32,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Avalonia.Media.Brushes.Red
        };
        StackPanel stackPanel = this.FindControl<StackPanel>("BasePanel");
        stackPanel.Children.Add(_textBlock);

    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        Grid grid = this.FindControl<Grid>("MatrixGrid");
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();
        _textBoxes = new TextBox[_measurements, _alternatives];

        for (int i = 0; i < _measurements; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            for (int j = 0; j < _alternatives; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                _textBoxes[i, j] = new TextBox()
                {
                    Watermark = "Alternative " + (j + 1) + " Measurement " + (i + 1),
                    Width = 100,
                    Height = 50

                };
                grid.Children.Add(_textBoxes[i, j]);
                Grid.SetColumn(_textBoxes[i, j], j);
                Grid.SetRow(_textBoxes[i, j], i);
            }
        }
    }


    private void CalculateButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        double[][] measurements = new double[_measurements][];

        for (int i = 0; i < _measurements; i++)
        {
            measurements[i] = new double[_alternatives];
            for (int j = 0; j < _alternatives; j++)
            {
                if (double.TryParse(_textBoxes[i, j].Text, out double value))
                {
                    measurements[i][j] = value;
                }
                else
                {
                    _textBlock.IsVisible = true;
                    return;
                }
            }
        }
        AnovaSummary summary = Anova.CalculateSummary(_alternatives, _measurements, measurements);
        var newWindow = new ResultsWindow(summary, measurements);

        newWindow.Show();

        this.Close();
    }
    private void QuitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

}
