using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class SoundSystem
    {
        WaveOut waveOut;
        MixingSampleProvider mixer;
        VolumeSampleProvider mixerVolume;
        public bool enableSound = true;

        public void ToggleSound(bool enabled)
        {
            enableSound = enabled;
            mixerVolume.Volume = enableSound ? 1 : 0;
        }

        // add mixer input
        public void AddMixerInput(ISampleProvider sampler, string closedCaption, float closedCaptionDuration)
        {
            mixer.AddMixerInput(sampler);
            float targetTime = StateMachine.totalGameTime + 3;
#if ADD_CLOSED_CAPTIONS
            if (closedCaptions.ContainsKey(closedCaption) == false)
                closedCaptions.Add(closedCaption, StateMachine.totalGameTime + 3);
            else
                closedCaptions[closedCaption] = targetTime;
#endif
        }

        public void Cleanup()
        {
            //unload audio, dispose audio, dispose sound, unload sound
            waveOut.Dispose();
        }

        public void Initialize()
        {
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 1));
                mixer.ReadFully = true;
                mixerVolume = new VolumeSampleProvider(mixer);
            mixerVolume.Volume = enableSound? 1 : 0;
                waveOut = new WaveOut();
            waveOut.DesiredLatency = 150;
                waveOut.NumberOfBuffers = 2;
                waveOut.Init(mixerVolume);
                waveOut.Play();
        }
    }
}
