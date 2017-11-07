using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using LiveSplit.Snes9x;
using System;

namespace LiveSplit.SuperMetroid.UI.Components
{
    [Flags]
    enum Beams : ushort
    {
        Wave    = 0x0001,
        Ice     = 0x0002,
        Spazer  = 0x0004,
        Plasma  = 0x0008,
        Charge  = 0x1000
    }

    [Flags]
    enum Items : ushort
    {
        VariaSuit       = 0x0001,
        SpringBall      = 0x0002,
        MorphBall       = 0x0004,
        ScrewAttack     = 0x0008,
        GravitySuit     = 0x0020,
        HighJumpBoots   = 0x0100,
        SpaceJump       = 0x0200,
        Bombs           = 0x1000,
        SpeedBooster    = 0x2000,
        Grapple         = 0x4000,
        XRay            = 0x8000
    }

    struct Bosses
    {
        [Flags]
        public enum Crateria : byte
        {
            BombTorizo      = 0x04
        }

        Crateria crateria;

        [Flags]
        public enum Brinstar : byte
        {
            Kraid           = 0x01,
            SporeSpawn      = 0x02
        }

        Brinstar brinstar;

        [Flags]
        public enum Norfair : byte
        {
            Ridley          = 0x01,
            Crocomire       = 0x02,
            GoldenTorizo    = 0x04
        }

        Norfair norfair;

        [Flags]
        public enum WreckedShip : byte
        {
            Phantoon        = 0x01
        }

        WreckedShip wreckedShip;

        [Flags]
        public enum Maridia : byte
        {
            Draygon         = 0x01,
            Botwoon         = 0x02
        }

        Maridia maridia;
    }

    class SuperMetroid : Game
    {
        bool randomized = false;

        public SuperMetroid()
        {
            gameName = "Super Metroid";

            emulator.RegisterWatcher("Game State", typeof(ushort), emulator.MemoryType.WRAM, 0x0998);
            emulator.RegisterWatcher("Room ID", typeof(ushort), emulator.MemoryType.WRAM, 0x079B);
            emulator.RegisterWatcher("Beams", typeof(Beams), emulator.MemoryType.WRAM, 0x09A8);
            emulator.RegisterWatcher("Items", typeof(Items), emulator.MemoryType.WRAM, 0x09A4);
            emulator.RegisterWatcher("Health", typeof(ushort), emulator.MemoryType.WRAM, 0x09C4);
            emulator.RegisterWatcher("Missile Count", typeof(ushort), emulator.MemoryType.WRAM, 0x09C8);
            emulator.RegisterWatcher("Super Missile Count", typeof(ushort), emulator.MemoryType.WRAM, 0x09CC);
            emulator.RegisterWatcher("Power Bomb Count", typeof(ushort), emulator.MemoryType.WRAM, 0x09D0);
            emulator.RegisterWatcher("Reserve Health", typeof(ushort), emulator.MemoryType.WRAM, 0x09D4);
            emulator.RegisterWatcher("IGT Frames", typeof(ushort), emulator.MemoryType.WRAM, 0x09DA);
            emulator.RegisterWatcher("IGT Seconds", typeof(ushort), emulator.MemoryType.WRAM, 0x09DC);
            emulator.RegisterWatcher("IGT Minutes", typeof(ushort), emulator.MemoryType.WRAM, 0x09DE);
            emulator.RegisterWatcher("IGT Hours", typeof(ushort), emulator.MemoryType.WRAM, 0x09E0);

            for (int i = 0; i < 20; ++i)
                emulator.RegisterWatcher("Pickups[" + i + "]", typeof(byte), emulator.MemoryType.WRAM, 0xD870 + i);

            emulator.RegisterWatcher("Bosses", typeof(Bosses), emulator.MemoryType.WRAM, 0xD828);

            for (int i = 0; i < 7; ++i)
                emulator.RegisterWatcher("Bosses[" + i + "]", typeof(byte), emulator.MemoryType.WRAM, 0xD828 + i);
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
                if (name?.StartsWith(gameName) ?? false)
                {
                    ushort state = Get<ushort>("Game State");
                    if (state < 0x2A)
                        return true;
                }

                else if (name?.StartsWith("SMRv") ?? false)
                {
                    randomized = true;
                    ushort state = Get<ushort>("Game State");
                    if (state < 0x2A)
                        return true;
                }
            }

            catch (Exception)
            { }

            return false;
        }

        public override bool IsRunning()
        {
            if (!IsLoaded())
                return false;

            return Get<ushort>("Game State") > 0x05;
        }

        public override TimeSpan GameTime(LiveSplitState state)
        {
            if (!IsRunning())
                return new TimeSpan(0, 0, 0, 0, 0);

            else
                return new TimeSpan(0, Get<ushort>("IGT Hours"), Get<ushort>("IGT Minutes"), Get<ushort>("IGT Seconds"), Get<ushort>("IGT Frames") * 1000 / 60);
        }
    }
}
