// <copyright file="ElectronVolt.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct ElectronVolt
        : IEnergy
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public ElectronVolt(double value)
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
            => "Electron Volt";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "eV";

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ElectronVolt(double value)
            => new ElectronVolt(value);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} eV";
    }
}
