using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.SuperMetroid.UI.Components
{
    class ItemTracker : IComponent
    {
        private Images itemIcons = new Images();
        private Dictionary<string, dynamic> items = new Dictionary<string, dynamic>();

        public string ComponentName => "Super Metroid Item Tracker";

        public float HorizontalWidth { get; set; }

        public float MinimumHeight => 336;

        public float VerticalHeight { get; set; }

        public float MinimumWidth => 320;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;


        class BossWatcherImage<T> : BoolWatcherImage<T> where T : struct, IComparable
        {
            public BossWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T flag)
            : base(name, frames, x, y, center, height, width, flag, Comparator.TestFlag<T>(false))
            { }

            public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                base.Update(g, state, width, height, mode);

                Draw(g, (int)width, (int)height, updateFunc(Current, Target), 1);
            }
        }


        private void AddItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, T target = default(T)) where T: struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolWatcherImage<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, target, Comparator.GetComparator<T>(comparator)));
        }


        private void AddFlagItem<T>(int x, int y, int scale, string name, string field, T flag, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolWatcherImage<T>(field, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, flag, Comparator.TestFlag<T>(set)));
        }


        private void AddIndexItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new IndexWatcherImage<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.GetComparator<T>(comparator)));
        }


        private void AddCounter(int x, int y, int scale, int digits, string name)
        {
            List<Image> images;
            itemIcons.TryGetValue("HUD Digits", out images);
            for (int i = digits; i > 0; --i)
            {
                int digit = i;
                items.Add(name + "[" + i + "]", new IndexWatcherImage<ushort>(name, images, x + ((digits - i) * images[0].Width * scale) + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, new List<ushort> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, (cur, tar) => {
                    ushort pow = (ushort)(digit - 1);
                    ushort div = (ushort)Math.Pow(10, pow);

                    if (div > 0)
                        cur /= div;

                    cur %= 10;
                    return cur == tar;
                }
                ));
            }
        }


        private void AddBoss<T>(int x, int y, int height, int width, string name, int idx, T flag) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BossWatcherImage<T>("Bosses[" + idx + "]", images, x + 1, y + 1, false, height, width, flag));
        }


        public ItemTracker()
        {
            AddIndexItem(16, 16, 4, "Missile Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });
            AddIndexItem(136, 16, 4, "Super Missile Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });
            AddIndexItem(224, 16, 4, "Power Bomb Count", Comparator.Type.GREATEROREQUAL, new List<ushort> { 0, 1 });

            AddCounter(28, 80, 3, 3, "Missile Count");
            AddCounter(144, 80, 3, 2, "Super Missile Count");
            AddCounter(232, 80, 3, 2, "Power Bomb Count");

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

        public void Dispose()
        {
        }

        private void DrawGeneral(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);

            foreach(var item in items)
            {
                item.Value.GetType().GetMethod("Update")?.Invoke(item.Value, new object[] { g, state, width, height, mode });
            }
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            if (HorizontalWidth < MinimumWidth)
                HorizontalWidth = MinimumWidth;

            DrawGeneral(g, state, HorizontalWidth, height, LayoutMode.Horizontal);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            if (VerticalHeight < MinimumHeight)
                VerticalHeight = MinimumHeight;

            DrawGeneral(g, state, width, VerticalHeight, LayoutMode.Vertical);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("x");
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public void SetSettings(XmlNode settings)
        {
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            invalidator?.Invalidate(0, 0, width, height);
        }
    }
}
