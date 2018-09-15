// <copyright file="GraphicItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
using System.ServiceModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Represents an object that can be displayed on screen.
    /// </summary>
    [DataContract, Serializable]
    //[XmlSerializerFormat]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GraphicItem
    {
        #region Constants
        /// <summary>
        /// Unique identifier indexer.
        /// </summary>
        private static uint _id = 0;
        #endregion Constants

        #region Fields
        /// <summary>
        /// The unique identifier for the object.
        /// </summary>
        protected uint id = _id++;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicItem"/> class.
        /// </summary>
        public GraphicItem()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicItem"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        /// <param name="metadata">The metadata.</param>
        public GraphicItem(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Shape = item;
            Name = Shape?.ToString();
            Style = style;
        }
        #endregion Constructors

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
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name"), XmlAttribute(AttributeName = "name"), SoapAttribute(AttributeName = "name")]
        [Category("Properties")]
        [Description("The name of the item.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The meta-data of the item.")]
        public Metadata Metadata { get; set; } = null;

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
        [XmlElement(typeof(SquareCellGrid))]
        [XmlElement(typeof(SquareDCellGrid))]
        [XmlElement(typeof(AngleVisualizerTester))]
        [XmlElement(typeof(NodeRevealer))]
        [XmlElement(typeof(ParametricDelegateCurve))]
        [XmlElement(typeof(ParametricPointTester))]
        [XmlElement(typeof(ParametricWarpGrid))]
        [Category("Properties")]
        [Description("The item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public GraphicsObject Shape { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The style of the item.")]
        public IStyle Style { get; set; }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Contains(Point2D point)
            => Shape.Contains(point);

        /// <summary>
        /// The visible test.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool VisibleTest(Rectangle2D bounds)
        {
            // Unbounded shapes have to be cropped to the visible bounds.
            switch (Shape)
            {
                case Ray r:
                    return Intersections.RayRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
                case Line l:
                    return Intersections.LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
                default:
                    return Shape.Bounds.IntersectsWith(bounds) || Shape.Bounds.Contains(bounds);
            }
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => "";//$"{nameof(GraphicItem)}{{{Item}}}";
        #endregion Public Methods
    }
}
