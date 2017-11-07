using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(LiveSplit.SuperMetroid.UI.Components.SuperMetroidFactory))]

namespace LiveSplit.SuperMetroid.UI.Components
{
    class SuperMetroidFactory : IComponentFactory
    {
        public string ComponentName => "Super Metroid Tracker";

        public string Description => "Tracks item collection during a randomized Super Metroid run.";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new SuperMetroidComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "https://raw.githubusercontent.com/qwertymodo/LiveSplit.SuperMetroid/master/update.LiveSplit.SuperMetroid.xml";

        public string UpdateURL => "https://github.com/qwertymodo/LiveSplit.SuperMetroid";

        public Version Version => Version.Parse("0.1.0");
    }
}
