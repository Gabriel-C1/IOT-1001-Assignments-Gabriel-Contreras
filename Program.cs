using System;
using System.IO;

Console.Write("Welcome to the Bill tip Calculator! \n \nEnter the bill amount: ");
string bill_amount = Console.ReadLine();

if (!decimal.TryParse(bill_amount, out decimal bill) || bill <= 0)
{
    Console.WriteLine("Sorry, the entered value is invalid.");
    return;
}

    Console.Write("Enter the tip percentage: ");
string tip_percentage = Console.ReadLine();

if (!decimal.TryParse(tip_percentage, out decimal tip) || tip < 0)
{
    Console.WriteLine("Sorry, the entered value is invalid.");
    return;
}

decimal  total_tip = bill * tip / 100;
decimal total_bill = bill + total_tip;
Console.WriteLine($"Your total tip will be: {total_tip} ");
Console.WriteLine($"Your total bill is: {total_bill}");

Console.Write("Do you want to split the bill? Y/N : ");
string choice = Console.ReadLine();

switch (choice)
{
    case "Y":
    Console.Write("Enter the amount of people: ");
    string num_people = Console.ReadLine();
        if (!int.TryParse(num_people, out int people) || people <= 0)
        {
            Console.WriteLine("Sorry, The input value is invalid.");
            return;
        }
        decimal split = ((bill + total_tip) / people);
        Console.WriteLine($"The total amount per person is: {split}");

        string[] names = new string[people];
        for (int i = 0; i < people; i++)
        {
            Console.WriteLine($"Enter the name of the person #{i + 1}: ");
            names[i] = Console.ReadLine();
        }

        foreach (string name in names)
        {
            string fileName = $"{name}.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(name);
                writer.WriteLine($"Total: {total_bill}");
                writer.WriteLine($"Split into {people} persons {split}");
            }
            Console.WriteLine($"Created file: {fileName}");
        }
        break;

    case "N":
        Console.WriteLine($"Your total bill is: {total_tip + bill}");
        break;
    default:
        Console.WriteLine("Is not a valid answer, Next time answer Y or N.");
        break;
}