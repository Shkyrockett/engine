namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public enum InsideOutside
    {
        /// <summary>
        /// Point lies outside the shape.
        /// </summary>
        Outside = 0,

        /// <summary>
        /// Point is contained inside the shape.
        /// </summary>
        Inside = 1,

        /// <summary>
        /// Touches the boundary of the shape.
        /// </summary>
        Boundary = -1,
    }
}
