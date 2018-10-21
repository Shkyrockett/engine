using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Engine.File;

namespace EventEditorMidi
{
    /// <summary>
    /// The form midi event editor class.
    /// </summary>
    public partial class FormMidiEventEditor
        : Form
    {
        /// <summary>
        /// The music files.
        /// </summary>
        private MusicFiles musicFiles = new MusicFiles();

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMidiEventEditor"/> class.
        /// </summary>
        public FormMidiEventEditor()
        {
            InitializeComponent();

            var fileFormats = MusicFiles.ListFileFormats();
            toolStripComboBoxFileFormat.ComboBox.DataSource = fileFormats;
            toolStripComboBoxFileFormat.ComboBox.ValueMember = "Name";
            toolStripComboBoxFileFormat.SelectedItem = toolStripComboBoxFileFormat.Items[0];
        }

        /// <summary>
        /// The form1 load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            musicFiles.Midi = new List<MediaFile> {
                new MediaFile(
                new Midi
                {
                    //Name = "midi",
                    //DisplayName = "midi",
                    Header = new MidiHeader(),
                    Tracks = new List<MidiTrack>
                    {
                        new MidiTrack
                        {
                            //Name = "Track 1",
                        }
                    }
                }
               )
            };

            AddNode(treeView, musicFiles.Midi);
        }

        /// <summary>
        /// The tree view1 after select.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The tree view event arguments.</param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
            => propertyGrid.SelectedObject = treeView.SelectedNode.Tag;

        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="items">The items.</param>
        private void AddNode(TreeView tree, List<MediaFile> items)
        {
            var node = new TreeNode("Midi Files")
            {
                Tag = items
            };
            tree.Nodes.Add(node);
            foreach (MediaFile item in items)
                AddNode(node, item);
        }

        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="item">The item.</param>
        private void AddNode(TreeNode tree, MediaFile item)
        {
            if (item is null) return;
            var typeString = item?.Media.GetType().Name;
            var filename = string.IsNullOrWhiteSpace(item?.FileName) ? $"New {typeString} File*" : Path.GetFileName(item.FileName);
            var node = new TreeNode(filename)
            {
                Tag = item
            };
            tree.Nodes.Add(node);
            AddNode(node, item?.Media);
        }

        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="item">The item.</param>
        private void AddNode(TreeNode tree, IMidiElement item)
        {
            if (item is null) return;
            string nodeName = null;
            var attributeDisplayName = (Engine.DisplayNameAttribute)Attribute.GetCustomAttribute(item?.GetType(), typeof(Engine.DisplayNameAttribute));
            nodeName = attributeDisplayName != null ? attributeDisplayName.DisplayName : item?.ToString();

            var node = new TreeNode(nodeName)
            {
                Tag = item
            };
            tree.Nodes.Add(node);

            foreach (PropertyInfo prop in item?.GetType().GetProperties()
                .Where(p =>
                        
                           p.ReflectedType.GetInterfaces().Contains(typeof(IMidiElement))
                        
                        ||
                        (
                               p.PropertyType.IsGenericType
                            && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                            && p.ReflectedType.GetInterfaces().Contains(typeof(IMidiElement))
                        )
                    )
                )
            {
                if (prop != null)
                {
                    var child = new TreeNode(prop.Name)
                    {
                        Tag = prop.GetValue(item, null)
                    };

                    if (prop.PropertyType.GetInterfaces().Contains(typeof(IMidiElement)))
                    {
                        node.Nodes.Add(child);
                    }
                    else if (
                           prop.PropertyType.IsGenericType
                        && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                        && prop.ReflectedType.GetInterfaces().Contains(typeof(IMidiElement))
                        )
                    {
                        node.Nodes.Add(child);
                        if (prop.PropertyType.IsGenericType)
                        {
                            foreach (IMidiElement subProp in (IEnumerable)child.Tag)
                                AddNode(child, subProp);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The tool strip button new file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonNewFile_Click(object sender, EventArgs e)
        {
            var type = (Type)toolStripComboBoxFileFormat.SelectedItem;
            var item = (IMediaContainer)Activator.CreateInstance(type);
            if (item is Riff)
                ((Riff)item).Contents = new List<IMediaContainer> { new Midi() };
            var musicFile = new MediaFile(item);
            musicFiles.Midi.Add(musicFile);
            AddNode(treeView.TopNode, musicFile);
        }

        /// <summary>
        /// The tool strip button open file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ToolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            switch (openFileDialog.ShowDialog())
            {
                case DialogResult.None:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                case DialogResult.OK:
                    OpenFile(openFileDialog.FileName);
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Open the file.
        /// </summary>
        /// <param name="fileName">The fileName.</param>
        private void OpenFile(string fileName)
        {
            MediaFile musicFile = null;
            //try
            //{
            using (Stream stream = File.OpenRead(fileName))
                musicFile = MediaFile.Load(fileName, stream);
            musicFiles.Midi.Add(musicFile);
            AddNode(treeView.TopNode, musicFile);
            //}
            //catch (Exception)
            //{
            //    // throw;
            //}
        }

        /// <summary>
        /// The tool strip button save file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private static void ToolStripButtonSaveFile_Click(object sender, EventArgs e)
        { }

        /// <summary>
        /// The tool strip button close file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private static void ToolStripButtonCloseFile_Click(object sender, EventArgs e)
        { }
    }
}
