using System;
using System.IO;

namespace Lost_Treasure_Game
{
    internal class Adventure
    {
        // Principal variables
        static string playerName;
        static int playerHealth = 100;
        static bool hasKey = false;
        static bool hasTorch = false;
        static string[] inventory = new string[3];
        static int itemCount = 0;
        static bool treasureFound = false;
        static void Main(string[] args)
        {
            bool playAgain = true;

            // Loops the game till user wants
            while (playAgain)
            {
                Console.Clear();
                playerHealth = 100;
                hasKey = false;
                hasTorch = false;
                treasureFound = false;
                itemCount = 0;

                // Clear inventory when playing again
                for (int i = 0; i < inventory.Length; i++)
                {
                    inventory[i] = null;
                }

                StartGame();
                Scene1_Beach();

                Console.WriteLine("Want to play again? (yes/no)");
                string answer = Console.ReadLine().ToLower();
                playAgain = (answer == "yes" || answer == "y");
            }
            Console.WriteLine("Thanks for playing The Lost Treasure Game!");
        }
        // Shows player info and stats
        static void DisplayStauts()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Player: " + playerName);
            Console.WriteLine("Health: " + playerHealth);
            Console.WriteLine("Inventory:");

            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] != null)
                {
                    Console.WriteLine("- " + inventory[i]);
                }
            }
            Console.WriteLine("------------------------");
        }
        // Displays welcome message and instructions
        static void StartGame()
        {
            Console.WriteLine("Welcome to the Lost Treasure Game!");
            Console.Write("Enter your name: ");
            playerName = Console.ReadLine();

            Console.WriteLine($"\nHello {playerName}! Explore the island, find the treasure and survive!");
            Console.WriteLine("You can only carry 3 items. Choose wisely.\n");
        }
        // Creates a file with the status
        static void LogStatus()
        {
            string fileName = $"{playerName}.csv";
            
            // Prepare inventory for file
            string item1 = inventory[0] ?? "NA";
            string item2 = inventory[1] ?? "NA";
            string item3 = inventory[2] ?? "NA";

            string line =
                $"{playerHealth},{hasKey},{hasTorch},{treasureFound},{itemCount},{item1},{item2},{item3}";

            File.AppendAllText(fileName, line + Environment.NewLine);

            Console.WriteLine($"\nStatus logged to file: {fileName}");
        }
        // Function that is called every time an item is added
        static void AddItem(string itemName)
        {
            if (itemCount < 3)
            {
                inventory[itemCount] = itemName;
                itemCount++;
                Console.WriteLine(itemName + " added to your inventory!");
            }
            else
            {
                Console.WriteLine("Your inventory is full! You cannot take the: " + itemName);
            }
        }
        // Scene when user starts playing and has to make a decision
        static void Scene1_Beach()
        {
            Console.Clear();
            Console.WriteLine("You wake up on a beach in a mysteroius island.");
            DisplayStauts();

            int choice = 0;

            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("1) Explore the jungle");
                Console.WriteLine("2) Search the beach");
                Console.WriteLine("3) Log Status");
                Console.Write("Choose: ");

                int.TryParse(Console.ReadLine(), out choice);

                if (choice == 3)
                {
                    LogStatus();
                    choice = 0;
                }
            }

            if (choice == 1)
                Scene2_Jungle();
            else
                Scene2_BearchSearch();
        }
        // Scene where the user gets a sword and have to make a choice 
        static void Scene2_Jungle()
        {
            Console.Clear();
            Console.WriteLine("\nYou enter the jungle. A wild animal suddenly attacks you! ");
            // When coming back from other place check if user already has a sword
            if (Array.Exists(inventory, item => item == "Sword"))
            {
                playerHealth -= 40;
            }
            else
            {
                Console.WriteLine("\nYou entered the jungle. A wild animal suddenly attacks you! ");

                playerHealth -= 50;
                Console.WriteLine("You escaped the wild animal but you lost 20 health while trying!");
                Console.WriteLine(".......");
                Console.WriteLine("You found something in the floor");
                Console.WriteLine("It is a sword!! It will help you to reduce 20% damage from further attacks.");

                AddItem("Sword");
            }
            DisplayStauts();


            if (playerHealth <= 0)
            {
                Endgame("Lose");
                return;
            }

            int choice = 0;

            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("1) Go to the village.");
                Console.WriteLine("2) Look around for items.");
                Console.WriteLine("3) Log Status");
                Console.Write("Choose: ");

                int.TryParse(Console.ReadLine(), out choice);

                if (choice == 3)
                {
                    LogStatus();
                    choice = 0;
                }
            }

            if (choice == 1)
                Scene3_Village();
            else
            {
                // Check if user already has torch in the inventory
                if (!hasTorch)
                {
                    Console.WriteLine("You found a torch on the ground");
                    AddItem("Torch");
                    hasTorch = true;
                }
                Scene3_Village();
            }
        }
        // Scene where the usser founds a bow in the beach and goes to the cave
        static void Scene2_BearchSearch()
        {
            Console.Clear();
            Console.WriteLine("\nYou found a bow stuck in the sand.");
            AddItem("Bow");

            DisplayStauts();

            int choice = 0;
            while (choice != 1)
            {
                Console.WriteLine("1) Continue to the dark cave");
                Console.WriteLine("2) Log Status");
                Console.Write("Choose: ");

                int.TryParse(Console.ReadLine(), out choice);

                if (choice == 2)
                {
                    LogStatus();
                    choice = 0;
                }
            }
           
            Scene3_Cave();
        }
        // Scene where user gets in to a cave with 2 possible outcomes depending if has torch
        static void Scene3_Cave()
        {
            Console.Clear();
            Console.WriteLine("\nYou arrived at a dark cave.");

            if (!hasTorch)
            {
                Console.WriteLine("You entered without a torch... it's to darrkkk!!");
                Console.WriteLine("You fall into the void and get trap");
                playerHealth = 0;
                Endgame("Trap");
                return;
            }

            Console.WriteLine("Your torch lights up the path safely.");
            Console.WriteLine("Walking in the path you founded a mysterious key!");

            hasKey = true;
            AddItem("Key");
            DisplayStauts();

            int choice = 0;
            while (choice != 1)
            {
                Console.WriteLine("1) Continue deeper into the dark cave");
                Console.WriteLine("2) Log Status");
                Console.Write("Choose: ");

                int.TryParse(Console.ReadLine(), out choice);

                if (choice == 2)
                {
                    LogStatus();
                    choice = 0;
                }
            }

            Scene4_Treasure();
        }
        // Scene where player gets to village to get heal
        static void Scene3_Village()
        {
            Console.Clear();
            Console.WriteLine("You arrive to a small village.");
            Console.WriteLine("A villager heals you +20 health.");
            playerHealth += 20;

            if (playerHealth > 100) playerHealth = 100;
            DisplayStauts();

            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("1) Go to the Cave.");
                Console.WriteLine("2) Return to the jungle.");
                Console.WriteLine("3) Log Status");
                Console.Write("Choose: ");

                int.TryParse(Console.ReadLine(), out choice);

                if (choice == 3)
                {
                    LogStatus();
                    choice = 0;
                }
            }

            if (choice == 1)
                Scene3_Cave();
            else
                Scene2_Jungle();
        }
        // Scene when treasure is found with or without key
        static void Scene4_Treasure()
        {
            Console.Clear();
            Console.WriteLine("\nYou discover the treasure chamber!!");
            //Verify if player has the key to open chest
            if (!hasKey)
            {
                Console.WriteLine("You try to open the chest without the key...");
                Console.WriteLine("A trap activates!!");
                Endgame("Trap");
                return;
            }

            Console.WriteLine("You use the key and open the treasure chest!");
            Endgame("Win");
        }
        // Contains every possible outcome of the game 
        static void Endgame(string outcome)
        {
            Console.WriteLine("\n==============GAME OVER============");

            if (outcome == "Win")
                Console.WriteLine("Congratulations!! You found the treasure and survive the island!");
            else if (outcome == "Trap")
                Console.WriteLine("You fell in to void and got trapped.... You lose");
            else
                Console.WriteLine("Your health reached 0. You died");

            // Show the REAL stats before resetting
            DisplayStauts();

            // Make final log of the game
            Console.WriteLine("\n(Logging final stats...)");
            LogStatus();

            // Pause so player sees info BEFORE reset happens in Main loop
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
} 