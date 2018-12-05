#ifndef TASKMANAGER
#define TASKMANAGER

#include "prep2_header.h";
#include <vector>

class TaskManager
{
public:
	TaskManager() {};
	~TaskManager();

	void Run();

private:
	std::vector<Task*>myTasks;
};

#endif