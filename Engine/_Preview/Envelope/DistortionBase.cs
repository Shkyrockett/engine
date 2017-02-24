// <copyright file="DistortionBase.cs" >
//    Copyright (c) 2017 Ben Morris. All rights reserved.
// </copyright>
// <author id="benmorris44">Ben Morris</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/benmorris44/EnvelopeDistortion </remarks>

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public class DistortionBase
    {
        /// <summary>
        /// The direction the effect will be applied, for example Bulge vertical will apply to top and bottom only
        /// </summary>
        public DistortionDirection Direction { get; set; }

        /// <summary>
        /// The intensity of the effect can be positive or negative
        /// intensity factor is based on the relative size of the source
        /// </summary>
        public double Intensity { get; set; }
    }
}
