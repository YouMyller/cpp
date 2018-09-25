#pragma once
#ifndef TEST_H
#define TEST_H

#include <iostream>
#include <string>
#include <math.h>
using namespace std;

class Array
{
public:

	Array(void);
	~Array(void);

	void AddItem(int i);
	void Print();

private:

	int i;
	int arr[10];
};

#endif