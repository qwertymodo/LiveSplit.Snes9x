using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static LiveSplit.ALinkToThePast.UI.Components.ALinkToThePast;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class ItemTracker : IComponent
    {
        private Images itemIcons = new Images();
        private Dictionary<string, dynamic> items = new Dictionary<string, dynamic>();

        public string ComponentName => "A Link to the Past Item Tracker";

        public float HorizontalWidth { get; set; }

        public float MinimumHeight => 336;

        public float VerticalHeight { get; set; }

        public float MinimumWidth => 360;

        public float PaddingTop => 0f;

        public float PaddingBottom => 0f;

        public float PaddingLeft => 0f;

        public float PaddingRight => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;


        private void AddItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, T target = default(T)) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, target, Comparator.GetComparator<T>(comparator)));
        }


        private void AddFlagItem<T>(int x, int y, int scale, string name, string field, T flag, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new BoolImageWatcher<T>(field, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, flag, Comparator.TestFlag<T>(set)));
        }


        private void AddFlagItem(int x, int y, int scale, string name, bool set = true)
        {
            AddFlagItem(x, y, scale, name, name, BoolFlag.TRUE, set);
        }


        private void AddIndexItem<T>(int x, int y, int scale, string name, Comparator.Type comparator, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.GetComparator<T>(comparator)));
        }


        private void AddIndexItem<T>(int x, int y, int scale, string name, string icon, Comparator.Type comparator, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(icon, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.GetComparator<T>(comparator)));
        }


        private void AddFlagIndexItem<T>(int x, int y, int scale, string name, string field, List<T> targets, bool set = true) where T : struct, IComparable
        {
            List<Image> images;
            itemIcons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new IndexImageWatcher<T>(field, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets, Comparator.TestFlag<T>(set)));
        }


        private void AddCounter(int x, int y, int scale, int digits, string name)
        {
            List<Image> images;
            itemIcons.TryGetValue("HUD Digits", out images);
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
            AddCounter(16, 296, 3, 4, "Rupees");

            //AddIndexItem(16, 16, 3, "Bow", Comparator.Type.EQUAL, new List<BowLevel> { BowLevel.BOW, BowLevel.ARROW, BowLevel.SILVER });
            AddFlagIndexItem(16, 16, 3, "Bow", "Swappable Inventory 2", new List<SwappableInventory2> { SwappableInventory2.BOW, SwappableInventory2.SILVERARROW, (SwappableInventory2.BOW | SwappableInventory2.SILVERARROW) });
            //AddIndexItem(72, 16, 3, "Boomerang", Comparator.Type.EQUAL, new List<BoomerangLevel> { BoomerangLevel.BLUE, BoomerangLevel.RED });
            AddFlagIndexItem(72, 16, 3, "Boomerang", "Swappable Inventory 1", new List<SwappableInventory1> { SwappableInventory1.BLUEBOOMERANG, SwappableInventory1.REDBOOMERANG, (SwappableInventory1.BLUEBOOMERANG | SwappableInventory1.REDBOOMERANG) });
            AddFlagItem(128, 16, 3, "Hookshot");
            //AddFlagItem(184, 16, 3, "Mushroom");
            AddFlagItem(184, 16, 3, "Mushroom", "Swappable Inventory 1", SwappableInventory1.MUSHROOM);
            //AddFlagItem(240, 16, 3, "Magic Powder");
            AddFlagItem(240, 16, 3, "Magic Powder", "Swappable Inventory 1", SwappableInventory1.MAGICPOWDER);
            AddIndexItem(296, 16, 3, "Bottle 1", "Bottle", Comparator.Type.EQUAL, new List<BottleItem> { BottleItem.EMPTY, BottleItem.REDPOTION, BottleItem.GREENPOTION, BottleItem.BLUEPOTION, BottleItem.FAIRY, BottleItem.BEE, BottleItem.GOODBEE });

            AddFlagItem(16, 72, 3, "Fire Rod");
            AddFlagItem(72, 72, 3, "Ice Rod");
            AddFlagItem(128, 72, 3, "Bombos Medallion");
            AddFlagItem(184, 72, 3, "Ether Medallion");
            AddFlagItem(240, 72, 3, "Quake Medallion");
            AddIndexItem(296, 72, 3, "Bottle 2", "Bottle", Comparator.Type.EQUAL, new List<BottleItem> { BottleItem.EMPTY, BottleItem.REDPOTION, BottleItem.GREENPOTION, BottleItem.BLUEPOTION, BottleItem.FAIRY, BottleItem.BEE, BottleItem.GOODBEE });

            AddFlagItem(16, 128, 3, "Lantern");
            AddFlagItem(72, 128, 3, "Hammer");
            //AddFlagItem(128, 128, 3, "Shovel");
            AddFlagItem(128, 128, 3, "Shovel", "Swappable Inventory 1", SwappableInventory1.SHOVEL);
            //AddFlagItem(184, 128, 3, "Flute");
            AddFlagIndexItem(184, 128, 3, "Flute", "Swappable Inventory 1", new List<SwappableInventory1> { SwappableInventory1.FLUTE, SwappableInventory1.ACTIVATEDFLUTE });
            AddFlagItem(240, 128, 3, "Net");
            AddIndexItem(296, 128, 3, "Bottle 3", "Bottle", Comparator.Type.EQUAL, new List<BottleItem> { BottleItem.EMPTY, BottleItem.REDPOTION, BottleItem.GREENPOTION, BottleItem.BLUEPOTION, BottleItem.FAIRY, BottleItem.BEE, BottleItem.GOODBEE });

            AddFlagItem(16, 184, 3, "Cane of Somaria");
            AddFlagItem(72, 184, 3, "Cane of Byrna");
            AddFlagItem(128, 184, 3, "Magic Cape");
            AddFlagItem(184, 184, 3, "Magic Mirror", "Magic Mirror", MirrorLevel.MIRROR);
            AddFlagItem(240, 184, 3, "Book of Mudora");
            AddIndexItem(296, 184, 3, "Bottle 4", "Bottle", Comparator.Type.EQUAL, new List<BottleItem> { BottleItem.EMPTY, BottleItem.REDPOTION, BottleItem.GREENPOTION, BottleItem.BLUEPOTION, BottleItem.FAIRY, BottleItem.BEE, BottleItem.GOODBEE });

            AddFlagItem(16, 240, 3, "Boots");
            AddIndexItem(72, 240, 3, "Gloves", Comparator.Type.EQUAL, new List<GlovesLevel> { GlovesLevel.POWER, GlovesLevel.TITAN });
            AddFlagItem(128, 240, 3, "Flippers");
            AddFlagItem(184, 240, 3, "Moon Pearl");
        }

        public void Dispose()
        {
        }

        private void DrawGeneral(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);

            foreach (var item in items)
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
