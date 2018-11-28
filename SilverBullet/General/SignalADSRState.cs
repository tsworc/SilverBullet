using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class SignalADSRState
    {
        public SignalGeneratorType signalT = SignalGeneratorType.Sin;
        public float attackP = 0.1f;
        public float freq = 440;
        public float freqEnd = 0;
        public float sweepL = 3.0f;
        public float gain = 0.5f;
    }
}
