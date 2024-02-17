namespace LAB1
{
    public class SBinary
    {
        private string _value;

        public SBinary(int n)
        {
            _value = Binary.DecimalToBinary(n);

            if (n < 0)
            {
                _value = "1" + _value;
            }
            else
                _value = "0" + _value;
        }

        public SBinary(string value)
        {
            _value = value;
        }

        public SBinary(Binary binary)
        {
            _value = binary.Value;

        }

        static public string DecimalToBinary(int decimalNumber)
        {
            string result = string.Empty;

            if (decimalNumber == 0)
                return "00";

            int decimalABS = Math.Abs(decimalNumber);
            int remainder;
            while (decimalABS > 0)
            {
                remainder = decimalABS % 2;
                decimalABS /= 2;
                result = remainder.ToString() + result;
            }

            if (decimalNumber < 0)
                result = "1" + result;
            else
                result = "0" + result;

            return result;
        }

        public bool Sign
        {
            get { return (_value[0] == '1') ? true : false; }
            set { _value = (value) ? "1" + Module : "0" + Module; }
        }

        public string Module
        {
            get
            {
                return _value.Substring(1);
            }
        }

        public string SignedMagnitude
        {
            get
            {
                return _value;
            }

            set { _value = value.ToString(); }
        }

        public string OnesComplement
        {
            get
            {
                if (Sign)
                {
                    return "1" + Binary.InvertValue(this.Module);
                }
                else
                    return _value;
            }
        }

        public string TwosComplement
        {
            get
            {
                if (Sign)
                {
                    return "1" + Binary.TwosComplement(this.Module);
                }
                else
                    return _value;
            }
        }

        public static SBinary operator +(SBinary binary1, SBinary binary2)
        {
            string first = binary1.TwosComplement;
            string second = binary2.TwosComplement;

            int maxLength = Math.Max(first.Length, second.Length) + 1;
            first = first.PadLeft(maxLength, (binary1.Sign) ? '1' : '0');
            second = second.PadLeft(maxLength, (binary2.Sign) ? '1' : '0');

            var sum = Binary.Sum(first, second);
            if (sum.Length > maxLength)
                sum = sum.Substring(1);
            var result = new SBinary(sum);

            result.SignedMagnitude = result.TwosComplement;
            return result;
        }
        public static SBinary operator -(SBinary first, SBinary second)
        {
            second = new SBinary(second.SignedMagnitude);
            second.Sign ^= true;

            return first + second;
        }

        public static SBinary operator *(SBinary a, SBinary b)
        {
            SBinary result = new SBinary("0");
            for (int i = b.SignedMagnitude.Length - 1; i >= 1; i--)
            {
                if (b.SignedMagnitude[i] == '1')
                {
                    SBinary temp = new SBinary(a.SignedMagnitude);
                    for (int j = 0; j < b.SignedMagnitude.Length - 1 - i; j++)
                    {
                        temp.SignedMagnitude += "0";
                    }
                    result = result + temp;
                }
            }
            result.Sign = (a.Sign ^ b.Sign);
            return result;
        }

        public static Fixed operator /(SBinary dividend, SBinary divisor)
        {
            bool sign = (dividend.Sign^divisor.Sign);
            divisor.Sign = false;
            dividend.Sign = false;

            string quotient = "";
            SBinary remainder = new SBinary("0");
            string tempDividend = "";

            int counter = 0;
            for (int i = 0; i < dividend.Module.Length + 5; i++)
            {
                if (i >= dividend.Module.Length)
                    tempDividend += '0';
                else
                {
                    counter++;
                    tempDividend += dividend.Module[i];
                }

                if (Binary.CompareBinaryNumbers(tempDividend, divisor.Module) >= 0)
                {
                    quotient += "1";
                    remainder = new SBinary('0' + tempDividend) - divisor;
                    tempDividend = remainder.Module;
                }
                else
                {
                    quotient += "0";
                }
            }
            return new Fixed(sign, quotient.Substring(0, counter), quotient.Substring(counter));
        }

        public override string ToString()
        {
            return
            "Число:" + ((this.Sign) ? '-' : "") + 
            Binary.BinaryToDecimal(this.Module) + "\n" +
            "Прямой код: [" + _value[0] + " " + Module + "]" +
            "\n" +
            "Обратный код: [" + _value[0] + " " + this.OnesComplement.Substring(1) + "]" + 
            "\n" +
            "Дополнительный код: [" + _value[0] + " " + this.TwosComplement.Substring(1) + "]";
        }
    }

}
