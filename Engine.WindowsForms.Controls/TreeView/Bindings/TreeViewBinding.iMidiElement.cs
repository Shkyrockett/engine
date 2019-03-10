// <copyright file="TreeViewBinding.iMidiElement.cs" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// The tree view binding class.
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
        /// The tree view (readonly).
        /// </summary>
        private readonly TreeView treeView;

        /// <summary>
        /// The tree node collection (readonly).
        /// </summary>
        private readonly TreeNodeCollection treeNodeCollection;

        /// <summary>
        /// The binding source (readonly).
        /// </summary>
        private readonly BindingSource bindingSource;

        /// <summary>
        /// The get data item func.
        /// </summary>
        private readonly Func<object, iMidiElement> getDataItemFunc;

        /// <summary>
        /// The add tree node func.
        /// </summary>
        private readonly Func<iMidiElement, TreeNode> addTreeNodeFunc;

        /// <summary>
        /// The update tree node action.
        /// </summary>
        private readonly Action<iMidiElement, TreeNode> updateTreeNodeAction;

        /// <summary>
        /// The current add item.
        /// </summary>
        private TreeNode currentAddItem;

        /// <summary>
        /// The parent tree node.
        /// </summary>
        private readonly TreeNode parentTreeNode;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewBinding{iMidiElement}"/> class.
        /// </summary>
        /// <param name="treeView">The treeView.</param>
        /// <param name="bindingSource">The bindingSource.</param>
        /// <param name="getDataItemFunc">The getDataItemFunc.</param>
        /// <param name="addTreeNodeFunc">The addTreeNodeFunc.</param>
        /// <param name="updateTreeNodeAction">The updateTreeNodeAction.</param>
        public TreeViewBinding(
            TreeView treeView,
            BindingSource bindingSource,
            Func<object, iMidiElement> getDataItemFunc,
            Func<iMidiElement, TreeNode> addTreeNodeFunc,
            Action<iMidiElement, TreeNode> updateTreeNodeAction)
            : this(treeView, treeView.Nodes, null, bindingSource, getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewBinding{iMidiElement}"/> class.
        /// </summary>
        /// <param name="parentTreeNode">The parentTreeNode.</param>
        /// <param name="bindingSource">The bindingSource.</param>
        /// <param name="getDataItemFunc">The getDataItemFunc.</param>
        /// <param name="addTreeNodeFunc">The addTreeNodeFunc.</param>
        /// <param name="updateTreeNodeAction">The updateTreeNodeAction.</param>
        public TreeViewBinding(
            TreeNode parentTreeNode,
            BindingSource bindingSource,
            Func<object, iMidiElement> getDataItemFunc,
            Func<iMidiElement, TreeNode> addTreeNodeFunc,
            Action<iMidiElement, TreeNode> updateTreeNodeAction)
            : this(parentTreeNode.TreeView, parentTreeNode.Nodes, parentTreeNode, bindingSource, getDataItemFunc, addTreeNodeFunc, updateTreeNodeAction)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewBinding{iMidiElement}"/> class.
        /// </summary>
        /// <param name="treeView">The treeView.</param>
        /// <param name="treeNodeCollection">The treeNodeCollection.</param>
        /// <param name="parentTreeNode">The parentTreeNode.</param>
        /// <param name="bindingSource">The bindingSource.</param>
        /// <param name="getDataItemFunc">The getDataItemFunc.</param>
        /// <param name="addTreeNodeFunc">The addTreeNodeFunc.</param>
        /// <param name="updateTreeNodeAction">The updateTreeNodeAction.</param>
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
        #endregion Constructors

        #region Methods
        /// <summary>
        /// The after node select.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The tree view event arguments.</param>
        private void AfterNodeSelect(object sender, TreeViewEventArgs e)
        {
            var treeNode = e.Node;
            // Skip, if the TreeNode belongs to a foreign collection.
            if (treeNode.Parent != parentTreeNode)
            {
                return;
            }

            bindingSource.Position = treeNode.Index;
        }

        /// <summary>
        /// Add the existing items.
        /// </summary>
        private void AddExistingItems()
        {
            foreach (var listItem in bindingSource.List)
            {
                var dataItem = getDataItemFunc(listItem);
                var treeNode = addTreeNodeFunc(dataItem);
                if (treeNode is null)
                {
                    continue;
                }

                updateTreeNodeAction(dataItem, treeNode);
                treeNodeCollection.Add(treeNode);
            }
        }

        /// <summary>
        /// Add the item.
        /// </summary>
        /// <param name="newIndex">The newIndex.</param>
        private void AddItem(int newIndex)
        {
            var dataItem = getDataItemFunc(bindingSource.Current);
            if (currentAddItem is null)
            {
                var treeNode = addTreeNodeFunc(dataItem);
                if (treeNode is null)
                {
                    return;
                }

                treeNodeCollection.Insert(newIndex, treeNode);
                currentAddItem = treeNode;
                return;
            }
            updateTreeNodeAction(dataItem, currentAddItem);
            currentAddItem = null;
        }

        /// <summary>
        /// Update the item.
        /// </summary>
        public void UpdateItem()
        {
            if (bindingSource.Current is null)
            {
                return;
            }

            var dataItem = getDataItemFunc(bindingSource.Current);
            var treeNode = treeNodeCollection[bindingSource.Position];
            updateTreeNodeAction(dataItem, treeNode);
        }

        /// <summary>
        /// Delete the item.
        /// </summary>
        /// <param name="index">The index.</param>
        private void DeleteItem(int index)
        {
            treeNodeCollection.RemoveAt(index);
            currentAddItem = null;
        }

        /// <summary>
        /// Move the item.
        /// </summary>
        /// <param name="oldIndex">The oldIndex.</param>
        /// <param name="newIndex">The newIndex.</param>
        private void MoveItem(int oldIndex, int newIndex)
        {
            var treeNode = treeNodeCollection[bindingSource.Position];
            treeNodeCollection.RemoveAt(oldIndex);
            treeNodeCollection.Insert(newIndex, treeNode);
        }

        /// <summary>
        /// Select the item.
        /// </summary>
        private void SelectItem()
        {
            if (bindingSource.Position < 0)
            {
                return;
            }

            if (treeNodeCollection.Count <= bindingSource.Position)
            {
                return;
            }

            var treeNode = treeNodeCollection[bindingSource.Position];
            treeNode.EnsureVisible();
            treeView.SelectedNode = treeNode;
        }
        #endregion Methods
    }
}
