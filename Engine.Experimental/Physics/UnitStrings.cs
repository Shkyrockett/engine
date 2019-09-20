// <copyright file="UnitStrings.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The unit strings class.
    /// </summary>
    public static class UnitStrings
    {
        /// <summary>
        /// The unit string (const). Value: "{0} {1}*{2}".
        /// </summary>
        public const string UnitString = "{0} {1}*{2}";

        /// <summary>
        /// The momentum (const). Value: "{0} kg*m/s".
        /// </summary>
        public const string Momentum = "{0} kg*m/s";

        /// <summary>
        /// The impulse ns (const). Value: "{0} kg*m/s".
        /// </summary>
        public const string ImpulseNs = "{0} kg*m/s"; // also N*s

        /// <summary>
        /// The impulse ft (const). Value: "{0} N*s".
        /// </summary>
        public const string ImpulseFt = "{0} N*s";

        /// <summary>
        /// The friction force (const). Value: "{0} N".
        /// </summary>
        public const string FrictionForce = "{0} N";
    }
}
