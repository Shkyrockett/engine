// <copyright file="Speed.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// The speed struct.
    /// </summary>
    public struct Speed
        : ISpeed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Speed"/> class.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="time">The time.</param>
        public Speed(ILength distance, ITime time)
        {
            Distance = distance;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Distance.Value / Time.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Seconds);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Distance.Abreviation}/{Time.Abreviation}";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} {Distance.Abreviation}/{Time.Abreviation}";
    }
}
