using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    //[Flags]
    public enum EdgeTypes
    {
        /// <summary>
        /// 
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 
        /// </summary>
        NonContributing = 1,

        /// <summary>
        /// 
        /// </summary>
        SameTransition = 2,

        /// <summary>
        /// 
        /// </summary>
        DifferentTransition = 3
    }
}
