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

        public T Get<T>(string name) where T : struct, IComparable
        {
            if (emulator.GetWatcher(name) == null)
                return default(T);

            return ((MemoryWatcher<T>)(emulator.GetWatcher(name))).Current;
        }

        public T Previous<T>(string name) where T : struct, IComparable
        {
            if (emulator.GetWatcher(name) == null)
                return default(T);

            return ((MemoryWatcher<T>)(emulator.GetWatcher(name))).Old;
        }

        public virtual bool IsLoaded()
        {
            return false;
        }

        public virtual bool IsRunning()
        {
            return false;
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