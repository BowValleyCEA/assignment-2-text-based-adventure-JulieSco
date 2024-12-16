using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    [Serializable] //the [Serializable] attribute will be needed if you ever want to save this info

    //reference
    public class Room
    {
        private string description;
        public string Name { get; private set; }
        public string Description { get => description; }
        public List<Item> Items { get; private set; }
        public Dictionary<string, string> Exits { get; private set; }

        public Room(string name, string description)
        {
            Name = name;
            this.description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, string>();
        }
        public void UpdateDescription(string newDescription)
        {
            description = newDescription;
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public Item GetItem(string itemName)
        {
            return Items.Find(i => i.Name.ToLower() == itemName.ToLower());
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void AddExit(string direction, string roomName)
        {
            Exits[direction.ToLower()] = roomName;
        }

        public string GetExit(string direction)
        {
            if (Exits.ContainsKey(direction.ToLower()))
            {
                return Exits[direction.ToLower()];
            }
            return null;
        }
    }

    public class Player
    {
        public string Name { get; private set; }
        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; private set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
        }

        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
        }

    }

    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
