using System.Collections;
namespace phoneNumber
{
    internal class Program
    {
        static string tuslar = "";
        static string[] character = { " ", " ", "abc", "def", "ghi", "jkl", "mno", "qprs", "tuv", "wxyz" };
        static ArrayList dizi = new ArrayList();
        static void Main(string[] args)
        {
            Console.Write("Tuşları Giriniz: ");
            tuslar = Console.ReadLine();
            kelimeUret(0, "");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tÜretilen Kelimeler: ");
            Console.WriteLine("---------------------------------------");
            Console.ResetColor();
            for (int i = 0; i < dizi.Count; i++)
            {
                Console.WriteLine((i + 1) + ".\t" + dizi[i]);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------------------------------------");
            Console.ResetColor();
        }
        static void kelimeUret(int sira, string str)
        {
            if (str.Length == tuslar.Length)
            {
                dizi.Add(str);
                return;
            }
            foreach (char c in character[Convert.ToInt16(tuslar[sira]) - '0'])
            {
                kelimeUret(sira + 1, str + c);
            }
        }
    }
}