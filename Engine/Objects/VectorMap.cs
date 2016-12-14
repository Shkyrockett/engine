// <copyright file="VectorMap.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Imaging;
using Engine.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
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

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<GraphicItem> this[Rectangle2D area]
        {
            get
            {
                return new List<GraphicItem>(
                from shape in Items
                where (shape?.Bounds != null) && (shape.Bounds.IntersectsWith(area) || shape.Bounds.Contains(area))
                select shape);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public List<GraphicItem> this[Point2D point]
            => new List<GraphicItem>(
            from shape in Items
            where shape.Bounds.Contains(point) && shape.Contains(point)
            select shape);

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public double Zoom { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public Point2D Pan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public Rectangle2D VisibleBounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Tweener Tweener { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        public List<GraphicItem> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public List<GraphicItem> SelectedItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public List<GraphicItem> RubberbandItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public int Count
            => Items.Count;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(GraphicItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public VectorMap AddItem(GraphicItem item)
        {
            Items.Add(item);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public VectorMap Add(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Items.Add(new GraphicItem(item, style, metadata));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public VectorMap Add(Shape item, ShapeStyle style = null, Metadata metadata = null)
        {
            if (style == null) style = ShapeStyle.DefaultStyle;
            Items.Add(new GraphicItem(item, style, metadata));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(GraphicItem item)
        {
            bool success = false;
            if (SelectedItems.Contains(item))
                success |= SelectedItems.Remove(item);
            if (Items.Contains(item))
                success |= Items.Remove(item);
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            SelectedItems.Clear();
            Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public GraphicItem SelectItem(Point2D point)
            => Items?.LastOrDefault(shape => shape.Bounds != null && (shape.Bounds.IntersectsWith(VisibleBounds) && shape.Contains(point)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public List<GraphicItem> SelectItems(Point2D point)
            => new List<GraphicItem>(
            from shape in Items
            where shape.Bounds.IntersectsWith(VisibleBounds) && shape.Contains(point)
            select shape);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(GraphicItem item)
            => Items.Contains(item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(GraphicItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GraphicItem> GetEnumerator()
            => Items.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => Items.GetEnumerator();
    }
}
