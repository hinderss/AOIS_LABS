namespace Logic
{
    public class Token
    {
        protected string _str;

        public Token(string str)
        {
            _str = str;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Token p)
                return false;
            return p._str.Equals(_str);
        }

        public override int GetHashCode()
        {
            return _str.GetHashCode();
        }

        public override string ToString()
        {
            return _str;
        }
    }
}
