namespace LAB1
{
    public class Fixed
    {
        private bool _sign;
        private string _integerPart;
        private string _fractionalPart;

        public Fixed(bool sign, string integer, string fractional)
        {
            _sign = sign;
            _integerPart = integer.TrimStart('0'); ;
            _fractionalPart = fractional;
        }

        public bool Sign
        {
            get { return _sign; }
        }

        public float GetSystemFloat
        {
            get 
            {
                int s = Sign ? -1 : 1;
                float i = (int)Binary.BinaryToDecimal(_integerPart);
                float f = Binary.BinaryToFractional(_fractionalPart);
                return s * i + s * f;
            }
        }

        public override string ToString()
        {
            return
            "Число:" + 
            GetSystemFloat.ToString() +
            "\n" +
            "Двоичное представление: [" + ((this.Sign) ? '1' : '0') + " " +
            _integerPart + '.' + _fractionalPart + "]";
        }
    }
}
