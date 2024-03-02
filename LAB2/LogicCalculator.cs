using System.Data;

namespace Logic
{
    public class LogicCalculator
    {
        private string _expression;
        private List<Token> _RPN;
        private List<Variable> _variables;
        public LogicCalculator(string expression1)
        {
            _expression = expression1;
            var tokens = Tokenize(_expression);
            _RPN = ConvertToRPN(tokens);
            _variables = _RPN.OfType<Variable>().Distinct().ToList();
        }

        private static List<Token> Tokenize(string expression)
        {
            var list = new List<Token>();
            var variableDict = new Dictionary<string, Token>();

            string buffer = "";
            foreach (char c in expression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    buffer += c;
                    continue;
                }
                if (buffer.Length != 0)
                {
                    if (!variableDict.ContainsKey(buffer))
                        variableDict.Add(buffer, new Variable(buffer));
                    list.Add(variableDict[buffer]);
                    buffer = "";
                }
                if (char.IsWhiteSpace(c))
                    continue;
                if (c == '-')
                {
                    list.Add(new Operator("->"));
                    continue;
                }
                if (c == '>')
                    continue;
                list.Add(new Operator(c.ToString()));
            }
            if (buffer.Length != 0)
            {
                if (!variableDict.ContainsKey(buffer))
                    variableDict.Add(buffer, new Variable(buffer));
                list.Add(variableDict[buffer]);
            }
            return list;
        }

        private List<Token> ConvertToRPN(List<Token> tokens)
        {
            Stack<Operator> operators = new Stack<Operator>();
            List<Token> rpnExpression = new List<Token>();

            foreach (var token in tokens)
            {
                if (token is Variable)
                {
                    rpnExpression.Add(token);
                    continue;
                }
                Operator op = (Operator)token;
                if (op.IsUnaryOperator())
                {
                    operators.Push(op);
                }
                else if (op.IsBinaryOperator())
                {
                    while (operators.Count > 0 && operators.Peek().Priority() >= op.Priority())
                    {
                        rpnExpression.Add(operators.Pop());
                    }
                    operators.Push((Operator)token);
                }
                else if (((Operator)token).IsOpenBracket())
                {
                    operators.Push((Operator)token);
                }
                else if (op.IsCloseBracket())
                {
                    while (!operators.Peek().IsOpenBracket())
                    {
                        rpnExpression.Add(operators.Pop());
                    }
                    operators.Pop();
                }
            }

            while (operators.Count > 0)
            {
                rpnExpression.Add(operators.Pop());
            }

            return rpnExpression;

        }

        private bool EvaluateRPN()
        {
            Stack<bool> stack = new Stack<bool>();

            foreach (var token in _RPN)
            {
                if (token is Variable)
                {
                    stack.Push(((Variable)token).Value);
                }
                else if (((Operator)token).IsUnaryOperator())
                {
                    var operand = stack.Pop();

                    stack.Push(((Operator)token).Operate(operand));
                }
                else if (((Operator)token).IsBinaryOperator())
                {
                    var operand2 = stack.Pop();
                    var operand1 = stack.Pop();

                    stack.Push(((Operator)token).Operate(operand1, operand2));
                }
            }

            return stack.Pop();
        }

        private List<(string, bool)> EvaluateTable()
        {
            var result = new List<(string, bool)>();
            var size = _variables.Count;
            for (int i = 0; i < Math.Pow(2, size); i++)
            {
                string binaryI = Convert.ToString(i, 2).PadLeft(size, '0');

                _variables = _variables.Zip(binaryI, (variable, character) =>
                {
                    variable.Value = character == '1' ? true : false;
                    return variable;
                }).ToList();
                result.Add((binaryI, EvaluateRPN()));
            }
            return result;
        }
        
        private string BuildHeader()
        {
            List<string> headerList = _variables.Select(v => v.ToString()).ToList();
            headerList.Add(_expression);

            var header = string.Join("\t", headerList);
            var tabs = header.Count(c => c == '\t');
            return header + "\n" + "".PadLeft(header.Length + tabs * 7 - tabs, '-');
        }

        private string BuildRows()
        {
            var tableList = EvaluateTable();
            string result = "\n";

            foreach (var v in tableList)
            {
                var bools = v.Item1.Select(c => c.ToString()).ToList();
                bools.Add((v.Item2 ? "1" : "0").PadLeft(_expression.Length / 2 + 1));
                result += string.Join("\t", bools) + '\n';
            }
            return result;

        }

        public string BuildTable()
        {
            return BuildHeader() + BuildRows();
        }

        public string BuildPDNFString()
        {
            string result = "";
            var tableList = EvaluateTable();
            foreach (var i in tableList)
            {
                if (i.Item2)
                {
                    result += "(";
                    for (int j = 0; j<i.Item1.Length; j++)
                    {
                        result += (i.Item1[j] == '0') ? "!" : "";
                        result += _variables[j].ToString() + "&";
                    }
                    result = result.TrimEnd('&');
                    result += ")|";
                }
            }
            return result.TrimEnd('|');
        }

        public string BuildPCNFString()
        {
            string result = "";
            var tableList = EvaluateTable();
            foreach (var i in tableList)
            {
                if (!i.Item2)
                {
                    result += "(";
                    for (int j = 0; j < i.Item1.Length; j++)
                    {
                        result += (i.Item1[j] == '0') ? "" : "!";
                        result += _variables[j].ToString() + "|";
                    }
                    result = result.TrimEnd('|');
                    result += ")&";
                }
            }
            return result.TrimEnd('&');
        }

        public List<int> NumericFormPDNF
        {
            get
            {
                List<int> result = new List<int>();
                var tableList = EvaluateTable();
                for (int i=0; i < tableList.Count; i++)
                {
                    if (tableList[i].Item2)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public List<int> NumericFormPCNF
        {
            get
            {
                List<int> result = new List<int>();
                var tableList = EvaluateTable();
                for (int i = 0; i < tableList.Count; i++)
                {
                    if (!tableList[i].Item2)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public string BuildNumFormPDNF()
        {
            return $"({string.Join(", ", NumericFormPDNF)}) |";
        }

        public string BuildNumFormPCNF()
        {
            return $"({string.Join(", ", NumericFormPCNF)}) &";
        }

        public string IndexForm
        {
            get
            {
                string result = "";
                var tableList = EvaluateTable();
                foreach (var i in tableList)
                {
                    result += (i.Item2) ? "1" : "0";
                }
                return result;
            }
        }
    }
}