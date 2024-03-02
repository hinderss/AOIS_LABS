using Logic;
using System.Diagnostics.CodeAnalysis;

namespace LAB2
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        public static void Main(string[] args)
        {
            string expression = "!x|(y&!z|x)&z";

            var x = new LogicCalculator(expression);

            Console.WriteLine("Таблица истинности:");
            Console.WriteLine(x.BuildTable());
            Console.WriteLine();

            Console.WriteLine("Совершенная дизъюнктивная нормальная форма (СДНФ):");
            Console.WriteLine(x.BuildPDNFString());
            Console.WriteLine();

            Console.WriteLine("Совершенная конъюнктивная нормальная форма (СKНФ):");
            Console.WriteLine(x.BuildPCNFString());
            Console.WriteLine();

            Console.WriteLine("Числовые формы:");
            Console.WriteLine(x.BuildNumFormPCNF());
            Console.WriteLine(x.BuildNumFormPDNF());
            Console.WriteLine();

            Console.WriteLine("Индексная форма:");
            Console.WriteLine($"{Convert.ToInt64(x.IndexForm, 2)} - {x.IndexForm}");
        }
    }
}
