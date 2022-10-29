/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package stack;

import java.util.Scanner;

/**
 *
 * @author AHMET
 */
class stackYapi {

    String Kelime;
    int value;
    stackYapi next;
}

public class Stack {

    /**
     * @param args the command line arguments
     */
    static stackYapi ilk;
    static stackYapi ilk2;

    static void Veri_Ekle(String Kelime, int sayi) {
        if (ilk == null) {
            ilk = new stackYapi();
            ilk.Kelime = Kelime;
            ilk.value = sayi;
            ilk.next = null;
        } else {
            stackYapi gecici = new stackYapi();
            gecici.Kelime = Kelime;
            gecici.value = sayi;
            gecici.next = ilk;
            ilk = gecici;
        }
    }

    static void Veri_Ekle2(String Kelime, int sayi) {
        if (ilk2 == null) {
            ilk2 = new stackYapi();
            ilk2.Kelime = Kelime;
            ilk2.value = sayi;
            ilk2.next = null;
        } else {
            stackYapi gecici2 = new stackYapi();
            gecici2.Kelime = Kelime;
            gecici2.value = sayi;
            gecici2.next = ilk2;
            ilk2 = gecici2;
        }
    }

    static void Print(stackYapi T) {
        while (T != null) {
            System.out.println(T.Kelime + ":" + T.value);
            T = T.next;
        }
    }

    static stackYapi Veri_Sil() {
        stackYapi data = ilk;
        ilk = ilk.next;
        return data;
    }

    static stackYapi Veri_Sil2() {
        stackYapi data = ilk2;
        ilk2 = ilk2.next;
        return data;
    }
    static String TamKelime;

    static void Soru(String deger) {

        char karakter;
        int ascii, adet = 0, s = 0;
        stackYapi yeni;
        for (int i = 0; i < deger.length(); i++) {

            karakter = deger.charAt(i);
            ascii = karakter;
            if (ascii >= 65 && ascii <= 90) {
                TamKelime = "";
                TamKelime += karakter;
                int f = deger.charAt(i + 1);

                if ((f >= 65 && f <= 90) || deger.charAt(i + 1) == ')' || deger.charAt(i + 1) == '(') {
                    Veri_Ekle(TamKelime, 1);
                    TamKelime = "";
                } else if (f >= 48 && f <= 57) {
                    TamKelime += karakter;
                    Veri_Ekle(TamKelime, deger.charAt(i + 1));
                    TamKelime = "";
                }
            } 
            else if (ascii >= 97 && ascii <= 122) {
                TamKelime += karakter;
                
                if (i + 1 >= deger.length()) {
                    Veri_Ekle(TamKelime, 1);
                    TamKelime = "";
                    break;
                }    
                int t = deger.charAt(i + 1);
                if (t >= 48 && t <= 57) {
                    Veri_Ekle(TamKelime, t-'0');
                    TamKelime = "";
                } else if ((t >= 65 && t <= 90) || deger.charAt(i + 1) == ')' || deger.charAt(i + 1) == '(') {
                    Veri_Ekle(TamKelime, 1);
                    TamKelime = "";
                }
            } 
            else if (karakter == '(') {
                Veri_Ekle("(", 0);
            }
            else if (karakter == ')') {
                if (deger.charAt(i + 1) >= 48 && deger.charAt(i + 1) <= 57) {
                    adet = deger.charAt(i + 1) - '0';
                } else {
                    adet = 1;
                }
                while (true) {
                    yeni = Veri_Sil();
                    if (yeni.Kelime != "(") {
                        Veri_Ekle2(yeni.Kelime, yeni.value);
                        s++;
                    } else {
                        break;
                    }
                }
                for (int x = 0; x < s; x++) {
                    yeni = Veri_Sil2();
                    Veri_Ekle(yeni.Kelime, yeni.value * adet);
                }
                s = 0;
            }
        }

    }
public static void main(String[] args) {
        // TODO code application logic here
        Scanner sc=new Scanner(System.in);
        
        System.out.println("Kelime: ");
        String oku = sc.nextLine();
        
        Soru(oku);
        //System.out.println("asdfasf "+ilk.Kelime+":"+ilk.value);
        Print(ilk);
        
    }
    
}
