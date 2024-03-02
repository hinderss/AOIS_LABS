namespace Logic.Tests
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void EqualsReturnsTrueWhenTwoTokensHaveSameString()
        {
            string str = "test";
            Token token1 = new Token(str);
            Token token2 = new Token(str);

            bool result = token1.Equals(token2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsReturnsFalseWhenTwoTokensHaveDifferentStrings()
        {
            Token token1 = new Token("test1");
            Token token2 = new Token("test2");

            bool result = token1.Equals(token2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCodeReturnsSameValueForEqualTokens()
        {
            string str = "test";
            Token token1 = new Token(str);
            Token token2 = new Token(str);

            int hashCode1 = token1.GetHashCode();
            int hashCode2 = token2.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void ToStringReturnsTokenString()
        {
            string str = "test";
            Token token = new Token(str);

            string result = token.ToString();

            Assert.AreEqual(str, result);
        }
    }
}
