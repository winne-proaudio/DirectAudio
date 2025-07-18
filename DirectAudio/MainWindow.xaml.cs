using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirectAudio;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void PlayToneButton_Click(object sender, RoutedEventArgs e)
    {
        // Erzeugt einen 1-Sekunden-Ton mit 440 Hz (Kammerton A)
        byte[] audioData = AudioPlayer.ToneGenerator.GenerateSineWave(
            frequency: 440,    // Frequenz in Hz
            amplitude: 0.5,    // Amplitude (0.0 - 1.0)
            duration: 1.0      // Dauer in Sekunden
        );

        // Ton abspielen
       // Task.Run(() => AudioPlayer.PlayRawAudio(audioData));
        Task.Run(() => AudioPlayer.PlayTone());
    }

}