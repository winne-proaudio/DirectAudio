using System.IO;

    public static class AudioPlayer
    {
        public static void PlayTone(float freq)
        {
            using (var player = new CrossPlatformAudioPlayer())
            {
                // Generiere einen 1-Sekunden 440Hz Ton
                float[] audioData = GenerateTone(freq, 0.5f, 44100);
                player.PlayRawAudio(audioData);
                player.WaitForPlaybackComplete();
            }
        }

        private static float[] GenerateTone(float frequency, float duration, int sampleRate)
        {
            int samples = (int)(sampleRate * duration);
            float[] buffer = new float[samples * 2]; // Stereo
            float amplitude = 0.25f;
            float angular_frequency = 2.0f * MathF.PI * frequency / sampleRate;

            for (int i = 0; i < samples; i++)
            {
                float sample = amplitude * MathF.Sin(angular_frequency * i);
                buffer[i * 2] = sample;     // Left channel
                buffer[i * 2 + 1] = sample; // Right channel
            }

            return buffer;
        }
        public static void PlayRawAudio(byte[] audioData)
        {
            //CrossPlatformAudioPlayer.PlayRawAudio(audioData);
        }
    
        public class ToneGenerator
        {
            public static byte[] GenerateSineWave(double frequency, double amplitude, double duration, int sampleRate = 44100)
            {
                int numSamples = (int)(duration * sampleRate);
                byte[] buffer = new byte[numSamples * 2]; // 16-bit = 2 bytes pro Sample
                double angleStep = (Math.PI * 2 * frequency) / sampleRate;

                for (int i = 0; i < numSamples; i++)
                {
                    short sample = (short)(amplitude * Math.Sin(angleStep * i) * short.MaxValue);
                    byte[] sampleBytes = BitConverter.GetBytes(sample);
                    buffer[i * 2] = sampleBytes[0];     // Low byte
                    buffer[i * 2 + 1] = sampleBytes[1]; // High byte
                }

                return buffer;
            }
        }
    }
    
 


