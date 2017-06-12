// <copyright file="TreeViewBinding.iMidiElement.cs" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="Shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <copyright file="TreeViewBinding.iMidiElement.cs" >
//     Copyright © 2017 Martin Schreiber. All rights reserved.
// </copyright>
// <author id="gruemscheli">Martin Schreiber</author>
// <license>
//     Feel free to use it.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="iMidiElement"></typeparam>
    /// <remarks>
    /// Based on the idea presented by Martin Schreiber.
    /// https://schreibermartin.wordpress.com/2014/12/16/winforms-treeview-data-binding-part-1-of-2/
    /// https://schreibermartin.wordpress.com/2014/12/17/winforms-treeview-data-binding-part-2-of-2/
    /// </remarks>
    public class TreeViewBinding<iMidiElement> where iMidiElement
        : class
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private TreeView treeView;

        /// <summary>
        /// 
        /// </summary>
        private TreeNodeCollection treeNodeCollection;

        /// <summary>
        /// 
        /// </summary>
        private BindingSource bindingSource;

        /// <summary>
        /// 
        /// </summary>
        private Func<object, iMidiElement> getDataItemFunc;

        /// <summary>
        /// 
        /// </summary>
        private Func<iMidiElement, TreeNode> addTreeNodeFunc;

        /// <summary>
        /// 
        /// </summary>
        private Action<iMidiElement, TreeNode> updateTreeNodeAction;

        /// <summary>
        /// 
        /// </summary>
        private TreeNode currentAddItem;

        /// <summary>
        /// 
        /// </summary>
        private TreeNode parentTreeNode;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="bindingSource"></param>
        /// <param name="getDataItemFunc"></param>
        /// <param name="addTreeNodeFunc"></param>
        /// <param name="updateTreeNodeAction"></param>
        public TreeViewBinding(
            TreeView treeView,
            BindingSource bindingSource,
            Func<object, iMidiElement> getDataItemFunc,
            Func<iMidiElement, TreeNode> addTreeNodeFunc,
            Action<iMidiElement, TreeNode> updateTreeNodeAction)
            : this(treeView, treeView.Nodes, null, bindingSource, getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentTreeNode"></param>
        /// <param name="bindingSource"></param>
        /// <param name="getDataItemFunc"></param>
        /// <param name="addTreeNodeFunc"></param>
        /// <param name="updateTreeNodeAction"></param>
        public TreeViewBinding(
            TreeNode parentTreeNode,
            BindingSource bindingSource,
            Func<object, iMidiElement> getDataItemFunc,
            Func<iMidiElement, TreeNode> addTreeNodeFunc,
            Action<iMidiElement, TreeNode> updateTreeNodeAction)
            : this(parentTreeNode.TreeView, parentTreeNode.Nodes, parentTreeNode, bindingSource, getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="treeNodeCollection"></param>
        /// <param name="parentTreeNode"></param>
        /// <param name="bindingSource"></param>
        /// <param name="getDataItemFunc"></param>
        /// <param name="addTreeNodeFunc"></param>
        /// <param name="updateTreeNodeAction"></param>
        private TreeViewBinding(
            TreeView treeView,
            TreeNodeCollection treeNodeCollection,
            TreeNode parentTreeNode,
            BindingSource bindingSource,
            Func<object, iMidiElement> getDataItemFunc,
            Func<iMidiElement, TreeNode> addTreeNodeFunc,
            Action<iMidiElement, TreeNode> updateTreeNodeAction)
        {
            this.treeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            this.treeNodeCollection = treeNodeCollection ?? throw new ArgumentNullException("treeNodeCollection ");
            this.parentTreeNode = parentTreeNode; // may be null.
            this.bindingSource = bindingSource ?? throw new ArgumentNullException(nameof(bindingSource));
            this.getDataItemFunc = getDataItemFunc ?? throw new ArgumentNullException(nameof(getDataItemFunc));
            this.addTreeNodeFunc = addTreeNodeFunc ?? throw new ArgumentNullException(nameof(addTreeNodeFunc));
            this.updateTreeNodeAction = updateTreeNodeAction ?? throw new ArgumentNullException(nameof(updateTreeNodeAction));

            // sync to binding source's current items and selection.
            AddExistingItems();

            this.bindingSource.ListChanged += (s, e) =>
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.ItemAdded:
                        AddItem(e.NewIndex);
                        SelectItem();
                        break;
                    case ListChangedType.ItemChanged:
                        UpdateItem();
                        break;
                    case ListChangedType.ItemDeleted:
                        DeleteItem(e.NewIndex);
                        break;
                    case ListChangedType.ItemMoved:
                        MoveItem(e.OldIndex, e.NewIndex);
                        break;
                    case ListChangedType.PropertyDescriptorAdded:
                    case ListChangedType.PropertyDescriptorChanged:
                    case ListChangedType.PropertyDescriptorDeleted:
                        break;
                    case ListChangedType.Reset:
                        AddExistingItems();
                        SelectItem();
                        break;
                        //Default:
                        //throw new NotImplementedException("...");
                }
            };

            this.bindingSource.PositionChanged += (s, e) => SelectItem();
            this.treeView.AfterSelect += AfterNodeSelect;

            SelectItem();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterNodeSelect(object sender, TreeViewEventArgs e)
        {
            var treeNode = e.Node;
            // Skip, if the TreeNode belongs to a foreign collection.
            if (treeNode.Parent != parentTreeNode) return;
            bindingSource.Position = treeNode.Index;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddExistingItems()
        {
            foreach (var listItem in bindingSource.List)
            {
                iMidiElement dataItem = getDataItemFunc(listItem);
                TreeNode treeNode = addTreeNodeFunc(dataItem);
                if (treeNode == null) continue;
                updateTreeNodeAction(dataItem, treeNode);
                treeNodeCollection.Add(treeNode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newIndex"></param>
        private void AddItem(int newIndex)
        {
            iMidiElement dataItem = getDataItemFunc(bindingSource.Current);
            if (currentAddItem == null)
            {
                TreeNode treeNode = addTreeNodeFunc(dataItem);
                if (treeNode == null) return;
                treeNodeCollection.Insert(newIndex, treeNode);
                currentAddItem = treeNode;
                return;
            }
            updateTreeNodeAction(dataItem, currentAddItem);
            currentAddItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateItem()
        {
            if (bindingSource.Current == null)
                return;
            iMidiElement dataItem = getDataItemFunc(bindingSource.Current);
            var treeNode = treeNodeCollection[bindingSource.Position];
            updateTreeNodeAction(dataItem, treeNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void DeleteItem(int index)
        {
            treeNodeCollection.RemoveAt(index);
            currentAddItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        private void MoveItem(int oldIndex, int newIndex)
        {
            var treeNode = treeNodeCollection[bindingSource.Position];
            treeNodeCollection.RemoveAt(oldIndex);
            treeNodeCollection.Insert(newIndex, treeNode);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SelectItem()
        {
            if (bindingSource.Position < 0)
                return;
            if (treeNodeCollection.Count <= bindingSource.Position)
                return;
            var treeNode = treeNodeCollection[bindingSource.Position];
            treeNode.EnsureVisible();
            treeView.SelectedNode = treeNode;
        }

        #endregion
    }
}
