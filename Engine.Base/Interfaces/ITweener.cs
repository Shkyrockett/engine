// <copyright file="VectorMap.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// 
/// </summary>
public interface ITweener
{
    ///// <summary>
    ///// Registers the lerper.
    ///// </summary>
    ///// <typeparam name="TLerper">The type of the lerper.</typeparam>
    ///// <param name="propertyType">Type of the property.</param>
    //void RegisterLerper<TLerper>(Type propertyType) where TLerper : IMemberLerper, new();

    ///// <summary>
    ///// Registers the lerper.
    ///// </summary>
    ///// <param name="lerperType">Type of the lerper.</param>
    ///// <param name="propertyType">Type of the property.</param>
    //void RegisterLerper(Type lerperType, Type propertyType);

    /// <summary>
    /// Tweens the specified target.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target">The target.</param>
    /// <param name="dests">The dests.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="delay">The delay.</param>
    /// <param name="overwrite">if set to <see langword="true"/> [overwrite].</param>
    /// <returns></returns>
    ITween Tween<T>(T target, object dests, double duration, double delay = 0, bool overwrite = true) where T : class;

    /// <summary>
    /// Timers the specified duration.
    /// </summary>
    /// <param name="duration">The duration.</param>
    /// <param name="delay">The delay.</param>
    /// <returns></returns>
    ITween Timer(double duration, double delay = 0);
}