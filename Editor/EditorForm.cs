using Engine;
using Engine.File.Palettes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditorForm
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();

            List<Type> shapes = EngineReflection.ListShapes();
            comboBox1.DataSource = shapes;
            comboBox1.ValueMember = "Name";
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];

            List<Type> tools = EngineReflection.ListTools();
            comboBox2.DataSource = tools;
            comboBox2.ValueMember = "Name";
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedItem = comboBox2.Items[0];

            List<Type> fileTypes = EngineReflection.ListFileObjects();
            comboBox3.DataSource = fileTypes;
            comboBox3.ValueMember = "Name";
            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedItem = comboBox3.Items[0];

            List<Type> graphicsTypes = EngineReflection.ListGraphicsObjects();
            comboBox4.DataSource = graphicsTypes;
            comboBox4.ValueMember = "Name";
            if (comboBox4.Items.Count > 0)
                comboBox4.SelectedItem = comboBox4.Items[0];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, System.EventArgs e)
        {
            palleteControl1.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });
        }
    }
}
