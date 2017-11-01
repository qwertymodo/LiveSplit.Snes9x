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

        public float MinimumHeight => 192;

        public float VerticalHeight { get; set; }

        public float MinimumWidth => 224;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        private void AddItem<T>(int x, int y, string name, Comparator.Type comparator, T target = default(T)) where T: struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolWatcherImage<T>(name, images, x + 1, y + 1, false, 32, 32, target, Comparator.GetComparator<T>(comparator)));
        }

        private void AddFlagItem<T>(int x, int y, string name, string field, T flag, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolWatcherImage<T>(field, images, x + 1, y + 1, false, 32, 32, flag, Comparator.TestFlag<T>(set)));
        }

        public ItemTracker()
        {
            AddItem<ushort>(0, 0, "Missiles", Comparator.Type.GREATER, 0);
            AddItem<ushort>(32, 0, "Super Missiles", Comparator.Type.GREATER, 0);
            AddItem<ushort>(64, 0, "Power Bombs", Comparator.Type.GREATER, 0);
            AddFlagItem(96, 0, "Grapple Beam", "Items", Items.GravitySuit);
            AddFlagItem(128, 0, "X-Ray Scope", "Items", Items.XRay);

            AddFlagItem(0, 32, "Varia Suit", "Items", Items.VariaSuit);
            AddFlagItem(32, 32, "Gravity Suit", "Items", Items.GravitySuit);
            AddFlagItem(64, 32, "Morph Ball", "Items", Items.MorphBall);
            AddFlagItem(96, 32, "Bombs", "Items", Items.Bombs);
            AddFlagItem(128, 32, "Spring Ball", "Items", Items.SpringBall);

            AddFlagItem(0, 64, "High Jump Boots", "Items", Items.HighJumpBoots);
            AddFlagItem(32, 64, "Speed Booster", "Items", Items.SpeedBooster);
            AddFlagItem(64, 64, "Space Jump", "Items", Items.SpaceJump);
            AddFlagItem(96, 64, "Screw Attack", "Items", Items.ScrewAttack);
            AddItem<ushort>(128, 64, "Reserve Tank", Comparator.Type.GREATER, 0);

            AddFlagItem(0, 96, "Charge Beam", "Beams", Beams.Charge);
            AddFlagItem(32, 96, "Spazer", "Beams", Beams.Spazer);
            AddFlagItem(64, 96, "Ice Beam", "Beams", Beams.Ice);
            AddFlagItem(96, 96, "Wave Beam", "Beams", Beams.Wave);
            AddFlagItem(128, 96, "Plasma Beam", "Beams", Beams.Plasma);
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
