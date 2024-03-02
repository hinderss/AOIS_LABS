namespace Logic
{
    public class Variable : Token
    {
        public bool Value { get; set; }

        public Variable(string str) : base(str)
        {
            if (!IsVariable())
            {
                throw new Exception("Invalid variable name");
            }
            Value = false;
        }
        public Variable(string str, bool value) : base(str)
        {
            if (!IsVariable())
            {
                throw new Exception("Invalid variable name");
            }
            Value = value;
        }

        private bool IsVariable()
        {
            return _str.All(char.IsLetterOrDigit);
        }
    }
}
