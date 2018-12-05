#include "prep2_header.h"

Task::Task(std::string n, std::string dl, int i, int p) : name(n), deadline(dl), id(i),
priority(p) {}

Task::~Task(void) {}

void Task::Print()
{
	std::cout << priority << ". " << name << ", " << deadline << " (id: " << id << ")" 
		<< std::endl;
}

