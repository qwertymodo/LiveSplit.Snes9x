using LiveSplit.Model;
using LiveSplit.SuperMetroid.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace LiveSplit.SuperMetroid.UI
{
    public class Comparator
    {
        public enum Type
        {
            EQUAL,
            NOTEQUAL,
            GREATER,
            GREATEROREQUAL,
            LESS,
            LESSOREQUAL
        }

        public static Func<T, T, bool> GetComparator<T>(Type comparator) where T : IComparable
        {
            switch (comparator)
            {
                case Type.EQUAL:
                    return (cur, tar) => cur.CompareTo(tar) == 0;

                case Type.NOTEQUAL:
                    return (cur, tar) => cur.CompareTo(tar) != 0;

                case Type.GREATER:
                    return (cur, tar) => cur.CompareTo(tar) > 0;

                case Type.GREATEROREQUAL:
                    return (cur, tar) => cur.CompareTo(tar) >= 0;

                case Type.LESS:
                    return (cur, tar) => cur.CompareTo(tar) < 0;

                case Type.LESSOREQUAL:
                    return (cur, tar) => cur.CompareTo(tar) <= 0;

                default:
                    return (cur, tar) => false;
            }
        }


        public static Func<T, T, bool> TestFlag<T>(bool set = true) where T : struct, IComparable
        {
            if (typeof(T).IsEnum)
            {
                if (set)
                    return (val, flag) => ((Enum)Enum.ToObject(val.GetType(), val)).HasFlag((Enum)Enum.ToObject(flag.GetType(), flag));

                else
                    return (val, flag) => !((Enum)Enum.ToObject(val.GetType(), val)).HasFlag((Enum)Enum.ToObject(flag.GetType(), flag));
            }

            else
                return (val, flag) => false;
        }
    }
    

    

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

        protected virtual void Draw(Graphics g, int width, int height, bool active, float opacity = 0.5f)
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

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            Previous = Current;
            Current = SuperMetroidComponent.game.IsLoaded() ? SuperMetroidComponent.game.Get<T>(Name) : default(T);
        }
    }


    class BoolWatcherImage<T> : ComparisonWatcherImage<T> where T : struct, IComparable
    {
        protected Func<T, T, bool> updateFunc;

        public BoolWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, T target, Func<T, T, bool> func)
            : base(name, frames, x, y, center, height, width, target)
        {
            updateFunc = func;
        }

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            base.Update(g, state, width, height, mode);

            Draw(g, (int)width, (int)height, updateFunc(Current, Target));
        }
    }


    class IndexWatcherImage<T> : ComparisonWatcherImage<T> where T : struct, IComparable
    {
        List<T> Targets = new List<T>();
        protected Func<T, T, bool> updateFunc;

        public IndexWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, List<T> targets)
            : base(name, frames, x, y, center, height, width, default(T))
        {
            updateFunc = Comparator.GetComparator<T>(Comparator.Type.EQUAL);
        }

        public IndexWatcherImage(string name, List<Image> frames, int x, int y, bool center, int height, int width, List<T> targets, Func<T, T, bool> func)
            : base(name, frames, x, y, center, height, width, default(T))
        {
            updateFunc = func;
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

        public override void Update(Graphics g, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            base.Update(g, state, width, height, mode);

            for(int i = Targets.Count - 1; i >= 0; --i)
            {
                if(updateFunc(Current, Targets[i]))
                {
                    Draw(g, (int)width, (int)height, i);
                    return;
                }
            }

            Draw(g, (int)width, (int)height, false);
        }
    }
}
