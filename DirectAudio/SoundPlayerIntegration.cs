using System.Media;
using System.Windows;
using Window = Avalonia.Controls.Window;

public partial class MainWindow : Window
{
    private SoundPlayer player = new SoundPlayer();

    private void PlaySound()
    {
        // Pfad zur .wav Datei
        string wavPath = "pfad/zu/ihrer/datei.wav";
        player.SoundLocation = wavPath;
        player.Play(); // asynchrones Abspielen
        // oder
        // player.PlaySync(); // synchrones Abspielen
    }
    
    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        PlaySound();
    }
    
}
