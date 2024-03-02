namespace Logic.Tests
{
    [TestClass]
    public class OperatorTests
    {
        [TestMethod]
        public void OperatorConstructBinaryOperatorValidOperatorSucceeds()
        {
            string operatorSymbol = "&";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.IsBinaryOperator());
        }

        [TestMethod]
        public void OperatorConstructUnaryOperatorValidOperatorSucceeds()
        {
            string operatorSymbol = "!";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.IsUnaryOperator());
        }

        [TestMethod]
        public void OperatorConstructOpenBracketValidBracketSucceeds()
        {
            string bracketSymbol = "(";
            var op = new Operator(bracketSymbol);
            Assert.IsTrue(op.IsOpenBracket());
        }

        [TestMethod]
        public void OperatorConstructCloseBracketValidBracketSucceeds()
        {
            string bracketSymbol = ")";
            var op = new Operator(bracketSymbol);
            Assert.IsTrue(op.IsCloseBracket());
        }

        [TestMethod]
        public void OperatorOperateBinaryOperatorCorrectResult()
        {
            string operatorSymbol = "&";
            bool operand1 = true;
            bool operand2 = false;
            var op = new Operator(operatorSymbol);
            var result = op.Operate(operand1, operand2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorOperateUnaryOperatorCorrectResult()
        {
            string operatorSymbol = "!";
            bool operand = true;
            var op = new Operator(operatorSymbol);
            var result = op.Operate(operand);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorPriorityValidPriorityReturned()
        {
            string operatorSymbol = "|";
            int expectedPriority = 3;
            var op = new Operator(operatorSymbol);
            var priority = op.Priority();
            Assert.AreEqual(expectedPriority, priority);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Unsupported operation")]
        public void OperatorConstructInvalidOperatorExceptionThrown()
        {
            string invalidOperatorSymbol = "$";
            var op = new Operator(invalidOperatorSymbol);
        }

        [TestMethod]
        public void OperatorOperateAndOperatorReturnsTrueOnlyForBothTrueOperands()
        {
            string operatorSymbol = "&";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.Operate(true, true));
            Assert.IsFalse(op.Operate(true, false));
            Assert.IsFalse(op.Operate(false, true));
            Assert.IsFalse(op.Operate(false, false));
        }

        [TestMethod]
        public void OperatorOperateOrOperatorReturnsTrueForAtLeastOneTrueOperand()
        {
            string operatorSymbol = "|";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.Operate(true, true));
            Assert.IsTrue(op.Operate(true, false));
            Assert.IsTrue(op.Operate(false, true));
            Assert.IsFalse(op.Operate(false, false));
        }

        [TestMethod]
        public void OperatorOperateImplicationOperatorReturnsFalseOnlyForTrueAntecedentAndFalseConsequent()
        {
            string operatorSymbol = "->";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.Operate(false, true));
            Assert.IsTrue(op.Operate(false, false));
            Assert.IsTrue(op.Operate(true, true));
            Assert.IsFalse(op.Operate(true, false));
        }

        [TestMethod]
        public void OperatorOperateEqualityOperatorReturnsTrueOnlyForEqualOperands()
        {
            string operatorSymbol = "~";
            var op = new Operator(operatorSymbol);
            Assert.IsTrue(op.Operate(true, true));
            Assert.IsTrue(op.Operate(false, false));
            Assert.IsFalse(op.Operate(true, false));
            Assert.IsFalse(op.Operate(false, true));
        }

        [TestMethod]
        public void OperatorPriorityNotOperatorReturns5()
        {
            string operatorSymbol = "!";
            var op = new Operator(operatorSymbol);
            Assert.AreEqual(5, op.Priority());
        }

        [TestMethod]
        public void OperatorPriorityAndOperatorReturns4()
        {
            string operatorSymbol = "&";
            var op = new Operator(operatorSymbol);
            Assert.AreEqual(4, op.Priority());
        }

        [TestMethod]
        public void OperatorPriorityOrOperatorReturns3()
        {
            string operatorSymbol = "|";
            var op = new Operator(operatorSymbol);
            Assert.AreEqual(3, op.Priority());
        }

        [TestMethod]
        public void OperatorPriorityImplicationOperatorReturns2()
        {
            string operatorSymbol = "->";
            var op = new Operator(operatorSymbol);
            Assert.AreEqual(2, op.Priority());
        }

        [TestMethod]
        public void OperatorPriorityEqualityOperatorReturns1()
        {
            string operatorSymbol = "~";
            var op = new Operator(operatorSymbol);
            Assert.AreEqual(1, op.Priority());
        }
    }
}
