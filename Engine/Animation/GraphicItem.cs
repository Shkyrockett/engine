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
        [XmlElement(typeof(Polycurve))]
        [XmlElement(typeof(PolycurveContour))]
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
    }
}
