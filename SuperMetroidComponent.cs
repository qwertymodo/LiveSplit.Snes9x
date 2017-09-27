using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LiveSplit.SuperMetroid
{
    public class SuperMetroidComponent : LogicComponent
    {
        public override string ComponentName => throw new NotImplementedException();

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("");
        }

        public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
        {
            throw new NotImplementedException();
        }

        public override void SetSettings(XmlNode settings)
        {
            throw new NotImplementedException();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
