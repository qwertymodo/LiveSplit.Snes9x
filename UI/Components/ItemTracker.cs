using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        public float MinimumHeight => 192;

        public float VerticalHeight { get; set; }

        public float MinimumWidth => 224;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        private void AddGreaterItem<T>(string name, int x, int y, ushort compareTo = 0)
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new GreaterWatcherImage<ushort>(name, images, x + 1, y + 1, false, 32, 32, compareTo));
        }

        private void AddByteFlagItem(string name, string field, byte flag, int x, int y)
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new ByteBitSetWatcherImage(field, images, x + 1, y + 1, false, 32, 32, flag));
        }

        private void AddShortFlagItem(string name, string field, ushort flag, int x, int y)
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new ShortBitSetWatcherImage(field, images, x + 1, y + 1, false, 32, 32, flag));
        }

        public ItemTracker()
        {
            AddGreaterItem<ushort>("Missiles", 0, 0);
            AddGreaterItem<ushort>("Super Missiles", 32, 0);
            AddGreaterItem<ushort>("Power Bombs", 64, 0);
            AddShortFlagItem("Grapple Beam", "Items", 0x4000, 96, 0);
            AddShortFlagItem("X-Ray Scope", "Items", 0x8000, 128, 0);
            AddShortFlagItem("Varia Suit", "Items", 0x0001, 0, 32);
            AddShortFlagItem("Gravity Suit", "Items", 0x0020, 32, 32);
            AddShortFlagItem("Morph Ball", "Items", 0x0004, 64, 32);
            AddShortFlagItem("Bombs", "Items", 0x1000, 96, 32);
            AddShortFlagItem("Spring Ball", "Items", 0x0002, 128, 32);
            AddShortFlagItem("High Jump Boots", "Items", 0x0100, 0, 64);
            AddShortFlagItem("Speed Booster", "Items", 0x2000, 32, 64);
            AddShortFlagItem("Space Jump", "Items", 0x0200, 64, 64);
            AddShortFlagItem("Screw Attack", "Items", 0x0008, 96, 64);
            AddGreaterItem<ushort>("Reserve Tank", 128, 64);
            AddShortFlagItem("Charge Beam", "Beams", 0x1000, 0, 96);
            AddShortFlagItem("Spazer", "Beams", 0x0004, 32, 96);
            AddShortFlagItem("Ice Beam", "Beams", 0x0002, 64, 96);
            AddShortFlagItem("Wave Beam", "Beams", 0x0001, 96, 96);
            AddShortFlagItem("Plasma Beam", "Beams", 0x0008, 128, 96);
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
            if(SuperMetroidComponent.game.IsLoaded())
                invalidator?.Invalidate(0, 0, width, height);
        }
    }
}
