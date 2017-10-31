using LiveSplit.Model;
using LiveSplit.SuperMetroid.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.SuperMetroid
{
    class WatcherImage<T> where T : IComparable
    {
        protected string Name;
        protected List<Image> Frames;
        protected int X = 0;
        protected int Y = 0;
        protected bool Centered = false;
        protected int Height = 0;
        protected int Width = 0;

        public WatcherImage()
        { }

        public WatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width)
        {
            Name = name;
            Frames = frames;
            X = x;
            Y = y;
            Centered = center;
            Height = height;
            Width = width;
        }

        protected void DrawImageGrayscale(Graphics g, Image img, int x, int y, int width, int height, float opacity = 1)
        {
            ColorMatrix matrix = new ColorMatrix(
            new float[][]
            {
                new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                new float[] {0.11f, 0.11f, .11f, 0, 0},
                new float[] {0, 0, 0, opacity, 0},
                new float[] {0, 0, 0, 0, 1},
            });

            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(img, new Rectangle(x, y, width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attr);
        }

        protected void DrawImageFromCenter(Graphics g, Image img, int x, int y, int iWidth, int iHeight, int gWidth, int gHeight)
        {
            g.DrawImage(img, (gWidth - iWidth) / 2 + x, (gHeight - iHeight) / 2 - y, iWidth, iHeight);
        }

        protected void DrawImageGrayscaleFromCenter(Graphics g, Image img, int x, int y, int iWidth, int iHeight, int gWidth, int gHeight, float opacity = 1)
        {
            DrawImageGrayscale(g, img, (gWidth - iWidth) / 2 + x, (gHeight - iHeight) / 2 - y, iWidth, iHeight, opacity);
        }

        protected void Draw(Graphics g, int width, int height, bool active, float opacity = 0.5f)
        {
            if(active)
            {
                if (Centered)
                    DrawImageFromCenter(g, Frames[0], X, Y, Width, Height, width, height);

                else
                    g.DrawImage(Frames[0], X, Y, Width, Height);
            }

            else
            {
                if (Centered)
                    DrawImageGrayscaleFromCenter(g, Frames[0], X, Y, Width, Height, width, height, opacity);

                else
                    DrawImageGrayscale(g, Frames[0], X, Y, Width, Height, opacity);
            }
        }

        public virtual void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        { }
    }

    class ComparisonWatcherImage<T> : WatcherImage<T> where T : IComparable
    {
        protected T Target;
        protected T Current;
        protected T Previous;

        public ComparisonWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T target)
            : base(name, frames, x, y, center, height, width)
        {
            Target = target;
        }
    }

    class EqualsWatcherImage<T> : ComparisonWatcherImage<T> where T : IComparable
    {
        public EqualsWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T target)
            : base(name, frames, x, y, center, height, width, target)
        { }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.Get<T>(Name);

            Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && Current.CompareTo(Target) == 0);
        }
    }

    class GreaterWatcherImage<T> : ComparisonWatcherImage<T> where T : IComparable
    {
        public GreaterWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T target)
            : base(name, frames, x, y, center, height, width, target)
        { }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.Get<T>(Name);

            Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && Current.CompareTo(Target) > 0);
        }
    }


    class ByteBitSetWatcherImage : ComparisonWatcherImage<byte>
    {
        public ByteBitSetWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, byte mask)
            : base(name, frames, x, y, center, height, width, mask)
        { }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.Get<byte>(Name);

            Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && (Current & Target) == Target);
        }
    }


    class ByteBitClearWatcherImage : ComparisonWatcherImage<byte>
    {
        public ByteBitClearWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, byte mask)
            : base(name, frames, x, y, center, height, width, mask)
        { }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.Get<byte>(Name);

            Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && (Current & Target) == 0);
        }
    }


    class ShortBitSetWatcherImage : ComparisonWatcherImage<ushort>
    {
        public ShortBitSetWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, ushort mask)
            : base(name, frames, x, y, center, height, width, mask)
        { }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.Get<ushort>(Name);

            Draw(g, (int)width, (int)height, SuperMetroidComponent.game.IsLoaded() && (Current & Target) == Target);
        }
    }
}
