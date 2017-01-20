// <copyright file="Distance.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct Distance
        : ILength
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="time"></param>
        public Distance(ISpeed speed, ITime time)
        {
            Speed = speed;
            Time = time;
        }

        /// <summary>
        ///
        /// </summary>
        public ISpeed Speed { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value
            => Time.Value * Speed.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Distance";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"{Speed.Abreviation}{Time.Abreviation}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} {Abreviation}";
    }
}
