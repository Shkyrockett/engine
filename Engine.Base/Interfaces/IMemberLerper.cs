// <copyright file="IMemberLerper.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Jacob Albano. All rights reserved.
// </copyright>
// <author id="jacobalbano">Jacob Albano</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMemberLerper
    {
        /// <summary>
        /// Initializes the specified from value.
        /// </summary>
        /// <param name="fromValue">From value.</param>
        /// <param name="toValue">To value.</param>
        /// <param name="behavior">The behavior.</param>
        void Initialize(object fromValue, object toValue, LerpBehaviors behavior);

        /// <summary>
        /// Interpolates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="currentValue">The current value.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns></returns>
        object Interpolate(double t, object currentValue, LerpBehaviors behavior);
    }
}
