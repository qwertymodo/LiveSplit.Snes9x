using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LiveSplit.SuperMetroid
{
    class Emulator
    {
        public enum MemoryType { ROM, WRAM, SRAM };
        private LiveSplitState currentState;

        internal class EmulatorOffsets
        {
            public string Process { get; internal set; }
            public string Version { get; internal set; }
            public int ROM { get; internal set; }
            public int WRAM { get; internal set; }
            public int SRAM { get; internal set; }
        }

        protected class AddressOffset
        {
            public Type ValueType { get; internal set; }
            public MemoryType BaseMemoryType { get; internal set; }
            public int Address { get; internal set; }
        }

        private Dictionary<string, AddressOffset> addressOffsets = new Dictionary<string, AddressOffset>();

        internal Process emulatorProcess = null;
        private EmulatorOffsets emulatorOffsets = null;

        private Dictionary<string, List<EmulatorOffsets>> offsets = new List<EmulatorOffsets>
        {
            new EmulatorOffsets { Process = "snes9x", Version = "1.53", WRAM = 0x2EFBA4, ROM = 0x2EFBA8, SRAM = 0x2EFBAC },
            new EmulatorOffsets { Process = "snes9x", Version = "1.54.1", WRAM = 0x3410D4, ROM = 0x3410D8, SRAM = 0x3410DC },
            new EmulatorOffsets { Process = "snes9x", Version = "1.55", WRAM = 0x35CFD4, ROM = 0x35CFD8, SRAM = 0x35CFDC },
            new EmulatorOffsets { Process = "snes9x-x64", Version = "1.53", WRAM = 0x405EC8, ROM = 0x405ED0, SRAM = 0x405ED8 },
            new EmulatorOffsets { Process = "snes9x-x64", Version  ="1.54.1", WRAM = 0x4DAF18, ROM = 0x4DAF20, SRAM = 0x4DAF28 }
        }.GroupBy(x => x.Process).ToDictionary(x => x.Key, x => x.ToList());

        private Dictionary<string, MemoryWatcher> watchers = new Dictionary<string, MemoryWatcher>();

        public Emulator()
        {

        }

        private int GetBaseAddress(MemoryType memoryType)
        {
            var offsets = GetOffsets();
            if(offsets != null)
            {
                switch(memoryType)
                {
                    case MemoryType.ROM:
                        return offsets.ROM;

                    case MemoryType.WRAM:
                        return offsets.WRAM;

                    case MemoryType.SRAM:
                        return offsets.SRAM;

                    default:
                        return 0;
                }
            }
            return 0;
        }

        internal EmulatorOffsets GetOffsets()
        {
            if (emulatorProcess?.HasExited ?? true)
            {
                emulatorProcess = null;
                emulatorOffsets = null;
                watchers = null;
            }

            if (emulatorProcess == null)
            {
                try
                {
                    foreach (var process in offsets)
                    {
                        emulatorProcess = Process.GetProcessesByName(process.Key).FirstOrDefault();
                        if (!(emulatorProcess?.HasExited ?? true))
                        {
                            var version = emulatorProcess.MainModuleWow64Safe().FileVersionInfo;
                            foreach (var offset in process.Value)
                            {
                                if (offset.Version == version.ProductVersion)
                                {
                                    emulatorOffsets = offset;
                                    watchers = new Dictionary<string, MemoryWatcher>();

                                    foreach (var address in addressOffsets)
                                        watchers.Add(address.Key, (MemoryWatcher)Activator.CreateInstance(typeof(MemoryWatcher<>).MakeGenericType(address.Value.ValueType), new DeepPointer(GetBaseAddress(address.Value.BaseMemoryType), address.Value.Address)));

                                    return emulatorOffsets;
                                }
                            }
                        }
                    }
                }

                catch (Exception)
                {
                    emulatorProcess = null;
                    emulatorOffsets = null;
                    watchers = null;
                }
            }

            return emulatorOffsets;
        }

        public bool IsRunning()
        {
            return GetOffsets() != null;
        }

        public void RegisterWatcher(string name, Type valueType, MemoryType memoryType, int address)
        {
            if (addressOffsets.ContainsKey(name))
                addressOffsets.Remove(name);

            addressOffsets.Add(name, new AddressOffset { ValueType = valueType, BaseMemoryType = memoryType, Address = address });

            if (IsRunning())
            {
                if (watchers.ContainsKey(name))
                    watchers.Remove(name);

                watchers.Add(name, (MemoryWatcher)Activator.CreateInstance(typeof(MemoryWatcher<>).MakeGenericType(valueType), new DeepPointer(GetBaseAddress(memoryType), address)));
            }
        }

        public MemoryWatcher GetWatcher(string name)
        {
            if(watchers?.Any() ?? false)
                return watchers.ContainsKey(name) ? watchers[name] : null;

            return null;
        }

        public void Update(LiveSplitState state)
        {
            currentState = state;

            if (IsRunning())
            {
                foreach (var watcher in watchers)
                {
                    watcher.Value.Update(emulatorProcess);
                }
            }
        }
    }
}
