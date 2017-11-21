namespace LiveSplit.ALinkToThePast.UI.Components
{
    partial class ALinkToThePastSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbMapTracker = new System.Windows.Forms.CheckBox();
            this.cbItemTracker = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cbMapTracker, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbItemTracker, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 66);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cbMapTracker
            // 
            this.cbMapTracker.AutoSize = true;
            this.cbMapTracker.Location = new System.Drawing.Point(3, 3);
            this.cbMapTracker.Name = "cbMapTracker";
            this.cbMapTracker.Size = new System.Drawing.Size(87, 17);
            this.cbMapTracker.TabIndex = 0;
            this.cbMapTracker.Text = "Map Tracker";
            this.cbMapTracker.UseVisualStyleBackColor = true;
            this.cbMapTracker.CheckedChanged += new System.EventHandler(this.cbMapTracker_CheckedChanged);
            // 
            // cbItemTracker
            // 
            this.cbItemTracker.AutoSize = true;
            this.cbItemTracker.Location = new System.Drawing.Point(238, 3);
            this.cbItemTracker.Name = "cbItemTracker";
            this.cbItemTracker.Size = new System.Drawing.Size(83, 17);
            this.cbItemTracker.TabIndex = 1;
            this.cbItemTracker.Text = "ItemTracker";
            this.cbItemTracker.UseVisualStyleBackColor = true;
            this.cbItemTracker.CheckedChanged += new System.EventHandler(this.cbItemTracker_CheckedChanged);
            // 
            // ALinkToThePastSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ALinkToThePastSettings";
            this.Size = new System.Drawing.Size(476, 72);
            this.Load += new System.EventHandler(this.ALinkToThePastSettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbMapTracker;
        private System.Windows.Forms.CheckBox cbItemTracker;
    }
}
