using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Synthesizer.Configuration;
using System.Collections.Generic;
using System.Drawing;

namespace Synthesizer.Models
{
    class WaveType
    {
        public static readonly WaveType Sin = new WaveType(Keyboard.SineColor, "Sinus", SignalGeneratorType.Sin);
        public static readonly WaveType Square = new WaveType(Keyboard.SquareColor, "Kwadrat", SignalGeneratorType.Square);
        public static readonly WaveType Triangle = new WaveType(Keyboard.TriangleColor, "Trójkąt", SignalGeneratorType.Triangle);
        public static readonly WaveType SawTooth = new WaveType(Keyboard.SawToothColor, "Saw Tooth", SignalGeneratorType.SawTooth);

        public static readonly List<WaveType> AllTypes = new List<WaveType>(new[] { Sin, Square, Triangle, SawTooth });

        public readonly Color Color;
        public readonly string Name;
        public readonly SignalGeneratorType Type;

        private WaveType(Color color, string name, SignalGeneratorType type)
        {
            Color = color;
            Name = name;
            Type = type;
        }

        public ISampleProvider GetSampleProvider(int sampleRate, double frequency)
        {
            return new SignalGenerator(sampleRate, 1)
            {
                Type = Type,
                Frequency = frequency
            };
        }
    }
}
