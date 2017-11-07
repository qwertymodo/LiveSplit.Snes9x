using LiveSplit.Snes9x;
using System.Collections.Generic;
using static LiveSplit.ALinkToThePast.UI.Components.ALinkToThePast;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class ItemTracker : Snes9x.ItemTracker
    {
        public override string ComponentName => "A Link to the Past Item Tracker";

        public override float MinimumHeight => 336;

        public override float MinimumWidth => 360;

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
