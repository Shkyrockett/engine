namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public interface IVelocity
    {
        /// <summary>
        /// 
        /// </summary>
        double Value { get; /*set;*/ }

        ///// <summary>
        ///// 
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Abreviation { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
