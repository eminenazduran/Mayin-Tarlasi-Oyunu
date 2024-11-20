using System;

class MayinTarlasi
{
    static int boyut = 20; // Tahta boyutu
    static int mayinSayisi = 60; // Mayın sayısı
    static char[,] tahta = new char[boyut, boyut];
    static bool[,] mayinlar = new bool[boyut, boyut];
    static bool[,] acilanKareler = new bool[boyut, boyut];
    static bool oyunBitti = false;

    static void Main()
    {
        MayinlariOlustur();
        TahtayiHazirla();

        while (!oyunBitti)
        {
            TahtayiYazdir();
            Console.Write("Satır seç (0-19): ");
            int satir = int.Parse(Console.ReadLine());
            Console.Write("Sütun seç (0-19): ");
            int sutun = int.Parse(Console.ReadLine());

            if (mayinlar[satir, sutun])
            {
                Console.WriteLine("BOOOOM! Mayına bastınız.");
                tahta[satir, sutun] = 'X'; // Patlayan mayın
                oyunBitti = true;
                TümMayınlarıGöster();
            }
            else
            {
                AcKare(satir, sutun);
                if (KontrolEt())
                {
                    Console.WriteLine("Tebrikler! Tüm mayınsız kareleri açtınız.");
                    oyunBitti = true;
                }
            }
        }
        TahtayiYazdir();
        Console.WriteLine(oyunBitti ? "Oyun sona erdi." : "Tebrikler!");
    }

    static void MayinlariOlustur()
    {
        Random random = new Random();
        int eklenenMayinlar = 0;

        while (eklenenMayinlar < mayinSayisi)
        {
            int x = random.Next(boyut);
            int y = random.Next(boyut);

            if (!mayinlar[x, y])
            {
                mayinlar[x, y] = true;
                eklenenMayinlar++;
            }
        }
    }

    static void TahtayiHazirla()
    {
        for (int i = 0; i < boyut; i++)
        {
            for (int j = 0; j < boyut; j++)
            {
                tahta[i, j] = '■'; // Kapalı kare sembolü
            }
        }
    }

    static void TahtayiYazdir()
    {
        Console.Clear();
        Console.WriteLine("Mayın Tarlası:");
        for (int i = 0; i < boyut; i++)
        {
            for (int j = 0; j < boyut; j++)
            {
                Console.Write(tahta[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void AcKare(int x, int y)
    {
        if (x < 0 || y < 0 || x >= boyut || y >= boyut || acilanKareler[x, y])
            return;

        acilanKareler[x, y] = true;

        int cevreMayinSayisi = CevreMayinSayisi(x, y);
        if (cevreMayinSayisi > 0)
        {
            tahta[x, y] = (char)('0' + cevreMayinSayisi);
        }
        else
        {
            tahta[x, y] = ' ';
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i == 0 && j == 0))
                        AcKare(x + i, y + j);
                }
            }
        }
    }

    static int CevreMayinSayisi(int x, int y)
    {
        int sayac = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int yeniX = x + i;
                int yeniY = y + j;

                if (yeniX >= 0 && yeniY >= 0 && yeniX < boyut && yeniY < boyut && mayinlar[yeniX, yeniY])
                {
                    sayac++;
                }
            }
        }

        return sayac;
    }

    static void TümMayınlarıGöster()
    {
        for (int i = 0; i < boyut; i++)
        {
            for (int j = 0; j < boyut; j++)
            {
                if (mayinlar[i, j])
                {
                    tahta[i, j] = 'X'; // Mayınları göster
                }
            }
        }
    }

    static bool KontrolEt()
    {
        for (int i = 0; i < boyut; i++)
        {
            for (int j = 0; j < boyut; j++)
            {
                if (!mayinlar[i, j] && !acilanKareler[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
