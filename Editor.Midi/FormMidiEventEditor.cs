using Engine.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace EventEditorMidi
{
    /// <summary>
    /// The form midi event editor class.
    /// </summary>
    public partial class FormMidiEventEditor
        : Form
    {
        #region Fields
        /// <summary>
        /// The music files.
        /// </summary>
        private readonly MediaFiles musicFiles = new MediaFiles();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMidiEventEditor"/> class.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FormMidiEventEditor()
        {
            InitializeComponent();

            Midi.RegisterMediaCodecs();
            Riff.RegisterMediaCodecs();
            Xmf.RegisterMediaCodecs();

            var fileFormats = MediaFile.RegisteredTypes.ToList();
            toolStripComboBoxFileFormat.ComboBox.DataSource = fileFormats;
            toolStripComboBoxFileFormat.ComboBox.ValueMember = "Name";
            toolStripComboBoxFileFormat.SelectedItem = toolStripComboBoxFileFormat.Items[0];
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// The form1 load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Form1_Load(object sender, EventArgs e)
        {
            musicFiles.Media = new List<MediaFile> { new MediaFile(new Midi(new MidiHeader(), new List<MidiTrack> { new MidiTrack() })) };

            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            treeView.SuspendLayout();
            SuspendLayout();
            treeView.BeginUpdate();
            AddNode(treeView, musicFiles.Media);
            treeView.EndUpdate();
            splitContainer1.Panel1.ResumeLayout(false);
            treeView.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            treeView.PerformLayout();
            ResumeLayout(false);
        }

        /// <summary>
        /// The tree view1 after select.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The tree view event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) => propertyGrid.SelectedObject = treeView.SelectedNode.Tag;

        /// <summary>
        /// The tool strip button new file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ToolStripButtonNewFile_Click(object sender, EventArgs e)
        {
            var type = (Type)toolStripComboBoxFileFormat.SelectedItem;
            var item = (IMediaContainer)Activator.CreateInstance(type);
            if (item is Riff riff)
            {
                riff.Contents = new List<IMediaContainer> { new Midi() };
            }

            var musicFile = new MediaFile(item);
            musicFiles.Media.Add(musicFile);

            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            treeView.SuspendLayout();
            SuspendLayout();
            treeView.BeginUpdate();
            AddNode(treeView.TopNode, musicFile);
            treeView.EndUpdate();
            splitContainer1.Panel1.ResumeLayout(false);
            treeView.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            treeView.PerformLayout();
            ResumeLayout(false);
        }

        /// <summary>
        /// The tool strip button open file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ToolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            switch (openFileDialog.ShowDialog())
            {
                case DialogResult.Yes:
                case DialogResult.OK:
                    splitContainer1.Panel1.SuspendLayout();
                    splitContainer1.SuspendLayout();
                    treeView.SuspendLayout();
                    SuspendLayout();
                    treeView.BeginUpdate();
                    OpenFile(openFileDialog.FileName);
                    treeView.EndUpdate();
                    splitContainer1.Panel1.ResumeLayout(false);
                    treeView.ResumeLayout(false);
                    splitContainer1.Panel1.PerformLayout();
                    treeView.PerformLayout();
                    ResumeLayout(false);
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
        /// The tool strip button save file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ToolStripButtonSaveFile_Click(object sender, EventArgs e)
        { }

        /// <summary>
        /// The tool strip button close file click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ToolStripButtonCloseFile_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            treeView.SuspendLayout();
            SuspendLayout();
            treeView.BeginUpdate();
            CloseSelectedFile();
            treeView.EndUpdate();
            splitContainer1.Panel1.ResumeLayout(false);
            treeView.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            treeView.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="items">The items.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddNode(TreeView tree, List<MediaFile> items)
        {
            if (tree is null || items is null)
            {
                return;
            }

            var node = new TreeNode("Media Files")
            {
                Tag = items
            };

            tree.Nodes.Add(node);
            foreach (var item in items)
            {
                AddNode(node, item);
            }
        }

        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="item">The item.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddNode(TreeNode tree, MediaFile item)
        {
            if (tree is null || item is null)
            {
                return;
            }

            var typeString = item.Media?.GetType().Name;
            var filename = string.IsNullOrWhiteSpace(item?.FileName) ? $"New {typeString} File*" : Path.GetFileName(item.FileName);
            var node = new TreeNode(filename)
            {
                Tag = item
            };

            tree.Nodes.Add(node);
            AddNode(node, item.Media);
        }

        /// <summary>
        /// Add the node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="item">The item.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddNode(TreeNode tree, IMediaElement item)
        {
            if (tree is null || item is null)
            {
                return;
            }

            Type element = item?.GetType();
            var nodeName = item?.ToString() ?? string.Empty;

            var node = new TreeNode(nodeName)
            {
                Tag = item
            };
            tree.Nodes.Add(node);

            // This is still slow. Either the switch pattern matching is slow, or creating the TreeView nodes is slow.
            foreach (var prop in element?.GetProperties())
            {
                if (prop.GetIndexParameters().Length > 0) continue;
                var value = prop.GetValue(item, null);
                var child = new TreeNode(prop.Name)
                {
                    Tag = value
                };
                switch (value)
                {
                    case IMediaElement _:
                        node.Nodes.Add(child);
                        break;
                    case IEnumerable<IMediaElement> list:
                        node.Nodes.Add(child);
                        foreach (var subProp in list)
                        {
                            AddNode(child, subProp);
                        }
                        break;
                    default:
                        break;
                }
            }

            //// Either the reflection code is very slow, or adding TreeView nodes is slow here.
            //var iMidiElement = typeof(IMidiElement);
            //var listType = typeof(List<>);
            //foreach (var (child, isInterface, isGenericType, isListType, containsInterface)
            //    in from prop in element?.GetProperties()
            //       let propertyType = prop.PropertyType
            //       let isInterface = propertyType.GetInterfaces().Contains(iMidiElement)
            //       let containsInterface = prop.ReflectedType.GetInterfaces().Contains(iMidiElement)
            //       let isGenericType = propertyType.IsGenericType
            //       let isListType = isGenericType && propertyType.GetGenericTypeDefinition() == listType
            //       where (prop is not null) && (isInterface || (isGenericType && isListType && containsInterface))
            //       let child = new TreeNode(prop.Name)
            //       {
            //           Tag = prop.GetValue(item, null)
            //       }
            //       select (child, isInterface, isGenericType, isListType, containsInterface))
            //{
            //    if (isInterface)
            //    {
            //        node.Nodes.Add(child);
            //    }
            //    else if (isGenericType && isListType && containsInterface)
            //    {
            //        node.Nodes.Add(child);
            //        if (isGenericType && child.Tag is IEnumerable<IMidiElement> tag)
            //        {
            //            foreach (var subProp in tag)
            //            {
            //                AddNode(child, subProp);
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Open the file.
        /// </summary>
        /// <param name="filename">The fileName.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OpenFile(string filename)
        {
            //try
            //{
            var musicFile = MediaFile.Load(filename);
            musicFiles.Media.Add(musicFile);
            AddNode(treeView.TopNode, musicFile);
            //}
            //catch (Exception)
            //{
            //    // throw;
            //}
        }

        /// <summary>
        /// Closes the file.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CloseSelectedFile()
        {
            var root = treeView.Nodes[0];
            if (treeView.SelectedNode is TreeNode t && t != root)
            {
                var node = t;
                while (node.Parent != root)
                {
                    node = node.Parent;
                }

                musicFiles.Media.Remove(node.Tag as MediaFile);
                treeView.Nodes.Remove(node);
                GC.Collect();
                // There seems to be a memory leak somewhere. Removing TreeView nodes and music files does not free up all the expected memory.
            }
        }
        #endregion
    }
}
