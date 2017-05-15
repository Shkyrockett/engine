using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct ColorTransform
    {
        #region Implementations

        /// <summary>
        /// 
        /// </summary>
        public static ColorTransform Identity = new ColorTransform(1d, 1d, 1d, 1d, 0, 0, 0, 0);

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double alphaMultiplier;

        /// <summary>
        /// 
        /// </summary>
        private double redMultiplier;

        /// <summary>
        /// 
        /// </summary>
        private double greenMultiplier;

        /// <summary>
        /// 
        /// </summary>
        private double blueMultiplier;

        /// <summary>
        /// 
        /// </summary>
        private int alphaOffset;

        /// <summary>
        /// 
        /// </summary>
        private int redOffset;

        /// <summary>
        /// 
        /// </summary>
        private int greenOffset;

        /// <summary>
        /// 
        /// </summary>
        private int blueOffset;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ColorTransform(double alphaMultiplier, double redMultiplier, double greenMultiplier, double blueMultiplier, int alphaOffset, int redOffset, int greenOffset, int blueOffset)
        {
            this.alphaMultiplier = alphaMultiplier;
            this.redMultiplier = redMultiplier;
            this.greenMultiplier = greenMultiplier;
            this.blueMultiplier = blueMultiplier;
            this.alphaOffset = alphaOffset;
            this.redOffset = redOffset;
            this.greenOffset = greenOffset;
            this.blueOffset = blueOffset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double AlphaMultiplier { get { return alphaMultiplier; } set { alphaMultiplier = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double RedMultiplier { get { return redMultiplier; } set { redMultiplier = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double GreenMultiplier { get { return greenMultiplier; } set { greenMultiplier = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double BlueMultiplier { get { return blueMultiplier; } set { blueMultiplier = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int AlphaOffset { get { return alphaOffset; } set { alphaOffset = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int RedOffset { get { return redOffset; } set { redOffset = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int GreenOffset { get { return greenOffset; } set { greenOffset = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int BlueOffset { get { return blueOffset; } set { blueOffset = value; } }

        #endregion
    }
}
