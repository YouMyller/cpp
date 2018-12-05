#include "prep2_header2.h";

TaskManager::~TaskManager()
{
	for (int i = 0; i < myTasks.size(); i++)
	{
		delete myTasks[i];
	}
}

void TaskManager::Run()
{
	int choice = 0;

	do
	{
		std::cout << "The Manager of Kakka" << std::endl;
		std::cout << "Press a button for stuff" << std::endl;
		std::cout << "(1) Add a task" << std::endl;
		std::cout << "(2) Print all tasks" << std::endl;
		std::cout << "(3) Remove a task" << std::endl;
		std::cout << "(0) Quit (Please use this)" << std::endl;
		std::cout << "Now do it. I know you got this, you beatiful husk of a person!" << std::endl;

		std::cin >> choice;
		std::cin.ignore();

		if (choice == 1)
		{
			std::string name, deadline;
			int id, priority;

			std::cout << "\nCreating a new task...!?\n";
			std::cout << "Input name here ->";
			std::getline(std::cin, name);
			std::cout << "Input ID number here -> ";
			std::cin >> id;
			std::cout << "Input deadline here -> ";
			std::cin >> deadline;
			std::cout << "Input priority value here -> ";
			std::cin >> priority;

			Task *t = new Task(name, deadline, id, priority);
			myTasks.push_back(t);
		}
		else if (choice == 2)
		{
			std::cout << "\nPrinting tasks...!?\n";

			for (int i = 0; i < myTasks.size(); i++)
			{
				myTasks[i]->Print();
			}
		}
		else if (choice == 3)
		{
			int index = 0;
			std::string answer;

			std::cout << "\nEnter the ID of the task to be removed ->\n";
			std::cin >> index;
			std::cout << "Uh, you REALLY wanna remove " << myTasks[index]->GetName()
				<< "? (Y/N)";
			std::cin >> answer;

			if (answer == "y")
			{
				std::cout << "n\Okay, weirdo.\n";
				myTasks.erase(myTasks.begin() + index);
			}
		}

	} while (choice != 0);
}

int main()
{
	TaskManager myManager;

	myManager.Run();
}