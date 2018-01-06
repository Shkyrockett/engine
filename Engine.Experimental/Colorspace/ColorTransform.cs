// <copyright file="ColorTransform.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
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
    /// The color transform struct.
    /// </summary>
    public struct ColorTransform
    {
        #region Implementations

        /// <summary>
        /// The identity.
        /// </summary>
        public static ColorTransform Identity = new ColorTransform(1d, 1d, 1d, 1d, 0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTransform"/> class.
        /// </summary>
        /// <param name="alphaMultiplier">The alphaMultiplier.</param>
        /// <param name="redMultiplier">The redMultiplier.</param>
        /// <param name="greenMultiplier">The greenMultiplier.</param>
        /// <param name="blueMultiplier">The blueMultiplier.</param>
        /// <param name="alphaOffset">The alphaOffset.</param>
        /// <param name="redOffset">The redOffset.</param>
        /// <param name="greenOffset">The greenOffset.</param>
        /// <param name="blueOffset">The blueOffset.</param>
        public ColorTransform(double alphaMultiplier, double redMultiplier, double greenMultiplier, double blueMultiplier, int alphaOffset, int redOffset, int greenOffset, int blueOffset)
        {
            AlphaMultiplier = alphaMultiplier;
            RedMultiplier = redMultiplier;
            GreenMultiplier = greenMultiplier;
            BlueMultiplier = blueMultiplier;
            AlphaOffset = alphaOffset;
            RedOffset = redOffset;
            GreenOffset = greenOffset;
            BlueOffset = blueOffset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the alpha multiplier.
        /// </summary>
        public double AlphaMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the red multiplier.
        /// </summary>
        public double RedMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the green multiplier.
        /// </summary>
        public double GreenMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the blue multiplier.
        /// </summary>
        public double BlueMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the alpha offset.
        /// </summary>
        public int AlphaOffset { get; set; }

        /// <summary>
        /// Gets or sets the red offset.
        /// </summary>
        public int RedOffset { get; set; }

        /// <summary>
        /// Gets or sets the green offset.
        /// </summary>
        public int GreenOffset { get; set; }

        /// <summary>
        /// Gets or sets the blue offset.
        /// </summary>
        public int BlueOffset { get; set; }

        #endregion
    }
}
