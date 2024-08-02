// <copyright file="Physics.cs" company="Shkyrockett" >
// Copyright © 2016 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine;

/// <summary>
/// The physics math class.
/// </summary>
public static partial class PhysicsMath
{
    /// <summary>
    /// Add the velocities.
    /// </summary>
    /// <param name="v1">The v1.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="c">The c.</param>
    /// <returns>The <see cref="T"/>.</returns>
    public static T AddVelocities<T>(T v1, T v2, T? c = default)
        where T : struct, INumber<T>
    {
        c ??= T.CreateSaturating(299790000);
        return v1 + (v2 / (T.One + (v1 * v2 / c.Value * c.Value)));
    }

    /// <summary>
    /// The average velocity.
    /// </summary>
    /// <param name="velocity">The velocity.</param>
    /// <param name="time">The time.</param>
    /// <returns>The <see cref="double"/>.</returns>
    public static T AverageVelocity<T>(List<T> velocity, T time)
        where T : struct, INumber<T> => velocity.Sum() / time;

    /// <summary>
    /// Sums the specified source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    public static T Sum<T>(this IEnumerable<T> source)
        where T : INumber<T>
    {
        T sum = T.Zero;
        foreach (var item in source)
        {
            sum += item;
        }

        return sum;
    }

    /// <summary>
    /// The distance traveled.
    /// </summary>
    /// <param name="acceleration">The acceleration.</param>
    /// <param name="time">The time.</param>
    /// <returns>The <see cref="double"/>.</returns>
    public static T DistanceTraveled<T>(Acceleration<T> acceleration, T time)
        where T : INumber<T> => acceleration.Value * time * time;

    /// <summary>
    /// The distance traveled.
    /// </summary>
    /// <param name="averageSpeed">The averageSpeed.</param>
    /// <param name="time">The time.</param>
    /// <returns>The <see cref="T"/>.</returns>
    public static T DistanceTraveled<T>(T averageSpeed, T time) where T : INumber<T> => averageSpeed * time;

    /// <summary>
    /// The free fall velocity.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>The <see cref="T"/>.</returns>
    public static T FreeFallVelocity<T>(ITime<T> time) where T : INumber<T> => PhysicsMath<T>.EarthGravity.Value * time.Value;

    /// <summary>
    /// The frequency.
    /// </summary>
    /// <param name="period">The period.</param>
    /// <returns>The <see cref="T"/>.</returns>
    public static T Frequency<T>(T period) where T : INumber<T> => T.One / period;

    /// <summary>
    /// The period.
    /// </summary>
    /// <param name="frequency">The frequency.</param>
    /// <returns>The <see cref="T"/>.</returns>
    public static T Period<T>(T frequency) where T : INumber<T> => T.One / frequency;
}
