using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DirectAudio;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    private float frequency;
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
        Task.Run(() => AudioPlayer.PlayTone(frequency));
    }
    private void FrequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is Slider slider)
        {
            frequency = (float)slider.Value;
        }
    }

    private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
    {
        Task.Run(() => AudioPlayer.PlayTone(frequency));
    }



}