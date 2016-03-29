namespace Editor
{
    partial class EditorForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            Engine.File.Palettes.Palette palette1 = new Engine.File.Palettes.Palette();
            this.palleteControl1 = new Editor.PalleteControl();
            ((System.ComponentModel.ISupportInitialize)(this.palleteControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // palleteControl1
            // 
            this.palleteControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("palleteControl1.BackgroundImage")));
            this.palleteControl1.Location = new System.Drawing.Point(12, 121);
            this.palleteControl1.Name = "palleteControl1";
            palette1.Colors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("palette1.Colors")));
            palette1.FileName = null;
            palette1.PaletteMimeFormat = Engine.File.Palettes.PaletteMimeFormats.Default;
            this.palleteControl1.Palette = palette1;
            this.palleteControl1.Size = new System.Drawing.Size(128, 128);
            this.palleteControl1.TabIndex = 0;
            this.palleteControl1.TabStop = false;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.palleteControl1);
            this.Name = "EditorForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.palleteControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PalleteControl palleteControl1;
    }
}

