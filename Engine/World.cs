﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monster> Monsters = new List<Monster>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RACCOON_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR = 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_FISH_SCALE = 8;
        public const int ITEM_ID_FISH_EGG = 9;
        public const int ITEM_ID_CANOE = 10;

        public const int UNSELLABLE_ITEM_PRICE = -1;

        public const int MONSTER_ID_RACCOON = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_FISH = 3;

        public const int QUEST_ID_CLEAR_OPEN_FIELD = 1;
        public const int QUEST_ID_CLEAR_CREEK = 2;

        public const int LOCATION_ID_CAMPSITE = 1;
        public const int LOCATION_ID_MAIN_ROAD = 2;
        public const int LOCATION_ID_DAM = 3;
        public const int LOCATION_ID_LAKE = 4;
        public const int LOCATION_ID_BIG_CABLE = 5;
        public const int LOCATION_ID_CREEK_LET_IN = 6;
        public const int LOCATION_ID_NE_CREEK = 7;
        public const int LOCATION_ID_NORTH_CREEK = 8;
        public const int LOCATION_ID_CREEPY_BUS = 9;
        public const int LOCATION_ID_OPEN_FIELD = 10;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5, 5));
            Items.Add(new Item(ITEM_ID_RACCOON_TAIL, "Raccoon tail", "Raccoon tails", 1));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur", 1));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs", 1));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins", 2));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10, 8));
            Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION,
                "Healing potion", "Healing potions", 5, 3));
            Items.Add(new Item(ITEM_ID_FISH_SCALE, "Fish scale", "Fish Scales", 1));
            Items.Add(new Item(ITEM_ID_FISH_EGG, "Fish egg", "Fish eggs", 1));
            Items.Add(new Item(ITEM_ID_CANOE, "Canoe", "Canoes", UNSELLABLE_ITEM_PRICE));
        }

        private static void PopulateMonsters()
        {
            Monster raccoon = new Monster(MONSTER_ID_RACCOON, "Raccoon", 5, 3, 10, 3, 3);
            raccoon.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RACCOON_TAIL), 75, false));
            raccoon.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

            Monster snake = new Monster(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 3, 3);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));

            Monster fish = new Monster(MONSTER_ID_FISH,
                    "Giant spider", 20, 5, 40, 10, 10);
            fish.LootTable.Add(new LootItem(ItemByID(ITEM_ID_FISH_SCALE), 75, true));
            fish.LootTable.Add(new LootItem(ItemByID(ITEM_ID_FISH_EGG), 25, false));

            Monsters.Add(raccoon);
            Monsters.Add(snake);
            Monsters.Add(fish);
        }

        private static void PopulateQuests()
        {
            Quest clearOpenField = new Quest(QUEST_ID_CLEAR_OPEN_FIELD, "Clear the OPEN FIELD",
                "Looks like you will need a canoe to get in the creek. Bring back 3 raccoon tails from the OPEN FIELD south of the DAM and I'll give you one. Plus some gold.",
                20, 10);

            clearOpenField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RACCOON_TAIL), 3));

            clearOpenField.RewardItem = ItemByID(ITEM_ID_CANOE);

            Quest clearCreek = new Quest(QUEST_ID_CLEAR_CREEK, "Clear the NE CREEK",
                "Charlie need snake fangs for making the snake venom. Can make healing potion for you. Will pay you 10 gold if you bring 3 snake fangs.", 20, 10);

            clearCreek.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));

            clearCreek.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);

            Quests.Add(clearOpenField);
            Quests.Add(clearCreek);
        }

        private static void PopulateLocations()
        {
            // Create each location
            Location campsite = new Location(LOCATION_ID_CAMPSITE, "Campsite",
                "The Nayb campsite. A place of magical happenings and 'bro time'. You can go SOUTH to get to the MAIN ROAD");

            Location mainRoad = new Location(LOCATION_ID_MAIN_ROAD,
                "Main road", "The main road. Go WEST to get to the DAM; or EAST to get to the BIG CABLE.");

            Location dam = new Location(LOCATION_ID_DAM,
                "Earthy Dam", "You can see the old camping spot at the end of the dam.  To the NORTH is the LAKE; To EAST is the MAIN ROAD.");
            //alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            Location lake = new Location(LOCATION_ID_LAKE,
                "Lake", "Big ol' concrete thing in it. Great for jumping off. You see some CANOES for the taking. Go back SOUTH for the DAM.", ItemByID(ITEM_ID_CANOE));
            lake.MonsterLivingHere = MonsterByID(MONSTER_ID_FISH);

            Location openField = new Location(LOCATION_ID_OPEN_FIELD, "Open Field", "Big open field in between two hills. Full of varment. NORTH will take you back to the DAM.");
            openField.MonsterLivingHere = MonsterByID(MONSTER_ID_RACCOON);

            Location bigCable = new Location(LOCATION_ID_BIG_CABLE,
                "Big Cable", "Here you see a big cable going across the creek. It'd be cool to use as a zipline. Go NORTH for the CREEK LET-IN; WEST for the MAIN ROAD.");
            //farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

            Location creekLetIn = new Location(LOCATION_ID_CREEK_LET_IN,
                "Creek Let-In", "Access to the creek is here. SOUTH is the BIG CABLE. Go NORTH to get into the creek (need a canoe).",
                ItemByID(ITEM_ID_CANOE));
            creekLetIn.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_OPEN_FIELD);

            Location neCreek = new Location(LOCATION_ID_NE_CREEK,
                "Northeast Creek", "Finally on the creek.  Watch for snakes! Go through the rapids to the WEST to get to the NORTH CREEK. Go SOUTH to exit the creek to the CREEK LET-IN.");

            Location northCreek = new Location(LOCATION_ID_NORTH_CREEK,
                "North Creek", "A relaxing ride down a wide part of the creek. Go SOUTH to get back to the CAMPSITE. A hard paddle back EAST will get you to the NE CREEK.");

            Location creepyBus = new Location(LOCATION_ID_CREEPY_BUS,
                "Creepy Bus", "You see a creepy bus off to the side of the road. As you approach you hear a noise within .. It's Charlie the Hobo! Here to sell you his goods.");
            creepyBus.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_CREEK);

            Vendor charlieTheHobo = new Vendor("Charlie the Hobo");

            charlieTheHobo.AddItemToInventory(ItemByID(ITEM_ID_PIECE_OF_FUR), 5);
            charlieTheHobo.AddItemToInventory(ItemByID(ITEM_ID_RACCOON_TAIL), 3);
            charlieTheHobo.AddItemToInventory(ItemByID(ITEM_ID_CLUB), 1);

            creepyBus.VendorWorkingHere = charlieTheHobo;



            // Link the locations together
            campsite.LocationToSouth = mainRoad;

            mainRoad.LocationToNorth = campsite;
            mainRoad.LocationToSouth = creepyBus;
            mainRoad.LocationToEast = bigCable;
            mainRoad.LocationToWest = dam;

            dam.LocationToNorth = lake;
            dam.LocationToEast = mainRoad;
            dam.LocationToSouth = openField;

            openField.LocationToNorth = dam;

            lake.LocationToSouth = dam;

            bigCable.LocationToWest = mainRoad;
            bigCable.LocationToNorth = creekLetIn;

            creekLetIn.LocationToNorth = neCreek;
            creekLetIn.LocationToSouth = bigCable;

            neCreek.LocationToSouth = creekLetIn;
            neCreek.LocationToWest = northCreek;

            northCreek.LocationToSouth = campsite;
            northCreek.LocationToEast = neCreek;

            creepyBus.LocationToNorth = mainRoad;

            // Add the locations to the static list
            Locations.Add(campsite);
            Locations.Add(mainRoad);
            Locations.Add(dam);
            Locations.Add(lake);
            Locations.Add(openField);
            Locations.Add(creepyBus);
            Locations.Add(bigCable);
            Locations.Add(creekLetIn);
            Locations.Add(neCreek);
            Locations.Add(northCreek);
        }

        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }
            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }
            return null;
        }

    }
}
