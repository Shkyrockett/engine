// <copyright file="AverageSpeed.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public struct AverageSpeed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="time"></param>
        public AverageSpeed(List<ISpeed> speed, ITime time)
        {
            Speed = speed;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ISpeed> Speed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Average Speed";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"∆{Value}/{Speed[0].Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} ∆{Speed[0].Abreviation}/{Time.Abreviation}";
    }
}
