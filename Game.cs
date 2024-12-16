using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    public class Game
    {
        private Player player;
        private Dictionary<string, Room> rooms;
        private bool isGameOver;
        private bool ladderCrafted = false;
        private bool hasTalkedToFrog = false;
        private bool hasFairyHouseRevealed = false;
        private bool hasBedroomKey = false;
        private bool hasRecipe = false;
        private bool isBedroomLightOn = false;
        private bool isKitchenLightOn = false;
        private bool hasAmethyst = false;
        private bool hasWhisker = false;
        private bool hasFoundBottle = false;
        private bool hasFoundMoss = false;
        private bool hasFoundMushroom = false;
        private bool hasPotionBrewed = false;

        public Game()
        {
            InitializeGame();
        }
        private void InitializeGame()
        {

            rooms = new Dictionary<string, Room> //create room directory
            
        {
            { "Woods", new Room("Woods",$"A dense forest with tall trees. Sunlight filters through the thick canopy. {Environment.NewLine}" +
            $"The ground is covered in soft moss and scattered fallen branches. You think you see a rope on the tree.{Environment.NewLine}" +
            "You notice a hidden opening in the ground, but it's too deep to jump down safely.") },
            { "Fairy Portal", new Room("Fairy Portal", $"A mysterious portal hidden beneath the forest floor. {Environment.NewLine}" +
            "You entered this mysterious land when you stepped into the mushroom ring, do you silly humans not know? " +
            $"You will have to find a way home, the portal will not let you cross back to your own realm... {Environment.NewLine}" +
            "A thick, magical, haze surrounds you. The haze obscures your view of this new realm.") },
            { "Fairy House", new Room("Fairy House", "A whimsical, delicate dwelling that seems to shimmer between reality and fantasy. " +
            $"Intricate wooden carvings cover the walls, and soft, magical light fills the space.{Environment.NewLine}" +
            "You notice several interesting objects: a curious frog sitting on a cushion, " +
            "an ornate desk, a staircase leading up (UP), and a door leading to another room (NORTH).") },
            { "Bedroom", new Room("Bedroom", "A dark bedroom. You can barely make out shapes in the darkness, " +
            $"but you sense there's a light switch near the door. {Environment.NewLine}" +
            "You can faintly hear purring somewhere in the room.") },
            { "Kitchen", new Room("Kitchen", $"A dark kitchen. You can feel a light switch on the wall near the entrance. {Environment.NewLine}" +
            "The air smells of herbs and magic.") }
        };

            //Set up room exits 
            RoomExits();

            //Add items to rooms
            AddRoomItems();


            //initialize player
            player = new Player("Explorer");
            player.CurrentRoom = rooms["Woods"];

            isGameOver = false;

        }
        private void RoomExits()
        {

            rooms["Woods"].AddExit("down", "Fairy Portal");


            rooms["Fairy Portal"].AddExit("up", "Woods");
            rooms["Fairy Portal"].AddExit("west", "Fairy House");


            rooms["Fairy House"].AddExit("east", "Fairy Portal");
            rooms["Fairy House"].AddExit("north", "Kitchen");
            rooms["Fairy House"].AddExit("up", "Bedroom");


            rooms["Bedroom"].AddExit("down", "Fairy House");


            rooms["Kitchen"].AddExit("south", "Fairy House");
        }
        private void AddRoomItems()
        {
            
            rooms["Woods"].AddItem(new Item(
                "LargeStick",
                "A sturdy oak branch about two feet long. It looks perfectly straight and strong enough to support weight."
            ));

            rooms["Woods"].AddItem(new Item(
                "SmallStick",
                "A smooth, slightly curved maple branch. It's lighter than the first stick but still seems reliable."
            ));

            rooms["Woods"].AddItem(new Item(
                "Rope",
                "An old, but well-preserved hemp rope. It's frayed at the edges but appears to be quite strong. "
            ));

            
            rooms["Fairy Portal"].AddItem(new Item(
                "Glasses",
                "A pair of delicate circular glasses, they appear to glow... They seem to be crafted from some sort of crystalline material. " +
                "Something urges you to put them on..."
            ));

            
            rooms["Fairy House"].AddItem(new Item(
                "Frog",
                "A peculiar, well-dressed frog sitting on a velvet cushion." +
                "They are reading a book, but put it down for a moment as you enter the house."
            ));

            rooms["Fairy House"].AddItem(new Item(
                "Desk",
                "An elegant wooden desk with intricate fairy-tale carvings. It looks like it might hold secrets."
            ));

            rooms["Fairy House"].AddItem(new Item(
                "Stairs",
                "A winding wooden staircase leading up to a bedroom. A locked door is visible at the top." + "UP"
            ));

            rooms["Fairy House"].AddItem(new Item(
                "Door",
                "A wooden door leading to what appears to be a kitchen." + "NORTH"
            ));
            
            rooms["Bedroom"].AddItem(new Item(
                "Light Switch",
                "A simple switch mounted on the wall."
            ));
            rooms["Bedroom"].AddItem(new Item(
                "Shelves",
                "A variety of items are placed on shelves all over the room, they display various things. You spot a sparkling purple rock displayed proudly."
            ));
            rooms["Bedroom"].AddItem(new Item(
                "Cat",
                "A large black cat stretches, and meows at you as if expecting something."
            ));
            
            rooms["Kitchen"].AddItem(new Item(
                "Light Switch",
                "A simple switch mounted on the wall."
            ));
            rooms["Kitchen"].AddItem(new Item(
            "Shelves",
            "Floor-to-ceiling shelves filled with mysterious ingredients and magical supplies."
            ));
            rooms["Kitchen"].AddItem(new Item(
                "Potion Stand",
                "A sturdy wooden stand with various attachments for brewing potions. A small fire burns beneath it."
            ));

        }
        public void Start()
        {
            Console.WriteLine("Welcome to the Magical Adventure!");
            Console.WriteLine(player.CurrentRoom.Description);

            while (!isGameOver)
            {
                Console.Write("What do you want to do? Type 'help' for command options.");
                string input = Console.ReadLine().Trim().ToLower();
                ProcessCommand(input);
            }
        }
        private void ProcessCommand(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0].ToLower();
            string subject = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

            if (parts.Length >= 3)
            {
                if (parts[0].ToLower() == "turn" && parts[1].ToLower() == "light")
                {
                    string action = parts[2].ToLower();
                    if (action == "on")
                    {
                        TurnLight(true);
                        return;
                    }
                    else if (action == "off")
                    {
                        TurnLight(false);
                        return;
                    }
                }
                if (parts[0].ToLower() == "talk" &&
                    parts[1].ToLower() == "to" &&
                    parts[2].ToLower() == "frog")
                {
                    TalkToFrog();
                    return;
                }
            }
            switch (command)
            {
                case "look":
                    LookAround();
                    break;
                case "inventory":
                    ShowInventory();
                    break;
                case "take":
                    if (parts.Length > 1)
                        TakeItem(parts[1]);
                    break;
                case "go":
                    if (parts.Length > 1)
                        GoDirection(parts[1]);
                    break;
                case "talk":
                    if (parts.Length > 1 && parts[1].ToLower() == "frog")
                        TalkToFrog();
                    break;
                case "craft":
                    CraftLadder();
                    break;
                case "quit":
                    isGameOver = true;
                    Console.WriteLine("Thanks for playing!");
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "search":
                    if (subject.ToLower() == "desk")
                        SearchDesk();
                    else if (subject.ToLower() == "shelves")
                        SearchShelves();
                    break;
                case "pet":
                    if (subject.ToLower() == "cat")
                        PetCat();
                    break;
                case "brew":
                    BrewPotion();
                    break;
                case "drink":
                    if (subject.ToLower() == "potion")
                        DrinkPotion();
                    break;
                default:
                    Console.WriteLine("I don't understand that command.");
                    break;
            }
        }
        public void ProcessString(string enteredString)
        {
            enteredString =
                enteredString.Trim()
                    .ToLower(); //trim any white space from the beginning or end of string and convert to lower case
            string[]
                commands = enteredString
                    .Split(" "); //split based on spaces. The length of this array will tell you whether you have 1, 2, 3, 4, or more commands.
            //modify these functions however you want, but this is where the beginning of calling functions to handle where you are
            string
                response =
                    "Default response"; //you will always do something when processing the string and then give a response
            Console.WriteLine(response); //what you tell the person after what they entered has been processed
        }
        private void GoDirection(string direction) 
        {

            string destinationRoomName = player.CurrentRoom.GetExit(direction);

            if (destinationRoomName != null)
            {
                if (player.CurrentRoom.Name == "Fairy Portal" && destinationRoomName == "Woods")
                {
                    Console.WriteLine("The opening above seems to have sealed itself. You'll need to find another way back...");
                    return;
                }
                if (destinationRoomName == "Fairy House" && !hasFairyHouseRevealed)
                {
                    Console.WriteLine("Everything looks hazy and indistinct. You can't make out a clear path.");
                    return;
                }
                if (destinationRoomName == "Fairy Portal" && !ladderCrafted)
                {
                    Console.WriteLine("Ya you can't make that jump, you're going to need something to climb down.");
                    return;
                }
                if (destinationRoomName == "Bedroom" && !hasBedroomKey)
                {
                    Console.WriteLine("Umm that's locked, you're going to need a key.");
                    return;
                }

                // Move to the new room
                player.CurrentRoom = rooms[destinationRoomName];

                Console.Clear();
                Console.WriteLine($"You go {direction} to the {player.CurrentRoom.Name}.");
                Console.WriteLine(player.CurrentRoom.Description);

                // Show available exits
                ShowAvailableExits();
            }
            else
            {
                Console.WriteLine($"You cannot go {direction} from here.");
            }
        }
        private void ShowAvailableExits()
        {
            var currentExits = player.CurrentRoom.Exits.Keys;

            if (currentExits.Count > 0)
            {
                Console.WriteLine("Available exits:");
                foreach (var exit in currentExits)
                {
                    Console.WriteLine($"- {exit.ToUpper()}");
                }
            }
            else
            {
                Console.WriteLine("There are no visible exits from this room.");
            }
        }
        private void LookAround()
        {
            Console.WriteLine($"\nYou are in the {player.CurrentRoom.Name}.");
            Console.WriteLine(player.CurrentRoom.Description);

            bool canSeeItems = true;
            if (player.CurrentRoom.Name == "Bedroom" && !isBedroomLightOn)
                canSeeItems = false;
            if (player.CurrentRoom.Name == "Kitchen" && !isKitchenLightOn)
                canSeeItems = false;

            if (canSeeItems && player.CurrentRoom.Items.Count > 0)
            {
                Console.WriteLine("You see the following items:");
                foreach (var item in player.CurrentRoom.Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
            else if (!canSeeItems)
            {
                Console.WriteLine("\nIt's too dark to see anything clearly.");
            }

            ShowAvailableExits();
        }
        private void ShowInventory()
        {
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Inventory:");
                foreach (var item in player.Inventory)
                {
                    if (item.Name == "Recipe")
                    {
                        // Store the current console color
                        ConsoleColor originalColor = Console.ForegroundColor;

                        // Change text color to magenta for the recipe
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"- {item.Name}: {item.Description}");

                        // Reset console color back to original
                        Console.ForegroundColor = originalColor;
                    }
                    else
                    {
                        Console.WriteLine($"- {item.Name}: {item.Description}");
                    }
                }
            }
        }
        private void TakeItem(string itemName)
        {
            Item item = player.CurrentRoom.GetItem(itemName);
            if (itemName == "key" || itemName == "bedroom key")
            {
                item = player.CurrentRoom.GetItem("Key");
            }
            else if (itemName == "recipe" || itemName == "potion recipe")
            {
                item = player.CurrentRoom.GetItem("Recipe");
            }
            else
            {
                item = player.CurrentRoom.GetItem(itemName);
            }

            if (item != null)
            {
                player.AddToInventory(item);
                player.CurrentRoom.RemoveItem(item);
                Console.WriteLine($"You picked up the {item.Name}.");

                // Special logic for glasses
                if (itemName == "glasses")
                {
                    RevealFairyHouse();
                }
            }
            else
            {
                Console.WriteLine($"There is no {itemName} here.");
            }
        }
        private void CraftLadder()
        {
            bool hasLargeStick = player.Inventory.Exists(i => i.Name == "LargeStick");
            bool hasSmallStick = player.Inventory.Exists(i => i.Name == "SmallStick");
            bool hasRope = player.Inventory.Exists(i => i.Name == "Rope");

            if (hasLargeStick && hasSmallStick && hasRope)
            {
                // Remove crafting materials
                player.Inventory.RemoveAll(i => i.Name == "LargeStick");
                player.Inventory.RemoveAll(i => i.Name == "SmallStick");
                player.Inventory.RemoveAll(i => i.Name == "Rope");

                // Add crafted ladder
                player.AddToInventory(new Item("Ladder", "A makeshift ladder crafted from two sticks and a rope."));
                Console.WriteLine("You successfully crafted a ladder! This should help you get down to check out that hole.");

                ladderCrafted = true;
            }
            else
            {
                Console.WriteLine("You don't have the right materials to craft a ladder.");
            }
        }
        private void RevealFairyHouse()
        {
            if (!hasFairyHouseRevealed)
            {
                hasFairyHouseRevealed = true;
                Console.Clear();
                Console.WriteLine("--- MAGICAL REVELATION ---");
                Console.WriteLine("As you put on the glasses, the world around you shifts and becomes clear.");
                Console.WriteLine("The haze no longer obstructs your view. A previously invisible path to the WEST now becomes visible!" +
                    "Through the trees you see smoke, a chimney peaks through the dense tree line. There must be a house!");
            }
        }
        private void TalkToFrog()
        {
            if (player.CurrentRoom.Name != "Fairy House")
            {
                Console.WriteLine("There's no frog to talk to here.");
                return;
            }

            if (!hasTalkedToFrog)
            {
                Console.Clear();
                Console.WriteLine("The frog adjusts its tiny spectacles and speaks in a refined voice:");
                Console.ReadLine();
                Console.WriteLine("Ah, a visitor! We don't get many of you in our parts... You must be lost!");
                Console.ReadLine();
                Console.WriteLine($"Oh you didn't mean to come here.. What a shame, it is a lovely realm. {Environment.NewLine}" +
                    "To leave this magical realm, you must brew a special potion. " +
                    "Luckily my boss should have everything you should need right in this very house!");
                Console.ReadLine();
                Console.WriteLine("Oh the recipe? Ya I don't know that one, I can't solve all of your problems!");
                Console.ReadLine();
                Console.WriteLine($"I think boss keeps it in her desk over there. {Environment.NewLine}" +
                    $"Don't tell her I told you, but she keeps a spell book in the hidden drawer in her desk {Environment.NewLine}" +
                    $"She claims she knows all of the spells in the realm by heart... But I know... {Environment.NewLine}" +
                    "and I guess you now know too.....");
                Console.ReadLine();
                Console.WriteLine($"The frog looks at you, this time with an odd look on their face... {Environment.NewLine}" +
                    "Lets hurry to make that potion and get out of here!");
                hasTalkedToFrog = true;
            }
            else
            {
                Console.WriteLine("The frog nods knowingly. 'If you really want to leave, you will need to potion to activate the portal'");
            }
        }
        private void SearchDesk()
        {
            if (player.CurrentRoom.Name != "Fairy House")
            {
                Console.WriteLine("There's no desk to search here.");
                return;
            }

            if (!hasBedroomKey)
            {

                Console.WriteLine("You search the top of the desk and find a small, ornate key." +
                    "This must open something close!");
                rooms["Fairy House"].AddItem(new Item(
                "Key",
                "A delicate, silver key with intricate fairy-tale engravings."
                ));

                hasBedroomKey = true;

            }

            if (!hasRecipe)
            {
                Console.WriteLine($"Searching the desk drawer, you find a hidden compartment... {Environment.NewLine}" +
                    "There is a large book inside. " +
                    "Just your luck! The first page contains scribbled writing, but you can make out 'portal potion'");
                Console.ForegroundColor = ConsoleColor.Magenta;
                rooms["Fairy House"].AddItem(new Item(
                "Recipe",
                $"A mysterious recipe with ingredients for a realm-crossing potion: {Environment.NewLine}" +
                $"One cat whisker (Given willingly!), shard of amethyst, mushroom essence, and moss {Environment.NewLine}" +
                "Combine in a glass bottle, brew over medium heat for 20 seconds."
                ));
                Console.ResetColor();

                hasRecipe = true;
            }
            if (hasBedroomKey && hasRecipe)
            {
                Console.WriteLine("You've already searched the desk thoroughly.");
            }
        }
        private void TurnLight(bool turnOn)
        {
            if (player.CurrentRoom.Name == "Bedroom")
            {
                if (turnOn == isBedroomLightOn)
                {
                    Console.WriteLine($"The light is already {(turnOn ? "on" : "off")}.");
                    return;
                }

                isBedroomLightOn = turnOn;
                if (isBedroomLightOn)
                {
                    Console.WriteLine("You turn on the light and the bedroom illuminates.");
                    player.CurrentRoom.UpdateDescription(
                        "A cozy bedroom with moonlight filtering through delicate curtains. " +
                        "Wooden shelves line the walls, filled with magical items and crystals. " +
                        "A beautiful black cat lounges on a plush cushion, watching you with interest."
                    );
                    LookAround();
                }
                else
                {
                    Console.WriteLine("You turn off the light and the room plunges into darkness.");
                    player.CurrentRoom.UpdateDescription(
                        "A dark bedroom. You can barely make out shapes in the darkness, " +
                        "but you sense there's a light switch near the door."
                    );
                }
            }
            else if (player.CurrentRoom.Name == "Kitchen")
            {
                if (turnOn == isKitchenLightOn)
                {
                    Console.WriteLine($"The light is already {(turnOn ? "on" : "off")}.");
                    return;
                }

                isKitchenLightOn = turnOn;
                if (isKitchenLightOn)
                {
                    Console.WriteLine("You turn on the light and the kitchen illuminates.");
                    player.CurrentRoom.UpdateDescription(
                        "A rustic kitchen with shelves lined with mysterious ingredients. " +
                        "Every shelf and counter is packed full, beside the stove there is a brewing stand."
                    );
                    LookAround();
                }
                else
                {
                    Console.WriteLine("You turn off the light and the room plunges into darkness.");
                    player.CurrentRoom.UpdateDescription(
                        "A dark kitchen. You can feel a light switch on the wall near the entrance."
                    );
                }
            }
            else
            {
                Console.WriteLine("There's no light switch here.");
            }
        }
        private void SearchShelves()
        {
            if (player.CurrentRoom.Name == "Bedroom")
            {
                if (!isBedroomLightOn)
                {
                    Console.WriteLine("It's too dark to search the shelves. You should turn on the light first.");
                    return;
                }

                if (!hasAmethyst)
                {
                    Console.WriteLine("You carefully search the shelves, moving aside various magical trinkets...");
                    Console.WriteLine("Beside a stack of old books, you find a beautiful amethyst crystal!");
                    player.AddToInventory(new Item(
                        "Amethyst",
                        "A deep purple crystal humming with magical energy."
                    ));
                    hasAmethyst = true;
                }
            }
            if (player.CurrentRoom.Name == "Kitchen")
            {
                if (!isKitchenLightOn)
                {
                    Console.WriteLine("It's too dark to search the shelves. You should turn on the light first.");
                    return;
                }
                if (!hasFoundBottle)
                {
                    Console.WriteLine("\nSearching the lower shelves, you find a delicate glass bottle!");
                    player.AddToInventory(new Item(
                        "Glass Bottle",
                        "A small, ornate bottle perfect for holding magical potions."
                    ));
                    hasFoundBottle = true;
                }
                if (!hasFoundMoss)
                {
                    Console.WriteLine("\nBehind some jars, you discover a container of glowing moss!");
                    player.AddToInventory(new Item(
                        "Moss",
                        "Luminescent moss that seems to pulse with magical energy."
                    ));
                    hasFoundMoss = true;
                }
                if (!hasFoundMushroom)
                {
                    Console.WriteLine("\nOn the top shelf, you spot a vial of mysterious mushroom essence!");
                    player.AddToInventory(new Item(
                        "Mushroom Essence",
                        "A concentrated extract from magical mushrooms."
                    ));
                    hasFoundMushroom = true;
                }

                if (hasFoundBottle && hasFoundMoss && hasFoundMushroom)
                {
                    Console.WriteLine("\nYou've found all the ingredients from the shelves!");
                }
            }
            else
            {
                Console.WriteLine("There are no shelves here to search.");
            }
        }
        private void PetCat()
        {
            if (player.CurrentRoom.Name != "Bedroom")
            {
                Console.WriteLine("There's no cat here to pet.");
                return;
            }

            if (!isBedroomLightOn)
            {
                Console.WriteLine("You can hear rustling...");
                return;
            }

            if (!hasWhisker)
            {
                Console.WriteLine("You gently pet the mysterious black cat. It purrs loudly and rubs against your hand.");
                Console.WriteLine("As you stroke its fur, a loose whisker floats into your hand.");
                player.AddToInventory(new Item(
                    "Cat Whisker",
                    "A delicate, mystical whisker from the magical cat. It seems to shimmer with an inner light."
                ));
                hasWhisker = true;
            }
            else
            {
                Console.WriteLine("The cat purrs contentedly as you pet it, but doesn't offer any more whiskers.");
            }
        }
        private void BrewPotion()
        {
            if (player.CurrentRoom.Name != "Kitchen")
            {
                Console.WriteLine("You need to be in the kitchen to brew a potion.");
                return;
            }

            if (!isKitchenLightOn)
            {
                Console.WriteLine("It's too dark to brew anything. Turn on the light first.");
                return;
            }

            if (hasPotionBrewed)
            {
                Console.WriteLine("You've already brewed the potion.");
                return;
            }

            bool hasBottle = player.Inventory.Exists(i => i.Name == "Glass Bottle");
            bool hasMoss = player.Inventory.Exists(i => i.Name == "Moss");
            bool hasMushroom = player.Inventory.Exists(i => i.Name == "Mushroom Essence");
            bool hasAmethyst = player.Inventory.Exists(i => i.Name == "Amethyst");
            bool hasWhisker = player.Inventory.Exists(i => i.Name == "Cat Whisker");
            bool hasRecipe = player.Inventory.Exists(i => i.Name == "Recipe");

            if (!hasRecipe)
            {
                Console.WriteLine("You should find the recipe first before attempting to brew anything.");
                return;
            }

            if (hasBottle && hasMoss && hasMushroom && hasAmethyst && hasWhisker)
            {
                player.Inventory.RemoveAll(i =>
                    i.Name == "Glass Bottle" ||
                    i.Name == "Moss" ||
                    i.Name == "Mushroom Essence" ||
                    i.Name == "Amethyst" ||
                    i.Name == "Cat Whisker"
                );

                player.AddToInventory(new Item(
                    "Magic Potion",
                    "A swirling, iridescent potion that seems to contain a universe within. It's your ticket home."
                ));

                Console.WriteLine("\nYou carefully follow the recipe, combining all ingredients...");
                Console.WriteLine("The mixture bubbles and swirls, changing colors rapidly...");
                Console.WriteLine("Success! You've brewed the realm-crossing potion!");

                hasPotionBrewed = true;
            }
            else
            {
                Console.WriteLine("You're missing some ingredients. You need:");
                if (!hasBottle) Console.WriteLine("- A Glass Bottle");
                if (!hasMoss) Console.WriteLine("- Moss");
                if (!hasMushroom) Console.WriteLine("- Mushroom Essence");
                if (!hasAmethyst) Console.WriteLine("- Amethyst");
                if (!hasWhisker) Console.WriteLine("- Cat Whisker");
            }
        }
        private void DrinkPotion()
        {
            bool hasPotion = player.Inventory.Exists(i => i.Name == "Magic Potion");

            if (!hasPotion)
            {
                Console.WriteLine("You don't have any potion to drink.");
                return;
            }

            if (player.CurrentRoom.Name == "Fairy Land Portal")
            {
                Console.WriteLine("\n--- RETURNING HOME ---");
                Console.WriteLine("You drink the magical potion. The world begins to spin...");
                Console.WriteLine("Colors swirl around you as reality bends...");
                Console.WriteLine("You find yourself back in the Woods, your adventure complete!");

                player.CurrentRoom = rooms["Woods"];
                isGameOver = true;
            }
            else
            {
                Console.WriteLine("You should return to the Fairy Land Portal before drinking the potion.");
            }
        }
        private void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("- look: Examine the current room");
            Console.WriteLine("- inventory: Check your items");
            Console.WriteLine("- take [item]: Pick up an item");
            Console.WriteLine($"- go [direction]: Move to another room {Environment.NewLine}    -Options: up, down, north, south, east, west");
            Console.WriteLine("- craft: Create items if you have the right materials");
            Console.WriteLine("- search: search desk/shelves");
            Console.WriteLine("- light: turn light on/turn light off");
            Console.WriteLine("- pet: pet animal");
            Console.WriteLine("- brew: brew potion");
            Console.WriteLine("- drink: consume beverage");
            Console.WriteLine("- help: Show this help menu");

        }

    }

}
