
using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PerformanceTest;
public partial class SimulationWindow : Window
{

    private string[] _files = null;
    private Image calculateImage;
    public SimulationWindow()
    {
        InitializeComponent();
        var backgroundImage = this.FindControl<Image>("Background");
        var wrenchImage = this.FindControl<Image>("Wrench");
        this.calculateImage = this.FindControl<Image>("Calc");
        backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
        wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
        this.calculateImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/calc.png");

        SelectedFilesTextBlock = this.FindControl<TextBlock>("SelectedFilesTextBlock");
        SelectedFilesListBox = this.FindControl<ListBox>("SelectedFilesListBox");
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void OpenFile_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        var selectedFiles = await openFileDialog.ShowAsync(this);
        _files = selectedFiles;

        if (selectedFiles != null && selectedFiles.Length > 0)
        {
            SelectedFilesListBox.IsVisible = true;
            var calcButton = this.FindControl<Button>("CalcButton");
            calcButton.IsVisible = true;
            // Show the Selected Files text
            SelectedFilesTextBlock.IsVisible = true;

            // Clear existing items before adding new ones
            SelectedFilesListBox.Items.Clear();

            // Add the selected file names to the ListBox
            foreach (var filePath in selectedFiles)
            {
                string fileName = Path.GetFileName(filePath);
                SelectedFilesListBox.Items.Add(fileName);
            }
        }
    }

    private async void ManualEntry_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var popupWindow = new PopupWindow();
        await popupWindow.ShowDialog(this);
        this.Close();
    }

    private async void CalculateButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        AnovaSummary sum = Anova.CalculateSummary(_files[0]);

        var resultsWindow = new ResultsWindow(sum, _files[0]);

        resultsWindow.Show();

        this.Close();
    }
}
