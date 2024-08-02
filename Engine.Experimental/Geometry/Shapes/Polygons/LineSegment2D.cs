namespace Engine;

///// <summary>
///// The Segment class is used to represent an edge of a polygon.
///// </summary>
//public class LineSegment2D
//{
//    /// <summary>
//    /// Segment endpoints
//    /// </summary>
//    private Point2D a;

//    /// <summary>
//    /// The b.
//    /// </summary>
//    private Point2D b;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
//    /// </summary>
//    private LineSegment2D()
//    { }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
//    /// </summary>
//    /// <param name="a">The a.</param>
//    /// <param name="b">The b.</param>
//    public LineSegment2D(Point2D a, Point2D b)
//    {
//        this.a = a;
//        this.b = b;
//    }

//    /// <summary>
//    /// Get the source point
//    /// </summary>
//    /// <returns></returns>
//    public Point2D A
//    {
//        get { return a; }
//        set { a = value; }
//    }

//    /// <summary>
//    /// Get the target point
//    /// </summary>
//    /// <returns></returns>
//    public Point2D B
//    {
//        get { return b; }
//        set { b = value; }
//    }

//    /// <summary>
//    /// Return the point of the segment with lexicographically smallest coordinate
//    /// </summary>
//    public Point2D Min => (a.X < b.X) || (a.X == b.X && a.Y < b.Y) ? a : b;

//    /// <summary>
//    /// Return the point of the segment with lexicographically largest coordinate
//    /// </summary>
//    public Point2D Max => (a.X > b.X) || (a.X == b.X && a.Y > b.Y) ? a : b;

//    /// <summary>
//    /// Gets a value indicating whether 
//    /// </summary>
//    public bool Degenerate
//        => a == b;

//    /// <summary>
//    /// Gets a value indicating whether 
//    /// </summary>
//    public bool IsVertical
//        => a.X == b.X;

//    /// <summary>
//    /// Change the segment orientation
//    /// </summary>
//    /// <returns></returns>
//    public LineSegment2D Reverse()
//    {
//        Operations.Swap(ref a, ref b);
//        return this;
//    }
//}