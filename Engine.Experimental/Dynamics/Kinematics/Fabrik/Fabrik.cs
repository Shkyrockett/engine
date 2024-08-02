namespace Engine.Experimental;

/// <summary>
/// The fabrik class.
/// </summary>
public static class Fabrik
{
    /// <summary>
    /// The reach.
    /// </summary>
    /// <param name="head">The head.</param>
    /// <param name="tail">The tail.</param>
    /// <param name="target">The tgt.</param>
    /// <returns>
    /// Returns new head and tail <see cref="ValueTuple{T1, T2}" /> where `new_head` has been moved to the target.
    /// </returns>
    /// <acknowledgment>
    /// http://sean.cm/a/fabrik-algorithm-2d
    /// </acknowledgment>
    public static (Point2D Head, Point2D Tail) Reach(Point2D head, Point2D tail, Point2D target)
    {
        // Calculate the current length (in practice, this should be calculated once and saved, not re-calculated every time `reach` is called)
        var c_dx = tail.X - head.X;
        var c_dy = tail.Y - head.Y;
        var c_dist = Math.Sqrt((c_dx * c_dx) + (c_dy * c_dy));

        // Calculate the stretched length
        var s_dx = tail.X - target.X;
        var s_dy = tail.Y - target.Y;
        var s_dist = Math.Sqrt((s_dx * s_dx) + (s_dy * s_dy));

        // Calculate how much to scale the stretched line
        var scale = c_dist / s_dist;

        // Return the result. Copy the target for the new head. Scale the new tail based on distance from target.
        return (target, new Point2D(target.X + (s_dx * scale), target.Y + (s_dy * scale)));
    }

    /// <summary>
    /// The reach.
    /// </summary>
    /// <param name="segments">The segments.</param>
    /// <param name="target">The target mouse Position.</param>
    /// <acknowledgment>
    /// http://sean.cm/a/fabrik-algorithm-2d
    /// </acknowledgment>
    public static void Reach(List<Point2D> segments, Point2D target)
    {
        // For each segment (except the last one)
        for (var i = 0; i < segments?.Count - 1; i++)
        {
            // Perform a reach for this segment
            var (Head, Tail) = Reach(segments[i], segments[i + 1], target);

            // Update this node (r.Head is guaranteed to be the same point as `target`)
            segments[i] = Head;

            // Update the target, so the next segment's head targets this segments new tail
            target = Tail;
        }

        // For the last segment, move it to the target
        segments[^1] = target;
    }

    /// <summary>
    /// The reach pinned.
    /// </summary>
    /// <param name="segments">The segments.</param>
    /// <param name="target">The tgt.</param>
    /// <acknowledgment>
    /// http://sean.cm/a/fabrik-algorithm-2d
    /// </acknowledgment>
    public static void ReachPinned(List<Point2D> segments, Point2D target)
    {
        // Save the fixed base location
        var constraint = segments[^1];

        // Perform the iterative reach in the forward direction (same as before)
        for (var i = 0; i < segments?.Count - 1; i++)
        {
            var (Head, Tail) = Reach(segments[i], segments[i + 1], target);
            segments[i] = Head;
            target = Tail;
        }

        segments[^1] = target;

        // At this point, our base has moved from its original location... so perform the iterative reach in reverse, with the target set to the initial base location
        target = constraint;
        for (var i = segments.Count - 1; i > 0; i--)
        {
            var (Head, Tail) = Reach(segments[i], segments[i - 1], target);
            segments[i] = Head;
            target = Tail;
        }

        segments[0] = target;
    }
}
