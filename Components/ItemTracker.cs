using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using static LiveSplit.Snes9x.Game;

namespace LiveSplit.Snes9x
{
    class ItemTracker : IComponent
    {
        public virtual string ComponentName => "Item Tracker Base Class";

        public virtual float HorizontalWidth { get; set; }

        public virtual float MinimumHeight => 0f;

        public virtual float VerticalHeight { get; set; }

        public virtual float MinimumWidth => 0f;

        public virtual float PaddingTop => 0f;

        public virtual float PaddingBottom => 0f;

        public virtual float PaddingLeft => 0f;

        public virtual float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        protected ImageDict icons = new ImageDict();

        protected Dictionary<string, dynamic> items = new Dictionary<string, dynamic>();

        protected Color backgroundColor = Color.Black;


        protected void AddItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, T target = default(T)) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, target, Comparator.GetComparator<T>(comparator)));
        }


        protected void AddFlagItem<T>(int x, int y, int scale, string name, string field, T flag, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolImageWatcher<T>(field, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, flag, Comparator.TestFlag<T>(set)));
        }


        protected void AddFlagItem(int x, int y, int scale, string name, bool set = true)
        {
            AddFlagItem(x, y, scale, name, name, BoolFlag.TRUE, set);
        }


        protected void AddIndexItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.GetComparator<T>(comparator)));
        }


        protected void AddIndexItem<T>(int x, int y, int scale, string name, string icon, Comparator.Type comparator, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(icon, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.GetComparator<T>(comparator)));
        }


        protected void AddFlagIndexItem<T>(int x, int y, int scale, string name, string field, List<T> targets, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(field, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.TestFlag<T>(set)));
        }


        protected void AddCounter(int x, int y, int scale, int digits, string name, string image)
        {
            List<Image> images;
            icons.TryGetValue(image, out images);
            for (int i = digits; i > 0; --i)
            {
                int digit = i;
                items.Add(name + "[" + i + "]", new IndexImageWatcher<ushort>(name, images, x + ((digits - i) * images[0].Width * scale) + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, new List<ushort> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, (cur, tar) => {
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


        public ItemTracker()
        {
        }

        public virtual void Dispose()
        {
        }

        protected virtual void DrawGeneral(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, width, height);

            if (!GameLoader.game?.IsRaceROM() ?? true)
            {
                foreach (var item in items)
                {
                    item.Value.GetType().GetMethod("Draw")?.Invoke(item.Value, new object[] { g, state, width, height, mode });
                }
            }
        }

        public virtual void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            if (HorizontalWidth < MinimumWidth)
                HorizontalWidth = MinimumWidth;

            DrawGeneral(g, state, HorizontalWidth, height, LayoutMode.Horizontal);
        }

        public virtual void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            if (VerticalHeight < MinimumHeight)
                VerticalHeight = MinimumHeight;

            DrawGeneral(g, state, width, VerticalHeight, LayoutMode.Vertical);
        }

        public virtual XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("x");
        }

        public virtual Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public virtual void SetSettings(XmlNode settings)
        {
        }

        public virtual void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            invalidator?.Invalidate(0, 0, width, height);
        }
    }
}
