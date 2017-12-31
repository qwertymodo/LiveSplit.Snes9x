using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class MapTracker : Snes9x.ItemTracker
    {
        public override float MinimumHeight => 512f;

        public override float MinimumWidth => 512f;

        protected virtual Bitmap backgroundImage => null;

        protected class LocationImageWatcher : ImageWatcher<LocationState>
        {
            protected Dictionary<LocationState, Color> LocationColor => new Dictionary<LocationState, Color>()
            {
                { LocationState.INACCESSIBLE, Color.Red },
                { LocationState.ACCESSIBLE, Color.Lime },
                { LocationState.PARTIAL, Color.Cyan },
                { LocationState.VISIBLE, Color.Yellow },
                { LocationState.DARK, Color.Blue },
                { LocationState.COMPLETE, Color.Gray },
                { LocationState.GLITCHACCESSIBLE, Color.Green },
                { LocationState.GLITCHVISIBLE, Color.DarkKhaki },
                { LocationState.UNKNOWN, Color.Orange }
            };
            protected virtual LocationWatcher locations => new LocationWatcher();

            protected Func<LocationState> updateFunc;

            protected ALinkToThePastSettings settings => new ALinkToThePastSettings();

            public LocationImageWatcher(string name, List<Image> frames, int x, int y, bool center, int width, int height)
                : base(name, frames, x, y, center, width, height)
            {
                locations.TryGetValue(name, out updateFunc);
                if (updateFunc == null)
                    updateFunc = () => LocationState.UNKNOWN;
            }

            public override void Draw(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
            {
                LocationState locationState = updateFunc();

                if ((GameLoader.game?.IsLoaded() ?? false) && !(GameLoader.game?.IsRaceROM() ?? false) && (settings.ShowCompleted || locationState != LocationState.COMPLETE))
                {
                    Rectangle location = new Rectangle(X, Y, Width, Height);
                    g.FillRectangle(new SolidBrush(LocationColor[updateFunc()]), location);
                    g.DrawRectangle(new Pen(Color.Black, 2), location);
                }

                if (Frames != null)
                    Draw(g, width, height, locationState != LocationState.COMPLETE);
            }
        }

        protected void AddLocation(string name, int x, int y, int width = 16, int height = 16)
        {
            List<Image> images;
            icons.TryGetValue(name, out images);
            items.Add(name, new LocationImageWatcher(name, images, x + 1, y + 1, false, width, height));
        }

        protected override void DrawGeneral(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, width, height);
            if (backgroundImage != null)
                g.DrawImage(backgroundImage, 0, 0, width, height);

            foreach (var item in items)
            {
                item.Value.GetType().GetMethod("Draw")?.Invoke(item.Value, new object[] { g, state, width, height, mode });
            }
        }

        public override void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            if (HorizontalWidth < MinimumWidth)
                HorizontalWidth = MinimumWidth;

            float size = height < HorizontalWidth ? height : HorizontalWidth;

            DrawGeneral(g, state, size, size, LayoutMode.Horizontal);
        }

        public override void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            if (VerticalHeight < MinimumHeight)
                VerticalHeight = MinimumHeight;

            float size = width < VerticalHeight ? width : VerticalHeight;

            DrawGeneral(g, state, size, size, LayoutMode.Vertical);
        }
    }
}
