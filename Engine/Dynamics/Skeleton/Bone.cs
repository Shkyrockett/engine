// <copyright file="Bone.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Bone
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Transform2D Transform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Bone Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Bone IK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Chain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ChainIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool InheritTranslation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool InheritRotation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool InheritScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool BendPositive { get; set; }
    }
}
