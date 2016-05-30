namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// 
        /// </summary>
        double Value { get; /*set;*/ }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
