using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Synthesizer.Models.Tests
{
    [TestClass()]
    public class TrackTests
    {
        [TestMethod()]
        public void LoadShouldRestoreSavedData()
        {
            var original = new Track(10);
            original.SetTempo(32);
            original.SetStateAndType(0, 1, true, WaveType.Square);

            var restored = new Track(20);
            restored.Load(original.Save());

            Assert.AreEqual(original.GetSize(), restored.GetSize());
            Assert.AreEqual(original.GetTempo(), restored.GetTempo());
            Assert.AreEqual(restored.GetState(0, 0), false);
            Assert.AreEqual(restored.GetState(0, 1), true);
            Assert.AreEqual(restored.GetType(0, 0), null);
            Assert.AreEqual(restored.GetType(0, 1), WaveType.Square);
        }
    }
}