﻿// <copyright file="Atmospheres.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    ///
    /// </summary>
    public struct Atmospheres
        : IPressure
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Atmospheres(double value)
            => Value = value;

        /// <summary>
        ///
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Atmospheres";

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Atmospheres(double value)
            => new Atmospheres(value);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} atm";
    }
}
