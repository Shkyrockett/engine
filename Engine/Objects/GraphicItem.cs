// <copyright file="GraphicItem.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicItem
    {
        #region Private properties

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        private Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public GraphicItem(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Item = item;
            item.OnUpdate(ClearCache);
            Style = style;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        [XmlElement]
        public GraphicsObject Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Metadata Metadata { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        public double Perimeter => (double)CachingProperty(() => Item.Perimeter);

        /// <summary>
        /// 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [XmlIgnore]
        public Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Item.Bounds);

        // ToDo: Need to update point list when the nodes are moved.
        /// <summary>
        /// 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [XmlIgnore]
        public List<Point2D> LengthInterpolatedPoints
            => (List<Point2D>)CachingProperty(() => Item.InterpolatePoints(Perimeter.RoundToInt()));

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point2D Interpolate(double t) => Item.Interpolate(t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Point2D> SmoothInterpolate(double t) => ((List<Point2D>)CachingProperty(() => Item.Interpolate(t)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Point2D> InterpolateToPolygon(double points)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool HitTest(Point2D point) => Item.HitTest(point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool VisibleTest(Rectangle2D bounds) => (Item.Bounds.IntersectsWith(bounds) || Item.Bounds.Contains(bounds));

        /// <summary>
        /// 
        /// </summary>
        public void Refresh() => ClearCache();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{nameof(GraphicItem)}{{{Item}}}";

        #endregion

        #region Private methods

        /// <summary>
        /// This should be run anytime a property of the item is modified.
        /// </summary>
        private void ClearCache()
        {
            propertyCache.Clear();
        }

        /// <summary>
        /// Private method for caching computationally and memory intensive properties of child objects
        /// so the child object's properties only get touched when necessary.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</remarks>
        private object CachingProperty(Func<object> property, [CallerMemberName]string name = "")
        {
            if (!propertyCache.ContainsKey(name))
            {
                propertyCache.Add(name, property.Invoke());
            }
            return propertyCache[name];
        }

        #endregion
    }
}
