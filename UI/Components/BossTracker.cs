using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Model;
using LiveSplit.UI;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Imaging;
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

        class BossHealthWatcherImage : GreaterWatcherImage<ushort>
        {
            public BossHealthWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, ushort segment)
                : base(name, frames, x, y, center, height, width, segment)
            {

            }
        }


        class BossWatcherImage : ByteBitClearWatcherImage
        {
            public BossWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, byte mask)
            : base(name, frames, x, y, center, height, width, mask)
            { }

            public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                Previous = Current;
                Current = SuperMetroidComponent.game.Get<byte>(Name);

                Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && (Current & Target) == 0, 1);
            }
        }


        public BossTracker()
        {
            List<Image> images;

            bossImages.TryGetValue("Kraid", out images);
            if (images?.Any() ?? false)
                bosses.Add("Kraid", new BossWatcherImage("Bosses[1]", images, -16, -24, true, 96, 96, 0x01));

            bossImages.TryGetValue("Draygon", out images);
            if (images?.Any() ?? false)
                bosses.Add("Draygon", new BossWatcherImage("Bosses[4]", images, 16, -24, true, 96, 96, 0x01));

            bossImages.TryGetValue("Phantoon", out images);
            if (images?.Any() ?? false)
                bosses.Add("Phantoon", new BossWatcherImage("Bosses[3]", images, -4, -20, true, 72, 72, 0x01));

            bossImages.TryGetValue("Ridley", out images);
            if (images?.Any() ?? false)
                bosses.Add("Ridley", new BossWatcherImage("Bosses[2]", images, 12, 28, true, 88, 88, 0x01));
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
            if (SuperMetroidComponent.game.IsLoaded())
                invalidator?.Invalidate(0, 0, width, height);
        }
    }
}
