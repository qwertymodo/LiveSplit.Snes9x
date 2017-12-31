using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;

namespace LiveSplit.ALinkToThePast.UI.Components
{
    public partial class ALinkToThePastSettings : UserControl
    {
        public bool MapTracker { get; set; }
        public bool ItemTracker { get; set; }
        public bool ShowCompleted { get; set; }
        public bool AutoSplitter { get; set; }

        public LayoutMode Mode { get; set; }

        public ALinkToThePastSettings()
        {
            InitializeComponent();

            MapTracker = true;
            ItemTracker = true;
            ShowCompleted = true;
            AutoSplitter = true;

            cbMapTracker.DataBindings.Add("Checked", this, "MapTracker", false, DataSourceUpdateMode.OnPropertyChanged);
            cbItemTracker.DataBindings.Add("Checked", this, "ItemTracker", false, DataSourceUpdateMode.OnPropertyChanged);
            cbShowCompleted.DataBindings.Add("Checked", this, "ShowCompleted", false, DataSourceUpdateMode.OnPropertyChanged);
            cbAutoSplitter.DataBindings.Add("Checked", this, "AutoSplitter", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ALinkToThePastSettings_Load(object sender, EventArgs e)
        {
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            Version version = SettingsHelper.ParseVersion(element["Version"]);

            MapTracker = SettingsHelper.ParseBool(element["MapTracker"]);
            ItemTracker = SettingsHelper.ParseBool(element["ItemTracker"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.0") ^
            (SettingsHelper.CreateSetting(document, parent, "MapTracker", MapTracker) > 0 ? "MapTracker".GetHashCode() : 0) ^
            (SettingsHelper.CreateSetting(document, parent, "ItemTracker", ItemTracker) > 0 ? "ItemTracker".GetHashCode() : 0);
        }

        private void cbMapTracker_CheckedChanged(object sender, EventArgs e)
        {
            MapTracker = cbMapTracker.Checked;
        }

        private void cbItemTracker_CheckedChanged(object sender, EventArgs e)
        {
            ItemTracker = cbItemTracker.Checked;
        }

        private void cbShowCompleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowCompleted = cbShowCompleted.Checked;
        }

        private void cbAutoSplitter_CheckedChanged(object sender, EventArgs e)
        {
            AutoSplitter = cbAutoSplitter.Checked;
        }
    }
}
