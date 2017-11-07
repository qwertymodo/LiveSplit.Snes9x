using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Drawing2D;

namespace LiveSplit.SuperMetroid.UI.Components
{
    class BossTracker : IComponent
    {
        private Images bossImages = new Images();
        private Dictionary<string, dynamic> bosses = new Dictionary<string, dynamic>();

        public string ComponentName => "Super Metroid Boss Tracker";

        public float HorizontalWidth { get; set; }

        public float MinimumHeight => 176f;

        public float VerticalHeight { get; set; }

        public float MinimumWidth => 128f;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        class BossHealthWatcherImage : ComparisonImageWatcher<ushort>
        {
            public BossHealthWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, ushort segment)
                : base(name, frames, x, y, center, height, width, segment)
            {

            }
        }


        class BossWatcherImage<T> : BoolImageWatcher<T> where T : struct, IComparable
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


        private void AddBoss<T>(int x, int y, int height, int width, string name, int idx, T flag) where T : struct, IComparable
        {
            List<Image> images;
            bossImages.TryGetValue(name, out images);
            if (images != null)
                bosses.Add(name, new BossWatcherImage<T>("Bosses[" + idx + "]", images, x, y, true, height, width, flag));
        }


        public BossTracker()
        {
            AddBoss(-16, -24, 96, 96, "Kraid", 1, Bosses.Brinstar.Kraid);
            AddBoss(16, -24, 96, 96, "Draygon", 4, Bosses.Maridia.Draygon);
            AddBoss(-4, -20, 72, 72, "Phantoon", 3, Bosses.WreckedShip.Phantoon);
            AddBoss(12, 28, 88, 88, "Ridley", 2, Bosses.Norfair.Ridley);
        }

        public void Dispose()
        {
        }

        private void DrawGeneral(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);

            foreach (var boss in bosses)
            {
                boss.Value.GetType().GetMethod("Update")?.Invoke(boss.Value, new object[] { g, state, width, height, mode });
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
