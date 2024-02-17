namespace LAB1
{
    public class Float
    {
        public static int EXPONENT_BIAS = 127;
        public static int EXPONENT_SIZE = 8;

        public static int MANTISSA_SIZE = 23;

        public string _value;

        public bool Sign
        {
            get { return (_value[0] == '1') ? true : false; }
        }
        public Float(string value)
        {
            _value = value;
        }

        public Float(float number)
        {
            char sign = '0';

            if (number < 0)
                sign = '1';

            number = Math.Abs(number);
            int intNumber = (int)number; 
            float fractionalPart = number - intNumber; 

            string binaryIntPart = SBinary.DecimalToBinary(intNumber).Substring(1); 
            string binaryFractionalPart = ConvertFractionalToBinary(fractionalPart); 

            int exponent = binaryIntPart.Length - 1;
            int count = 1;
            if (intNumber == 0)
            {
                foreach (char c in binaryFractionalPart)
                    if (c == '0')
                        count++;
                    else
                        break;
                exponent = -1 * count;
            }

            string exponentBinary = SBinary.DecimalToBinary(exponent + EXPONENT_BIAS).Substring(1); 
            string mantissa = binaryIntPart.Substring(1) + binaryFractionalPart; 

            _value = sign + exponentBinary.PadLeft(EXPONENT_SIZE, '0') + mantissa.PadRight(MANTISSA_SIZE, '0'); 
        }


        public string Exponent
        {
            get { return _value.Substring(1, EXPONENT_SIZE); }
        }
        public string Mantissa
        {
            get { return _value.Substring(EXPONENT_SIZE + 1); }
        }
        public string MantissaTwosComplement
        {
            get
            {
                if (this.Sign)
                {
                    return Binary.TwosComplement(this.Mantissa);
                }
                else
                    return Mantissa;
            }
        }

        public static string TwosComplement(char sign, string binary)
        {
            if (sign == '1')
            {
                return Binary.TwosComplement(binary);
            }
            else
                return binary;
        }

        public static (int, string) Preparer(Float f)
        {
            int exponent = Binary.BinaryToDecimal(f.Exponent); 
            string mantissa = "1" + f.MantissaTwosComplement;

            return (exponent, mantissa);
        }

        public static char GetSign(Float binary1, Float binary2)
        {
            (int exponent1, string mantissa1) = Float.Preparer(binary1);
            (int exponent2, string mantissa2) = Float.Preparer(binary2);
            var sign1 = binary1.Sign;
            var sign2 = binary2.Sign;

            if (!(sign1 ^ sign2))
                return sign1 ? '1' : '0';

            if (exponent1 > exponent2)
                return sign1 ? '1' : '0';
            else if (exponent1 < exponent2)
                return sign2 ? '1' : '0';

            int comparisonResult = Binary.CompareBinaryNumbers(mantissa1, mantissa2);
            if (comparisonResult > 0)
                return sign1 ? '1' : '0';
            else if (comparisonResult < 0)
                return sign2 ? '1' : '0';

            return '0';
        }

        public static Float operator +(Float binary1, Float binary2)
        {
            (int exponent1, string mantissa1) = Float.Preparer(binary1);
            (int exponent2, string mantissa2) = Float.Preparer(binary2);
            var sign = Float.GetSign(binary1, binary2);

            if (exponent1 < exponent2)
            {
                (exponent1, exponent2) = (exponent2, exponent1);
                (mantissa1, mantissa2) = (mantissa2, mantissa1);
            }

            mantissa1 = mantissa1.PadRight((exponent1 - exponent2) + mantissa1.Length, '0');
            mantissa2 = mantissa2.PadLeft((exponent1 - exponent2) + mantissa2.Length, '0'); 

            string result = Binary.Sum(mantissa1, mantissa2); 

            if (result.Length > mantissa1.Length)
                exponent1++;

            result = Float.TwosComplement(sign, result);

            var binaryExponent = Binary.DecimalToBinary(exponent1).PadLeft(EXPONENT_SIZE, '0');
            var roundedMantissa = result.Substring(1, MANTISSA_SIZE - 1) + '0';

            return new Float(sign + binaryExponent + roundedMantissa);
        }
        private static string ConvertFractionalToBinary(float number)
        {
            string binary = "";

            while (number != 0 && binary.Length < MANTISSA_SIZE)
            {
                number *= 2;
                if (number >= 1)
                {
                    binary += "1";
                    number -= 1;
                }
                else
                {
                    binary += "0";
                }
            }

            return binary;
        }

        public float FloatToDecimal
        {
            get
            {
                var sign = Sign ? -1f : 1f;
                float exponent = Binary.BinaryToDecimal(Exponent) - EXPONENT_BIAS;
                float mantissa = 1;

                int l = Mantissa.Length;
                for (int i = 0; i < l; i++)
                {
                    mantissa += (Mantissa[i] - '0') * (float)Math.Pow(2, -1 * (i + 1));
                }

                return sign * mantissa * (float)Math.Pow(2, exponent);
            }
        }

        public override string ToString()
        {
            return $"Число: {FloatToDecimal}\nIEEE 754 32 bit: {_value}";
        }
    }
}
