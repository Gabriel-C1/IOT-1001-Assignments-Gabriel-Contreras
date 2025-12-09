# AA5 – Lost Treasure Adventure Game (C#)

A text-based adventure game where the player explores an island, collects items, and tries to find the treasure.

## Added Features for Applied Activity 6 (Q2)
- A new menu option **"Log Status"** was added to every scene.
- When selected, the program writes a row to `<playerName>.csv`
- The CSV contains:
  - playerHealth  
  - hasKey  
  - hasTorch  
  - treasureFound  
  - itemCount  
  - inventoryItem1  
  - inventoryItem2  
  - inventoryItem3  
- Each log action appends a new line to the CSV.

A sample CSV file is included (e.g., `Gabriel.csv`).

## How to Run
1. Open the project in Visual Studio.
2. Run the game.
3. Choose any adventure path.
4. Select **"Log Status"** at any moment to generate CSV entries.
