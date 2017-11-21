using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using static LiveSplit.ALinkToThePast.UI.Components.ALinkToThePast;
using static LiveSplit.Snes9x.Game;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class ItemTracker : Snes9x.ItemTracker
    {
        public override string ComponentName => "A Link to the Past Item Tracker";

        public override float MinimumHeight => 336;

        public override float MinimumWidth => 360;

        class TunicImageWatcher : ImageWatcher<Tuple<ArmorLevel,BoolFlag>>
        {
            protected List<Tuple<ArmorLevel, BoolFlag>> Targets;
            protected Tuple<ArmorLevel, BoolFlag> Current;
            protected Tuple<ArmorLevel, BoolFlag> Previous;

            public TunicImageWatcher(string name, List<Image> frames, int x, int y, bool center, int width, int height)
            : base(name, frames, x, y, center, width, height)
            {
                Targets = new List<Tuple<ArmorLevel, BoolFlag>> {
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.GREEN, BoolFlag.FALSE),
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.BLUE, BoolFlag.FALSE),
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.RED, BoolFlag.FALSE),
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.GREEN, BoolFlag.TRUE),
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.BLUE, BoolFlag.TRUE),
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.RED, BoolFlag.TRUE)
                };
            }

            protected virtual void Draw(Graphics g, int width, int height, int index, float opacity = 0.5f)
            {
                if (index >= Frames.Count)
                    index = Frames.Count - 1;

                if (index < 0)
                    Draw(g, width, height, false, opacity);

                else
                {
                    if (Centered)
                        DrawImageFromCenter(g, Frames[index], X, Y, Width, Height, width, height);

                    else
                        g.DrawImage(Frames[index], X, Y, Width, Height);
                }
            }

            public override void Draw(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                Update();

                for (int i = Targets.Count - 1; i >= 0; --i)
                {
                    if (Current.Equals(Targets[i]))
                    {
                        Draw(g, (int)width, (int)height, i);
                        return;
                    }
                }

                Draw(g, (int)width, (int)height, true);
            }

            protected void Update()
            {
                Previous = Current;
                Current = (GameLoader.game?.IsRunning() ?? false) ?
                    new Tuple<ArmorLevel, BoolFlag>(GameLoader.game.Get<ArmorLevel>("Tunic"), GameLoader.game.Get<BoolFlag>("Moon Pearl")) :
                    new Tuple<ArmorLevel, BoolFlag>(ArmorLevel.GREEN, BoolFlag.FALSE);
            }
        }

        protected void AddTunic(int x, int y, int scale)
        {
            List<Image> images;
            icons.TryGetValue("Tunic", out images);
            if (images != null)
                items.Add("Tunic", new TunicImageWatcher("Tunic", images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale));
        }

        class EquipmentImageWatcher<T> : IndexImageWatcher<T> where T : struct, IComparable
        {
            public EquipmentImageWatcher(string name, List<Image> frames, int x, int y, bool center, int width, int height, List<T> targets)
                : base(name, frames, x, y, center, width, height, targets)
            { }

            public override void Draw(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                base.Draw(g, state, width, height, mode);

                Draw(g, (int)width, (int)height, updateFunc(Current, Target), 0);
            }
        }

        protected void AddEquipment<T>(int x, int y, int scale, string name, List<T> targets) where T : struct, IComparable
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            if (images != null)
                items.Add(name, new EquipmentImageWatcher<T>(name, images, x + 1, y + 1, false, images[0].Height * scale, images[0].Width * scale, targets));
        }

        public ItemTracker()
        {
            icons = new Images();

            AddCounter(16, 296, 3, 4, "Rupees", "HUD Digits");

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
    }
}
