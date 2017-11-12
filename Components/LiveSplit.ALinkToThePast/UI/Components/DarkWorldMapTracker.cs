using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class DarkWorldMapTracker : MapTracker
    {
        protected override Bitmap backgroundImage => Properties.Resources.DarkWorldMap;

        public DarkWorldMapTracker()
        {
            AddLocation("Bomb Hut", 48, 288);
            AddLocation("Bumper Cave", 168, 72);
            AddLocation("Bunny Cave", 424, 64);
            AddLocation("C House", 96, 236);
            AddLocation("Digging Game", 16, 344);
            AddLocation("Fat Fairy", 232, 240);
            AddLocation("Flute Boy", 149, 340);
            AddLocation("Hammer Cave", 152, 296);
            AddLocation("Hookshot Cave", 416, 24);
            AddLocation("Palace of Darkness", 478, 192, 24, 24);
            AddLocation("Pyramid", 288, 224);
            AddLocation("Skull Woods", 6, 16, 24, 24);
            AddLocation("Spike Cave", 284, 64);
            AddLocation("Thieves' Town", 52, 232, 24, 24);
            AddLocation("Treasure Chest Game", 17, 228);
            AddLocation("Turtle Rock", 469, 20, 24, 24);
        }
    }
}
