using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PerformanceTest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var backgroundImage = this.FindControl<Image>("Background");
        var wrenchImage = this.FindControl<Image>("Wrench");
        var startButtonImage = this.FindControl<Image>("Start");
        var quitButtonImage = this.FindControl<Image>("Quit");

        backgroundImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/Bg.png");
        wrenchImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/wrench.png");
        startButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/start.png");
        quitButtonImage.Source = new Avalonia.Media.Imaging.Bitmap("GUI/Assets/quit.png");
    }
    private void InitializeComponent(){
        AvaloniaXamlLoader.Load(this);        
    }

    private void StartButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var newWindow = new SimulationWindow();
        
        newWindow.Show();

        this.Close();
    }
    private void QuitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

}