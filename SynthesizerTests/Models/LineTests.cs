using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synthesizer.Configuration;
using System;

namespace Synthesizer.Models.Tests
{
    [TestClass()]
    public class LineTests
    {
        [TestMethod()]
        public void ShouldGenerateSoundOnlyForActiveNotes()
        {
            var sample = Sound.SampleRateHz;
            var line = new Line(10, 440, 1000, 1000);
            line.SetStateAndType(0, true, WaveType.Sin);
            line.SetStateAndType(2, true, WaveType.Sin);

            var data = line.GenerateSoundData();

            Assert.AreNotEqual(0f, GetArraySampleSum(data, 0));
            Assert.AreEqual(0f, GetArraySampleSum(data, 1));
            Assert.AreNotEqual(0f, GetArraySampleSum(data, 2));
        }

        private float GetArraySampleSum(float[] array, int noteIndex)
        {
            int offset = (int)Math.Floor(Sound.SampleRateHz * noteIndex + Sound.SampleRateHz / 2f);
            float sum = 0f;
            for (int i = 0; i < 10; i++)
                sum += array[offset + i];

            return sum;
        }
    }
}