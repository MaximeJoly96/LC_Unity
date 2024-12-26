using NUnit.Framework;
using Utils;

namespace Testing.Utils
{
    public class TimeConverterTests : TestFoundation
    {
        [Test]
        public void FormatTimeFromSecondsTest()
        {
            float time = 59.0f;

            Assert.AreEqual("00:00:59", TimeConverter.FormatTimeFromSeconds(time));

            time = 128.5f;

            Assert.AreEqual("00:02:08", TimeConverter.FormatTimeFromSeconds(time));

            time = 3956.3f;

            Assert.AreEqual("01:05:56", TimeConverter.FormatTimeFromSeconds(time));
        }
    }
}
