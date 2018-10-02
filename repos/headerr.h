#pragma once

#ifndef POINTERTEST
#define POINTERTEST

#include "math.h""
#include <string>
#include <iostream>

using namespace std;

class Pointers
{
public:

	Pointers(void);
	~Pointers(void);

	void AddItem(int i);
	void Print();

	int point;
	int arrayOfPoints[3];
	int size;
};

#endif