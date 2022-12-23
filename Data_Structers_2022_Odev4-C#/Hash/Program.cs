namespace Hash
{
    internal class Program
    {
        static int satir = 10;
        static string[,] hash = new string[satir, 2];
        static void init()
        {
            for(int i=0;i<satir;i++)
            {
                for(int j=0; j<2; j++)
                {
                    if (j == 0)
                        hash[i, j] = "-1";
                    else hash[i, j] = "NULL";
                }
            }
        }
        static void print()
        {
            Console.WriteLine("Hash Tablosu: ");
            for (int i = 0; i < satir; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(hash[i, j].ToString()+"\t");
                }
                Console.WriteLine("");
            }
        }
        static void txt()
        {
            FileStream fs = new FileStream("veri.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] parca = line.Split(' ');
                ekle(Convert.ToInt32(parca[0]), parca[1], parca[2]);
                line = sr.ReadLine();
            }
            sr.Close(); fs.Close();
        }
        static void ekle(int no,string ad,string soyad)
        {
            int hesap = (3 * no + 2) % satir;
            if (hash[hesap,0] == "-1")
            {
                hash[hesap, 0] = no.ToString();
                hash[hesap, 1] = ad + " " + soyad;
            }
            else
            {
                int yenihesap = (7 * no + 2) % satir;
                for (int i = 1; i < satir; i++)
                {
                    int x = (hesap + i * yenihesap) % satir;
                    if (yenihesap == 0) yenihesap++;
                    if (hash[x,0] == "-1")
                    {
                        hash[x,0] = no.ToString();
                        hash[x, 1] = ad + " " + soyad;
                        break;
                    }
                }
            }
        }
        static int arama(int no)
        {
            int hesap = (3 * no + 2) % satir;
            int sayac = 0;
            if (hash[hesap, 0] == no.ToString())
            {
                sayac++;
                return sayac;
            }
            else
            {
                sayac++;
                int yenihesap = (7 * no + 2) % satir;
                for (int i = 1; i < satir; i++)
                {
                    sayac++;
                    int x = (hesap + i * yenihesap) % satir;
                    if (yenihesap == 0)
                    {
                        sayac--;
                        yenihesap++;
                    }
                    if (hash[x, 0] ==no.ToString())
                    {
                        return sayac;
                    }
                }
            }
            return -1;
        }
        static double ortalama()
        {
            double toplam = 0;
            int sayac = 0;
            for (int i=0;i<satir;i++)
            {
                if (hash[i,0] !="-1")
                {
                    sayac++;
                    Console.WriteLine("Ort:" + arama(Convert.ToInt32(hash[i, 0])));
                    toplam += arama(Convert.ToInt32(hash[i, 0]));
                }
            }
            return toplam / sayac;
        }
        static void Main(string[] args)
        {
            init();
            txt();
            int sec, no, ara;
            string ad, soyad;
            while(true)
            {
                Console.Write("1:Ekle\n2:Arama\n3:Listele\n4:Ortalama Adım Sayısı\n5:Çıkış\nSeçim Yapınız: ");
                sec = Convert.ToInt16(Console.ReadLine());
                switch(sec)
                {
                    case 1:
                        Console.Write("Numara Giriniz: ");
                        no = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Adın Giriniz: ");
                        ad = Console.ReadLine();
                        Console.Write("Soyad Giriniz: ");
                        soyad = Console.ReadLine();
                        ekle(no, ad, soyad);
                        break;
                    case 2:
                        Console.Write("Aranacak Numara Giriniz: ");
                        no = Convert.ToInt32(Console.ReadLine());
                        ara = arama(no);
                        if (ara == -1) Console.WriteLine("Aranan Numara Bulunamadı.");
                        else Console.WriteLine("Veri {0} adımda bulundu", ara);
                        break;
                    case 3:
                        print();
                        break;
                    case 4:
                        Console.WriteLine("Ortalama Adım Sayısı: " + ortalama().ToString("0.00"));
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Hatalı Giriş.");
                        break;
                }
            }
        }
    }
}