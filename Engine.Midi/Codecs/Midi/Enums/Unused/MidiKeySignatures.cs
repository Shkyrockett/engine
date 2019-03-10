// <copyright file="MidiKeySignatures.cs" company="Shkyrockett">
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File
{
    /// <summary>
    /// Key Signatures.
    /// </summary>
    public enum MidiKeySignatures
        : sbyte
    {
        /// <summary>
        /// No Flats Or Sharps.
        /// </summary>
        NoFlatsOrSharps = 0,

        /// <summary>
        /// First Flat.
        /// </summary>
        Flat1 = -1,

        /// <summary>
        /// Second Flat.
        /// </summary>
        Flat2 = -2,

        /// <summary>
        /// Third Flat.
        /// </summary>
        Flat3 = -3,

        /// <summary>
        /// Fourth Flat.
        /// </summary>
        Flat4 = -4,

        /// <summary>
        /// Fifth Flat.
        /// </summary>
        Flat5 = -5,

        /// <summary>
        /// Sixth Flat.
        /// </summary>
        Flat6 = -6,

        /// <summary>
        /// Seventh Flat.
        /// </summary>
        Flat7 = -7,

        /// <summary>
        /// First Sharp.
        /// </summary>
        Sharp1 = 1,

        /// <summary>
        /// Second Sharp.
        /// </summary>
        Sharp2 = 2,

        /// <summary>
        /// Third Sharp.
        /// </summary>
        Sharp3 = 3,

        /// <summary>
        /// Fourth Sharp.
        /// </summary>
        Sharp4 = 4,

        /// <summary>
        /// Fifth Sharp.
        /// </summary>
        Sharp5 = 5,

        /// <summary>
        /// Sixth Sharp.
        /// </summary>
        Sharp6 = 6,

        /// <summary>
        /// Seventh Sharp.
        /// </summary>
        Sharp7 = 7,
    }
}
