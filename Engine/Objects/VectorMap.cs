// <copyright file="VectorMap.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Engine.Tweening;
using System.ComponentModel;
using System.Runtime.Serialization;
using System;

namespace Engine
{
    /// <summary>
    /// The vector map class.
    /// </summary>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class VectorMap
        : ICollection<GraphicItem>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public VectorMap()
            : this(new List<GraphicItem>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapes"></param>
        public VectorMap(List<GraphicItem> shapes)
        {
            Items = shapes;
        }
        #endregion Constructors

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<GraphicItem> this[Rectangle2D area]
            => new List<GraphicItem>(
                from shape in Items
                where (shape?.Shape?.Bounds != null) && shape.VisibleTest(area)
                select shape);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public List<GraphicItem> this[Point2D point]
            => new List<GraphicItem>(
                from shape in Items
                where (shape?.Shape?.Bounds != null) && shape.Shape.Bounds.Contains(point) && shape.Shape.Contains(point)
                select shape);
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Zoom { get; set; }

        /// <summary>
        /// Gets or sets the pan.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public Point2D Pan { get; set; }

        /// <summary>
        /// Gets or sets the visible bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Rectangle2D VisibleBounds { get; set; }

        /// <summary>
        /// Gets or sets the tweener.
        /// </summary>
        [XmlElement]
        public Tweener Tweener { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [XmlArray]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<GraphicItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<GraphicItem> SelectedItems { get; set; }

        /// <summary>
        /// Gets or sets the rubberband items.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<GraphicItem> RubberbandItems { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count
            => Items.Count;
        #endregion Properties

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(GraphicItem item)
            => Items.Add(item);

        /// <summary>
        /// Add the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="VectorMap"/>.</returns>
        public VectorMap AddItem(GraphicItem item)
        {
            Items.Add(item);
            return this;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The <see cref="VectorMap"/>.</returns>
        public VectorMap Add(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            var graphicItem = new GraphicItem(item, style, metadata);
            Items.Add(graphicItem);
            return this;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The <see cref="VectorMap"/>.</returns>
        public VectorMap Add(Shape item, IStyle style = null, Metadata metadata = null)
        {
            //if (style is null) style = IStyle.DefaultStyle;
            var graphicItem = new GraphicItem(item, style, metadata);
            Items.Add(graphicItem);
            return this;
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Remove(GraphicItem item)
        {
            var success = false;
            if (SelectedItems.Contains(item))
                success |= SelectedItems.Remove(item);
            if (Items.Contains(item))
                success |= Items.Remove(item);
            return success;
        }

        /// <summary>
        /// Clear.
        /// </summary>
        public void Clear()
        {
            SelectedItems.Clear();
            Items.Clear();
        }

        /// <summary>
        /// Select the item.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="GraphicItem"/>.</returns>
        public GraphicItem SelectItem(Point2D point)
            => Items?.LastOrDefault(shape => shape.Shape.Bounds != null && shape.Shape.Bounds.IntersectsWith(VisibleBounds) && shape.Contains(point));

        /// <summary>
        /// Select the items.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="T:List{GraphicItem}"/>.</returns>
        public List<GraphicItem> SelectItems(Point2D point)
            => new List<GraphicItem>(
            from shape in Items
            where shape.Shape.Bounds.IntersectsWith(VisibleBounds) && shape.Contains(point)
            select shape);

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Contains(GraphicItem item)
            => Items.Contains(item);

        /// <summary>
        /// Copy the to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">The arrayIndex.</param>
        public void CopyTo(GraphicItem[] array, int arrayIndex)
            => Items.CopyTo(array, arrayIndex);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{GraphicItem}"/>.</returns>
        public IEnumerator<GraphicItem> GetEnumerator()
            => Items.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => Items.GetEnumerator();
    }
}
