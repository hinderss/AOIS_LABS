namespace LAB1
{
    public class Binary
    {
        private string _value;

        public Binary(string value)
        {
            _value = value;
        }

        public static int BinaryToDecimal(string binary)
        {
            int l = binary.Length;
            int sum = 0;
            for (int i = 0; i < l; i++)
            {
                sum += (binary[i] - '0') * (int)Math.Pow(2, (l - 1 - i));
            }

            return sum;
        }
        public static float BinaryToFractional(string binary)
        {
            float fractional = 0;
            int l = binary.Length;
            for (int i = 0; i < l; i++)
            {
                fractional += (binary[i] - '0') * (float)Math.Pow(2, -1 * (i + 1));
            }

            return fractional;
        }

        static public string DecimalToBinary(int decimalNumber)
        {
            string result = string.Empty;

            if (decimalNumber == 0)
                return "0";

            int decimalABS = Math.Abs(decimalNumber);
            int remainder;
            while (decimalABS > 0)
            {
                remainder = decimalABS % 2;
                decimalABS /= 2;
                result = remainder.ToString() + result;
            }

            return result;
        }

        public int Bit
        {
            get { return _value.Length; }
        }

        public string Value
        {
            get { return _value; }
        }

        public static string InvertValue(string value)
        {
            string result = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                result += (value[i] == '0') ? '1' : '0';
            }

            return result;
        }

        public static string TwosComplement(string value)
        {
            string one = "1";
            one = one.PadLeft(value.Length, '0');
            value = Binary.InvertValue(value);
            return Binary.Sum(value, one);
        }

        public static Binary operator +(Binary binary1, Binary binary2)
        {
            return new Binary(binary1.Value + binary2.Value);
        }

        public static string Sum(string first, string second)
        {
            string result = string.Empty;

            int carry = 0;

            for (int i = first.Length - 1; i >= 0; i--)
            {
                int sum = (first[i] - '0') + (second[i] - '0') + carry;
                result = (sum % 2) + result;
                carry = sum / 2;
            }

            if (carry == 1)
            {
                result = "1" + result;
            }
            return result;
        }

        public static int CompareBinaryNumbers(string binary1, string binary2)
        {
            int maxLength = Math.Max(binary1.Length, binary2.Length);
            binary1 = binary1.PadLeft(maxLength, '0');
            binary2 = binary2.PadLeft(maxLength, '0');

            for (int i = 0; i < maxLength; i++)
            {
                if (binary1[i] != binary2[i])
                {
                    return binary1[i] == '1' ? 1 : -1;
                }
            }

            return 0;
        }
    }
}
