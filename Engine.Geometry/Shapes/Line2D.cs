// <copyright file="Line2D.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine;

/// <summary>
/// 2D Line Structure
/// </summary>
/// <structure>Engine.Geometry.Line2D</structure>
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Line2D))]
[TypeConverter(typeof(StructConverter<Line2D>))]
[XmlType(TypeName = "line", Namespace = "http://www.w3.org/2000/svg")]
[DebuggerDisplay("{ToString()}")]
public class Line2D
    : Shape2D, IShapeSegment
{
    #region Implementations
    /// <summary>
    /// Represents a Engine.Geometry.Segment that is null.
    /// </summary>

    public static readonly Line2D Empty = new(0d, 0d, 0d, 0d);
    #endregion Implementations

    #region Fields
    /// <summary>
    /// The location.
    /// </summary>
    private Point2D location;

    /// <summary>
    /// The direction.
    /// </summary>
    private Vector2D direction;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Line2D"/> class.
    /// </summary>
    public Line2D()
        : this(Point2D.Empty, Vector2D.Empty)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Line2D"/> class.
    /// </summary>
    /// <param name="tuple"></param>
    public Line2D((double x, double y, double i, double j) tuple)
        : this(tuple.x, tuple.y, tuple.i, tuple.j)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Line2D"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    public Line2D(double x, double y, double i, double j)
        : this(new Point2D(x, y), new Vector2D(i, j))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
    /// </summary>
    /// <param name="Point">Starting Point</param>
    /// <param name="RadAngle">Ending Angle</param>
    public Line2D(Point2D Point, double RadAngle)
        : this(Point.X, Point.Y, Cos(RadAngle), Sin(RadAngle))
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Line2D"/> class.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="direction">The direction.</param>
    public Line2D(Point2D location, Vector2D direction)
    {
        Location = location;
        Direction = direction;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
    /// </summary>
    /// <param name="a">Starting Point</param>
    /// <param name="b">Ending Point</param>
    public Line2D(Point2D a, Point2D b)
        : this(a.X, a.Y, b.X - a.X, b.Y - a.Y)
    { }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// The deconstruct.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    public void Deconstruct(out double x, out double y, out double i, out double j)
    {
        x = Location.X;
        y = Location.Y;
        i = Direction.I;
        j = Direction.J;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    /// <value>
    /// The location.
    /// </value>
    [DataMember, XmlElement, SoapElement]
    public Point2D Location
    {
        get { return location; }
        set
        {
            location = value;
            ClearCache();
            OnPropertyChanged(nameof(Location));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets or sets the direction.
    /// </summary>
    /// <value>
    /// The direction.
    /// </value>
    [DataMember, XmlElement, SoapElement]
    public Vector2D Direction
    {
        get { return direction; }
        set
        {
            direction = value;
            ClearCache();
            OnPropertyChanged(nameof(Direction));
            update?.Invoke();
        }
    }

    /// <summary>
    /// Gets the bounds.
    /// </summary>
    /// <value>
    /// The bounds.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public override Rectangle2D Bounds => new(location, location + direction);

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IShapeSegment" /> position should be calculated relative to the last item, or from Origin.
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if relative; otherwise, <see langword="false" />.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public bool Relative { get; set; }

    /// <summary>
    /// Gets or sets a reference to the segment after this segment.
    /// </summary>
    /// <value>
    /// The before.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IShapeSegment Before { get; set; }

    /// <summary>
    /// Gets or sets a reference to the segment before this segment.
    /// </summary>
    /// <value>
    /// The after.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IShapeSegment After { get; set; }

    /// <summary>
    /// Gets or sets the head point.
    /// </summary>
    /// <value>
    /// The head.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D Head { get; set; }

    /// <summary>
    /// Gets or sets the next to first point from the head point.
    /// </summary>
    /// <value>
    /// The next to head point.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D NextToHead { get; set; }

    /// <summary>
    /// Gets or sets the next to last point to the tail point.
    /// </summary>
    /// <value>
    /// The next to tail point.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D NextToTail { get; set; }

    /// <summary>
    /// Gets or sets the tail point.
    /// </summary>
    /// <value>
    /// The tail.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D Tail { get; set; }
    #endregion Properties

    #region Methods
    /// <summary>
    /// The interpolate.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="Point2D"/>.</returns>
    public override Point2D Interpolate(double t) => Interpolators.Linear(t, location, location + direction);

    /// <summary>
    /// Creates a string representation of this <see cref="Line2D"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ConvertToString(string format, IFormatProvider provider)
    {
        if (this is null)
        {
            return $"{nameof(Line2D)}";
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        return $"{nameof(Line2D)}={{{nameof(Location)}={Location.ToString(format, provider)}{sep}{nameof(Direction)}={Direction.ToString(format, provider)}}}";
    }
    #endregion Methods
}
