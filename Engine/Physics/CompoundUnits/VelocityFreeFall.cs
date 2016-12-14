// <copyright file="VelocityFreeFall.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
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
    public class VelocityFreeFall
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gravity"></param>
        /// <param name="time"></param>
        public VelocityFreeFall(IAcceleration gravity, ITime time)
        {
            Gravity = gravity;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Gravity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Gravity.Value * Time.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Velocity at free fall";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Gravity.Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Time.Abreviation}{Time.Abreviation}";
    }
}
