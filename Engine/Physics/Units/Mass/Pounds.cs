// <copyright file="Pounds.cs" >
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
    public struct Pounds
        : IMass
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Pounds(double value)
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
        public string Name => "Pounds";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "lb";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Pounds(double value) => new Pounds(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} lb";
    }
}
