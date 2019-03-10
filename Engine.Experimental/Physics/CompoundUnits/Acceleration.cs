// <copyright file="Acceleration.cs" company="Shkyrockett" >
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
    /// The acceleration struct.
    /// </summary>
    public struct Acceleration
        : IAcceleration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Acceleration"/> class.
        /// </summary>
        /// <param name="velocityChange">The velocityChange.</param>
        /// <param name="timeInterval">The timeInterval.</param>
        public Acceleration(IVelocity velocityChange, ITime timeInterval)
        {
            VelocityChange = velocityChange;
            TimeInterval = timeInterval;
        }

        /// <summary>
        /// Gets or sets the velocity change.
        /// </summary>
        public IVelocity VelocityChange { get; set; }

        /// <summary>
        /// Gets or sets the time interval.
        /// </summary>
        public ITime TimeInterval { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value
            => VelocityChange.Value / TimeInterval.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Acceleration);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"∆{Value}/∆{VelocityChange.Abreviation}";

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} {Abreviation}";
    }
}
