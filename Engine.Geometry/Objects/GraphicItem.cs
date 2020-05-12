// <copyright file="GraphicItem.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
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
        private static uint indexedID = 0;
        #endregion Constants

        #region Fields
        /// <summary>
        /// The unique identifier for the object.
        /// </summary>
        protected uint id = indexedID++;
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
        public GraphicItem(GraphicsObject item, IStyle style, MetadataObject metadata = null)
        {
            Shape = item;
            Name = Shape?.ToString(CultureInfo.InvariantCulture);
            Style = style;
            _ = metadata;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the unique identifier for the object.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The unique identifier for the object.")]
        public uint ID => id;

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
        public MetadataObject Metadata { get; set; } = null;

        /// <summary>
        /// The item that gets displayed on screen.
        /// </summary>
        [Browsable(true)]
        [XmlElement(typeof(Circle2D))]
        [XmlElement(typeof(CircularArc2D))]
        [XmlElement(typeof(CircularSegment2D))]
        [XmlElement(typeof(Ellipse2D))]
        [XmlElement(typeof(EllipticalArc2D))]
        [XmlElement(typeof(BezierSegment2D))]
        [XmlElement(typeof(CubicBezier2D))]
        [XmlElement(typeof(QuadraticBezier2D))]
        [XmlElement(typeof(Line2D))]
        [XmlElement(typeof(Ray2D))]
        [XmlElement(typeof(LineSegment2D))]
        [XmlElement(typeof(Triangle2D))]
        [XmlElement(typeof(PolygonContour2D))]
        [XmlElement(typeof(Polygon2D))]
        [XmlElement(typeof(Polyline2D))]
        [XmlElement(typeof(PolylineSet2D))]
        [XmlElement(typeof(Polycurve2D))]
        [XmlElement(typeof(PolycurveContour2D))]
        [XmlElement(typeof(Rectangle2D))]
        [XmlElement(typeof(RectangleCellGrid))]
        [XmlElement(typeof(RectangleDCellGrid))]
        [XmlElement(typeof(RotatedRectangle2D))]
        [XmlElement(typeof(SquareCellGrid))]
        [XmlElement(typeof(SquareDCellGrid))]
        [XmlElement(typeof(AngleVisualizerTester))]
        [XmlElement(typeof(NodeRevealer))]
        [XmlElement(typeof(ParametricDelegateCurve2D))]
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
        public bool VisibleTest(Rectangle2D bounds) =>
            // Unbounded shapes have to be cropped to the visible bounds.
            Shape switch
            {
                Ray2D r => Intersections.RayRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, (bounds?.X).Value, bounds.Y, bounds.Right, bounds.Bottom),
                Line2D l => Intersections.LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, (bounds?.X).Value, bounds.Y, bounds.Right, bounds.Bottom),
                _ => Shape.Bounds.IntersectsWith(bounds) || Shape.Bounds.Contains(bounds),
            };

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => "";//$"{nameof(GraphicItem)}{{{Item}}}";
        #endregion Public Methods
    }
}
