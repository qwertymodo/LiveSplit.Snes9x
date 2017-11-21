using LiveSplit.Model;
using LiveSplit.Snes9x;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using static LiveSplit.ALinkToThePast.UI.Components.ALinkToThePast;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    public class ALinkToThePastComponent : IComponent
    {
        public ComponentRendererComponent InternalComponent { get; protected set; }

        public ALinkToThePastSettings Settings { get; set; }

        private static int SettingsHash = 0;

        public string ComponentName => "A Link to the Past Tracker";

        public float HorizontalWidth => InternalComponent.HorizontalWidth;

        public float MinimumHeight => InternalComponent.MinimumHeight;

        public float VerticalHeight => InternalComponent.VerticalHeight;

        public float MinimumWidth => InternalComponent.MinimumWidth;

        public float PaddingTop => InternalComponent.PaddingTop;

        public float PaddingBottom => InternalComponent.PaddingBottom;

        public float PaddingLeft => InternalComponent.PaddingLeft;

        public float PaddingRight => InternalComponent.PaddingRight;

        public IDictionary<string, Action> ContextMenuControls => null;

        public ALinkToThePastComponent(LiveSplitState state)
        {
            Settings = new ALinkToThePastSettings();
            GameLoader.Load(new ALinkToThePast());
            InternalComponent = new ComponentRendererComponent();
            InternalComponent.VisibleComponents = new List<IComponent>();
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
            return Settings.GetSettings(document);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public int GetSettingsHashCode()
        {
            int hash = Settings.GetSettingsHashCode();

            if(hash != SettingsHash)
            {
                var components = new List<IComponent>();

                if (Settings.MapTracker)
                {
                    components.Add(new LightWorldMapTracker());
                    components.Add(new DarkWorldMapTracker());
                }

                if (Settings.ItemTracker)
                {
                    components.Add(new ItemTracker());
                }

                InternalComponent.VisibleComponents = components;
            }

            SettingsHash = hash;
            return SettingsHash;
        }
        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            GameLoader.game?.Update(state);

            if (invalidator != null && (GameLoader.game?.IsRunning() ?? false))
                InternalComponent.Update(invalidator, state, width, height, mode);
        }
    }
}
