using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(LiveSplit.ALinkToThePast.UI.Components.ALinkToThePastFactory))]

namespace LiveSplit.ALinkToThePast.UI.Components
{
    class ALinkToThePastFactory : IComponentFactory
    {
        public string ComponentName => "A Link to the Past Tracker";

        public string Description => "Tracks item collection during a randomized Link to the Past run.";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new ALinkToThePastComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "https://raw.githubusercontent.com/qwertymodo/LiveSplit.SuperMetroid/master/update.LiveSplit.SuperMetroid.xml";

        public string UpdateURL => "https://github.com/qwertymodo/LiveSplit.SuperMetroid";

        public Version Version => Version.Parse("0.1.0");
    }
}
