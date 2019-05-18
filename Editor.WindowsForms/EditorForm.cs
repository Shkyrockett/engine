// <copyright file="EditorForm.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using Engine.Colorspace;
using Engine.Tools;
using Engine.Tweening;
using Engine.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Editor
{
    /// <summary>
    /// The editor form class.
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
        private readonly Tweener tweener = new Tweener();

        /// <summary>
        /// Amount to advance the timer every tick
        /// </summary>
        private readonly int tick = 1;

        /// <summary>
        /// The vector filename.
        /// </summary>
        private readonly string vectorFilename = string.Empty;

        /// <summary>
        /// The vector map serializer.
        /// </summary>
        private readonly XmlSerializer vectorMapSerializer = new XmlSerializer(typeof(VectorMap));

        /// <summary>
        /// The boundary item.
        /// </summary>
        private GraphicItem boundaryItem = new GraphicItem(Rectangle2D.Empty, new ShapeStyle(Brushes.Red, new Pen(Brushes.Plum)));

        /// <summary>
        /// The updating list flag.
        /// </summary>
        private bool updatinglist;

        /// <summary>
        /// The text measurer.
        /// </summary>
        private readonly WinFormsTextMeasurer textMeasurer = new WinFormsTextMeasurer();
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForm"/> class.
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();

            SetDoubleBuffered(CanvasPanel);

            var tools = EngineReflection.ListTools();
            toolStripComboBoxTools.ComboBox.DataSource = tools;
            toolStripComboBoxTools.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxTools.ComboBox.Items.Count > 0)
            {
                toolStripComboBoxTools.ComboBox.SelectedItem = typeof(SelectTop);
            }

            var fileTypes = EngineReflection.ListFileObjects();
            toolStripComboBoxFiles.ComboBox.DataSource = fileTypes;
            toolStripComboBoxFiles.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFiles.ComboBox.Items.Count > 0)
            {
                toolStripComboBoxFiles.ComboBox.SelectedItem = toolStripComboBoxFiles.ComboBox.Items[0];
            }

            var graphicsTypes = EngineReflection.ListGraphicsObjects();
            toolStripComboBoxObjects.ComboBox.DataSource = graphicsTypes;
            toolStripComboBoxObjects.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxObjects.ComboBox.Items.Count > 0)
            {
                toolStripComboBoxObjects.ComboBox.SelectedItem = toolStripComboBoxObjects.ComboBox.Items[0];
            }

            var brushTypes = EngineReflection.ListBrushes();
            comboBox1.DataSource = brushTypes;
            comboBox1.ValueMember = "Name";
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = typeof(SolidBrush);
            }

            //propertyGrid1.SelectedObject = toolStack;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the reset action.
        /// </summary>
        public Action ResetAction { get; set; }
        #endregion Properties

        #region Events
        /// <summary>
        /// Events to execute when the form loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            // Turn on redraw on resize.
            SetStyle(ControlStyles.ResizeRedraw, true);

            // Build the color palette.
            paletteToolStripItem1.PaletteControl.Palette = new Palette(new[] {
                Colors.Black,
                Colors.White,
                Colors.Red,
                Colors.Orange,
                Colors.Yellow,
                Colors.Lime,
                Colors.Green,
                Colors.Cyan,
                Colors.Blue,
                Colors.Purple,
                Colors.Magenta
            });

            // ToDo: Rework tweener into time line.
            vectorMap.Tweener = tweener;

            // Set up the tool stack.
            toolStack = new ToolStack(vectorMap);
            toolStack?.RegisterMouseLeftButton(new SelectTop());
            toolStack?.RegisterMouseMiddleButton(new Pan());
            toolStack?.RegisterMouseScroll(new Zoom());

            // Build the map.
            BuildMap();

            // list the shapes in the vector map.
            listBox1.DataSource = vectorMap.Items;
            listBox1.ValueMember = "Name";
        }

        /// <summary>
        /// The button1 click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            // Call the reset action if it exists.
            ResetAction?.Invoke();

            // Enable and start the timer.
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Start();
        }

        /// <summary>
        /// The timer1 tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Timer1_Tick(object sender, EventArgs e)
            => tweener.Update(tick);

        /// <summary>
        /// The list box1 selected value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatinglist)
            {
                updatinglist = true;
                var list = sender as ListBox;
                vectorMap.SelectedItems?.Clear();
                if (vectorMap.SelectedItems is null)
                {
                    vectorMap.SelectedItems = new List<GraphicItem>();
                }

                foreach (var item in list.SelectedItems)
                {
                    vectorMap.SelectedItems.Add(item as GraphicItem);
                }
                updatinglist = false;
            }

            propertyGrid1.SelectedObject = vectorMap.SelectedItems.ToArray();
            CanvasPanel.Invalidate();
        }

        /// <summary>
        /// The property grid1 property value changed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The property value changed event arguments.</param>
        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
            => CanvasPanel.Invalidate();

        /// <summary>
        /// The tool strip combo box tools selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripComboBoxTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            var item = box.SelectedItem as Type;
            var constructor = Activator.CreateInstance(item);
            toolStack?.RegisterMouseLeftButton(constructor as Tool);
        }

        /// <summary>
        /// The tool strip combo box objects selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripComboBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ToolStripComboBox;
            var constructors = EngineReflection.ListStaticFactoryConstructors((Type)box.SelectedItem);
            toolStripComboBoxFactories.ComboBox.DataSource = constructors;
            toolStripComboBoxFactories.ComboBox.ValueMember = "Name";
            if (toolStripComboBoxFactories.ComboBox.Items.Count > 0)
            {
                toolStripComboBoxFactories.ComboBox.SelectedItem = toolStripComboBoxFactories.ComboBox.Items[0];
            }
            else
            {
                toolStripComboBoxFactories.ComboBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// The canvas panel paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            // Reference the panel.
            //var panel = sender as CanvasPanel;

            // Run the base painting first.
            base.OnPaint(e);

            // Set to smooth anti aliasing
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // We are using the Winforms renderer shim.
            var renderer = e.Graphics.ToWinformsRenderer();

            // Only need to draw the shapes that are on screen.
            foreach (var item in vectorMap[vectorMap.VisibleBounds])
            {
                if (vectorMap?.SelectedItems != null && vectorMap.SelectedItems.Contains(item))
                {
                    // Render selected items with selection style
                    Renderer.Render(item, e.Graphics, renderer, new ShapeStyle(Brushes.Aquamarine, Brushes.AliceBlue));
                }
                else
                {
                    // Render items according to their set style.
                    Renderer.Render(item, e.Graphics, renderer, item.Style);
                }
            }

            if (vectorMap?.RubberbandItems != null)
            {
                foreach (var item in vectorMap?.RubberbandItems)
                {
                    // Render temporary rubber band tools shapes.
                    Renderer.Render(item, e.Graphics, renderer, new ShapeStyle(Brushes.Red, Brushes.Red));
                }
            }
        }

        /// <summary>
        /// The canvas panel mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Update the tool stack.
            toolStack.MouseDown((Engine.Tools.MouseButtons)e?.Button, e.Clicks);

            //propertyGrid1.Refresh();

            // Repaint.
            CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// The canvas panel mouse up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (!updatinglist)
            {
                updatinglist = true;

                // Update the tool stack.
                toolStack.MouseUp((Engine.Tools.MouseButtons)e?.Button, e.Clicks);

                // This should all probably be handled by the tool stack
                {
                    listBox1.SuspendLayout();
                    listBox1.SelectedItem = vectorMap.SelectedItems.Count > 0 ? vectorMap?.SelectedItems[0] : null;
                    listBox1.SelectedItems.Clear();
                    foreach (var item in vectorMap.SelectedItems)
                    {
                        listBox1.SelectedItems.Add(item);
                    }
                    listBox1.ResumeLayout();
                    propertyGrid1.Refresh();
                }

                // Repaint.
                CanvasPanel.Invalidate(true);
                updatinglist = false;
            }
        }

        /// <summary>
        /// The canvas panel mouse move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // Update the tool stack.
#pragma warning disable IDE0022 // Use expression body for methods
            toolStack.MouseMove(e.Location.ToPoint2D());
#pragma warning restore IDE0022 // Use expression body for methods

            //propertyGrid1.Refresh();
            //CanvasPanel.Invalidate(true);
        }

        /// <summary>
        /// The canvas panel mouse wheel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasPanel_MouseWheel(object sender, MouseEventArgs e)
            // Update the tool stack.
            => toolStack.MouseScroll(Engine.Tools.ScrollOrientation.VerticalScroll, e.Delta);

        /// <summary>
        /// The canvas panel mouse wheel tilt.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse event arguments.</param>
        private void CanvasPanel_MouseWheelTilt(object sender, MouseEventArgs e)
            // Update the tool stack.
            => toolStack.MouseScroll(Engine.Tools.ScrollOrientation.HorizontalScroll, e.Delta);

        /// <summary>
        /// The canvas panel preview key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The preview key down event arguments.</param>
        private static void CanvasPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        { }

        /// <summary>
        /// The canvas panel key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The key event arguments.</param>
        private static void CanvasPanel_KeyDown(object sender, KeyEventArgs e)
        {
            //toolStack.KeyDown((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// The canvas panel key up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The key event arguments.</param>
        private static void CanvasPanel_KeyUp(object sender, KeyEventArgs e)
        {
            //toolStack.KeyUp((Engine.Tools.Keys)e.KeyData);
            //propertyGrid1.Refresh();
        }

        /// <summary>
        /// The canvas panel key press.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The key press event arguments.</param>
        private static void CanvasPanel_KeyPress(object sender, KeyPressEventArgs e)
        { }

        /// <summary>
        /// The canvas panel resize.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void CanvasPanel_Resize(object sender, EventArgs e)
        {
            // Reference the canvas panel.
            var panel = sender as CanvasPanel;

            if (vectorMap != null)
            {
                // Resize the vector map when the canvas is resized.
                vectorMap.VisibleBounds = new Rectangle2D(
                    panel.ClientRectangle.X,
                    panel.ClientRectangle.Y,
                    panel.ClientRectangle.Width - 1,
                    panel.ClientRectangle.Height - 1);
                boundaryItem.Shape = vectorMap.VisibleBounds;
            }
        }

        /// <summary>
        /// Open the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// Save the tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(vectorFilename))
            {
                SaveAs(vectorFilename);
            }
            else
            {
                Serialize(vectorFilename, vectorMap);
            }
        }

        /// <summary>
        /// Save the as tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
            => SaveAs(vectorFilename);
        #endregion Events

        #region Helpers
        /// <summary>
        /// Build the map.
        /// </summary>
        public void BuildMap()
            => TestCases.Tests(this, vectorMap, toolStack, CanvasPanel, textMeasurer, out boundaryItem);

        /// <summary>
        /// Save the as.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void SaveAs(string filename = "")
        {
            saveFileDialog1.FileName = filename;
            saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
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
        /// Load the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The <see cref="object"/>.</returns>
        private object LoadFile(string filename)
        {
            using (TextReader reader = new StreamReader(filename))
            {
                return vectorMapSerializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="item">The item.</param>
        private void Serialize(string filename, VectorMap item)
        {
            using (TextWriter tw = new StreamWriter(filename))
            {
                Serialize(tw, item);
            }
        }

        /// <summary>
        /// The serialize.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="item">The item.</param>
        private void Serialize(TextWriter writer, VectorMap item)
            => vectorMapSerializer.Serialize(writer, item);

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
        #endregion Helpers
    }
}
