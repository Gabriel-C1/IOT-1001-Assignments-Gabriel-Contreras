// Gabriel Contreras
// Applied Activity 4- TODO List Manager
/* The program let the user to input tasks, marked them completed
 * delete them and view tasks not completed
 */

using System.Diagnostics.CodeAnalysis;

namespace TODO_List_Manager
{
    internal class Program
    {
        static string[] todoList = new string[10];
        static int taskCounter = 0;
        static void Main(string[] args)
        {
            int choice = 0;  // User menu selection

            while (choice != 10)     // Run program until exit
            {
                Console.Clear();
                DisplayMenu();
            

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 10)   // Menu input validation
            {
                Console.WriteLine("Invalid Option. Try again.");
            }

            switch (choice) // Control every input selection
            {
                case 1: AddTask(); break;
                case 2: ViewAllTasks(); break;
                case 3: MarkTaskComplete(); break;
                case 4: DeleteTask(); break;
                case 5: ViewIncompleteTask(); break;
                case 6: CLearAllTasks(); break;
                case 7: ShowStats(); break;
                case 8: ExportToFile(); break;
                case 9: ImportFromFile(); break;
                case 10: Console.WriteLine("Thank you for using the program!"); break;
            }

            if (choice != 10) //Pause screen before returning to menu
            {
                Console.WriteLine("Press ENTER to continue.");
                Console.ReadLine();
            }
        }

        }
        // Shows the user the menu
        static void DisplayMenu()
        {
            Console.WriteLine("====== TODO List Manager ======");
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. View all tasks");
            Console.WriteLine("3. Mark task as complete");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. View incomplete tasks only");
            Console.WriteLine("6. Clear all tasks");
            Console.WriteLine("7. Show task stats");
            Console.WriteLine("8. Import ToDo list to a file");
            Console.WriteLine("9. Import ToDo list from a file");
            Console.WriteLine("10. Exit");
            Console.Write("Enter your choice: ");
            
        }

        // When Option 1 is selected allows the user to input a task if there is space in the array
        static void AddTask()
        {
            if (taskCounter >= 10) // Array max capacity
            {
                Console.WriteLine("Task list is full!");
                return;
            }

            Console.Write("Enter new task: ");
            string task = Console.ReadLine();

            while (task == "")  //Prevents empty tasks
            {
                Console.Write("Task can not be empty. Try again.");
                task = Console.ReadLine();
            }

            todoList[taskCounter] = task; // add task to the array
            taskCounter++; // Increase number of tasks

            Console.WriteLine("Task added succesfully");

        }
        // When option 2 is selected displays all tasks added at the moment
        static void ViewAllTasks()
        {
            if (taskCounter == 0) // No task added yet
            {
                Console.WriteLine("No tasks added yet");
                return;
            }

            for (int i = 0; i < taskCounter; i++)
            {
                Console.WriteLine($"{i + 1}. {todoList[i]}");
            }


        }
        // When option 3 is selected allows user to mark a task done by adding "[DONE] before the task"
        static void MarkTaskComplete()
        {
            if (taskCounter == 0)           // Check if there are tasks
            {
                Console.WriteLine("There are no tasks to complete.");
                return;
            }

            ViewAllTasks(); // Shows tasks before choosing
            Console.Write("Enter task number to mark complete: ");

            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > taskCounter) // validate user input
            {
                Console.WriteLine("Invalid task number. Try again.");
            }

            int index = num - 1;

            if (todoList[index].Contains("[DONE]"))  // Verify if the task hasn't been marked before
            {
                Console.WriteLine("Task is already marked completed");
            }
            else
            {
                todoList[index] = "[DONE] " + todoList[index]; // Add "[DONE]"
                Console.WriteLine("Task marked completed.");
            }
        }
        // When option 4 is selected Allows user to delete and shift the tasks
        static void DeleteTask()
        {
            if (taskCounter == 0)       // Check if there are tasks to delete
            {
                Console.WriteLine("There are no tasks to delete.");
                return;
            }

            ViewAllTasks();
            Console.Write("Enter task number to delete: ");

            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 1 || num > taskCounter) // Verify user input
            {
                Console.WriteLine("Invalid task number. Try again.");
            }

            int index = num - 1;

            for (int i = index; i < taskCounter - 1; i++)
            {
                todoList[i] = todoList[i + 1];  // Shift items 
            }
            
            taskCounter--;  // Reduce number of tasks
            Console.WriteLine("Task was deleted succesfully.");
        }
        // when option 5 is selected shows the tasks that aren't marked completed
        static void ViewIncompleteTask()
        {
            if (taskCounter == 0)       // Check if there are tasks added
            {
                Console.WriteLine("No tasks added yet");
                return;
            }

            int count = 0;  // Count incomplete tasks

            for (int i = 0; i < taskCounter; i++)
            {
                if (!todoList[i].Contains("[DONE] "))  // Task is incomplete
                {
                    Console.WriteLine($"{i + 1}. {todoList[i]}");
                    count++;
                }

            }   
                if (count == 0)  // Every task is complete
                {
                    Console.WriteLine("All tasks completed! Great job!");
                }
        }
        // When option 6 is selected
        static void CLearAllTasks()
        {
            if (taskCounter == 0) // No task added yet
            {
                Console.WriteLine("No tasks added yet");
                return;
            }

            taskCounter = 0;
            Console.WriteLine("All task Cleared!");
        }
        // When option 7 is selected
        static void ShowStats()
        {
            if (taskCounter == 0)       // Check if there are tasks added
            {
                Console.WriteLine("No tasks added yet");
                return;
            }

            int done = 0, notDone = 0; // Counter of the done and not done tasks

            for (int i = 0; i < taskCounter; i++)
            {
                if (todoList[i].Contains("[DONE]")) // If tasks has done means it is completed
                    done++;
                else
                    notDone++;
            }

            Console.WriteLine($"Completed: {done}");
            Console.WriteLine($"Incomplete: {notDone}");
        }
        // When option 8 is selected
        static void ExportToFile()
        {
            string fileName = "todo_export.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < taskCounter;i++)
                {
                    writer.WriteLine(todoList[i]);
                }
            }
            Console.WriteLine($"\nTasks exported succesfully to {fileName}");
        }
        // When Option 9 is selected
        static void ImportFromFile()
        {
            string fileName = "todo_export.txt";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("No export file found. Please export first or check the file name.");
                return;
            }

            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length > 10)
            {
                Console.WriteLine("File has more tasks than available space (10). Import cancelled.");
                return;
            }

            taskCounter = 0;  // Resets the list before importing

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    todoList[taskCounter] = line;
                    taskCounter++;
                }
            }
            Console.WriteLine("Tasks imported succesfully.");
        }
    }
}
