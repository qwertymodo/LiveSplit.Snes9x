using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.SuperMetroid
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

    class Bosses
    {
        [Flags]
        public enum Crateria : byte
        {
            BombTorizo      = 0x04
        }

        [Flags]
        public enum Brinstar : byte
        {
            Kraid           = 0x01,
            SporeSpawn      = 0x02
        }

        [Flags]
        public enum Norfair : byte
        {
            Ridley          = 0x01,
            Crocomire       = 0x02,
            GoldenTorizo    = 0x04
        }

        [Flags]
        public enum WreckedShip : byte
        {
            Phantoon        = 0x01
        }

        [Flags]
        public enum Maridia : byte
        {
            Draygon         = 0x01,
            Botwoon         = 0x02
        }
    }

    class Game
    {
        string GameName => "Super Metroid";
        private Emulator emulator = new Emulator();
        DeepPointer ROMName;

        public Game()
        {
            emulator.RegisterWatcher("Game State", typeof(ushort), Emulator.MemoryType.WRAM, 0x0998);
            emulator.RegisterWatcher("Room ID", typeof(ushort), Emulator.MemoryType.WRAM, 0x079B);
            emulator.RegisterWatcher("Beams", typeof(Beams), Emulator.MemoryType.WRAM, 0x09A8);
            emulator.RegisterWatcher("Items", typeof(Items), Emulator.MemoryType.WRAM, 0x09A4);
            emulator.RegisterWatcher("Energy Tank", typeof(ushort), Emulator.MemoryType.WRAM, 0x09C4);
            emulator.RegisterWatcher("Missiles", typeof(ushort), Emulator.MemoryType.WRAM, 0x09C8);
            emulator.RegisterWatcher("Super Missiles", typeof(ushort), Emulator.MemoryType.WRAM, 0x09CC);
            emulator.RegisterWatcher("Power Bombs", typeof(ushort), Emulator.MemoryType.WRAM, 0x09D0);
            emulator.RegisterWatcher("Reserve Tank", typeof(ushort), Emulator.MemoryType.WRAM, 0x09D4);

            for (int i = 0; i < 20; ++i)
                emulator.RegisterWatcher("Pickups[" + i + "]", typeof(byte), Emulator.MemoryType.WRAM, 0xD870 + i);

            for (int i = 0; i < 7; ++i)
                emulator.RegisterWatcher("Bosses[" + i + "]", typeof(byte), Emulator.MemoryType.WRAM, 0xD828 + i);
        }

        public T Get<T>(string name)
        {
            if (emulator.GetWatcher(name) == null)
                return default(T);

            return (T)emulator.GetWatcher(name).Current;
        }

        public bool IsLoaded()
        {
            if (!emulator.IsRunning())
                return false;

            ROMName = new DeepPointer(emulator.GetOffsets().ROM, 0x7FC0);
            string name;
            ROMName.DerefString(emulator.emulatorProcess, 21, out name);
            if (name != null && name.StartsWith(GameName))
            {
                ushort state = Get<ushort>("Game State");
                if (state < 0x0100 && state > 0x05)
                    return true;
            }

            return false;
        }

        public void Update(LiveSplitState state)
        {
            emulator.Update(state);
        }
    }
}
