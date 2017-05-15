// <copyright file="GraphicItem.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// Represents an object that can be displayed on screen.
    /// </summary>
    public class GraphicItem
    {
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
            Item = item;
            Name = Item.ToString();
            Style = style;
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        //[XmlElement]
        //[DisplayName(nameof(Name))]
        [Category("Properties")]
        [Description("The name of the item.")]
        public string Name { get; set; }

        /// <summary>
        /// The item that gets displayed on screen.
        /// </summary>
        //[Browsable(true)]
        //[XmlElement(typeof(Circle))]
        //[XmlElement(typeof(CircularArc))]
        //[XmlElement(typeof(CircularSegment))]
        //[XmlElement(typeof(Ellipse))]
        //[XmlElement(typeof(EllipticalArc))]
        //[XmlElement(typeof(BezierSegment))]
        //[XmlElement(typeof(CubicBezier))]
        //[XmlElement(typeof(QuadraticBezier))]
        //[XmlElement(typeof(Line))]
        //[XmlElement(typeof(Ray))]
        //[XmlElement(typeof(LineSegment))]
        //[XmlElement(typeof(Triangle))]
        //[XmlElement(typeof(Contour))]
        //[XmlElement(typeof(Polygon))]
        //[XmlElement(typeof(Polyline))]
        //[XmlElement(typeof(PolylineSet))]
        //[XmlElement(typeof(Polycurve))]
        //[XmlElement(typeof(PolycurveContour))]
        //[XmlElement(typeof(Rectangle2D))]
        //[XmlElement(typeof(RectangleCellGrid))]
        //[XmlElement(typeof(RectangleDCellGrid))]
        //[XmlElement(typeof(RotatedRectangle2D))]
        //[XmlElement(typeof(Oval))]
        //[XmlElement(typeof(SquareCellGrid))]
        //[XmlElement(typeof(SquareDCellGrid))]
        //[XmlElement(typeof(AngleVisualizerTester))]
        //[XmlElement(typeof(NodeRevealer))]
        //[XmlElement(typeof(ParametricDelegateCurve))]
        //[XmlElement(typeof(ParametricPointTester))]
        //[XmlElement(typeof(ParametricWarpGrid))]
        //[DisplayName(nameof(Item))]
        [Category("Properties")]
        [Description("The item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [RefreshProperties(RefreshProperties.All)]
        //[NotifyParentProperty(true)]
        public GraphicsObject Item { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        //[DisplayName(nameof(Style))]
        [Category("Properties")]
        [Description("The style of the item.")]
        public IStyle Style { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlIgnore, SoapIgnore]
        //[DisplayName(nameof(Metadata))]
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
            => Item.Contains(point);

        /// <summary>
        ///
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool VisibleTest(Rectangle2D bounds)
        {
            // Unbounded shapes have to be cropped to the visible bounds.
            switch (Item)
            {
                case Ray r:
                    return (Intersections.LineRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom));
                case Line l:
                    return (Intersections.LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, bounds.X, bounds.Y, bounds.Right, bounds.Bottom));
                default:
                    return (Item.Bounds.IntersectsWith(bounds) || Item.Bounds.Contains(bounds));
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
