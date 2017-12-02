using LiveSplit.Snes9x;
using System;
using System.Collections.Generic;
using static LiveSplit.ALinkToThePast.UI.Components.ALinkToThePast;
using static LiveSplit.Snes9x.Game;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    enum LocationState
    {
        INACCESSIBLE,
        ACCESSIBLE,
        PARTIAL,
        VISIBLE,
        DARK,
        COMPLETE,
        GLITCHACCESSIBLE,
        GLITCHVISIBLE,
        UNKNOWN
    }

    enum DeathMountainRegion
    {
        SOUTHWEST,
        SOUTHEAST,
        NORTHWEST,
        NORTHEAST,
        DARKWEST,
        DARKEAST
    }

    enum DarkWorldRegion
    {
        NORTHWEST,
        NORTHEAST,
        SOUTH,
        SOUTHWEST,
        DEATHMOUNTAINWEST,
        DEATHMOUNTAINEAST
    }

    class LocationWatcher : Dictionary<string, Func<LocationState>>
    {
        bool CheckRoom(int room, int count = 1, bool checkAll = true)
        {
            var game = GameLoader.game;
            if (!game?.IsRunning() ?? false)
                return false;

            if (room >= 0 && room <= 296)
            {

                if (game.Get<BoolFlag>("Indoors").HasFlag(BoolFlag.TRUE) && game.Get<ushort>("Current Room Number") == room)
                {
                    CurrentRoomState state = GameLoader.game.Get<CurrentRoomState>("Current Room Items");

                    switch (count)
                    {
                        case 1:
                            return state.HasFlag(CurrentRoomState.UNLOCK1);

                        case 2:
                            return !checkAll ? state.HasFlag(CurrentRoomState.UNLOCK2) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2);

                        case 3:
                            return !checkAll ? state.HasFlag(CurrentRoomState.UNLOCK3) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3);

                        case 4:
                            return !checkAll ? state.HasFlag(CurrentRoomState.UNLOCK4) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3 | CurrentRoomState.UNLOCK4);

                        case 5:
                            return !checkAll ? state.HasFlag(CurrentRoomState.RUPEETILE) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3 | CurrentRoomState.UNLOCK4 | CurrentRoomState.RUPEETILE);

                        case 6:
                            return !checkAll ? state.HasFlag(CurrentRoomState.KEYITEM) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3 | CurrentRoomState.UNLOCK4 | CurrentRoomState.RUPEETILE | CurrentRoomState.KEYITEM);

                        case 7:
                            return !checkAll ? state.HasFlag(CurrentRoomState.KEY2) : state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3 | CurrentRoomState.UNLOCK4 | CurrentRoomState.RUPEETILE | CurrentRoomState.KEYITEM | CurrentRoomState.KEY2);

                        case 8:
                            return !checkAll ? state.HasFlag(CurrentRoomState.HEARTPIECE) : state.HasFlag(CurrentRoomState.HEARTPIECE) | state.HasFlag(CurrentRoomState.UNLOCK1 | CurrentRoomState.UNLOCK2 | CurrentRoomState.UNLOCK3 | CurrentRoomState.UNLOCK4 | CurrentRoomState.RUPEETILE | CurrentRoomState.KEYITEM | CurrentRoomState.KEY2);

                        default:
                            break;
                    }
                }

                else
                {
                    RoomState state = GameLoader.game.Get<RoomState>("Room State", room);

                    switch (count)
                    {
                        case 1:
                            return state.HasFlag(RoomState.UNLOCK1);

                        case 2:
                            return !checkAll ? state.HasFlag(RoomState.UNLOCK2) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2);

                        case 3:
                            return !checkAll ? state.HasFlag(RoomState.UNLOCK3) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3);

                        case 4:
                            return !checkAll ? state.HasFlag(RoomState.UNLOCK4) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3 | RoomState.UNLOCK4);

                        case 5:
                            return !checkAll ? state.HasFlag(RoomState.RUPEETILE) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3 | RoomState.UNLOCK4 | RoomState.RUPEETILE);

                        case 6:
                            return !checkAll ? state.HasFlag(RoomState.KEYITEM) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3 | RoomState.UNLOCK4 | RoomState.RUPEETILE | RoomState.KEYITEM);

                        case 7:
                            return !checkAll ? state.HasFlag(RoomState.KEY2) : state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3 | RoomState.UNLOCK4 | RoomState.RUPEETILE | RoomState.KEYITEM | RoomState.KEY2);

                        case 8:
                            return !checkAll ? state.HasFlag(RoomState.BOSSDEFEATED) : state.HasFlag(RoomState.BOSSDEFEATED) | state.HasFlag(RoomState.UNLOCK1 | RoomState.UNLOCK2 | RoomState.UNLOCK3 | RoomState.UNLOCK4 | RoomState.RUPEETILE | RoomState.KEYITEM | RoomState.KEY2);

                        default:
                            break;
                    }
                }
            }

            return false;
        }

        bool CheckBoss(Dungeon dungeon)
        {
            switch(dungeon)
            {
                case Dungeon.DARKPALACE:
                    return CheckRoom(90, 8, false);

                case Dungeon.DESERTPALACE:
                    return CheckRoom(51, 8, false);

                case Dungeon.EASTERNPALACE:
                    return CheckRoom(200, 8, false);

                case Dungeon.ICEPALACE:
                    return CheckRoom(222, 8, false);

                case Dungeon.MISERYMIRE:
                    return CheckRoom(144, 8, false);

                case Dungeon.SKULLWOODS:
                    return CheckRoom(41, 8, false);

                case Dungeon.SWAMPPALACE:
                    return CheckRoom(6, 8, false);

                case Dungeon.THIEVESTOWN:
                    return CheckRoom(172, 8, false);

                case Dungeon.TOWEROFHERA:
                    return CheckRoom(7, 8, false);

                case Dungeon.TURTLEROCK:
                    return CheckRoom(164, 8, false);

                default:
                    return false;
            }
        }

        private bool CheckFlag<T>(string name, T flag, bool set = true) where T : struct, IComparable
        {
            if (!GameLoader.game?.IsRunning() ?? false)
                return false;

            if (typeof(T).IsEnum)
            {
                if (set)
                    return ((Enum)Enum.ToObject(typeof(T), GameLoader.game.Get<T>(name))).HasFlag((Enum)Enum.ToObject(typeof(T), flag));

                else
                    return !((Enum)Enum.ToObject(typeof(T), GameLoader.game.Get<T>(name))).HasFlag((Enum)Enum.ToObject(typeof(T), flag));
            }

            return false;
        }

        bool CheckFlag(string name, bool set = true)
        {
            return CheckFlag(name, BoolFlag.TRUE, true);
        }

        bool CheckFlag<T>(string name, int index, T flag, bool set = true) where T : struct, IComparable
        {
            if (index >= 0)
                name = name + "[" + index + "]";

            return CheckFlag(name, flag, set);
        }

        bool CheckLevel<T>(string name, T level) where T : struct, IComparable
        {
            if (!GameLoader.game?.IsRunning() ?? false)
                return false;

            return GameLoader.game.Get<T>(name).CompareTo(level) >= 0;
        }

        bool CheckAccess(DeathMountainRegion region)
        {
            switch (region)
            {
                case DeathMountainRegion.DARKEAST:
                    return CheckAccess(DarkWorldRegion.DEATHMOUNTAINEAST);

                case DeathMountainRegion.DARKWEST:
                    return CheckAccess(DarkWorldRegion.DEATHMOUNTAINWEST);

                case DeathMountainRegion.NORTHEAST:
                    return CheckAccess(DeathMountainRegion.SOUTHEAST) || (CheckAccess(DeathMountainRegion.NORTHWEST) && CheckFlag("Hammer"));

                case DeathMountainRegion.NORTHWEST:
                    if (!CheckAccess(DeathMountainRegion.SOUTHWEST))
                        return false;

                    return (CheckMirror() || (CheckFlag("Hookshot") && CheckFlag("Hammer")));

                case DeathMountainRegion.SOUTHEAST:
                    if (!CheckAccess(DeathMountainRegion.SOUTHWEST))
                        return false;

                    return (CheckFlag("Hookshot") || (CheckMirror() && CheckFlag("Hammer")));

                case DeathMountainRegion.SOUTHWEST:
                    return (CheckLevel("Gloves", GlovesLevel.POWER) || CheckFlute());

                default:
                    return false;
            }
        }

        bool CheckAccess(DarkWorldRegion region, bool requirePearl = false)
        {
            if (requirePearl && !CheckFlag("Moon Pearl"))
                return false;

            switch (region)
            {
                case DarkWorldRegion.NORTHWEST:
                    return CheckFlag("Moon Pearl") && 
                        (((CheckLevel("Gloves", GlovesLevel.TITAN)) || (CheckFlag("Hammer")) ||
                        (CheckFlag("Hookshot") && (CheckLevel("Sword", SwordLevel.MASTER) || CheckFlag("Magic Cape")))));

                case DarkWorldRegion.NORTHEAST:
                    return (CheckLevel("Sword", SwordLevel.MASTER) ||
                        CheckFlag("Magic Cape") ||
                        (CheckFlag("Moon Pearl") && CheckFlag("Hammer")));

                case DarkWorldRegion.SOUTH:
                    if (CheckFlag("Hammer") && CheckLevel("Gloves", GlovesLevel.POWER) && CheckFlag("Moon Pearl"))
                        return true;

                    goto case DarkWorldRegion.NORTHWEST;

                case DarkWorldRegion.SOUTHWEST:
                    return CheckFlute() && CheckLevel("Gloves", GlovesLevel.TITAN);

                case DarkWorldRegion.DEATHMOUNTAINWEST:
                    if (CheckLevel("Gloves", GlovesLevel.POWER))
                        return true;

                    goto case DarkWorldRegion.DEATHMOUNTAINEAST;

                case DarkWorldRegion.DEATHMOUNTAINEAST:
                    return (CheckLevel("Gloves", GlovesLevel.TITAN) &&
                        (CheckFlag("Hookshot") ||
                        CheckFlag("Hammer")));

                default:
                    return false;
            }
        }

        bool CheckBombs()
        {
            return (GameLoader.game?.Get<byte>("Bombs") ?? 0) > 0;
        }

        bool CheckRupees(int amount)
        {
            return (GameLoader.game?.Get<ushort>("Rupees") ?? 0) >= amount;
        }

        bool CheckMirror()
        {
            return CheckFlag("Magic Mirror", MirrorLevel.MIRROR);
        }

        bool CheckFlute()
        {
            return CheckLevel("Flute", FluteLevel.BIRD);
        }

        bool CheckOverworldArea(int area, OverworldState state = OverworldState.COLLECTED)
        {
            return CheckFlag("Overworld State", area, state);
        }

        bool CheckDungeon(DungeonItem dungeon, params Func<bool>[] locations)
        {
            int count = 0;

            foreach(var location in locations)
            {
                if (!location())
                    ++count;
            }

            if (dungeon != DungeonItem.CASTLE && !CheckFlag("Compass", dungeon))
                --count;

            if (!CheckFlag("Big Key", dungeon))
                --count;

            if (!CheckFlag("Map", dungeon))
                --count;

            return count <= 0;
        }

        private void AddLocation(string name, LocationState defaultState = LocationState.INACCESSIBLE, Func<bool> complete = null, Func<bool> accessible = null, Func<bool> partial = null, Func<bool> glitchAccessible = null, Func<bool> visible = null, Func<bool> glitchVisible = null, bool dark = false)
        {
            if (ContainsKey(name))
                Remove(name);

            Add(name, () =>
            {
                LocationState _default = defaultState;
                bool _dark = dark;
                bool lantern = CheckFlag("Lantern");

                var game = GameLoader.game;
                if (!(game?.IsRunning() ?? false))
                    return _default;

                if (_default == LocationState.DARK && lantern)
                    _default = LocationState.ACCESSIBLE;

                if (complete?.Invoke() ?? false)
                    return LocationState.COMPLETE;

                if (accessible?.Invoke() ?? false)
                    return _dark && !lantern ? LocationState.DARK : LocationState.ACCESSIBLE;

                if (partial?.Invoke() ?? false)
                    return _dark && !lantern ? LocationState.DARK : LocationState.PARTIAL;

                if (glitchAccessible?.Invoke() ?? false)
                    return _dark && !lantern ? LocationState.DARK : LocationState.GLITCHACCESSIBLE;

                if (visible?.Invoke() ?? false)
                    return LocationState.VISIBLE;

                if (glitchVisible?.Invoke() ?? false)
                    return LocationState.GLITCHVISIBLE;

                return _default;
            });
        }

        private void AddLocation(string name, LocationState defaultState, List<Tuple<int, int>> rooms, Func<bool> accessible = null, Func<bool> partial = null, Func<bool> glitchAccessible = null, Func<bool> visible = null, Func<bool> glitchVisible = null, bool dark = false)
        {
            AddLocation(name,
                defaultState,
                () =>
                {
                    List<Tuple<int, int>> _rooms = rooms;
                    foreach (var room in _rooms)
                    {
                        if (!CheckRoom(room.Item1, room.Item2))
                            return false;
                    }

                    return true;
                },
                accessible,
                partial,
                glitchAccessible,
                visible,
                glitchVisible,
                dark);
        }

        private void AddLocation(string name, LocationState defaultState, List<int> rooms, Func<bool> accessible = null, Func<bool> partial = null, Func<bool> glitchAccessible = null, Func<bool> visible = null, Func<bool> glitchVisible = null, bool dark = false)
        {
            AddLocation(name,
                defaultState,
                () =>
                {
                    List<int> _rooms = rooms;
                    foreach (var room in _rooms)
                    {
                        if (!CheckRoom(room))
                            return false;
                    }

                    return true;
                },
                accessible,
                partial,
                glitchAccessible,
                visible,
                glitchVisible,
                dark);
        }

        private void AddLocation(string name, LocationState defaultState, int room, int count, Func<bool> accessible = null, Func<bool> partial = null, Func<bool> glitchAccessible = null, Func<bool> visible = null, Func<bool> glitchVisible = null, bool dark = false)
        {
            AddLocation(name, defaultState, new List<Tuple<int, int>> { new Tuple<int, int>(room, count) }, accessible, partial, glitchAccessible, visible, glitchVisible, dark);
        }

        private void AddLocation(string name, LocationState defaultState, int room, Func<bool> accessible = null, Func<bool> partial = null, Func<bool> glitchAccessible = null, Func<bool> visible = null, Func<bool> glitchVisible = null, bool dark = false)
        {
            AddLocation(name,
                defaultState,
                () =>
                {
                    int _room = room;

                    var game = GameLoader.game;
                    if (!(game?.IsRunning() ?? false))
                        return false;

                    return CheckRoom(room, 1) || CheckRoom(room, 6, false);
                },
                accessible,
                partial,
                glitchAccessible,
                visible,
                glitchVisible,
                dark);
        }

        public LocationWatcher()
        {
            AddLocation("Aginah's Cave", LocationState.INACCESSIBLE, 266, () => CheckBombs());
            AddLocation("Armos Knights", LocationState.INACCESSIBLE, () => CheckBoss(Dungeon.EASTERNPALACE), () => CheckLevel("Bow", BowLevel.BOW));
            AddLocation("Blacksmiths", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.TEMPEREDSWORD), () => CheckLevel("Gloves", GlovesLevel.TITAN) && CheckFlag("Moon Pearl"));
            AddLocation("Bombos Tablet", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.BOMBOS), () => CheckAccess(DarkWorldRegion.SOUTH) && CheckMirror() && CheckFlag("Book of Mudora") && CheckLevel("Sword", SwordLevel.MASTER));
            AddLocation("Bomb Hut", LocationState.INACCESSIBLE, 262, () => CheckAccess(DarkWorldRegion.NORTHWEST, true) && CheckBombs());
            AddLocation("Bonk Rocks", LocationState.INACCESSIBLE, 292, () => CheckFlag("Boots"));
            AddLocation("Bottle Vendor", LocationState.INACCESSIBLE, () => CheckFlag("Events 2", Events2.MERCHANT), () => CheckRupees(100));
            AddLocation("Bumper Cave", LocationState.INACCESSIBLE, () => CheckOverworldArea(74), () => CheckAccess(DarkWorldRegion.NORTHWEST) && CheckFlag("Moon Pearl") && CheckFlag("Magic Cape"));
            AddLocation("Bunny Cave", LocationState.INACCESSIBLE, 248, 2, () => CheckAccess(DeathMountainRegion.DARKEAST));
            AddLocation("Catfish", LocationState.INACCESSIBLE, () => CheckOverworldArea(79), () => CheckAccess(DarkWorldRegion.NORTHEAST));
            AddLocation("Checkerboard Cave", LocationState.INACCESSIBLE, 294, () => CheckAccess(DarkWorldRegion.SOUTHWEST) && CheckMirror());
            AddLocation("Chicken House", LocationState.INACCESSIBLE, 264, () => CheckBombs());
            AddLocation("C House", LocationState.INACCESSIBLE, 284, () => CheckAccess(DarkWorldRegion.NORTHWEST));
            AddLocation("Death Mountain East", LocationState.INACCESSIBLE, () => CheckRoom(239, 5) && CheckRoom(255, 2), () => CheckAccess(DeathMountainRegion.SOUTHEAST) && CheckBombs(), () => CheckAccess(DeathMountainRegion.SOUTHEAST));
            AddLocation("Desert Cliff", LocationState.INACCESSIBLE, () => CheckOverworldArea(48), () => CheckLevel("Gloves", GlovesLevel.POWER));
            AddLocation("Desert Palace", LocationState.INACCESSIBLE, () => CheckDungeon(DungeonItem.DESERTPALACE, () => CheckBoss(Dungeon.DESERTPALACE), () => CheckRoom(115), () => CheckRoom(115, 7, false), () => CheckRoom(116), () => CheckRoom(117), () => CheckRoom(133)), () => CheckFlag("Book of Mudora") && CheckFlag("Boots"), () => CheckFlag("Book of Mudora"));
            AddLocation("Digging Game", LocationState.INACCESSIBLE, () => CheckOverworldArea(104), () => CheckAccess(DarkWorldRegion.SOUTH, true));
            AddLocation("Eastern Palace", LocationState.ACCESSIBLE, () => CheckDungeon(DungeonItem.EASTERNPALACE, () => CheckBoss(Dungeon.EASTERNPALACE), () => CheckRoom(168), () => CheckRoom(169), () => CheckRoom(170), () => CheckRoom(184), () => CheckRoom(185)), () => true);
            AddLocation("Ether Tablet", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.ETHER), () => CheckAccess(DeathMountainRegion.NORTHWEST) && CheckFlag("Book of Mudora") && CheckLevel("Sword", SwordLevel.MASTER));
            AddLocation("Fat Fairy", LocationState.INACCESSIBLE, 278, 2, () => false);
            AddLocation("Floating Island", LocationState.INACCESSIBLE, () => CheckOverworldArea(5), () => CheckAccess(DarkWorldRegion.DEATHMOUNTAINEAST, true) && CheckMirror() && CheckBombs());
            AddLocation("Floodgate", LocationState.ACCESSIBLE, () => CheckOverworldArea(59) && CheckRoom(267));
            AddLocation("Flute Boy", LocationState.INACCESSIBLE, () => CheckFlag("Events 2", Events2.FLUTEBOY), () => CheckAccess(DarkWorldRegion.SOUTH, true));
            AddLocation("Forest Hideout", LocationState.ACCESSIBLE, 225);
            AddLocation("Graveyard Cliff", LocationState.INACCESSIBLE, () => CheckRoom(283, 6, false), () => CheckAccess(DarkWorldRegion.NORTHWEST) && CheckMirror() && CheckBombs());
            AddLocation("Hammer Cave", LocationState.INACCESSIBLE, 295, () => CheckAccess(DarkWorldRegion.NORTHWEST, true) && CheckFlag("Hammer") && CheckLevel("Gloves", GlovesLevel.TITAN));
            AddLocation("Haunted Grove", LocationState.INACCESSIBLE, () => CheckFlag("Haunted Grove"), () => (CheckFlag("Flute", FluteLevel.SHOVEL) && !CheckFlag("Flute", FluteLevel.FLUTE)) || CheckFlag("Swappable Inventory 1", SwappableInventory1.SHOVEL));
            AddLocation("Hobo", LocationState.GLITCHACCESSIBLE, () => CheckFlag("Events 2", Events2.HOBO), () => CheckFlag("Flippers"));
            AddLocation("Hookshot Cave", LocationState.INACCESSIBLE, 60, 4, () => CheckAccess(DeathMountainRegion.DARKEAST) && CheckFlag("Moon Pearl") && CheckFlag("Hookshot"), () => CheckAccess(DeathMountainRegion.DARKEAST) && CheckFlag("Moon Pearl") && CheckFlag("Boots"));
            AddLocation("Hype Cave", LocationState.INACCESSIBLE, () => CheckRoom(286, 4) && CheckRoom(286, 7, false), () => CheckAccess(DarkWorldRegion.SOUTH, true) && CheckBombs());
            AddLocation("Hyrule Castle Dungeon", LocationState.ACCESSIBLE, new List<int> { 113, 114, 128 });
            AddLocation("Ice Palace", LocationState.INACCESSIBLE, () => false, () => CheckLevel("Gloves", GlovesLevel.TITAN) && CheckFlag("Fire Rod") && CheckFlag("Flippers"), glitchAccessible: () => CheckLevel("Gloves", GlovesLevel.TITAN) && CheckFlag("Fire Rod"));
            AddLocation("Kakariko Well", LocationState.PARTIAL, 47, 5, () => CheckBombs());
            AddLocation("King's Tomb", LocationState.INACCESSIBLE, 275, () => CheckFlag("Boots") && (CheckLevel("Gloves", GlovesLevel.TITAN) || (CheckAccess(DarkWorldRegion.NORTHWEST, true) && CheckMirror())));
            AddLocation("King Helmasaur", LocationState.INACCESSIBLE, () => CheckBoss(Dungeon.DARKPALACE), () => CheckFlag("Hammer") && CheckLevel("Bow", BowLevel.BOW), dark: true);
            AddLocation("King Zora", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.KINGZORA), () => (CheckFlag("Flippers") || CheckLevel("Gloves", GlovesLevel.POWER)) && CheckRupees(500), null, () => CheckRupees(500));
            AddLocation("Lake Hylia Cave", LocationState.INACCESSIBLE, 288, () => CheckBombs());
            AddLocation("Lake Hylia Island", LocationState.INACCESSIBLE, () => CheckOverworldArea(53), () => (CheckAccess(DarkWorldRegion.SOUTH, true) || CheckAccess(DarkWorldRegion.NORTHEAST, true)) && CheckFlag("Flippers") && CheckMirror());
            AddLocation("Lanmolas", LocationState.INACCESSIBLE, () => CheckBoss(Dungeon.DESERTPALACE), () => CheckFlag("Book of Mudora") && CheckLevel("Gloves", GlovesLevel.POWER) && CheckFlag("Lantern"));
            AddLocation("Library", LocationState.VISIBLE, () => CheckFlag("Special Items", SpecialItems.LIBRARY), () => CheckFlag("Boots"));
            AddLocation("Link's House", LocationState.ACCESSIBLE, 260);
            AddLocation("Thieves' Town", LocationState.INACCESSIBLE, () => false, () => CheckAccess(DarkWorldRegion.NORTHWEST, true) && CheckFlag("Hammer"), () => CheckAccess(DarkWorldRegion.NORTHWEST, true));
            AddLocation("Lost Old Man", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.OLDMAN), () => CheckLevel("Gloves", GlovesLevel.POWER) || CheckFlute(), dark: true);
            AddLocation("Lost Woods", LocationState.ACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.MUSHROOM));
            AddLocation("Lumberjack's Tree", LocationState.VISIBLE, 226, () => GameLoader.game.Get<Progress>("Progress") == Progress.AGAHNIM);
            AddLocation("Mad Batter", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.MADBATTER), () => CheckFlag("Hammer") && (CheckFlag("Mushroom", MushroomLevel.POWDER) || CheckFlag("Swappable Inventory 1", SwappableInventory1.MAGICPOWDER)));
            AddLocation("Master Sword Pedestal", LocationState.INACCESSIBLE, () => CheckOverworldArea(128), () => CheckFlag("Pendants", Pendants.GREEN | Pendants.BLUE | Pendants.RED));
            AddLocation("Mimic Cave", LocationState.INACCESSIBLE, 268, () => CheckAccess(DarkWorldRegion.DEATHMOUNTAINEAST) && CheckMirror() && CheckFlag("Fire Rod") && CheckFlag("Cane of Somaria"));
            AddLocation("Misery Mire", LocationState.INACCESSIBLE, () => false, visible: () => CheckAccess(DarkWorldRegion.SOUTHWEST, true)); AddLocation("Moldorm Cave", LocationState.INACCESSIBLE, () => CheckRoom(291, 4) && CheckRoom(291, 7, false), () => CheckBombs());
            AddLocation("Moldorm", LocationState.INACCESSIBLE, () => CheckBoss(Dungeon.TOWEROFHERA), () => CheckAccess(DeathMountainRegion.NORTHWEST));
            AddLocation("Palace of Darkness", LocationState.INACCESSIBLE, () => false, () => CheckAccess(DarkWorldRegion.NORTHEAST) && (CheckOverworldArea(94, OverworldState.OVERLAY) || CheckRupees(100)));
            AddLocation("Pyramid", LocationState.INACCESSIBLE, () => CheckOverworldArea(91), () => CheckAccess(DarkWorldRegion.NORTHEAST));
            AddLocation("Race", LocationState.VISIBLE, () => CheckOverworldArea(40), () => CheckBombs());
            AddLocation("Sahasrahla", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.SAHASRAHLA), () => CheckFlag("Pendants", Pendants.GREEN));
            AddLocation("Sahasrahla's Shrine", LocationState.INACCESSIBLE, 261, 3, () => CheckBombs() || CheckFlag("Boots"));
            AddLocation("Sanctuary", LocationState.ACCESSIBLE, 18);
            AddLocation("Secret Passage", LocationState.ACCESSIBLE, () => CheckFlag("Events 1", Events1.TALKTOUNCLE) && CheckRoom(85));
            AddLocation("Sewer Dark Room", LocationState.DARK, 50);
            AddLocation("Sewer Escape", LocationState.INACCESSIBLE, 17, 3, () => (CheckLevel("Gloves", GlovesLevel.POWER) || CheckFlag("Lantern")) && (CheckFlag("Boots") || CheckBombs()), () => CheckLevel("Gloves", GlovesLevel.POWER) || CheckFlag("Lantern"));
            AddLocation("Sewer Escape Dark Room", LocationState.DARK, 50);
            AddLocation("Sick Kid", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.SICKKID), ()=> CheckFlag("Bottles"));
            AddLocation("Skull Woods", LocationState.INACCESSIBLE, () => false, () => CheckAccess(DarkWorldRegion.NORTHWEST) && CheckFlag("Fire Rod"), () => CheckAccess(DarkWorldRegion.NORTHWEST));
            AddLocation("Spectacle Rock", LocationState.INACCESSIBLE, () => CheckOverworldArea(3), () => CheckAccess(DeathMountainRegion.SOUTHWEST) && CheckMirror(), visible: () => CheckAccess(DeathMountainRegion.SOUTHWEST));
            AddLocation("Spectacle Rock Cave", LocationState.INACCESSIBLE, () => CheckRoom(234, 7, false), () => CheckLevel("Gloves", GlovesLevel.POWER) || CheckFlute());
            AddLocation("Spike Cave", LocationState.INACCESSIBLE, 279, () => CheckAccess(DeathMountainRegion.DARKWEST) && CheckFlag("Moon Pearl") && CheckFlag("Hammer"));
            AddLocation("Spiral Cave", LocationState.INACCESSIBLE, 254, () => CheckAccess(DeathMountainRegion.NORTHEAST));
            AddLocation("South of Grove", LocationState.INACCESSIBLE, () => CheckRoom(283, 7, false), () => CheckAccess(DarkWorldRegion.SOUTH) && CheckMirror());
            AddLocation("Swamp Palace", LocationState.INACCESSIBLE, () => false, () => CheckAccess(DarkWorldRegion.SOUTH) && CheckMirror() && CheckFlag("Flippers"));
            AddLocation("Tavern", LocationState.ACCESSIBLE, 259);
            AddLocation("Thief's Chest", LocationState.INACCESSIBLE, () => CheckFlag("Events 2", Events2.THIEFSCHEST), () => CheckLevel("Gloves", GlovesLevel.TITAN) && CheckFlag("Moon Pearl"));
            AddLocation("Thieves' Hideout", LocationState.PARTIAL, 285, 5, () => CheckBombs());
            AddLocation("Tower of Hera", LocationState.INACCESSIBLE, () => CheckDungeon(DungeonItem.TOWEROFHERA, () => CheckBoss(Dungeon.TOWEROFHERA), () => CheckRoom(39), () => CheckRoom(39, 2, false), () => CheckRoom(119), () => CheckRoom(135), () => CheckRoom(135, 7, false)), () => CheckAccess(DeathMountainRegion.NORTHWEST));
            AddLocation("Treasure Chest Game", LocationState.INACCESSIBLE, () => CheckRoom(262, 7, false), () => CheckAccess(DarkWorldRegion.NORTHWEST));
            AddLocation("Turtle Rock", LocationState.INACCESSIBLE, visible: () => CheckAccess(DeathMountainRegion.DARKEAST));
            AddLocation("Waterfall of Wishing", LocationState.INACCESSIBLE, 276, 2, () => CheckFlag("Flippers"), glitchAccessible: () => CheckFlag("Moon Pearl"));
            AddLocation("West Mire", LocationState.INACCESSIBLE, 269, 2, () => CheckAccess(DarkWorldRegion.SOUTHWEST, true));
            AddLocation("Witch", LocationState.INACCESSIBLE, () => CheckFlag("Special Items", SpecialItems.WITCH), () => CheckFlag("Mushroom", MushroomLevel.MUSHROOM) || CheckFlag("Swappable Inventory 1", SwappableInventory1.MUSHROOM));
            AddLocation("Zora River Ledge", LocationState.GLITCHVISIBLE, () => CheckOverworldArea(129), () => CheckFlag("Flippers"), visible: () => CheckFlag("Flippers") || CheckLevel("Gloves", GlovesLevel.POWER), glitchVisible: () => true);
        }
    }
}
