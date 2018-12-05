
#include "pch.h"
#include <iostream>
#include "prep1_header.h"
#include <string>
#include <math.h>

int main()
{
	NODE n5 = { 11, 0 };	//Jonon viimeisen next = 0
	NODE n4 = { 22, &n5 };	//next = n5
	NODE n3 = { 33, &n4 };	//next = n4
	NODE n2 = { 44, &n3 };	//next = n3
	NODE n1 = { 55, &n2 };	//next = n2

	NODE *head = &n1;	//Jono pää eli n1

	PrintList(head);		//Printataan jono

	NODE i = { 100, 0 };
	NODE j = { 200, 0 };

	AddToTheEnd(head, &i);	
	AddToTheEnd(head, &j);

	PrintList(head);
}

void PrintList(NODE *head)
{
	NODE *n;	//väliaikainen pointer

	for (n = head; n != 0; n = n->next)		//Käydään läpi jono viimeiseen jäseneen saakka
											//(jossa n->next = 0)
	{
		std::cout << n->i << "\n";			//Tulostetaan tämänhetkinen jäsen
	}
}

void AddToTheEnd(NODE *head, NODE *item)
{
	NODE *n = head;		//n = n1

	while (n->next != 0)		//Mennään listan loppuun (jossa n->next = 0)
	{
		n = n->next;
	}

	n->next = item;		//Lopuksi asetetaan jonon viimeisen jäsenen next itemin arvoiseksi
}