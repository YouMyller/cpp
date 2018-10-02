#include "headerr.h"

Pointers::Pointers(void): point(0)
{
	
}

Pointers::~Pointers(void)
{

}

int main()
{
	Pointers *p1 = new Pointers;

	for (int i = 0; i < 3; i++)
	{
		p1->AddItem(i);
	}

	p1->Print();

	int *kakka = new int[2];
	kakka[0] = 12;
	kakka[1] = 33;
	delete [] kakka;
	kakka = 0;
	//*kakka = 1;

	cout << kakka << "\n";

	delete p1;

	return 0;
}

void Pointers::AddItem(int i)
{
	if (point != 3)
	{
		arrayOfPoints[point++] = i;
	}
}

void Pointers::Print()
{
	for (int i = 0; i < 3; i++)
	{
		cout << "Value is " << arrayOfPoints[i] << " right now.\n";
	}
}