﻿// <copyright file="GraphicItem.cs" company="Shkyrockett" >
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
        //#region Fields

        ///// <summary>
        ///// Property cache for commonly used properties that may take time to calculate.
        ///// </summary>
        ///// <remarks>This needs to be statically initialized because not all classes initialize the base constructor.</remarks>
        //private Dictionary<object, object> propertyCache = new Dictionary<object, object>();

        //#endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GraphicItem()
        {
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
            //item?.OnUpdate(ClearCache);
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

        #endregion

        #region Public methods

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public Point2D Interpolate(double t)
        //    => Item.Interpolate(t);

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public List<Point2D> SmoothInterpolate(double t)
        //    => ((List<Point2D>)CachingProperty(() => Item.Interpolate(t)));

        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public List<Point2D> InterpolatePoints()
        //    => ((List<Point2D>)CachingProperty(() => Item?.InterpolatePoints(500)));

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="points"></param>
        ///// <returns></returns>
        //public List<Point2D> InterpolateToPolygon(double points)
        //    => null;

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

        ///// <summary>
        /////
        ///// </summary>
        //public void Refresh()
        //    => ClearCache();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(GraphicItem)}{{{Item}}}";

        #endregion

        //#region Private methods

        ///// <summary>
        ///// This should be run anytime a property of the item is modified so that cached properties get recalculated for the new values.
        ///// </summary>
        //private void ClearCache()
        //    => propertyCache.Clear();

        ///// <summary>
        ///// Private method for caching computationally and memory intensive properties of child objects
        ///// so the child object's properties only get touched when necessary.
        ///// </summary>
        ///// <param name="property">
        ///// The method to execute if the property hasn't been cached.
        ///// To pass a method or property, use the Lambda notation (T)CachingProperty(()=>PropertyName) 
        ///// or (T)CachingProperty(()=>MethodName(Parameters)) where T is the type of the return value.
        ///// </param>
        ///// <param name="name">Auto-filled parameter representing the name of the property being accessed.</param>
        ///// <returns>Returns an <see cref="object"/> containing the results of the delegate property, or cached value.</returns>
        ///// <remarks>http://syncor.blogspot.com/2010/11/passing-getter-and-setter-of-c-property.html</remarks>
        //private object CachingProperty(Func<object> property, [CallerMemberName]string name = "")
        //{
        //    if (!propertyCache.ContainsKey(name))
        //    {
        //        var value = property.Invoke();
        //        propertyCache.Add(name, value);
        //        return value;
        //    }

        //    return propertyCache[name];
        //}

        //#endregion
    }
}
