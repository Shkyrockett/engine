// <copyright file="GraphicItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Represents an object that can be displayed on screen.
    /// </summary>
    public class GraphicItem
    {
        #region Fields

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        private Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GraphicItem()
        {
            propertyCache = new Dictionary<object, object>();
            Item = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public GraphicItem(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Item = item;
            item?.OnUpdate(ClearCache);
            Style = style;
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        [XmlElement]
        [DisplayName(nameof(Name))]
        [Category("Properties")]
        [Description("The name of the item.")]
        public string Name { get; set; }

        /// <summary>
        /// The item that gets displayed on screen.
        /// </summary>
        [Browsable(true)]
        [XmlElement(typeof(AngleVisualizerTester))]
        [XmlElement(typeof(BezierSegment))]
        [XmlElement(typeof(Circle))]
        [XmlElement(typeof(CircularArc))]
        [XmlElement(typeof(CircularSegment))]
        [XmlElement(typeof(CubicBezier))]
        [XmlElement(typeof(Ellipse))]
        [XmlElement(typeof(EllipticalArc))]
        [XmlElement(typeof(Line))]
        [XmlElement(typeof(LineSegment))]
        [XmlElement(typeof(NodeRevealer))]
        [XmlElement(typeof(Oval))]
        [XmlElement(typeof(ParametricDelegateCurve))]
        [XmlElement(typeof(ParametricPointTester))]
        [XmlElement(typeof(ParametricWarpGrid))]
        [XmlElement(typeof(Contour))]
        [XmlElement(typeof(Polygon))]
        [XmlElement(typeof(Polyline))]
        [XmlElement(typeof(PolylineSet))]
        [XmlElement(typeof(Ray))]
        [XmlElement(typeof(Rectangle2D))]
        [XmlElement(typeof(RectangleCellGrid))]
        [XmlElement(typeof(RectangleDCellGrid))]
        [XmlElement(typeof(RotatedRectangle2D))]
        [XmlElement(typeof(QuadraticBezier))]
        [XmlElement(typeof(PathContour))]
        [XmlElement(typeof(SquareCellGrid))]
        [XmlElement(typeof(SquareDCellGrid))]
        [XmlElement(typeof(Triangle))]
        [DisplayName(nameof(Item))]
        [Category("Properties")]
        [Description("The item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public GraphicsObject Item { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DisplayName(nameof(Style))]
        [Category("Properties")]
        [Description("The style of the item.")]
        public IStyle Style { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DisplayName(nameof(Metadata))]
        [Category("Properties")]
        [Description("The meta-data of the item.")]
        public Metadata Metadata { get; set; } = null;

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DisplayName(nameof(Perimeter))]
        [Category("Properties")]
        [Description("The perimeter length of the item.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double Perimeter
            => (double)CachingProperty(() => Item?.Perimeter);

        /// <summary>
        /// Gets the bounding rectangle of the graphical object.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DisplayName(nameof(Bounds))]
        [Category("Properties")]
        [Description("The bounding box of the item.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Rectangle2D Bounds
            => CachingProperty(() => Item?.Bounds) as Rectangle2D;

        // ToDo: Need to update point list when the nodes are moved.
        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [Browsable(false)]
        [DisplayName(nameof(LengthInterpolatedPoints))]
        [Category("Properties")]
        [Description("The length of the interpolated points of the item.")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<Point2D> LengthInterpolatedPoints
            => (List<Point2D>)CachingProperty(() => Item?.InterpolatePoints((int)Perimeter.Round()));

        #endregion

        #region Public methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Point2D Interpolate(double t)
            => Item.Interpolate(t);

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public List<Point2D> SmoothInterpolate(double t)
            => ((List<Point2D>)CachingProperty(() => Item.Interpolate(t)));

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<Point2D> InterpolatePoints()
            => ((List<Point2D>)CachingProperty(() => Item?.InterpolatePoints(500)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Point2D> InterpolateToPolygon(double points)
            => null;

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point2D point)
            => Item.Contains(point);

        /// <summary>
        ///
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool VisibleTest(Rectangle2D bounds)
            => (Item.Bounds.IntersectsWith(bounds) || Item.Bounds.Contains(bounds));

        /// <summary>
        ///
        /// </summary>
        public void Refresh()
            => ClearCache();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(GraphicItem)}{{{Item}}}";

        #endregion

        #region Private methods

        /// <summary>
        /// This should be run anytime a property of the item is modified.
        /// </summary>
        private void ClearCache()
            => propertyCache.Clear();

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
                var value = property.Invoke();
                propertyCache.Add(name, value);
                return value;
            }

            return propertyCache[name];
        }

        #endregion
    }
}
