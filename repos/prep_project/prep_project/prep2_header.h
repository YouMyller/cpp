#pragma once
#ifndef TASKHEADER
#define TASKHEADER

#include <string>
#include <iostream>

class Task
{
public:
	Task(std::string n, std::string dl, int i, int p);
	~Task();

	void Print();

	std::string GetName() { return name; }
	std::string GetDeadLine() { return deadline;  }
	int GetId() { return id; }
	int GetPriority() { return priority; }

private:
	std::string name;
	std::string deadline;
	int id;
	int priority;
};

#endif // !TASKHEADER