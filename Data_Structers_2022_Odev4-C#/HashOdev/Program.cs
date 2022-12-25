namespace HashOdev
{
    struct Musteri
    {
        public int musteri_no;
        public string ad;
        public string soyad;
    }
    internal class Program
    {
        static int satir = 10;
        static Musteri[] veri_liste = new Musteri[satir];
        static int[] hash_tablo = new int[satir];
        static void init()
        {

            for (int i = 0; i < satir; i++)
            {
                hash_tablo[i] = -1;
                veri_liste[i].musteri_no = -1;
                veri_liste[i].ad = "-";
                veri_liste[i].soyad = "-";
            }
        }
        static void print()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tHash Tablosu: ");
            for (int i = 0; i < satir; i++)
            {
                Console.WriteLine("\t" + veri_liste[i].musteri_no + "\t" + veri_liste[i].ad + "\t" + veri_liste[i].soyad);
            }
            Console.ResetColor();
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
        static bool tablodolumu()
        {
            for (int i = 0; i < satir; i++)
            {
                if (hash_tablo[i] == -1)
                {
                    return true;
                }
            }
            return false;
        }
        static void ekle(int no, string ad, string soyad)
        {
            int dene = 0;
            if (!tablodolumu())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tHash Tablosu Dolu!");
                Console.ResetColor();
                return;
            }
            int hesap = (3 * no + 2) % satir;
            if (hash_tablo[hesap] == -1)
            {
                hash_tablo[hesap] = no;
                veri_liste[hesap].musteri_no = no;
                veri_liste[hesap].ad = ad;
                veri_liste[hesap].soyad = soyad;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tEkleme Başarılı");
                Console.ResetColor();
            }
            else
            {
                int yenihesap = (7 * no + 2) % satir;
                while(true)
                {
                    int x = (hesap + dene * yenihesap) % satir;
                    if (yenihesap == 0) yenihesap++;
                    if (hash_tablo[x] == -1)
                    {
                        hash_tablo[x] = no;
                        veri_liste[x].musteri_no = no;
                        veri_liste[x].ad = ad;
                        veri_liste[x].soyad = soyad;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\tEkleme Başarılı");
                        Console.ResetColor();
                        break;
                    }
                    dene++;
                    if(dene > satir)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tHatalı değer");
                        Console.ResetColor();
                        break;
                    }
                }
                /*for (int i = 1; i < satir; i++)
                {
                    int x = (hesap + i * yenihesap) % satir;
                    if (yenihesap == 0) yenihesap++;
                    if (hash_tablo[x] == -1)
                    {
                        hash_tablo[x] = no;
                        veri_liste[x].musteri_no = no;
                        veri_liste[x].ad = ad;
                        veri_liste[x].soyad = soyad;
                        break;
                    }
                }*/
            }
        }
        static int arama(int no)
        {
            int hesap = (3 * no + 2) % satir;
            int sayac = 0;
            if (hash_tablo[hesap] == no)
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
                    if (hash_tablo[x] == no)
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
            for (int i = 0; i < satir; i++)
            {
                if (hash_tablo[i] != -1)
                {
                    sayac++;
                    toplam += arama(Convert.ToInt32(hash_tablo[i]));
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
            while (true)
            {
                Console.ResetColor();
                Console.Write("1:Ekle\n2:Arama\n3:Listele\n4:Ortalama Adım Sayısı\n5:Çıkış\nSeçim Yapınız: ");
                sec = Convert.ToInt16(Console.ReadLine());
                switch (sec)
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (ara == -1) Console.WriteLine("\tAranan Numara Bulunamadı.");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\tVeri {0} adımda bulundu", ara);
                        }
                        break;
                    case 3:
                        print();
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\tOrtalama Adım Sayısı: " + ortalama().ToString("0.00"));
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hatalı Giriş.");
                        break;
                }
            }
        }
    }
}