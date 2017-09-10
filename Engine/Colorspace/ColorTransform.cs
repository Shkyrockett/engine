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

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public double AlphaMultiplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double RedMultiplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double GreenMultiplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double BlueMultiplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AlphaOffset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RedOffset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GreenOffset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BlueOffset { get; set; }

        #endregion
    }
}
