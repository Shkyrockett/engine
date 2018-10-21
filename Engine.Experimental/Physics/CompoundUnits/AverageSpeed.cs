// <copyright file="AverageSpeed.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// The average speed struct.
    /// </summary>
    public struct AverageSpeed
        : ISpeed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AverageSpeed"/> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="time">The time.</param>
        public AverageSpeed(List<ISpeed> speed, ITime time)
        {
            Speed = speed;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public List<ISpeed> Speed { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value
        {
            get
            {
                double rSpeed = 0;
                foreach (ISpeed cSpeed in Speed)
                    rSpeed += cSpeed.Value;

                return rSpeed / Time.Value;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Average Speed";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"∆{Value}/{Speed[0].Abreviation}";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} ∆{Speed[0].Abreviation}/{Time.Abreviation}";
    }
}
