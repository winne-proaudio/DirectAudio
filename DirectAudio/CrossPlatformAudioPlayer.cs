using OpenTK.Audio.OpenAL;
using System;

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
        try
        {
            // Initialisiere OpenAL mit dem Standard-Audiogerät
            string defaultDeviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
            device = ALC.OpenDevice(defaultDeviceName);
            
            if (device == ALDevice.Null)
                throw new Exception("Konnte kein Audio-Gerät öffnen");

            // Erstelle Audio-Kontext
            context = ALC.CreateContext(device, new ALContextAttributes());
            if (context == ALContext.Null)
                throw new Exception("Konnte keinen Audio-Kontext erstellen");

            ALC.MakeContextCurrent(context);

            // Erstelle Source und Buffer
            buffer = AL.GenBuffer();
            source = AL.GenSource();
            
            AL.Source(source, ALSourcef.Gain, 1.0f);           // Lautstärke
            AL.Source(source, ALSourcef.Pitch, 1.0f);          // Tonhöhe
            AL.Source(source, ALSource3f.Position, 0, 0, 0);   // Position (Stereo-Mitte)
            
            initialized = true;
        }
        catch (Exception)
        {
            Dispose();
            throw;
        }
    }

    public void PlayRawAudio(float[] audioData)
    {
        if (!initialized)
            throw new InvalidOperationException("Audio nicht initialisiert");

        try
        {
            // Stoppe vorherige Wiedergabe
            AL.SourceStop(source);
            
            // Konvertiere zu 16-bit PCM
            short[] pcmData = new short[audioData.Length];
            for (int i = 0; i < audioData.Length; i++)
            {
                // Begrenze die Amplitude auf [-1.0, 1.0]
                float sample = Math.Clamp(audioData[i], -1.0f, 1.0f);
                pcmData[i] = (short)(sample * short.MaxValue);
            }

            byte[] byteData = new byte[pcmData.Length * sizeof(short)];
            Buffer.BlockCopy(pcmData, 0, byteData, 0, byteData.Length);

            // Lade Audio-Daten in den Buffer
            AL.BufferData(buffer, ALFormat.Stereo16, byteData, DEFAULT_SAMPLE_RATE);
            
            // Verknüpfe Buffer mit Source und spiele ab
            AL.Source(source, ALSourcei.Buffer, buffer);
            
            // Überprüfe auf Fehler
            ALError error = AL.GetError();
            if (error != ALError.NoError)
                throw new Exception($"OpenAL Fehler: {error}");

            AL.SourcePlay(source);
        }
        catch (Exception)
        {
            AL.SourceStop(source);
            throw;
        }
    }

    public void WaitForPlaybackComplete()
    {
        if (!initialized) return;

        while (true)
        {
            AL.GetSource(source, ALGetSourcei.SourceState, out int state);
            if (state != (int)ALSourceState.Playing)
                break;
            Thread.Sleep(10);
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
            if (context != ALContext.Null)
                ALC.DestroyContext(context);
            if (device != ALDevice.Null)
                ALC.CloseDevice(device);
            initialized = false;
        }
    }
}