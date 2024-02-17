using LAB1;

namespace LAB1
{
    [TestClass]
    public class SBinaryTest
    {
        [TestMethod]
        public void SBinaryConstrucror()
        {
            var a = new SBinary(15);
            var b = new SBinary(9);

            var c = a + b;

            Assert.AreEqual("011000", c.SignedMagnitude);
        }

        [TestMethod]
        public void Sum1()
        {
            var a = new SBinary(15);
            var b = new SBinary("01001");

            var c = a + b;

            Assert.AreEqual("011000", c.SignedMagnitude);
        }

        [TestMethod]
        public void Sum2()
        {
            var a = new SBinary(-15);
            var b = new SBinary(-9);

            var c = a + b;

            Assert.AreEqual("111000", c.SignedMagnitude);
        }

        [TestMethod]
        public void Sum3()
        {
            var a = new SBinary(-15);
            var b = new SBinary(9);

            var c = a + b;

            Assert.AreEqual("111001", c.OnesComplement);
        }

        [TestMethod]
        public void Sum4()
        {
            var a = new SBinary(15);
            var b = new SBinary(-9);

            var c = a + b;

            Assert.AreEqual("000110", c.TwosComplement);
        }

        [TestMethod]
        public void Subtraction()
        {
            var a = new SBinary(25);
            var b = new SBinary(158);

            var result = a - b;
            Console.WriteLine(result);

            Assert.AreEqual("-133", "-" + Binary.BinaryToDecimal(result.Module));

        }

        [TestMethod]
        public void Multiplication()
        {
            var a = new SBinary(5);
            var b = new SBinary(40);

            var result = a * b;
            Console.WriteLine(result);

            Assert.AreEqual(200, Binary.BinaryToDecimal(result.Module));
            Assert.IsFalse(result.Sign);
        }

        [TestMethod]
        public void Division()
        {
            var a = new SBinary(13);
            var b = new SBinary(4);

            var result = a / b;
            Console.WriteLine(result);

            Assert.AreEqual(3.25, result.GetSystemFloat);
            Assert.IsFalse(result.Sign);
        }
    }
}