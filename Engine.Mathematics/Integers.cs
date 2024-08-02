// <copyright file="MathematicalConstants.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
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
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public static partial class Integers<T>
    where T : INumber<T>
{
    #region Integers
    /// <summary>
    /// Negative two.
    /// </summary>
    public static readonly T NegativeTwo = T.CreateChecked(-2);

    /// <summary>
    /// Negative one.
    /// </summary>
    public static readonly T NegativeOne = T.CreateChecked(-1);

    /// <summary>
    /// The zero Value: 0.
    /// </summary>
    public static readonly T Zero = T.Zero;

    /// <summary>
    /// The one Value: 1.
    /// </summary>
    public static readonly T One = T.One;

    /// <summary>
    /// The two Value: 2.
    /// </summary>
    public static readonly T Two = T.CreateChecked(2);

    /// <summary>
    /// The three Value: 3.
    /// </summary>
    public static readonly T Three = T.CreateChecked(3);

    /// <summary>
    /// The four Value: 4.
    /// </summary>
    public static readonly T Four = T.CreateChecked(4);

    /// <summary>
    /// The five Value: 5.
    /// </summary>
    public static readonly T Five = T.CreateChecked(5);

    /// <summary>
    /// The six Value: 6.
    /// </summary>
    public static readonly T Six = T.CreateChecked(6);

    /// <summary>
    /// The seven Value: 7.
    /// </summary>
    public static readonly T Seven = T.CreateChecked(7);

    /// <summary>
    /// The eight Value: 8.
    /// </summary>
    public static readonly T Eight = T.CreateChecked(8);

    /// <summary>
    /// The nine Value: 9.
    /// </summary>
    public static readonly T Nine = T.CreateChecked(9);

    /// <summary>
    /// The ten Value: 10.
    /// </summary>
    public static readonly T Ten = T.CreateChecked(10);

    /// <summary>
    /// The eleven Value: 11.
    /// </summary>
    public static readonly T Eleven = T.CreateChecked(11);

    /// <summary>
    /// The twelve Value: 12.
    /// </summary>
    public static readonly T Twelve = T.CreateChecked(12);

    /// <summary>
    /// The twenty Value: 20.
    /// </summary>
    public static readonly T Twenty = T.CreateChecked(20);
    #endregion
}
