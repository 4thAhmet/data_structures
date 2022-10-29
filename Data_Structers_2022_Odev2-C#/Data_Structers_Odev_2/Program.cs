namespace Data_Structers_Odev_2
{
    internal class Program
    {
        static Stack head;
        static Stack head2;
        static string yeni_kelime;


        static void Main(string[] args)
        {
            Console.Write("Kelimenizi giriniz: ");
            string okunan = Console.ReadLine();
            method(okunan);
            ekrana_yaz();
        }


        static void method(string kelime)
        {
            char harf;
            int adet = 1;
            string gelen = " ";
            int sayac = 0;
            int kontrol_adet = 1;
            for (int i = 0; i < kelime.Length; i++)
            {
                harf = char.Parse(kelime.Substring(i, 1));
                if ((int)harf >= 97 && (int)harf <= 122) // Küçük Harf 
                {
                    yeni_kelime += harf;
                    if (i + 1 >= kelime.Length)
                    {
                        yeni_kelime += ":1";
                        ekle(yeni_kelime);
                        yeni_kelime = null;
                    }
                    else if ((int)char.Parse(kelime.Substring(i + 1, 1)) >= 48 && (int)char.Parse(kelime.Substring(i + 1, 1)) <= 57)
                    {
                        yeni_kelime += ":" + kelime.Substring(i + 1, 1).ToString();
                        ekle(yeni_kelime);
                        yeni_kelime = null;
                    }
                    else if (((int)char.Parse(kelime.Substring(i + 1, 1)) >= 65 && (int)char.Parse(kelime.Substring(i + 1, 1)) <= 90) || kelime.Substring(i + 1, 1) == "(" || kelime.Substring(i + 1, 1) == ")")
                    {
                        yeni_kelime += ":1";
                        ekle(yeni_kelime);
                        yeni_kelime = null;

                    }
                }
                else if ((int)harf >= 65 && (int)harf <= 90) // Büyük Harf
                {
                    yeni_kelime = null;
                    yeni_kelime += harf;
                    if (((int)char.Parse(kelime.Substring(i + 1, 1)) >= 65 && (int)char.Parse(kelime.Substring(i + 1, 1)) <= 90) || kelime.Substring(i + 1, 1) == "(" || kelime.Substring(i + 1, 1) == "(")
                    {
                        yeni_kelime += ":1";
                        ekle(yeni_kelime);
                        yeni_kelime = null;
                    }
                    else if ((int)char.Parse(kelime.Substring(i + 1, 1)) >= 48 && (int)char.Parse(kelime.Substring(i + 1, 1)) <= 57)
                    {
                        yeni_kelime += harf + ":" + kelime.Substring(i + 1, 1);
                        ekle(yeni_kelime);
                        yeni_kelime = null;
                    }
                }
                else if (harf == '(') // ( 
                {
                    ekle("(");
                }
                else if (harf == ')') // )
                {
                    if ((int)char.Parse(kelime.Substring(i + 1, 1)) >= 48 && (int)char.Parse(kelime.Substring(i + 1, 1)) <= 57)
                    {
                        adet = Convert.ToInt32(kelime.Substring(i + 1, 1));
                    }
                    else adet = 1;
                    while (1 == 1)
                    {
                        gelen = sil();
                        if (gelen != "(")
                        {
                            ekle2(gelen);
                            gelen = null;
                            sayac++;
                        }
                        else
                        {
                            gelen = null;
                            break;
                        }
;
                    }
                    for (int k = 0; k < sayac; k++)
                    {
                        string stack2al = sil2();
                        kontrol_adet = Convert.ToInt32(stack2al.Substring(stack2al.Length - 1, 1));
                        kontrol_adet = kontrol_adet * adet;
                        ekle(stack2al.Substring(0, stack2al.Length - 2) + ":" + kontrol_adet);
                    }
                    gelen = null;
                    adet = 1;
                    sayac = 0;
                    kontrol_adet = 1;
                }
            }
        }


        static string sil()
        {
            string kelime = head.kelime;
            head = head.next;
            return kelime;
        }
        static string sil2()
        {
            if (head2 == null) return " ";
            string kelime = head2.kelime;
            head2 = head2.next;
            return kelime;
        }


        static void ekle(string kelime)
        {
            if (head == null)
            {
                head = new Stack();
                head.kelime = kelime;
                head.next = null;
            }
            else
            {
                Stack temp = new Stack();
                temp.kelime = kelime;
                temp.next = head;
                head = temp;
            }
        }
        static void ekle2(string kelime)
        {
            if (head2 == null)
            {
                head2 = new Stack();
                head2.kelime = kelime;
                head2.next = null;
            }
            else
            {
                Stack temp = new Stack();
                temp.kelime = kelime;
                temp.next = head2;
                head2 = temp;
            }
        }


        static void ekrana_yaz()
        {
            Stack iter;
            iter = head;
            while (iter != null)
            {
                Console.WriteLine("iter: " + iter.kelime);
                iter = iter.next;
            }
        }
    }


    class Stack
    {
        public string kelime;
        public Stack next;
    }
}