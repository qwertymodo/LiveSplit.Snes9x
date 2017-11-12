using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using System;

namespace LiveSplit.Snes9x
{
    class Game
    {
        public enum REGION { NTSC_U, NTSC_J, PAL };

        protected string gameName = null;
        
        protected REGION region;

        protected TimerModel timer = new TimerModel();

        protected emulator emulator = new emulator();

        protected DeepPointer romName;

        protected bool randomized = false;

        protected bool tourney = false;

        [Flags]
        public enum BoolFlag : byte
        {
            FALSE = 0x00,
            TRUE = 0x01,
        }

        public T Get<T>(string name, int index = -1) where T : struct, IComparable
        {
            return ((MemoryWatcher<T>)emulator.GetWatcher(name, index))?.Current ?? default(T);
        }

        public T Previous<T>(string name, int index = -1) where T : struct, IComparable
        {
            return ((MemoryWatcher<T>)emulator.GetWatcher(name, index))?.Old ?? default(T);
        }

        public virtual bool IsLoaded()
        {
            return false;
        }

        public virtual bool IsRunning()
        {
            return false;
        }

        public bool IsRandomized()
        {
            return IsLoaded() && randomized;
        }

        public bool IsRaceROM()
        {
            return IsLoaded() && tourney;
        }

        public virtual TimeSpan GameTime(LiveSplitState state)
        {
            return state.CurrentTime.RealTime ?? new TimeSpan(0);
        }

        public virtual void Update(LiveSplitState state)
        {
            emulator.Update(state);

            if (!state.IsGameTimeInitialized)
            {
                timer.CurrentState = state;
                timer.InitializeGameTime();
            }

            state.SetGameTime(GameTime(state));
        }
    }


    class GameLoader
    {
        private static Game _game = null;

        public static Game game { get { return _game; } }

        public static void Load(Game game)
        {
            _game = game;
        }
    }
}