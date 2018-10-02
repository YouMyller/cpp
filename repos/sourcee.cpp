
#include "headerr.h"

Pointers::Pointers(void) : point(0)
{
	
}


int main()
{
	int *hmm = 0;
	int i = 100;

	hmm = &i;		
	*hmm = 10;

	cout << &hmm << "\n";	//Pointterin sisältämän luvun memory location
	cout << hmm << "\n";	//Pointterin memory location
	cout << *hmm << "\n";	//Luku, johon pointteri osoittaa

	Pointers p;

	p.point = 1;

	cout << p.point;
	
	p.arrayOfPoints[0] = 500;
	p.arrayOfPoints[1] = p.point;
	p.arrayOfPoints[2] = *hmm;

	cout << p.arrayOfPoints[2] << "\n";
	p.arrayOfPoints[3] = 60;
	hmm = &p.arrayOfPoints[3];

	cout << hmm << ", also " << &p.arrayOfPoints[3] << ", also " << p.arrayOfPoints << "\n";

	//for (int i = 0; i < 3; i++)
	//{
		cout << *hmm;
	//}

	//cout << p.arrayOfPoints[0] << "\n" << p.arrayOfPoints[2] << "\n" << p.arrayOfPoints[0];

	return 0;
}