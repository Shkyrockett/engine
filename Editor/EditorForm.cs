using Engine;
using Engine.File.Palettes;
using Engine.Geometry;
using Engine.Imaging;
using Engine.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
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
        VectorMap vectorMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForm"/> class.
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();

            SetDoubleBuffered(CanvasPanel);

            List<Type> tools = EngineReflection.ListTools();
            toolStripComboBoxTools.ComboBox.DataSource = tools;
            toolStripComboBoxTools.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxTools.ComboBox.Items.Count > 0)
                toolStripComboBoxTools.ComboBox.SelectedItem = toolStripComboBoxTools.ComboBox.Items[0];

            List<Type> fileTypes = EngineReflection.ListFileObjects();
            toolStripComboBoxFiles.ComboBox.DataSource = fileTypes;
            toolStripComboBoxFiles.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFiles.ComboBox.Items.Count > 0)
                toolStripComboBoxFiles.ComboBox.SelectedItem = toolStripComboBoxFiles.ComboBox.Items[0];

            List<Type> graphicsTypes = EngineReflection.ListGraphicsObjects();
            toolStripComboBoxObjects.ComboBox.DataSource = graphicsTypes;
            toolStripComboBoxObjects.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxObjects.ComboBox.Items.Count > 0)
                toolStripComboBoxObjects.ComboBox.SelectedItem = toolStripComboBoxObjects.ComboBox.Items[0];

            List<Type> brushTypes = EngineReflection.ListBrushes();
            comboBox1.DataSource = brushTypes;
            comboBox1.ValueMember = "Name";
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedItem = comboBox1.Items[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            paletteToolStripItem1.PaletteControl.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });

            vectorMap = new VectorMap();

            List<ShapeStyle> styles = new List<ShapeStyle>()
            {
                new ShapeStyle(new Pen(Brushes.Red), new Pen(Brushes.Plum)),
                new ShapeStyle(new Pen(Brushes.DarkGreen), new Pen(Brushes.ForestGreen)),
                new ShapeStyle(new Pen(Brushes.BlueViolet), new Pen(Brushes.AliceBlue)),
                new ShapeStyle(new Pen(Brushes.Bisque), new Pen(Brushes.Beige)),
                new ShapeStyle(new Pen(Brushes.Azure), new Pen(Brushes.BlanchedAlmond)),
                new ShapeStyle(new Pen(Brushes.DarkCyan), new Pen(Brushes.Cyan)),
                new ShapeStyle(new Pen(Brushes.Maroon), new Pen(Brushes.MediumPurple)),
                new ShapeStyle(new Pen(Brushes.DarkGoldenrod), new Pen(Brushes.Honeydew)),
            };

            Shape triangle = new Triangle(new PointF(10, 10), new PointF(50, 50), new PointF(10, 100))
            { Style = styles[0] };
            vectorMap.Add(triangle);

            Shape rect = new Rect(new Point(200, 200), new Size(100, 100))
            { Style = styles[1] };
            vectorMap.Add(rect);

            Shape rectf = new RectF(new Point(100, 100), new Size(100, 100))
            { Style = styles[2] };
            vectorMap.Add(rectf);

            Shape polygon = new Polygon(new List<PointF>() { new Point(20, 100), new PointF(300, 60), new PointF(40, 30) })
            { Style = styles[3] };
            vectorMap.Add(polygon);

            Shape polyline = new Polyline(new List<PointF>() { new Point(10, 40), new PointF(80, 30), new PointF(100, 60) })
            { Style = styles[4] };
            vectorMap.Add(polyline);

            Shape line = new LineSegment(new PointF(160, 250), new PointF(130, 145))
            { Style = styles[5] };
            vectorMap.Add(line);

            Shape circle = new Circle(new PointF(200, 200), 100)
            { Style = styles[6] };
            vectorMap.Add(circle);

            Shape ellipse = new Ellipse(new PointF(200, 200), 50, 25, 45)
            { Style = styles[7] };
            vectorMap.Add(ellipse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            // Only need to draw the shapes that are on screen.
            foreach (Shape shape in vectorMap[CanvasPanel.Bounds])
            {
                Renderer.Render(shape, e.Graphics, shape.Style);
            }
        }

        /// <summary>
        /// Set an arbitrary control to double-buffer.
        /// </summary>
        /// <param name="control">The control to set as double buffered.</param>
        /// <remarks>
        /// Taxes: Remote Desktop Connection and painting: http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
        /// </remarks>
        private static void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession) return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(control, true, null);
        }
    }
}
