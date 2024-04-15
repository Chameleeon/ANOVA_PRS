using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using System;

namespace PerformanceTest;

public partial class PopupWindow : Window
{
    private TextBox _textBox1;
    private TextBox _textBox2;
    private TextBlock _textBlock;
    private int alternatives;
    private int measurements;
    public PopupWindow()
    {
        InitializeComponent();
        // Set the size and title of the popup window
        Width = 300;
        Height = 200;
        Title = "Popup Window";

        // Create a stack panel to hold the input fields
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Create the first input field (TextBox)
        _textBox1 = new TextBox
        {
            Watermark = "Number of alternatives"
        };
        stackPanel.Children.Add(_textBox1);

        // Create the second input field (TextBox)
        _textBox2 = new TextBox
        {
            Watermark = "Number of measurements"
        };
        stackPanel.Children.Add(_textBox2);
        _textBlock = new TextBlock
        {
            Text = "Invalid input! Must be positive integers!",
            IsVisible = false,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Avalonia.Media.Brushes.Red
        };
        stackPanel.Children.Add(_textBlock);

        // Create a button to close the popup
        var closeButton = new Button
        {
            Content = "Ok",
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Avalonia.Thickness(0, 10, 0, 0)
        };
        closeButton.Click += ClosePopup;
        stackPanel.Children.Add(closeButton);

        // Add the stack panel and close button to the content of the popup window
        Content = new Grid
        {
            Children = { stackPanel }
        };
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void ClosePopup(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

        if (int.TryParse(_textBox1.Text, out alternatives) && int.TryParse(_textBox2.Text, out measurements))
        {
            if (alternatives > 0 && measurements > 0)
            {
                var manualEntryWindow = new ManualEntryWindow(alternatives, measurements);
                manualEntryWindow.Show();
                Close();
                this.Close();
            }
            else
            {
                _textBlock.IsVisible = true;
            }
        }
        else
        {
            _textBlock.IsVisible = true;
        }

    }
}
