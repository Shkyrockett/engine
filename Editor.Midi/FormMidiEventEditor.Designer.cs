namespace EventEditorMidi
{
    partial class FormMidiEventEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStripFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewFile;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxFileFormat;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonCloseFile;
        private System.Windows.Forms.ToolStrip toolStripObject;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewObject;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxObject;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMidiEventEditor));
            this.treeView = new System.Windows.Forms.TreeView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripFile = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxFileFormat = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCloseFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripObject = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNewObject = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxObject = new System.Windows.Forms.ToolStripComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripFile.SuspendLayout();
            this.toolStripObject.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(3, 26);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(335, 385);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid.Location = new System.Drawing.Point(0, 26);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(420, 385);
            this.propertyGrid.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripFile);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripObject);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.splitContainer1.Size = new System.Drawing.Size(762, 414);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStripFile
            // 
            this.toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewFile,
            this.toolStripComboBoxFileFormat,
            this.toolStripButtonOpenFile,
            this.toolStripButtonSaveFile,
            this.toolStripButtonCloseFile});
            this.toolStripFile.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripFile.Location = new System.Drawing.Point(3, 3);
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Size = new System.Drawing.Size(335, 23);
            this.toolStripFile.TabIndex = 1;
            // 
            // toolStripButtonNewFile
            // 
            this.toolStripButtonNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNewFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewFile.Image")));
            this.toolStripButtonNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewFile.Name = "toolStripButtonNewFile";
            this.toolStripButtonNewFile.Size = new System.Drawing.Size(35, 19);
            this.toolStripButtonNewFile.Text = "&New";
            this.toolStripButtonNewFile.Click += new System.EventHandler(this.ToolStripButtonNewFile_Click);
            // 
            // toolStripComboBoxFileFormat
            // 
            this.toolStripComboBoxFileFormat.AutoToolTip = true;
            this.toolStripComboBoxFileFormat.Name = "toolStripComboBoxFileFormat";
            this.toolStripComboBoxFileFormat.Size = new System.Drawing.Size(75, 23);
            // 
            // toolStripButtonOpenFile
            // 
            this.toolStripButtonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenFile.Image")));
            this.toolStripButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFile.Name = "toolStripButtonOpenFile";
            this.toolStripButtonOpenFile.Size = new System.Drawing.Size(40, 19);
            this.toolStripButtonOpenFile.Text = "&Open";
            this.toolStripButtonOpenFile.Click += new System.EventHandler(this.ToolStripButtonOpenFile_Click);
            // 
            // toolStripButtonSaveFile
            // 
            this.toolStripButtonSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveFile.Image")));
            this.toolStripButtonSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveFile.Name = "toolStripButtonSaveFile";
            this.toolStripButtonSaveFile.Size = new System.Drawing.Size(35, 19);
            this.toolStripButtonSaveFile.Text = "&Save";
            this.toolStripButtonSaveFile.Click += new System.EventHandler(FormMidiEventEditor.ToolStripButtonSaveFile_Click);
            // 
            // toolStripButtonCloseFile
            // 
            this.toolStripButtonCloseFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCloseFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCloseFile.Image")));
            this.toolStripButtonCloseFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCloseFile.Name = "toolStripButtonCloseFile";
            this.toolStripButtonCloseFile.Size = new System.Drawing.Size(40, 19);
            this.toolStripButtonCloseFile.Text = "&Close";
            this.toolStripButtonCloseFile.Click += new System.EventHandler(FormMidiEventEditor.ToolStripButtonCloseFile_Click);
            // 
            // toolStripObject
            // 
            this.toolStripObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNewObject,
            this.toolStripComboBoxObject});
            this.toolStripObject.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripObject.Location = new System.Drawing.Point(0, 3);
            this.toolStripObject.Name = "toolStripObject";
            this.toolStripObject.Size = new System.Drawing.Size(420, 23);
            this.toolStripObject.TabIndex = 2;
            // 
            // toolStripButtonNewObject
            // 
            this.toolStripButtonNewObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNewObject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewObject.Image")));
            this.toolStripButtonNewObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewObject.Name = "toolStripButtonNewObject";
            this.toolStripButtonNewObject.Size = new System.Drawing.Size(35, 19);
            this.toolStripButtonNewObject.Text = "New";
            // 
            // toolStripComboBoxObject
            // 
            this.toolStripComboBoxObject.Name = "toolStripComboBoxObject";
            this.toolStripComboBoxObject.Size = new System.Drawing.Size(75, 23);
            // 
            // FormMidiEventEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 414);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMidiEventEditor";
            this.Text = "Midi Event Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripFile.ResumeLayout(false);
            this.toolStripFile.PerformLayout();
            this.toolStripObject.ResumeLayout(false);
            this.toolStripObject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

