using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using LiveSplit.Snes9x;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class ALinkToThePast : Game
    {
        public enum Progress : byte
        {
            START   = 0x00,
            SWORD   = 0x01,
            ZELDA   = 0x02,
            AGAHNIM = 0x03
        }

        [Flags]
        public enum Events1 : byte
        {
            TALKTOUNCLE     = 0x01, // Talk to your uncle in the secret passage
            TALKTOPRIEST    = 0x02, // Talk to the dying priest after Zelda is captured
            SANCTUARY       = 0x04, // Bring Zelda to the sanctuary
            UNCLELEAVES     = 0x10, // Uncle leaves the house
            MUDORA          = 0x20, // Get the Book of Mudora (??)
            FORTUNE         = 0x40  // Toggles between 2 fortune dialogs
        }

        [Flags]
        public enum Events2 : byte
        {
            HOBO        = 0x01, // Received the hobo's bottle
            MERCHANT    = 0x02, // Received the merchant's bottle
            THIEFSCHEST = 0x10, // Purple chest has been opened
            BLACKSMITH  = 0x20, // Returned the frog to his partner
            FORGING     = 0x80  // The blacksmiths currently have your sword
        }

        [Flags]
        public enum SpecialItems : ushort
        {
            OLDMAN          = 0x0001, // Received the mirror from the lost old man
            KINGZORA        = 0x0002, // Received the flippers from King Zora
            SICKKID         = 0x0004, // Received the bug net from the sick kid
            FLUTEBOY        = 0x0008, // Received the flute from the boy in the Dark World Haunted Grove
            SAHASRAHLA      = 0x0010, // Received the boots from Sahasrahla
            CATFISH         = 0x0020, // Received the medallion from the catfish
            LIBRARY         = 0x0080, // Collected the book from the library
            ETHER           = 0x0100, // Received the ether medallion
            BOMBOS          = 0x0200, // Received the bombos medallion
            TEMPEREDSWORD   = 0x0400, // Received the tempered sword from the blacksmiths
            MUSHROOM        = 0x1000, // Collected the mushroom
            WITCH           = 0x2000, // Received the magic powder from the witch
            MADBATTER       = 0x8000  // Received the half magic from the mad batter
        }

        public enum GameModule : byte
        {
            TITLE           = 0x00, // Triforce / Zelda startup screens
            FILESELECT      = 0x01, // Game Select screen
            FILECOPY        = 0x02, // Copy Player Mode
            FILEERASE       = 0x03, // Erase Player Mode
            PLAYERNAME      = 0x04, // Name Player Mode
            FILELOAD        = 0x05, // Loading Game Mode
            DUNGEONLOAD     = 0x06, // Pre Dungeon Mode
            DUNGEON         = 0x07, // Dungeon Mode
            OVERWORLDLOAD   = 0x08, // Pre Overworld Mode
            OVERWORLD       = 0x09, // Overworld Mode
            SPECIALAREALOAD = 0x0A, // Pre Overworld Mode (special overworld)
            SPECIALAREA     = 0x0B, // Overworld Mode (special overworld)
            UNK0            = 0x0C, // ???? I think we can declare this one unused, almost with complete certainty.
            UNK1            = 0x0D, // Blank Screen
            DIALOG          = 0x0E, // Text Mode/Item Screen/Map
            FADEOUT         = 0x0F, // Closing Spotlight
            FADEIN          = 0x10, // Opening Spotlight
            FALLING         = 0x11, // Happens when you fall into a hole from the OW.
            DEATH           = 0x12, // Death Mode
            LWVICTORY       = 0x13, // Boss Victory Mode (refills stats)
            STORY           = 0x14, // History Mode
            MIRRORWARP      = 0x15, // Module for Magic Mirror
            DWVICTORY       = 0x16, // Module for refilling stats after boss.
            RESTART         = 0x17, // Restart mode (save and quit)
            GANONEMERGES    = 0x18, // Ganon exits from Agahnim's body. Chase Mode.
            TRIFORCE        = 0x19, // Triforce Room scene
            ENDING          = 0x1A, // End sequence
            LOCATIONSELECT  = 0x1B, // Screen to select where to start from (House, sanctuary, etc.)
            MAX // any modules beyond this aren't used in-game.
        }

        public enum PlayerState : byte
        {
            DEFAULT         = 0x00, // Ground state
            FALLING         = 0x01, // Falling into a hole
            BONK            = 0x02, // Recoil from hitting wall / enemies 
            SPINATTACK      = 0x03, // Spin attacking
            SWIM            = 0x04, // Swimming
            PLATFORM        = 0x05, // Turtle Rock platforms
            KNOCKBACK       = 0x06, // Recoil again (other movement)
            ZAP             = 0x07, // Being electrocuted
            ETHER           = 0x08, // Using ether medallion
            BOMBOS          = 0x09, // Using bombos medallion
            QUAKE           = 0x0A, // Using quake medallion
            JUMPHOLE        = 0x0B, // Falling into a hold by jumping off of a ledge.
            JUMPLR          = 0x0C, // Falling to the left / right off of a ledge.
            JUMPDIAGUP      = 0x0D, // Jumping off of a ledge diagonally up and left / right.
            JUMPDIAGDOWN    = 0x0E, // Jumping off of a ledge diagonally down and left / right.
            JUMPOTHER1      = 0x0F, // More jumping off of a ledge but with dashing maybe + some directions.
            JUMPOTHER2      = 0x10, // Same or similar to 0x0F?
            JUMPDOWN        = 0x11, // Falling off a ledge
            DASHCANCEL      = 0x12, // Used when coming out of a dash by pressing a direction other than the dash direction.
            HOOKSHOT        = 0x13, // Hookshot
            MIRROR          = 0x14, // Magic mirror
            ITEM            = 0x15, // Holding up an item
            ASLEEP          = 0x16, // Asleep in his bed
            OWBUNNY         = 0x17, // Permabunny
            CANTLIFT        = 0x18, // Stuck under a heavy rock
            GETETHER        = 0x19, // Receiving Ether Medallion
            GETBOMBOS       = 0x1A, // Receiving Bombos Medallion
            OPENDP          = 0x1B, // Opening Desert Palace
            DUNGEONBUNNY    = 0x1C, // Temporary bunny
            PULLROLL        = 0x1D, // Rolling back from Gargoyle gate or PullForRupees object
            SPINNING        = 0x1E  // The actual spin attack motion.
        }

        public enum Dungeon : byte
        {
            SEWER           = 0x00,
            HYRULECASTLE    = 0x02,
            EASTERNPALACE   = 0x04,
            DESERTPALACE    = 0x06,
            CASTLETOWER     = 0x08,
            SWAMPPALACE     = 0x0A,
            DARKPALACE      = 0x0C,
            MISERYMIRE      = 0x0E,
            SKULLWOODS      = 0x10,
            ICEPALACE       = 0x12,
            TOWEROFHERA     = 0x14,
            THIEVESTOWN     = 0x16,
            TURTLEROCK      = 0x18,
            GANONSTOWER     = 0x1A,
            UNUSED1         = 0x1C,
            UNUSED2         = 0x1E
        }

        [Flags]
        public enum RoomState : ushort
        {
            Q1VISITED       = 0x0001,   // Visited SE quadrant
            Q2VISITED       = 0x0002,   // Visited SW quadrant
            Q3VISITED       = 0x0004,   // Visited NE quadrant
            Q4VISITED       = 0x0008,   // Visited NW quadrant
            UNLOCK1         = 0x0010,   // Unlocked chest/big key door
            UNLOCK2         = 0x0020,   // Unlocked chest/big key door
            UNLOCK3         = 0x0040,   // Unlocked chest/big key door
            UNLOCK4         = 0x0080,   // Unlocked chest/big key door
            RUPEETILE       = 0x0100,   // Collected rupee tiles (or unlocked chest/big key door)
            KEYITEM         = 0x0200,   // Collected key/special item (or unlocked chest/big key door)
            KEY2            = 0x0400,   // Collected key
            BOSSDEFEATED    = 0x0800,   // Defeated boss
            DOOR1           = 0x1000,   // Opened door
            DOOR2           = 0x2000,   // Opened door
            DOOR3           = 0x4000,   // Opened door
            DOOR4           = 0x8000    // Opened door
        }

        [Flags]
        public enum CurrentRoomState : byte
        {
            UNLOCK1     = 0x01, // Unlocked chest/big key door
            UNLOCK2     = 0x02, // Unlocked chest/big key door
            UNLOCK3     = 0x04, // Unlocked chest/big key door
            UNLOCK4     = 0x08, // Unlocked chest/big key door
            RUPEETILE   = 0x10, // Collected rupee tiles (or unlocked chest/big key door)
            KEYITEM     = 0x20, // Collected key/special item (or unlocked chest/big key door)
            KEY2        = 0x40, // Collected key
            HEARTPIECE  = 0x80  // Collected heart piece
        }

        [Flags]
        public enum OverworldState : byte
        {
            SECONDARY   = 0x02, // Secondary overlay active
            OVERLAY     = 0x20, // Primary overlay active
            COLLECTED   = 0x40 // Item collected from this area
        }

        public enum BowLevel : byte
        {
            NONE    = 0x00, // No bow
            BOW     = 0x01, // Bow, no arrows
            ARROW   = 0x02, // Bow and arrows
            SILVER  = 0x03  // Bow and silver arrows
        }

        [Flags]
        public enum BoomerangLevel : byte
        {
            NONE    = 0x00, // No boomerang
            BLUE    = 0x01, // Blue boomerang
            RED     = 0x02  // Magic (red) boomerang
        }

        [Flags]
        public enum MushroomLevel : byte
        {
            NONE        = 0x00, // No mushroom or powder
            MUSHROOM    = 0x01, // Mushroom
            POWDER      = 0x02  // Mushroom returned, powder collected
        }

        public enum FluteLevel : byte
        {
            NONE    = 0x00, // Nothing
            SHOVEL  = 0x01, // Shovel
            FLUTE   = 0x02, // Flute collected
            BIRD    = 0x03  // Bird activated
        }

        [Flags]
        public enum MirrorLevel
        {
            NONE    = 0x00, // Mirror not collected
            SCROLL  = 0x01, // ?? Scroll item with mirror functionality
            MIRROR  = 0x02  // Mirror
        }

        public enum GlovesLevel : byte
        {
            NONE    = 0x00, // No gloves
            POWER   = 0x01, // Power gloves
            TITAN   = 0x02  // Titan's Mitt
        }

        public enum SwordLevel : byte
        {
            NONE        = 0x00, // No sword
            FIGHTER     = 0x01, // Fighter's sword
            MASTER      = 0x02, // Master sword
            TEMPERED    = 0x03, // Tempered sword
            GOLDEN      = 0x04, // Golden sword
            LOST        = 0xFF  // Lost sword (gave to the blacksmiths)
        }

        public enum ShieldLevel : byte
        {
            NONE    = 0x00, // No shield
            FIGHTER = 0x01, // Fighter's shield
            RED     = 0x02, // Red shield
            MIRROR  = 0x03, // Mirror shield
        }

        public enum ArmorLevel : byte
        {
            GREEN   = 0x00, // Green tunic
            BLUE    = 0x01, // Blue mail
            RED     = 0x02  // Red mail
        }

        public enum BottleItem : byte
        {
            NONE            = 0x00, // No bottle
            MUSHROOM        = 0x01, // ?? Useless
            EMPTY           = 0x02, // Empty bottle
            REDPOTION       = 0x03, // Red potion
            GREENPOTION     = 0x04, // Green potion
            BLUEPOTION      = 0x05, // Blue potion
            FAIRY           = 0x06, // Fairy
            BEE             = 0x07, // Bee
            GOODBEE         = 0x08  // Good (gold) bee
        }

        [Flags]
        public enum Pendants : byte
        {
            RED     = 0x01, // Pendant of Power
            BLUE    = 0x02, // Pendant of Wisdom
            GREEN   = 0x04  // Pendant of Courage
        }

        [Flags]
        public enum DungeonItem : ushort
        {
            GANONSTOWER     = 0x0004,   // Ganon's Tower
            TURTLEROCK      = 0x0008,   // Turtle Rock
            THIEVESTOWN     = 0x0010,   // Thieves' Town
            TOWEROFHERA     = 0x0020,   // Tower of Hera
            ICEPALACE       = 0x0040,   // Ice Palace
            SKULLWOODS      = 0x0080,   // Skull Woods
            MISERYMIRE      = 0x0100,   // Misery Mire
            DARKPALACE      = 0x0200,   // Palace of Darkness
            SWAMPPALACE     = 0x0400,   // Swamp Palace
            CASTLE2         = 0x0800,   // Hyrule Castle 2 (Tower)
            DESERTPALACE    = 0x1000,   // Desert Palace
            EASTERNPALACE   = 0x2000,   // Eastern Palace
            CASTLE          = 0x4000,   // Hyrule Castle
            SEWER           = 0x8000    // Hyrule Castle Sewer
        }

        [Flags]
        public enum Crystals : byte
        {
            NONE        = 0x00,
            CRYSTAL2    = 0x01, // Misery Mire
            CRYSTAL1    = 0x02, // Palace of Darkness
            CRYSTAL5    = 0x04, // Ice Palace
            CRYSTAL7    = 0x08, // Turtle Rock
            CRYSTAL6    = 0x10, // Swamp Palace
            CRYSTAL4    = 0x20, // Thieves' Town
            CRYSTAL3    = 0x40  // Skull Woods
        }
        
        [Flags]
        public enum SwappableInventory1 : byte
        {
            ACTIVATEDFLUTE  = 0x01,
            FLUTE           = 0x02,
            SHOVEL          = 0x04,
            MAGICPOWDER     = 0x10,
            MUSHROOM        = 0x20,
            REDBOOMERANG    = 0x40,
            BLUEBOOMERANG   = 0x80
        }

        [Flags]
        public enum SwappableInventory2 : byte
        {
            SILVERARROW = 0x40,
            BOW         = 0x80
        }

        bool running = false;
        ImageDict icons = new Images();
        ALinkToThePastSettings settings = new ALinkToThePastSettings();

        public ALinkToThePast()
        {
            gameName = "THE LEGEND OF ZELDA";

            emulator.RegisterWatcher("Big Key", typeof(DungeonItem), emulator.MemoryType.WRAM, 0xF366);
            emulator.RegisterWatcher("Bombos Medallion", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF347);
            emulator.RegisterWatcher("Bombs", typeof(byte), emulator.MemoryType.WRAM, 0xF343);
            emulator.RegisterWatcher("Book of Mudora", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF34E);
            emulator.RegisterWatcher("Boomerang", typeof(BoomerangLevel), emulator.MemoryType.WRAM, 0xF341);
            emulator.RegisterWatcher("Boots", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF355);
            emulator.RegisterWatcher("Bottles", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF34F);
            emulator.RegisterWatcher("Bottle 1", typeof(BottleItem), emulator.MemoryType.WRAM, 0xF35C);
            emulator.RegisterWatcher("Bottle 2", typeof(BottleItem), emulator.MemoryType.WRAM, 0xF35D);
            emulator.RegisterWatcher("Bottle 3", typeof(BottleItem), emulator.MemoryType.WRAM, 0xF35E);
            emulator.RegisterWatcher("Bottle 4", typeof(BottleItem), emulator.MemoryType.WRAM, 0xF35F);
            emulator.RegisterWatcher("Bow", typeof(BowLevel), emulator.MemoryType.WRAM, 0xF340);
            emulator.RegisterWatcher("Cane of Somaria", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF350);
            emulator.RegisterWatcher("Cane of Byrna", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF351);
            emulator.RegisterWatcher("Compass", typeof(DungeonItem), emulator.MemoryType.WRAM, 0xF364);
            emulator.RegisterWatcher("Crystals", typeof(Crystals), emulator.MemoryType.WRAM, 0xF37A);
            emulator.RegisterWatcher("Current Dungeon", typeof(Dungeon), emulator.MemoryType.WRAM, 0x040C);
            emulator.RegisterWatcher("Current Room Items", typeof(CurrentRoomState), emulator.MemoryType.WRAM, 0x0403);
            emulator.RegisterWatcher("Current Room Number", typeof(ushort), emulator.MemoryType.WRAM, 0x00A0);
            emulator.RegisterWatcher("Ether Medallion", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF348);
            emulator.RegisterWatcher("Events 1", typeof(Events1), emulator.MemoryType.WRAM, 0xF3C6);
            emulator.RegisterWatcher("Events 2", typeof(Events2), emulator.MemoryType.WRAM, 0xF3C9);
            emulator.RegisterWatcher("Fire Rod", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF345);
            emulator.RegisterWatcher("Flippers", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF356);
            emulator.RegisterWatcher("Flute", typeof(FluteLevel), emulator.MemoryType.WRAM, 0xF34C);
            emulator.RegisterWatcher("Gloves", typeof(GlovesLevel), emulator.MemoryType.WRAM, 0xF354);
            emulator.RegisterWatcher("Hammer", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF34B);
            emulator.RegisterWatcher("Haunted Grove", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF416);
            emulator.RegisterWatcher("Hookshot", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF342);
            emulator.RegisterWatcher("Ice Rod", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF346);
            emulator.RegisterWatcher("Indoors", typeof(BoolFlag), emulator.MemoryType.WRAM, 0x001B);
            emulator.RegisterWatcher("Lantern", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF34A);
            emulator.RegisterWatcher("Magic Cape", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF352);
            emulator.RegisterWatcher("Magic Mirror", typeof(MirrorLevel), emulator.MemoryType.WRAM, 0xF353);
            emulator.RegisterWatcher("Main Module", typeof(GameModule), emulator.MemoryType.WRAM, 0x0010);
            emulator.RegisterWatcher("Map", typeof(DungeonItem), emulator.MemoryType.WRAM, 0xF368);
            emulator.RegisterWatcher("Moon Pearl", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF357);
            emulator.RegisterWatcher("Mushroom", typeof(MushroomLevel), emulator.MemoryType.WRAM, 0xF344);
            emulator.RegisterWatcher("Net", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF34D);
            emulator.RegisterWatcher("Pendants", typeof(Pendants), emulator.MemoryType.WRAM, 0xF374);
            emulator.RegisterWatcher("Player State", typeof(PlayerState), emulator.MemoryType.WRAM, 0x005D);
            emulator.RegisterWatcher("Progress", typeof(Progress), emulator.MemoryType.WRAM, 0xF3C5);
            emulator.RegisterWatcher("Quake Medallion", typeof(BoolFlag), emulator.MemoryType.WRAM, 0xF349);
            emulator.RegisterWatcher("Room ID", typeof(ushort), emulator.MemoryType.WRAM, 0x00A0);
            emulator.RegisterWatcher("Rupees", typeof(ushort), emulator.MemoryType.WRAM, 0xF362);
            emulator.RegisterWatcher("Shield", typeof(ShieldLevel), emulator.MemoryType.WRAM, 0xF35A);
            emulator.RegisterWatcher("Special Items", typeof(SpecialItems), emulator.MemoryType.WRAM, 0xF410);
            emulator.RegisterWatcher("Swappable Inventory 1", typeof(SwappableInventory1), emulator.MemoryType.WRAM, 0xF412);
            emulator.RegisterWatcher("Swappable Inventory 2", typeof(SwappableInventory2), emulator.MemoryType.WRAM, 0xF414);
            emulator.RegisterWatcher("Sword", typeof(SwordLevel), emulator.MemoryType.WRAM, 0xF359);
            emulator.RegisterWatcher("Tunic", typeof(ArmorLevel), emulator.MemoryType.WRAM, 0xF35B);
            
            for (int i = 0; i < 130; ++i)
            {
                emulator.RegisterWatcher("Overworld State", i, typeof(OverworldState), emulator.MemoryType.WRAM, 0xF280 + i);
            }

            for (int i = 0; i < 296; ++i)
            {
                emulator.RegisterWatcher("Room State", i, typeof(RoomState), emulator.MemoryType.WRAM, 0xF000 + (i * 2));
            }
        }


        public override bool IsLoaded()
        {
            if (!emulator.IsRunning())
                return false;

            try
            {
                romName = new DeepPointer(emulator.GetOffsets().ROM, 0x7FC0);
                string name;
                romName.DerefString(emulator.emulatorProcess, 21, out name);
                GameModule currentModule = Get<GameModule>("Main Module");

                //tourney = name?.Substring(3, 7).Equals("TOURNEY") ?? false;

                if (name?.StartsWith(gameName) ?? false)
                {
                    region = REGION.NTSC_U;
                    return currentModule < GameModule.MAX;
                }

                else if (name?.StartsWith("ZELDANODENSETSU") ?? false)
                {
                    region = REGION.NTSC_J;
                    return currentModule < GameModule.MAX;
                }

                else if (name?.StartsWith("VT") ?? false)
                {
                    region = REGION.NTSC_J;
                    randomized = true;
                    return currentModule < GameModule.MAX;
                }
            }

            catch (Exception)
            { }

            return false;
        }

        public override bool IsRunning()
        {
            if (!IsLoaded())
                running = false;

            else
            {
                GameModule currentModule = Get<GameModule>("Main Module");

                if (running && currentModule < GameModule.FILELOAD)
                    running = false;

                else if (currentModule == GameModule.DUNGEON || currentModule == GameModule.OVERWORLD || currentModule == GameModule.SPECIALAREA)
                    running = true;
            }

            return running;
        }

        public override void Update(LiveSplitState state)
        {
            base.Update(state);

            if (IsRunning() && settings.AutoSplitter)
            {
                if (state.CurrentPhase == TimerPhase.NotRunning)
                {
                    if(randomized)
                    {
                        while (state.Run.Count > 8)
                            state.Run.RemoveAt(8);

                        for (int i = 0; i < 7; ++i)
                        {
                            if (state.Run.Count <= i)
                                state.Run.AddSegment("Crystal " + (i + 1), icon: icons["Crystal"][0]);

                            else
                            {
                                state.Run[i].Name = "Crystal " + (i + 1);
                                state.Run[i].Icon = icons["Crystal"][0];
                            }
                        }

                        if (state.Run.Count <= 7)
                            state.Run.AddSegment("Triforce");

                        else
                            state.Run[7].Name = "Triforce";
                    }

                    if (state.Run.Count < 2)
                    {
                        state.Run[0].Name = "Start";
                        state.Run.AddSegment("Triforce");
                    }

                    timer.Start();
                }

                Crystals newCrystal = (Crystals)((byte)Get<Crystals>("Crystals") - (byte)Previous<Crystals>("Crystals"));

                if (newCrystal != Crystals.NONE)
                {
                    switch (newCrystal)
                    {
                        case Crystals.CRYSTAL1:
                            state.CurrentSplit.Icon = icons["Crystal"][1];
                            break;

                        case Crystals.CRYSTAL2:
                            state.CurrentSplit.Icon = icons["Crystal"][2];
                            break;

                        case Crystals.CRYSTAL3:
                            state.CurrentSplit.Icon = icons["Crystal"][3];
                            break;

                        case Crystals.CRYSTAL4:
                            state.CurrentSplit.Icon = icons["Crystal"][4];
                            break;

                        case Crystals.CRYSTAL5:
                            state.CurrentSplit.Icon = icons["Crystal"][5];
                            break;

                        case Crystals.CRYSTAL6:
                            state.CurrentSplit.Icon = icons["Crystal"][6];
                            break;

                        case Crystals.CRYSTAL7:
                            state.CurrentSplit.Icon = icons["Crystal"][7];
                            break;

                        default:
                            break;
                    }
                    
                    switch (Get<Dungeon>("Current Dungeon"))
                    {
                        case Dungeon.DARKPALACE:
                            state.CurrentSplit.Name = "Palace of Darkness";
                            break;

                        case Dungeon.DESERTPALACE:
                            state.CurrentSplit.Name = "Desert Palace";
                            break;

                        case Dungeon.EASTERNPALACE:
                            state.CurrentSplit.Name = "Eastern Palace";
                            break;

                        case Dungeon.ICEPALACE:
                            state.CurrentSplit.Name = "Ice Palace";
                            break;

                        case Dungeon.MISERYMIRE:
                            state.CurrentSplit.Name = "Misery Mire";
                            break;

                        case Dungeon.SKULLWOODS:
                            state.CurrentSplit.Name = "Skull Woods";
                            break;

                        case Dungeon.SWAMPPALACE:
                            state.CurrentSplit.Name = "Swamp Palace";
                            break;

                        case Dungeon.THIEVESTOWN:
                            state.CurrentSplit.Name = "Thieves' Town";
                            break;

                        case Dungeon.TOWEROFHERA:
                            state.CurrentSplit.Name = "Tower of Hera";
                            break;

                        case Dungeon.TURTLEROCK:
                            state.CurrentSplit.Name = "Turtle Rock";
                            break;

                        default:
                            state.CurrentSplit.Name = Get<Dungeon>("Current Dungeon").ToString();
                            break;
                    }

                    if (state.CurrentSplitIndex == state.Run.Count)
                        state.Run.AddSegment("Crystal " + state.CurrentSplitIndex + 2, icon: icons["Crystal"][0]);

                    timer.Split();
                }

                if (Get<ushort>("RoomID") == 0x0189)
                {
                    timer.Split();
                }
            }
        }
    }
}
