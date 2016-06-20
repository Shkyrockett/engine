namespace Engine
{
    partial class AngleControl
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDegrees = new System.Windows.Forms.TabPage();
            this.tabPageRadians = new System.Windows.Forms.TabPage();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.needleControl1 = new Engine.NeedleControl();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageDegrees);
            this.tabControl.Controls.Add(this.tabPageRadians);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(150, 156);
            this.tabControl.TabIndex = 1;
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_DrawItem);
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageDegrees
            // 
            this.tabPageDegrees.Location = new System.Drawing.Point(4, 22);
            this.tabPageDegrees.Name = "tabPageDegrees";
            this.tabPageDegrees.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDegrees.Size = new System.Drawing.Size(142, 130);
            this.tabPageDegrees.TabIndex = 0;
            this.tabPageDegrees.Text = "Degrees";
            this.tabPageDegrees.UseVisualStyleBackColor = true;
            this.tabPageDegrees.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPageDegrees_Paint);
            // 
            // tabPageRadians
            // 
            this.tabPageRadians.Location = new System.Drawing.Point(4, 22);
            this.tabPageRadians.Name = "tabPageRadians";
            this.tabPageRadians.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRadians.Size = new System.Drawing.Size(142, 130);
            this.tabPageRadians.TabIndex = 1;
            this.tabPageRadians.Text = "Radians";
            this.tabPageRadians.UseVisualStyleBackColor = true;
            this.tabPageRadians.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPageRadians_Paint);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown.DecimalPlaces = 20;
            this.numericUpDown.Location = new System.Drawing.Point(3, 163);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(143, 20);
            this.numericUpDown.TabIndex = 2;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_KeyDown);
            // 
            // needleControl1
            // 
            this.needleControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.needleControl1.Angle = 0D;
            this.needleControl1.BackColor = System.Drawing.Color.Transparent;
            this.needleControl1.Location = new System.Drawing.Point(10, 28);
            this.needleControl1.Name = "needleControl1";
            this.needleControl1.Size = new System.Drawing.Size(130, 118);
            this.needleControl1.TabIndex = 0;
            this.needleControl1.ValueChanged += new Engine.NeedleControl.ValueChangedDelegate(this.needleControl1_ValueChanged);
            this.needleControl1.ValueCommitted += new Engine.NeedleControl.ValueCommittedDelegate(this.needleControl1_ValueCommitted);
            // 
            // AngleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.needleControl1);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.tabControl);
            this.Name = "AngleControl";
            this.Size = new System.Drawing.Size(150, 186);
            this.tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NeedleControl needleControl1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDegrees;
        private System.Windows.Forms.TabPage tabPageRadians;
        private System.Windows.Forms.NumericUpDown numericUpDown;
    }
}
