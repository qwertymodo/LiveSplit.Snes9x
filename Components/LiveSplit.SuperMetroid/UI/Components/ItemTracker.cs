using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LiveSplit.SuperMetroid.UI.Components
{
    class ItemTracker : Snes9x.ItemTracker
    {
        public override string ComponentName => "Super Metroid Item Tracker";

        public override float MinimumHeight => 336;

        public override float MinimumWidth => 320;

        class BossWatcherImage<T> : BoolImageWatcher<T> where T : struct, IComparable
        {
            public BossWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T flag)
            : base(name, frames, x, y, center, height, width, flag, Comparator.TestFlag<T>(false))
            { }

            public override void Draw(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                base.Draw(g, state, width, height, mode);

                Draw(g, (int)width, (int)height, updateFunc(Current, Target), 1);
            }
        }

        private void AddBoss<T>(int x, int y, int height, int width, string name, int idx, T flag) where T : struct, IComparable
        {
            List<Image> images = icons[name];
            if (images != null)
                items.Add(name, new BossWatcherImage<T>("Bosses[" + idx + "]", images, x + 1, y + 1, false, height, width, flag));
        }


        public ItemTracker()
        {
            icons = new Images();

            AddIndexItem(16, 16, 4, "Missile Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });
            AddIndexItem(136, 16, 4, "Super Missile Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });
            AddIndexItem(224, 16, 4, "Power Bomb Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });

            AddCounter(28, 80, 3, 3, "Missile Count", "HUD Digits");
            AddCounter(144, 80, 3, 2, "Super Missile Count", "HUD Digits");
            AddCounter(232, 80, 3, 2, "Power Bomb Count", "HUD Digits");

            AddFlagItem(16, 104, 3, "Charge Beam", "Beams", Beams.Charge);
            AddFlagItem(72, 104, 3, "Spazer", "Beams", Beams.Spazer);
            AddFlagItem(128, 104, 3, "Ice Beam", "Beams", Beams.Ice);
            AddFlagItem(184, 104, 3, "Wave Beam", "Beams", Beams.Wave);
            AddFlagItem(240, 104, 3, "Plasma Beam", "Beams", Beams.Plasma);

            AddFlagItem(16, 160, 3, "Varia Suit", "Items", Items.VariaSuit);
            AddFlagItem(72, 160, 3, "Gravity Suit", "Items", Items.GravitySuit);
            AddFlagItem(128, 160, 3, "Morph Ball", "Items", Items.MorphBall);
            AddFlagItem(184, 160, 3, "Bombs", "Items", Items.Bombs);
            AddFlagItem(240, 160, 3, "Spring Ball", "Items", Items.SpringBall);

            AddFlagItem(128, 216, 3, "High Jump Boots", "Items", Items.HighJumpBoots);
            AddFlagItem(184, 216, 3, "Space Jump", "Items", Items.SpaceJump);
            AddFlagItem(240, 216, 3, "Speed Booster", "Items", Items.SpeedBooster);

            AddFlagItem(128, 272, 3, "Screw Attack", "Items", Items.ScrewAttack);
            AddFlagItem(184, 272, 3, "Grapple Beam", "Items", Items.Grapple);
            AddFlagItem(240, 272, 3, "X-Ray Scope", "Items", Items.XRay);

            AddBoss(0, 264, 96, 96, "Kraid", 1, Bosses.Brinstar.Kraid);
            AddBoss(32, 264, 96, 96, "Draygon", 4, Bosses.Maridia.Draygon);
            AddBoss(32, 272, 72, 72, "Phantoon", 3, Bosses.WreckedShip.Phantoon);
            AddBoss(32, 216, 88, 88, "Ridley", 2, Bosses.Norfair.Ridley);
        }
    }
}
