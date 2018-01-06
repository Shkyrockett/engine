// <copyright file="Lerper.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

namespace Engine.Tweening
{
    /// <summary>
    /// The lerper class.
    /// </summary>
    public abstract class Lerper
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="fromValue">The fromValue.</param>
        /// <param name="toValue">The toValue.</param>
        /// <param name="behavior">The behavior.</param>
        public abstract void Initialize(object fromValue, object toValue, LerpBehavior behavior);

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="currentValue">The currentValue.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public abstract object Interpolate(double t, object currentValue, LerpBehavior behavior);
    }
}
