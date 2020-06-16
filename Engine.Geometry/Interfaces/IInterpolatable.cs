// <copyright file="IInterpolatable.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The Interpolatable object interface.
    /// </summary>
    public interface IInterpolatable<T>
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        double Length { get; }

        /// <summary>
        /// Gets the interpolation minimum.
        /// </summary>
        /// <value>
        /// The interpolation minimum.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        double InterpolationMin { get; }

        /// <summary>
        /// Gets the interpolation maximum.
        /// </summary>
        /// <value>
        /// The interpolation maximum.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        double InterpolationMax { get; }

        /// <summary>
        /// Interpolates over the length of this instance from min to max.
        /// </summary>
        /// <returns>Returns an array of interpolated values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T[] Interpolate()
        {
            (var len, var min, var max) = (Length, InterpolationMin, InterpolationMax);
            var stepSize = (max - min) / len;
            return Interpolate(Enumerable.Range(0, (int)(len + 1)).Select(stepIndex => min + (stepIndex * stepSize)));
        }

        /// <summary>
        /// Interpolates this instance at the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>Returns an array of interpolated values.</returns>
        T Interpolate(double t);

        /// <summary>
        /// Interpolates this instance at the specified t values.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>Returns an array of interpolated values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T[] Interpolate(params double[] ts) => ts.Select(t => Interpolate(t)).ToArray();

        /// <summary>
        /// Interpolates this instance at the specified t values.
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>Returns an array of interpolated values.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T[] Interpolate(IEnumerable<double> ts) => ts.Select(t => Interpolate(t)).ToArray();
    }
}
