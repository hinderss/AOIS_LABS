using LAB1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTests
{
    [TestClass]
    public class FloatTest
    {
        [TestMethod]
        public void Constructor()
        {
            var a = new Float(1.4f);

            Assert.AreEqual("00111111101100110011001100110011", "0" + a.Exponent + a.Mantissa);
            Assert.IsFalse(a.Sign);
            
        }

        [TestMethod]
        public void Addition1()
        {
            var a = new Float(1.4f);

            var b = new Float(256.589f);

            var c = a + b;

            float accuracy = 0.0001f;

            Console.WriteLine(c.FloatToDecimal);
            Console.WriteLine((1.4f + 256.589f));

            Assert.IsTrue((c.FloatToDecimal - (1.4f + 256.589f) < accuracy));
        }

        [TestMethod]
        public void Addition2()
        {
            var a = new Float(0.45554f);

            var b = new Float(-89.565f);

            var c = a + b;

            float accuracy = 0.001f;

            Console.WriteLine(c.FloatToDecimal);
            Console.WriteLine((0.45554f - 89.565f));

            Assert.IsTrue((c.FloatToDecimal - (0.45554f - 89.565f) < accuracy));
        }

    }
}
