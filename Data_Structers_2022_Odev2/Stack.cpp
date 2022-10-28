#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
// ------------- Stack Veri Yapýsý ---------------- //
struct Stack
{
	char data[10];
	int adet;
	Stack* next;
};
// ------------- Stack Veri Yapýsý ---------------- //




Stack* stack1;
Stack* stack2;
char ch[50];
int stackadet = 0;
char Kelime[50];
int kelimesayac = 0;







// ------------- Stack Veri Yapýsý Ekleme ---------------- //
void stack_Push(char data[],int adet,int stackType)
{
	if(stackType == 1)
	{
		
		if(stack1 == NULL)
		{
			stack1 = (Stack *)malloc(sizeof(Stack));
			strcpy(stack1->data,data);
			stack1->adet = adet;
			stack1->next = NULL;
		}
		else 
		{
			Stack* temp = (Stack *)malloc(sizeof(Stack));
			temp->next = stack1;
			temp->adet = adet;
			strcpy(temp->data,data);
			stack1 = temp;
		}
	}
	else if (stackType == 2)
	{
		if(stack2 == NULL)
		{
			stack2=(Stack *)malloc(sizeof(Stack));
			strcpy(stack2->data,data);
			stack2->adet = adet;
			stack2->next = NULL;
		}
		else
		{
			Stack* temp2 = (Stack *)malloc(sizeof(Stack));
			temp2->next = stack2;
			temp2->adet = adet;
			strcpy(temp2->data,data);
			stack2 = temp2;
		}
	}
	else printf("Hatali Stack Type");
}
// ------------- Stack Veri Yapýsý Ekleme ---------------- //





// ------------- Stack Veri Yapýsý Silme ---------------- //
Stack* stack_Pop(int stackType)
{
	if(stackType == 1)
	{
		Stack* temp1 = stack1;
		stack1 = stack1->next;
		strcpy(ch,temp1->data);
		stackadet = stack1->adet;
		return temp1;
	}
	else if(stackType == 2)
	{
		Stack* temp2 = stack2;
		stack2 = stack2->next;
		strcpy(ch,temp2->data);
		return temp2;
	}
	else printf("Hatali Stack Type");
}
// ------------- Stack Veri Yapýsý Silme ---------------- //


// ------------- Stack Veri Yapýsý Ekrana Yazdýrma ---------------- //
void Yazdir(int stackType)
{
	if(stackType == 1)
	{
		Stack *temp;
		temp=stack1;
		while(temp!=NULL)
		{
			printf("\n%s -> %d ",temp->data,temp->adet);
			temp=temp->next;
		}
	}
	else if(stackType == 2)
	{
		Stack *temp;
		temp=stack2;
		while(temp!=NULL)
		{
			printf("\n%s -> %d ",temp->data,temp->adet);
			temp=temp->next;
		}
	}
}
// ------------- Stack Veri Yapýsý Ekrana Yazdýrma ---------------- //



// ------------- Odev Algoritmasý ---------------- //
char kelimesil()
{
	for(int i =0; i<50; i++)
		Kelime[i]= NULL;
	kelimesayac=0;
}
// ------------- ---------------- //

void dolas(char data[])
{
	char gelen;
	int stackadetsayac = 0;
	int stackadetcarp =0;
	Stack* AraDeger;
	for(int i=0; i<strlen(data); i++)
	{
		gelen = data[i];
		if((int)gelen == 41)
		{
			if(isdigit(data[i+1]) != 0)
			{
				stackadetcarp = data[i+1] - '0';
			}
			else stackadetcarp = 1;
			while(true)
			{
				AraDeger = stack_Pop(1);
				if(strcmp(AraDeger->data,"(") != 0)
				{
					stack_Push(AraDeger->data,AraDeger->adet,2);
					free(AraDeger);	
					stackadetsayac++;
				}
				else 
				{
					free(AraDeger);
					break;
				}
			}
			
			for(int j=0; j<stackadetsayac;j++)
			{
				AraDeger=stack_Pop(2);
				AraDeger->adet = AraDeger->adet * stackadetcarp;
				stack_Push(AraDeger->data,AraDeger->adet,1);
			}
			free(AraDeger);
			stackadetsayac = 0;
		}
		else if((int)gelen >= 65 && (int)gelen <= 90)
		{
			kelimesil();
			Kelime[kelimesayac] = gelen; 
			kelimesayac++;
			if(((int)data[i+1] >= 65 && (int)data[i+1] <= 90) || (int)data[i+1] == 40 || (int)data[i+1] == 41)
			{		
				stack_Push(Kelime,1,1);
				kelimesil();
			}
			else if(isdigit(data[i+1]) != 0 )
			{
				stack_Push(Kelime,data[i+1]-'0',1);
				kelimesil();
			}
			else if ( data[i+1] == '\0')
			{
				stack_Push(Kelime,1,1);
				kelimesil();
			}
		}
		else if ((int)gelen >= 97 && (int)gelen <= 122)
		{
			
			Kelime[kelimesayac] = gelen;
			kelimesayac++;
			if(isdigit(data[i+1]) != 0 )
			{
				stack_Push(Kelime,data[i+1]-'0',1);
				kelimesil();
			}
			else if(((int)data[i+1] >= 65 && (int)data[i+1] <= 90) || (int)data[i+1] == 40 || (int)data[i+1] == 41)
			{
				stack_Push(Kelime,1,1);
				kelimesil();	
			}	
			else if ( data[i+1] == '\0')
			{
				stack_Push(Kelime,1,1);
				kelimesil();
			}
		}
		else if((int)gelen == 40)
		{
			stack_Push("(",0,1);		
		}
	}
}
// ------------- Odev Algoritmasý ---------------- //


// ------------ Main ------------ //
int main()
{
	char word[50];
	printf("Kelime Giriniz: ");
	scanf("%s",word);
	printf("\n ------------------- \n");
	dolas(word);
	Yazdir(1);
	printf("\n ------------------- \n");
	return 0;
}
// ------------ Main ------------ //
