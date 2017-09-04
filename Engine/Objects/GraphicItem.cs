// <copyright file="GraphicItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Represents an object that can be displayed on screen.
    /// </summary>
    [DataContract, Serializable]
    public class GraphicItem
    {
        #region Constants

        /// <summary>
        /// Unique identifier indexer.
        /// </summary>
        private static uint _id = 0;

        #endregion

        #region Fields

        /// <summary>
        /// The unique identifier for the object.
        /// </summary>
        protected uint id = _id++;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GraphicItem()
        { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public GraphicItem(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Shape = item;
            Name = Shape.ToString();
            Style = style;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unique identifier for the object.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The unique identifier for the object.")]
        public uint ID
            => id;

        /// <summary>
        ///
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        [Category("Properties")]
        [Description("The name of the item.")]
        public string Name { get; set; }

        /// <summary>
        /// The item that gets displayed on screen.
        /// </summary>
        [Browsable(true)]
        [XmlElement(typeof(Circle))]
        [XmlElement(typeof(CircularArc))]
        [XmlElement(typeof(CircularSegment))]
        [XmlElement(typeof(Ellipse))]
        [XmlElement(typeof(EllipticalArc))]
        [XmlElement(typeof(BezierSegment))]
        [XmlElement(typeof(CubicBezier))]
        [XmlElement(typeof(QuadraticBezier))]
        [XmlElement(typeof(Line))]
        [XmlElement(typeof(Ray))]
        [XmlElement(typeof(LineSegment))]
        [XmlElement(typeof(Triangle))]
        [XmlElement(typeof(PolygonContour))]
        [XmlElement(typeof(Polygon))]
        [XmlElement(typeof(Polyline))]
        [XmlElement(typeof(PolylineSet))]
        [XmlElement(typeof(Polycurve))]
        [XmlElement(typeof(PolycurveContour))]
        [XmlElement(typeof(Rectangle2D))]
        [XmlElement(typeof(RectangleCellGrid))]
        [XmlElement(typeof(RectangleDCellGrid))]
        [XmlElement(typeof(RotatedRectangle2D))]
        //[XmlElement(typeof(Oval))]
        [XmlElement(typeof(SquareCellGrid))]
        [XmlElement(typeof(SquareDCellGrid))]
        [XmlElement(typeof(AngleVisualizerTester))]
        [XmlElement(typeof(NodeRevealer))]
        [XmlElement(typeof(ParametricDelegateCurve))]
        [XmlElement(typeof(ParametricPointTester))]
        [XmlElement(typeof(ParametricWarpGrid))]
        //[DisplayName(nameof(Item))]
        [Category("Properties")]
        [Description("The item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public GraphicsObject Shape { get; set; }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The style of the item.")]
        public IStyle Style { get; set; }

        /// <summary>
        ///
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The meta-data of the item.")]
        public Metadata Metadata { get; set; } = null;

        #endregion

        #region Public methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Point2D point)
            => Shape.Contains(point);

        /// <summary>
        ///
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool VisibleTest(Rectangle2D bounds)
        {
            // Unbounded shapes have to be cropped to the visible bounds.
            switch (Shape)
            {
                case Ray r:
                    return (Intersections.LineRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom));
                case Line l:
                    return (Intersections.LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom));
                default:
                    return (Shape.Bounds.IntersectsWith(bounds) || Shape.Bounds.Contains(bounds));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => "";//$"{nameof(GraphicItem)}{{{Item}}}";

        #endregion
    }
}
