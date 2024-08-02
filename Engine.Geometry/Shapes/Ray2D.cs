// <copyright file="Ray2D.cs" company="Shkyrockett" >
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

namespace Engine;

/// <summary>
/// The ray class.
/// </summary>
/// <seealso cref="Engine.Shape2D" />
/// <seealso cref="Engine.IShapeSegment" />
[DataContract, Serializable]
[GraphicsObject]
[DisplayName(nameof(Ray2D))]
[TypeConverter(typeof(StructConverter<Ray2D>))]
[DebuggerDisplay("{ToString()}")]
public class Ray2D
    : Shape2D, IShapeSegment
{
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
    /// Initializes a new instance of the <see cref="Ray2D" /> class.
    /// </summary>
    public Ray2D()
        : this(Point2D.Empty, Vector2D.Empty)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ray2D" /> class.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="direction">The direction.</param>
    public Ray2D(Point2D location, Vector2D direction)
    {
        this.location = location;
        this.direction = direction;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ray2D" /> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    public Ray2D(double x, double y, double i, double j)
        : this(new Point2D(x, y), new Vector2D(i, j))
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
    /// The to line.
    /// </summary>
    /// <returns>The <see cref="Line2D"/>.</returns>
    public Line2D ToLine() => new(location, direction);

    /// <summary>
    /// Creates a string representation of this <see cref="Ray2D"/> struct based on the format string
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
            return $"{nameof(Ray2D)}";
        }

        var sep = Tokenizer.GetNumericListSeparator(provider);
        return $"{nameof(Ray2D)}={{{nameof(Location)}={Location.ToString(format, provider)}{sep}{nameof(Direction)}={Direction.ToString(format, provider)}}}";
    }
    #endregion Methods
}
