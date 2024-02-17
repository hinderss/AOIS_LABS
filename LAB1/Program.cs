using System.Diagnostics.CodeAnalysis;

namespace LAB1
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main(string[] args)
        {
            int a = -250;

            int b = 249;

            CalculateIntegers(a, b);

            float p1 = 1000.1f;
            float p2 = 1.3f;

            Float floatA = new Float(p1);
            Float floatB = new Float(p2);

            Console.WriteLine($"Сложение чисел с плавающей точкой: {p1} + {p2} = {p1 + p2}");
            CalculateFloat(floatA, floatB);
        }
        static void CalculateIntegers(int a, int b)
        {
            Console.WriteLine($"Введено: ");
            SBinary binaryA = new SBinary(a);
            Console.WriteLine(binaryA);
            Console.WriteLine();

            Console.WriteLine($"Введено: ");
            SBinary binaryB = new SBinary(b);
            Console.WriteLine(binaryB);
            Console.WriteLine();

            Console.WriteLine($"Сложение: {a} + {b}\n");
            Console.WriteLine(Calculate(binaryA, binaryB, '+'));
            Console.WriteLine();

            Console.WriteLine($"Вычитание: {a} - {b}\n");
            Console.WriteLine(Calculate(binaryA, binaryB, '-'));
            Console.WriteLine();


            Console.WriteLine($"Умножение: {a} * {b}\n");
            Console.WriteLine(Calculate(binaryA, binaryB, '*'));
            Console.WriteLine();

            Console.WriteLine($"Частное: {a} / {b}\n");
            Console.WriteLine(binaryA / binaryB);
            Console.WriteLine();
        }

        static SBinary? Calculate(SBinary a, SBinary b, char operation)
        {
            switch (operation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }

            return null;
        }

        static void CalculateFloat(Float a, Float b)
        {
            Console.WriteLine($"Число 1:");
            Console.WriteLine(a);

            Console.WriteLine();

            Console.WriteLine($"Число 2:");
            Console.WriteLine(b);
            Console.WriteLine();


            Console.WriteLine("Результат: ");
            Console.WriteLine(a + b);
        }
    }
}
