using LAB1;

namespace LAB1
{
    [TestClass]
    public class BinaryTest
    {
        [TestMethod]
        public void BinaryConstrucror()
        {
            var a = new Binary("10101");

            Assert.AreEqual("10101", a.Value);
            Assert.AreEqual(21, Binary.BinaryToDecimal(a.Value));
        }

    }
}
