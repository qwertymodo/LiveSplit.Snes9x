using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.SuperMetroid.UI.Components
{
    public class SuperMetroidComponent : IComponent
    {
        public ComponentRendererComponent InternalComponent { get; protected set; }

        public string ComponentName => "Super Metroid Tracker";

        public float HorizontalWidth => InternalComponent.HorizontalWidth;

        public float MinimumHeight => InternalComponent.MinimumHeight;

        public float VerticalHeight => InternalComponent.VerticalHeight;

        public float MinimumWidth => InternalComponent.MinimumWidth;

        public float PaddingTop => InternalComponent.PaddingTop;

        public float PaddingBottom => InternalComponent.PaddingBottom;

        public float PaddingLeft => InternalComponent.PaddingLeft;

        public float PaddingRight => InternalComponent.PaddingRight;

        public IDictionary<string, Action> ContextMenuControls => null;
        
        public SuperMetroidComponent(LiveSplitState state)
        {
            GameLoader.Load(new SuperMetroid());
            InternalComponent = new ComponentRendererComponent();
            var components = new List<IComponent>();
            components.Add(new ItemTracker());
            //components.Add(new BossTracker());
            InternalComponent.VisibleComponents = components;
        }

        public void Dispose()
        {
        }

        public void DrawHorizontal(System.Drawing.Graphics g, LiveSplitState state, float height, System.Drawing.Region clipRegion)
        {
            InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(System.Drawing.Graphics g, LiveSplitState state, float width, System.Drawing.Region clipRegion)
        {
            InternalComponent.DrawVertical(g, state, width, clipRegion);
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
            GameLoader.game?.Update(state);

            if (invalidator != null)
                InternalComponent.Update(invalidator, state, width, height, mode);
        }
    }
}
