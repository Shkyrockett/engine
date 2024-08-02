namespace Engine;

/// <summary>
/// The bounding bbox2d class.
/// </summary>
public class BoundingBbox2D
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBbox2D"/> class.
    /// </summary>
    /// <param name="xmin">The xmin.</param>
    /// <param name="ymin">The ymin.</param>
    /// <param name="xmax">The xmax.</param>
    /// <param name="ymax">The ymax.</param>
    private BoundingBbox2D(double xmin = 0, double ymin = 0, double xmax = 0, double ymax = 0)
    {
        Xmin = xmin;
        Ymin = ymin;
        Xmax = xmax;
        Ymax = ymax;
    }

    /// <summary>
    /// Gets or sets the xmin.
    /// </summary>
    /// <value>
    /// The xmin.
    /// </value>
    public double Xmin { get; set; }

    /// <summary>
    /// Gets or sets the xmax.
    /// </summary>
    /// <value>
    /// The xmax.
    /// </value>
    public double Xmax { get; set; }

    /// <summary>
    /// Gets or sets the ymin.
    /// </summary>
    /// <value>
    /// The ymin.
    /// </value>
    public double Ymin { get; set; }

    /// <summary>
    /// Gets or sets the ymax.
    /// </summary>
    /// <value>
    /// The ymax.
    /// </value>
    public double Ymax { get; set; }

    /// <summary>
    /// Add an amount to both values in the <see cref="Point2D" /> classes.
    /// </summary>
    /// <param name="a">The original value</param>
    /// <param name="b">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>                                                                                                                                                                                            public static BoundingBbox2D operator +(BoundingBbox2D a, BoundingBbox2D b)
        => new(Math.Min((a?.Xmin).Value, (b?.Xmin).Value),
                              Math.Min(a.Ymin, b.Ymin),
                              Math.Max(a.Xmax, b.Xmax),
                              Math.Max(a.Ymax, b.Ymax));

    /// <summary>
    /// Adds the specified left.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    public static BoundingBbox2D Add(BoundingBbox2D left, BoundingBbox2D right) => left + right;
}