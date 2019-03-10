// <copyright file="Power.cs" company="Shkyrockett" >
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
    /// The power struct.
    /// </summary>
    public struct Power
        : IPower
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Power"/> class.
        /// </summary>
        /// <param name="work">The work.</param>
        /// <param name="time">The time.</param>
        public Power(IEnergy work, ITime time)
        {
            Work = work;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the work.
        /// </summary>
        public IEnergy Work { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Work.Value / Time.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Power);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "J";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value}";
    }
}
