namespace Logic.Tests
{
    [TestClass]
    public class LogicCalculator
    {
        [TestMethod]
        public void NumericFormPCNFTest()
        {
            string expression = "!x|(y&!z|x)&z";

            var x = new Logic.LogicCalculator(expression);
            var result = x.NumericFormPCNF;
            CollectionAssert.AreEqual(new List<int> { 4, 6 }, result);
        }

        [TestMethod]
        public void NumericFormPDNFTest()
        {
            string expression = "!x|(y&!z|x)&z";

            var x = new Logic.LogicCalculator(expression);
            var result = x.NumericFormPDNF;
            CollectionAssert.AreEqual(new List<int> { 0, 1, 2, 3, 5, 7 }, result);
        }

        [TestMethod]
        public void IndexFormTest()
        {
            string expression = "!x|(y&!z|x)&z";
            var x = new Logic.LogicCalculator(expression);

            var result = x.IndexForm;
            var expected = "11110101";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildPDNFTest()
        {
            string expression = "!x|(y&!z|x)&z";
            var x = new Logic.LogicCalculator(expression);

            var result = x.BuildPDNFString();
            var expected = "(!x&!y&!z)|(!x&!y&z)|(!x&y&!z)|(!x&y&z)|(x&!y&z)|(x&y&z)";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildPCNFTest()
        {
            string expression = "!x|(y&!z|x)&z";
            var x = new Logic.LogicCalculator(expression);

            var result = x.BuildPCNFString();
            var expected = "(!x|y|z)&(!x|!y|z)";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildNumFormPCNFTest()
        {
            string expression = "!x|(y&!z|x)&z";
            var x = new Logic.LogicCalculator(expression);

            var result = x.BuildNumFormPCNF();
            var expected = "(4, 6) &";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildNumFormPDNFTest()
        {
            string expression = "!x|(y&!z|x)&z";
            var x = new Logic.LogicCalculator(expression);

            var result = x.BuildNumFormPDNF();
            var expected = "(0, 1, 2, 3, 5, 7) |";
            Assert.AreEqual(expected, result);
        }
    }
}