#pragma once
#ifndef NODEHEADER
#define NODEHEADER

struct NODE
{
public:
	int i;
	NODE* next;
};

NODE *head;
NODE *curr;
NODE *temp;

void PrintList(NODE *head);
void AddToTheEnd(NODE *head, NODE *item);

#endif // !NODEHEADER

