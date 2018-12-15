//#pragma once
//#ifndef NODEHEADER
//#define NODEHEADER

struct NODE
{
	int i;
	NODE* next;
};

void PrintList(NODE *head);
void AddToTheEnd(NODE *head, NODE *item);

//#endif // !NODEHEADER
