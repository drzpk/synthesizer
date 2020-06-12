using NAudio.Wave.SampleProviders;
using Synthesizer.Configuration;
using System;
using System.Collections.Generic;

namespace Synthesizer.Models
{
    /// <summary>
    /// Umożliwia generowanie punktów do wykresu przedstawiającego przebieg fali dźwiękowej
    /// </summary>
    class WaveVisualizer
    {
        public static List<Tuple<float, float>> Plot(WaveType type)
        {
            var list = new List<Tuple<float, float>>();
            int count = Sound.VisualizationPointCount;
            var provider = type.GetSampleProvider(count, 1);
            var buffer = new float[count];
            provider.Read(buffer, 0, count);

            for (int i = 0; i < count; i++)
            {
                list.Add(new Tuple<float, float>(i / (float)count, buffer[i]));
            }

            return list;
        }
    }
}
