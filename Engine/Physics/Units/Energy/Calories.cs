// <copyright file="Calories.cs" >
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
    public struct Calories
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Calories(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Calories);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "cal";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Calories(double value) => new Calories(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} cal";
    }
}
