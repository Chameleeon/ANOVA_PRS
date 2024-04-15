using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace PerformanceTest;

public partial class ContrastWindow : Window
{
    private string _path;
    private int _system1;
    private int _system2;
    private ComboBox _cb1;
    private ComboBox _cb2;
    private AnovaSummary _summary;
    private double[][] _measurements;
    public void InitWindow()
    {

        InitializeComponent();
        var backgroundImage = this.FindControl<Image>("Background");
        var wrenchImage = this.FindControl<Image>("Wrench");
        var startButtonImage = this.FindControl<Image>("Calculate");
        var quitButtonImage = this.FindControl<Image>("Quit");

        backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
        wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
        startButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/calculate.png");
        quitButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/quit.png");

        List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
        List<ComboBoxItem> comboBoxItems2 = new List<ComboBoxItem>();
        _cb1 = this.FindControl<ComboBox>("ComboBox1");
        _cb2 = this.FindControl<ComboBox>("ComboBox2");
        var stackPanel = this.FindControl<StackPanel>("StackPanel");

        int numberOfItems = _summary.SumOfSquares.Length;

        for (int i = 1; i <= numberOfItems; i++)
        {
            ComboBoxItem comboBoxItem = new ComboBoxItem();
            ComboBoxItem comboBoxItem2 = new ComboBoxItem();
            comboBoxItem.Content = "System " + i;
            comboBoxItem2.Content = "System " + i;
            comboBoxItems.Add(comboBoxItem);
            comboBoxItems2.Add(comboBoxItem2);
        }

        // Add the ComboBoxItems to the ComboBox
        foreach (var item in comboBoxItems)
        {
            _cb1.Items.Add(item);
        }
        foreach (var item in comboBoxItems2)
        {
            _cb2.Items.Add(item);
        }
        _cb1.SelectedIndex = 0;
        _cb2.SelectedIndex = 0;
    }
    public ContrastWindow(AnovaSummary summary, double[][] measurements)
    {
        _summary = summary;
        _measurements = measurements;
        InitWindow();
    }
    public ContrastWindow(AnovaSummary summary, string path)
    {
        _summary = summary;
        _path = path;
        InitWindow();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CalculateButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Tuple<double, double> contrast;
        if (_path != null)
        {
            contrast = Anova.CalculateContrastInterval(_path, _system1, _system2);
        }
        else
        {
            contrast = Anova.CalculateContrastInterval(_measurements, _system1, _system2);
        }
        string text = "Result: (" + contrast.Item1.ToString("F4") + ", " + contrast.Item2.ToString("F4") + ")";
        var resultTextBlock = this.FindControl<TextBlock>("ResultTextBlock");
        resultTextBlock.Text = text;
    }
    private void QuitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
        if (_system1 != 0)
        {
            string prevValue = "System " + _system1;
            if (selectedItem.Content.ToString() != prevValue)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = prevValue;
                _cb2.Items.Add(comboBoxItem);
            }
        }

        if (selectedItem != null)
        {
            foreach (ComboBoxItem item2 in _cb2.Items)
            {
                if (item2.Content.ToString() == selectedItem.Content.ToString())
                {
                    _cb2.Items.Remove(item2);
                    break; // Exit the loop after removing the first occurrence
                }
            }
            _system1 = (int.Parse(selectedItem.Content.ToString().Split(" ")[1]));
        }
    }

    private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;

        if (_system2 != 0)
        {
            string prevValue = "System " + _system2;
            if (selectedItem.Content.ToString() != prevValue)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = prevValue;
                _cb1.Items.Add(comboBoxItem);
            }
        }

        if (selectedItem != null)
        {
            bool itemExists = false;
            foreach (ComboBoxItem item1 in _cb1.Items)
            {
                if (item1.Content.ToString() == selectedItem.Content.ToString())
                {
                    _cb1.Items.Remove(item1);
                    itemExists = true;
                    break; // Exit the loop after removing the first occurrence
                }
            }
            _system2 = (int.Parse(selectedItem.Content.ToString().Split(" ")[1]));
        }
    }
}
