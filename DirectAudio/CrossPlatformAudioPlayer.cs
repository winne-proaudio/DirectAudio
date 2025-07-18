using OpenTK.Audio.OpenAL;

public class CrossPlatformAudioPlayer : IDisposable
{
    private readonly ALDevice device;
    private readonly ALContext context;
    private readonly int source;
    private readonly int buffer;
    private bool initialized;
    private const int DEFAULT_SAMPLE_RATE = 44100;

    public CrossPlatformAudioPlayer()
    {
        device = ALC.OpenDevice(null);
        if (device == ALDevice.Null)
            throw new Exception("Konnte kein Audio-Gerät öffnen");

        context = ALC.CreateContext(device, (int[])null);
        ALC.MakeContextCurrent(context);

        buffer = AL.GenBuffer();
        source = AL.GenSource();

        initialized = true;
    }

    public void PlayRawAudio(float[] audioData)
    {
        if (!initialized)
            throw new InvalidOperationException("Audio nicht initialisiert");

        // Stoppe aktuelle Wiedergabe
        AL.SourceStop(source);
        
        // Warte bis die vorherige Wiedergabe beendet ist
        AL.GetSource(source, ALGetSourcei.SourceState, out int state);
        while (state == (int)ALSourceState.Playing)
        {
            Thread.Sleep(10);
            AL.GetSource(source, ALGetSourcei.SourceState, out state);
        }

        // Entferne den alten Buffer
        AL.Source(source, ALSourcei.Buffer, 0);

        // Konvertiere zu 16-bit PCM
        var byteData = new byte[audioData.Length * 2];
        for (int i = 0; i < audioData.Length; i++)
        {
            short value = (short)(audioData[i] * short.MaxValue);
            byte[] bytes = BitConverter.GetBytes(value);
            byteData[i * 2] = bytes[0];
            byteData[i * 2 + 1] = bytes[1];
        }

        // Lade neue Audiodaten
        AL.BufferData(buffer, ALFormat.Stereo16, byteData, DEFAULT_SAMPLE_RATE);
        
        // Verknüpfe Buffer mit Source
        AL.Source(source, ALSourcei.Buffer, buffer);
        
        // Spiele ab
        AL.SourcePlay(source);
    }

    public void WaitForPlaybackComplete()
    {
        if (!initialized) return;

        AL.GetSource(source, ALGetSourcei.SourceState, out int state);
        while (state == (int)ALSourceState.Playing)
        {
            Thread.Sleep(10);
            AL.GetSource(source, ALGetSourcei.SourceState, out state);
        }
    }

    public void Dispose()
    {
        if (initialized)
        {
            AL.SourceStop(source);
            AL.DeleteSource(source);
            AL.DeleteBuffer(buffer);
            ALC.MakeContextCurrent(ALContext.Null);
            ALC.DestroyContext(context);
            ALC.CloseDevice(device);
            initialized = false;
        }
    }
}