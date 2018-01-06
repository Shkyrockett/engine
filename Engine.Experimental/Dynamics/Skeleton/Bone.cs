// <copyright file="Bone.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The bone class.
    /// </summary>
    public class Bone
        : Shape
    {
        /// <summary>
        /// Gets or sets the transform.
        /// </summary>
        public Transform2D Transform { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public Bone Parent { get; set; }

        /// <summary>
        /// Gets or sets the IK.
        /// </summary>
        public Bone IK { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the chain.
        /// </summary>
        public uint Chain { get; set; }

        /// <summary>
        /// Gets or sets the chain index.
        /// </summary>
        public int ChainIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool InheritTranslation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool InheritRotation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool InheritScale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool BendPositive { get; set; }
    }
}
