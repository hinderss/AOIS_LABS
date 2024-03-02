namespace Logic.Tests
{
    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void VariableConstructorValidNameValueFalse()
        {
            string variableName = "x";
            var variable = new Variable(variableName);
            Assert.IsFalse(variable.Value);
        }

        [TestMethod]
        public void VariableConstructorValidNameWithValueValueMatches()
        {
            string variableName = "y";
            bool value = true;
            var variable = new Variable(variableName, value);
            Assert.AreEqual(value, variable.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid variable name")]
        public void VariableConstructorInvalidNameExceptionThrown()
        {
            string variableName = "&invalid";
            var variable = new Variable(variableName);
        }
    }
}

