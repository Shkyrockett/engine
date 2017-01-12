// <copyright file="Lerper.cs" company="Shkyrockett" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Lerper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="behavior"></param>
        public abstract void Initialize(object fromValue, object toValue, LerpBehavior behavior);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="currentValue"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
		public abstract object Interpolate(double t, object currentValue, LerpBehavior behavior);
    }
}
