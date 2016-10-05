// <copyright file="AtomicMassUnit.cs" >
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
    public struct AtomicMassUnit
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public AtomicMassUnit(double value)
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
        public string Name => "Atomic Mass Unit";
        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "amu";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator AtomicMassUnit(double value) => new AtomicMassUnit(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} amu";
    }
}
