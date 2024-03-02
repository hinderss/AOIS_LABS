namespace Logic
{
    public class Operator : Token
    {
        public Operator(string str) : base(str)
        {
            if (!Validate())
            {
                throw new Exception("Unsupported operation");
            }
        }

        public bool Operate(bool a, bool b)
        {
            switch (_str)
            {
                case "&":
                    return a && b;
                case "|":
                    return a || b;
                case "->":
                    return !a || b;
                case "~":
                    return a == b;
                default:
                    return false;
            }
        }
        public bool Operate(bool a)
        {
            switch (_str)
            {
                case "!":
                    return !a;
                default:
                    return false;
            }
        }

        private bool Validate()
        {
            return IsBinaryOperator() || IsUnaryOperator() || IsBracket();
        }

        public bool IsUnaryOperator()
        {
            return _str == "!";
        }

        public bool IsBinaryOperator()
        {
            return _str == "&" || _str == "|" || _str == "->" || _str == "~";
        }

        private bool IsBracket()
        {
            return IsOpenBracket() || IsCloseBracket();
        }

        public bool IsOpenBracket()
        {
            return _str == "(";
        }

        public bool IsCloseBracket()
        {
            return _str == ")";
        }

        public int Priority()
        {
            switch (_str)
            {
                case "!":
                    return 5;
                case "&":
                    return 4;
                case "|":
                    return 3;
                case "->":
                    return 2;
                case "~":
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
