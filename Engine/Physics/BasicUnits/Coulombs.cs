// <copyright file="Coulombs.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
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
    /// Unit of Charge
    /// </summary>
    public struct Coulombs
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Coulombs(double value)
        {
            Value = value;
        }

        /// <summary>
        ///
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Coulombs";

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Coulombs(double value) => new Coulombs(value);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} C";
    }
}
