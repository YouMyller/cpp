
#include <string>
#include <iostream>
#include <math.h>
using namespace std;

struct Node
{
	int number;
	Node* next;
};

void AddToTheEnd(Node *newNode, Node *tempNode, Node *headNode);
void PrintList(Node *headNode, Node *tempNode);

int main()
{
	Node* newNode = NULL;
	Node* tempNode = NULL;
	Node* headNode = NULL;

	//int numberoo[3] = {0,0,0};

	AddToTheEnd(newNode, tempNode, headNode);
	PrintList(headNode, tempNode);

	return 0;
}

void AddToTheEnd(Node *newNode, Node *tempNode, Node *headNode)
{
	for (int i = 0; i < 2; i++)
	{
		newNode = new Node;
		newNode->number = i;

		if (i == 0)
		{
			tempNode = newNode;
			headNode = newNode;
		}
		else
		{
			tempNode->next = newNode;
			tempNode = tempNode->next;
		}

		//numberoo[i] = newNode->number;
	}
}

void PrintList(Node *headNode, Node *tempNode)
{
	//cout << numberoo[0] << numberoo[1] << numberoo[2] << "\n";
	cout << headNode->number << tempNode->number;
}

	/*newNode = new Node;		//newNode ei ole uusi Node, vaan osoittaa uuteen Nodeen
	newNode->number = 1;		//uuden Noden number = 1
	tempNode = newNode;		//temp seruaa newNodea, kun menee pitkin listaa
	headNode = newNode;		//head pysyy samassa
	numberoo[0] = newNode->number;

	newNode = new Node;		//newNode osoittaa jälleen uuteen Nodeen
	newNode->number = 2;	//toisen uuden Noden number = 2
	tempNode->next = newNode;	//tempNoden (ensimmäisen uuden Noden) next-Node asetataan toisesi uudeksi Nodeksi
	tempNode = tempNode->next;	//tempNode asetetaan toiseksi uudeksi nodeksi
	numberoo[1] = newNode->number;

	newNode = new Node;		//kolmas uusi node
	newNode->number = 3;
	tempNode->next = newNode;
	tempNode = tempNode->next;
	numberoo[2] = newNode->number;*/