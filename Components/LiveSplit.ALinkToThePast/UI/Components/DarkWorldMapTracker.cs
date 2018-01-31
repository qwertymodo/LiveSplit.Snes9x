using System.Drawing;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class DarkWorldMapTracker : MapTracker
    {
        protected override Bitmap backgroundImage => Properties.Resources.DarkWorldMap;

        public DarkWorldMapTracker()
        {
            HorizontalWidth = MinimumWidth;
            VerticalHeight = MinimumHeight;

            icons = new Images();

            AddLocation("Bomb Hut", 48, 288);
            AddLocation("Bumper Cave", 168, 72);
            AddLocation("Bunny Cave", 424, 64);
            AddLocation("C House", 96, 236);
            AddLocation("Catfish", 448, 80);
            AddLocation("Digging Game", 16, 344);
            AddLocation("Fat Fairy", 232, 240);
            AddLocation("Flute Boy", 149, 340);
            AddLocation("Hammer Cave", 152, 296);
            AddLocation("Hookshot Cave", 416, 24);
            AddLocation("Hype Cave", 296, 392);
            AddLocation("Ice Palace", 395, 428, 24, 24);
            AddLocation("Misery Mire", 25, 412, 24, 24);
            AddLocation("Palace of Darkness", 478, 192, 24, 24);
            AddLocation("Pyramid", 288, 224);
            AddLocation("Skull Woods", 6, 16, 24, 24);
            AddLocation("Spike Cave", 284, 64);
            AddLocation("Swamp Palace", 227, 464, 24, 24);
            AddLocation("Thieves' Town", 52, 232, 24, 24);
            AddLocation("Treasure Chest Game", 17, 228);
            AddLocation("Turtle Rock", 469, 20, 24, 24);
            AddLocation("West Mire", 12, 396);

            AddLocation("Arrghus", 231, 468);
            AddLocation("Blind", 56, 236);
            AddLocation("Kholdstare", 399, 432);
            AddLocation("King Helmasaur", 482, 196);
            AddLocation("Mothula", 10, 20);
            AddLocation("Trinexx", 473, 24);
            AddLocation("Vitreous", 29, 416);
        }
    }
}
