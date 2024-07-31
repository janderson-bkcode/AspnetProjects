using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace FunkBeat
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Gerando batida de funk...");

            // Cria um objeto WaveOut para reproduzir o som
            WaveOutEvent waveOut = new WaveOutEvent();

            // Cria um objeto MixingSampleProvider para misturar vários sons
            MixingSampleProvider mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));

            // Adiciona vários sons ao objeto MixingSampleProvider
            mixer.AddMixerInput(CreateSampleProvider(100, 500, 0.5));
            mixer.AddMixerInput(CreateSampleProvider(100, 250, 0.2));
            mixer.AddMixerInput(CreateSampleProvider(100, 125, 0.1));

            // Atribui o objeto MixingSampleProvider ao objeto WaveOut
            waveOut.Init(mixer);

            // Inicia a reprodução
            waveOut.Play();

            Console.WriteLine("Pressione qualquer tecla para parar.");
            Console.ReadKey();

            // Para a reprodução
            waveOut.Stop();

            Console.WriteLine("Batida de funk encerrada.");
        }

        private static ISampleProvider CreateSampleProvider(double frequency, int sampleRate, double volume)
        {
            // Cria um objeto SineWaveProvider para gerar o som
            SineWaveProvider sineWaveProvider = new SineWaveProvider(sampleRate, frequency);
            sineWaveProvider.Volume = (float)volume;

            return sineWaveProvider;
        }

       
        }
    }
}