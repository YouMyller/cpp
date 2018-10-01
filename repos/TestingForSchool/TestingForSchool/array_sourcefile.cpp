#include "array_headerfile.h"

Array::Array(void) : value(0)
{

}

Array::~Array(void)
{

}

int main()
{
	Array array1;

	for (int i = 0; i < 10; i++)
	{
		array1.AddItem(i);
	}

	array1.Print();

	return 0;
}

void Array::AddItem(int i)
{
	if (value != 10)
	{
		arr[value++] = i;
	}
}

void Array::Print()
{
	for (int i = 0; i < 10; i++)
	{
		cout << "Value is " << arr[i] << " right now.\n";
	}
}