using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class LightWorldMapTracker : MapTracker
    {
        protected override Bitmap backgroundImage => Properties.Resources.LightWorldMap;

        public LightWorldMapTracker()
        {
            HorizontalWidth = MinimumWidth;
            VerticalHeight = MinimumHeight;

            icons = new Images();

            AddLocation("Aginah's Cave", 92, 412);
            AddLocation("Blacksmiths", 148, 264);
            AddLocation("Bombos Tablet", 96, 460);
            AddLocation("Bonk Rocks", 192, 144);
            AddLocation("Bottle Vendor", 40, 228);
            AddLocation("Checkerboard Cave", 80, 392);
            AddLocation("Chicken House", 43, 264);
            AddLocation("Death Mountain East", 432, 104);
            AddLocation("Desert Cliff", 4, 456);
            AddLocation("Desert Palace", 25, 392, 24, 24);
            AddLocation("Eastern Palace", 478, 192, 24, 24);
            AddLocation("Ether Tablet", 208, 4);
            AddLocation("Floating Island", 408, 4);
            AddLocation("Floodgate", 231, 468);
            AddLocation("Graveyard Cliff", 280, 132);
            AddLocation("Haunted Grove", 136, 328);
            AddLocation("Hobo", 346, 346);
            AddLocation("Hyrule Castle Dungeon", 247, 216, 16, 8);
            AddLocation("Hyrule Castle Tower", 243, 188, 24, 24);
            AddLocation("Kakariko Well", 4, 208);
            AddLocation("King's Tomb", 300, 144);
            AddLocation("King Zora", 488, 48);
            AddLocation("Forest Hideout", 88, 64);
            AddLocation("Lake Hylia Cave", 448, 384);
            AddLocation("Lake Hylia Island", 360, 416);
            AddLocation("Library", 72, 328);
            AddLocation("Link's House", 272, 340);
            AddLocation("Lost Old Man", 200, 88);
            AddLocation("Lost Woods", 56, 40);
            AddLocation("Lumberjack's Tree", 144, 32);
            AddLocation("Mad Batter", 156, 280);
            AddLocation("Master Sword Pedestal", 8, 8);
            AddLocation("Mimic Cave", 424, 40);
            AddLocation("Moldorm Cave", 324, 472);
            AddLocation("Race", 8, 344);
            AddLocation("Sahasrahla", 408, 228, 16, 8);
            AddLocation("Sahasrahla's Shrine", 408, 220, 16, 8);
            AddLocation("Sanctuary", 227, 128);
            AddLocation("Secret Passage", 294, 208);
            AddLocation("Sewer Dark Room", 247, 224, 16, 8);
            AddLocation("Sewer Escape", 256, 140, 16, 8);
            AddLocation("Sewer Escape Dark Room", 256, 148, 16, 8);
            AddLocation("Sick Kid", 71, 262);
            AddLocation("South of Grove", 128, 412);
            AddLocation("Spectacle Rock", 256, 32);
            AddLocation("Spectacle Rock Cave", 240, 64);
            AddLocation("Spiral Cave", 400, 40);
            AddLocation("Tavern", 72, 288);
            AddLocation("Thief's Chest", 164, 452);
            AddLocation("Thieves' Hideout", 56, 200);
            AddLocation("Tower of Hera", 274, 4, 24, 24);
            AddLocation("Waterfall of Wishing", 448, 64);
            AddLocation("Witch", 400, 160);
            AddLocation("Zora River Ledge", 472, 56);

            AddLocation("Armos Knights", 482, 196);
            AddLocation("Lanmolas", 29, 396);
            AddLocation("Moldorm", 278, 8);
        }
    }
}
