#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include "string.h"


//--------- Struct Yapýsý ------------ //
struct YemekListe
{
	char yemekAdi[50];
	int YemekGramaj;
	float yemekFiyat;
	struct YemekListe* next;
};


FILE *fp;
YemekListe* ilk = NULL;
YemekListe* gecici;
YemekListe* silgecici;
 
 
 
 //--------- Düðüm ekleme  ------------ //
void Insert(char ad[50],int gram, float fiyat)
{
	if (ilk == NULL)
	{
		ilk= (YemekListe*) malloc (sizeof(YemekListe));
		for(int i=0; i<50; i++)
		{
			ilk -> yemekAdi[i] = ad[i];
		}
		ilk -> yemekFiyat = fiyat;
		ilk -> YemekGramaj = gram;
		ilk -> next = NULL;
	}
	else {
		if ((ilk -> yemekFiyat) > (fiyat))
		{
			YemekListe* new_Yemek= (YemekListe*) malloc (sizeof(YemekListe));
			for(int i=0; i<50; i++)
			{
				new_Yemek -> yemekAdi[i] = ad[i];
			}
			new_Yemek -> yemekFiyat = fiyat;
			new_Yemek -> YemekGramaj = gram;
			new_Yemek ->next = ilk; // asdasdas
			ilk = new_Yemek;
		}
		else 
		{
			gecici = ilk;
			YemekListe* new_Yemek= (YemekListe*) malloc (sizeof(YemekListe));
			for(int i=0; i<50; i++)
			{
				new_Yemek -> yemekAdi[i] = ad[i];
			}
			new_Yemek -> yemekFiyat = fiyat;
			new_Yemek -> YemekGramaj = gram;
			while(gecici !=NULL){
				if ( gecici->next == NULL && (gecici->yemekFiyat) <= (fiyat)){
					new_Yemek -> next = NULL;
					gecici -> next = new_Yemek;
					break;
				}
				
				if((gecici -> next -> yemekFiyat)> (fiyat)){
					new_Yemek -> next = gecici -> next;
					gecici->next = new_Yemek;
					break; 
				}
				gecici = gecici->next;
			}
		}
	}
}



//--------- Düðümleri Yazdýrma ------------ //
int Print()
{
	char yad[50];
	if(ilk == NULL)
		return -1;
	else 
	{
		for(int i =0; i<50; i++)
		{
			yad[i] = ilk->yemekAdi[i];
		}
		gecici = ilk;
		while (gecici != NULL)
		{
			printf("\n Yemek Adi: %s Yemek Gramaj: %d Yemek Fiyat: %3.2f \n",gecici->yemekAdi,gecici->YemekGramaj,gecici->yemekFiyat);
			gecici=gecici->next;
		}
	}
	return 1;
}

//--------- Metin Belgesinden Okuma ------------ //
void read_txt()
{
	char dizi[50];
	int ygram;
	float yfiyat;
	fp=fopen("dosya.txt","r");
	while (!feof(fp)){
		fscanf(fp,"%s",dizi);
		fscanf(fp,"%d",&ygram);
		fscanf(fp,"%f",&yfiyat);
		Insert(dizi,ygram,yfiyat);
	}

	fclose(fp);
}



//--------- Metim Belgesine Yazma ------------ //
void write_txt()
{
	fp=fopen("dosya.txt","w");
	gecici = ilk;
	char writer[250];
	while(gecici != NULL)
	{
		if(gecici->next == NULL)
			fprintf(fp,"%s %d %.2f",gecici->yemekAdi,gecici->YemekGramaj,gecici->yemekFiyat);
		else
			fprintf(fp,"%s %d %.2f \n",gecici->yemekAdi,gecici->YemekGramaj,gecici->yemekFiyat);
		gecici=gecici->next;
	}
	fclose(fp);
}


//--------- Düðüm Arama ------------ //
int traverse(char isim[50])
{
	gecici  = ilk;
	while(gecici != NULL && strcmp(gecici->yemekAdi,isim) != 0)
	{
		gecici = gecici -> next;
	}
	if(gecici == NULL)
		return -1;
	else return 1;
}


//--------- Düðüm Silme ------------ //
int Removeitem(char isim[50])
{
	int k=traverse(isim);
	if(k == -1)
		return 0;
	if(ilk == NULL)
		return -1;
	else if (strcmp(ilk->yemekAdi,isim)==0)
	{
		silgecici=ilk;
		ilk = ilk->next;
		free(silgecici);
		return 1;
	}
	else {
		gecici = ilk;
		while (strcmp(gecici->next->yemekAdi,isim) != 0 && gecici != NULL)
			gecici=gecici->next;
		if (gecici->next->next == NULL && strcmp(gecici->next->yemekAdi,isim) == 0)
		{
			gecici=ilk;
			while(gecici->next->next != NULL)
				gecici=gecici->next;
			free(gecici->next);
			gecici->next = NULL; 
			return 1;
		}
		else 
		{
			silgecici = gecici->next;
			struct YemekListe* baglanacakeleman=silgecici->next;
			gecici->next = baglanacakeleman;
			free(silgecici);
			return 1;
		}
	}
	return 0;
}


//--------- 4. Soru Yapýsý ------------ //
void Select()
{
	char key;
	gecici = ilk;
	printf("\n Yemek secimi icin: 'e' \n bir Sonraki yemek icin : 'h' \n ana menuye donmek icin: 'a' degerini giriniz.\n\n");
	while(gecici != NULL)
	{
		printf("\n Yemek Adi: %s Yemek Gramaj: %d Yemek Fiyat: %3.2f : ",gecici->yemekAdi,gecici->YemekGramaj,gecici->yemekFiyat);
		key=getche();
		if (key == 'e'){
			printf("\n\n %s Sectiniz.\nSiparisiniz en kisa zamanda teslim edilecektir.\nAfiyet Olsun!\n\n",gecici->yemekAdi);
			return ;
		}
		else if (key == 'h') {
			gecici=gecici->next;
			if(gecici == NULL) gecici=ilk;
		}
		else if (key == 'a')
		{
			break;
		}
		else {
			printf("\nYanlis Secim!");
		}
	}

}

//--------- Main Fonksiyon ------------ //
int main()
{
	int select_int;
	char yisim[50];
	int ygram;
	float yfiyat; 
	int kontrol;
	char sil[50];
	char select_Char;
	read_txt();
	printf("Metin Belgesi iceri Aktarildi");
	while(true)
	{
		printf("\nSecim Yapiniz: \n 1-) Ekleme \n 2-) Listeleme \n 3-)Silme \n 4-) Yemek Seçim \n 5-) Cikis \n ");
		select_Char = getche();
		printf("\n");
		select_int = select_Char - '0';
		switch(select_int)
		{
			case 1:
				printf("Eklenecek Yemek Adi: ");
				scanf("%s",yisim);
				printf("Eklenecek Yemek Gramaji : ");
				scanf("%d",&ygram);
				printf("Eklenecek Yemek Fiyati: ");
				scanf("%f",&yfiyat);
				Insert(yisim,ygram,yfiyat);
				write_txt();
				break;
			case 2:
				kontrol = Print();
				if (kontrol == -1) printf("Bagli liste bos \n");
				break;
				kontrol = 9;
			case 3:
				printf("Silinecek Yemek Adi Giriniz: ");
				scanf("%s",sil);
				kontrol = Removeitem(sil);
				if (kontrol == -1) printf("Liste Bos!");
				else if ( kontrol == 0 ) printf("Listede Bulunamadi !");
				else if( kontrol == 1 ) printf("Silme Basarili");
				write_txt();
				kontrol = 9;
				break;
			case 4:
				Select();
				break;
			case 5:
				exit(1);
		}
	}
	return 0;
}


//---------  ------------ //
