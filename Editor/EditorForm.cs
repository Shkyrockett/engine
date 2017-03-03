// <copyright file="EditorForm.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine.File.Palettes;
using Engine.Imaging;
using Engine.Tools;
using Engine.Tweening;
using Engine.Winforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Editor
{
    /// <summary>
    ///
    /// </summary>
    public partial class EditorForm
        : Form
    {
        #region Fields

        /// <summary>
        /// Map containing all of the vector objects.
        /// </summary>
        private VectorMap vectorMap = new VectorMap();

        /// <summary>
        /// Container for actions to take on input.
        /// </summary>
        private ToolStack toolStack;

        /// <summary>
        /// Tweening interpolator for animation.
        /// </summary>
        private Tweener tweener = new Tweener();

        /// <summary>
        /// Amount to advance the timer every tick
        /// </summary>
        private int tick = 1;

        /// <summary>
        /// 
        /// </summary>
        private string vectorFilename = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        XmlSerializer vectorMapSserializer = new XmlSerializer(typeof(VectorMap));

        /// <summary>
        /// 
        /// </summary>
        private GraphicItem boundaryItem = new GraphicItem(Rectangle2D.Empty, new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum)));

        #endregion

        #region Constructors

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
                toolStripComboBoxTools.ComboBox.SelectedItem = typeof(SelectTop);

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
                comboBox1.SelectedItem = typeof(SolidBrush);

            //propertyGrid1.SelectedObject = toolStack;

            var val = (0, 3, new Point2D());

            propertyGrid1.SelectedObject = val;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void BuildMap()
        {
            /* Experimental Previews */
            TestCases.CurveFitting(vectorMap);
            TestCases.WarpGeometry(vectorMap);
            //TestCases.ComplexPolygonClipping(vectorMap);
            //TestCases.PolyClipping(vectorMap);
            //TestCases.PathContourWArcLine(vectorMap);
            //TestCases.Pathfinding(vectorMap);
            //TestCases.PlainOval(vectorMap);
            //TestCases.PolylineClicking(vectorMap);
            //TestCases.TextRendering(vectorMap, this);
            //TestCases.SutherlandHodgman(vectorMap);
            //TestCases.ParametricEllipseBounds(vectorMap);
            //TestCases.ParametricEllipseArc(vectorMap);
            //TestCases.ParametricTesting(vectorMap);
            //TestCases.ParametricTesting2(vectorMap);
            //TestCases.GridTests(vectorMap, ForeColor, BackColor);

            /* Regression Test Cases */
            //TestCases.QuadraticBezierHorizontalLineIntersection(vectorMap);
            //TestCases.CubicBezierHorizontalLineIntersection(vectorMap);
            //TestCases.SegmentIntersections(vectorMap,this);
            //TestCases.IntersectionsTests(vectorMap);
            //TestCases.CircularArcBounds(vectorMap);
            //TestCases.EllipseBound(vectorMap);
            //TestCases.EllipticalArcBounds(vectorMap);

            /* Interactive */
            //TestCases.ResizeRefreshBounds(vectorMap, CanvasPanel, out boundaryItem);
            //TestCases.Tweenning(vectorMap, this);

            //TestCases.PlainTriangle(vectorMap);
            //TestCases.TrianglePointingRight(vectorMap);
            //TestCases.PaperPlaneTriangles(vectorMap);
            //TestCases.PlainCircle(vectorMap);
            //TestCases.PlainSquare(vectorMap);
            //TestCases.CircleBounds(vectorMap);
            //TestCases.QuadraticLength(vectorMap);
            //TestCases.CubicBezierLength(vectorMap);
        }

        #region Events

        /// <summary>
        /// Events to execute when the form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);

            paletteToolStripItem1.PaletteControl.Palette = new Palette(new Color[] { Color.Black, Color.White, Color.Red, Color.Green, Color.Blue });

            vectorMap.Tweener = tweener;
            toolStack = new ToolStack(vectorMap);
            toolStack?.RegisterMouseLeftButton(new SelectTop());
            toolStack?.RegisterMouseMiddleButton(new Pan());
            toolStack?.RegisterMouseScroll(new Zoom());

            var foreColor = Color.Black;
            var backColor = Color.White;

            BuildMap();

            listBox1.DataSource = vectorMap.Items;
            //listBox1.ValueMember = "Name";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Start();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
            => tweener.Update(tick);

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
            => propertyGrid1.SelectedObject = (sender as ListBox)?.SelectedItem as GraphicItem;

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
            => CanvasPanel.Invalidate();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripComboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            var item = box.SelectedItem as Type;
            object constructor = Activator.CreateInstance(item);
            toolStack?.RegisterMouseLeftButton(constructor as Tool);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripComboBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            List<MethodInfo> constructors = EngineReflection.ListStaticFactoryConstructors((Type)box.SelectedItem);
            toolStripComboBoxFactories.ComboBox.DataSource = constructors;
            toolStripComboBoxFactories.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFactories.ComboBox.Items.Count > 0)
                toolStripComboBoxFactories.ComboBox.SelectedItem = toolStripComboBoxFactories.ComboBox.Items[0];
            else
                toolStripComboBoxFactories.ComboBox.Text = string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Only need to draw the shapes that are on screen.
            foreach (GraphicItem item in vectorMap[vectorMap.VisibleBounds])
            {
                if (vectorMap?.SelectedItems != null && vectorMap.SelectedItems.Contains(item))
                    Renderer.Render(item, e.Graphics, new ShapeStyle(Brushes.Aquamarine, Brushes.AliceBlue));
                else
                    Renderer.Render(item, e.Graphics);
            }

            if (vectorMap?.RubberbandItems != null)
            {
                foreach (GraphicItem item in vectorMap?.RubberbandItems)
                    Renderer.Render(item, e.Graphics, new ShapeStyle(Brushes.Red, Brushes.Red));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            toolStack.MouseDown((Engine.Tools.MouseButtons)e?.Button, e.Clicks);
            //propertyGrid1.Refresh();
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            toolStack.MouseUp((Engine.Tools.MouseButtons)e?.Button, e.Clicks);
            //propertyGrid1.Refresh();
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
#pragma warning disable IDE0021 // Use expression body for methods
#pragma warning disable IDE0022 // Use expression body for methods
            toolStack.MouseMove(new Point2D(e.X, e.Y));
#pragma warning restore IDE0022 // Use expression body for methods
#pragma warning restore IDE0021 // Use expression body for methods
            //propertyGrid1.Refresh();
            //CanvasPanel.Invalidate(true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseWheel(object sender, MouseEventArgs e)
        {
#pragma warning disable IDE0021 // Use expression body for methods
#pragma warning disable IDE0022 // Use expression body for methods
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.VerticalScroll, e.Delta);
#pragma warning restore IDE0022 // Use expression body for methods
#pragma warning restore IDE0021 // Use expression body for methods
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_MouseWheelTilt(object sender, MouseEventArgs e)
        {
#pragma warning disable IDE0021 // Use expression body for methods
#pragma warning disable IDE0022 // Use expression body for methods
            toolStack.MouseScroll(Engine.Tools.ScrollOrientation.HorizontalScroll, e.Delta);
#pragma warning restore IDE0022 // Use expression body for methods
#pragma warning restore IDE0021 // Use expression body for methods
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyDown(object sender, KeyEventArgs e)
        {
            //toolStack.KeyDown((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyUp(object sender, KeyEventArgs e)
        {
            //toolStack.KeyUp((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_KeyPress(object sender, KeyPressEventArgs e)
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPanel_Resize(object sender, EventArgs e)
        {
            var panel = sender as CanvasPanel;
            if (vectorMap != null)
            {
                vectorMap.VisibleBounds = new Rectangle2D(
                    panel.ClientRectangle.X,
                    panel.ClientRectangle.Y,
                    panel.ClientRectangle.Width - 1,
                    panel.ClientRectangle.Height - 1);
                boundaryItem.Item = vectorMap.VisibleBounds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            openFileDialog1.FileName = vectorFilename;
            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    vectorMap = LoadFile(openFileDialog1.FileName) as VectorMap;
                    break;
                case DialogResult.None:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.No:
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            if (vectorFilename == String.Empty)
            {
                SaveAs(vectorFilename);
            }
            else
            {
                Serialize(saveFileDialog1.FileName, vectorMap);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(Object sender, EventArgs e)
            => SaveAs(vectorFilename);

        #endregion

        #region Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        private void SaveAs(string filename = "")
        {
            saveFileDialog1.FileName = filename;
            //saveFileDialog1.Filter = ".xml";
            switch (saveFileDialog1.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    Serialize(saveFileDialog1.FileName, vectorMap);
                    break;
                case DialogResult.None:
                case DialogResult.No:
                case DialogResult.Cancel:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private object LoadFile(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                return vectorMapSserializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="item"></param>
        private void Serialize(string filename, VectorMap item)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                Serialize(tw, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void Serialize(TextWriter writer, VectorMap item)
            => vectorMapSserializer.Serialize(writer, item);

        /// <summary>
        /// Tweening update callback.
        /// </summary>
        public void UpdateCallback()
            => CanvasPanel.Invalidate(true);

        /// <summary>
        /// Callback for when tweening completes.
        /// </summary>
        public void CompleteCallback()
            => CanvasPanel.Invalidate(true);

        #endregion
    }
}
