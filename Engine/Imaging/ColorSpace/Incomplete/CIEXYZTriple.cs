namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// 
    /// </summary>
    public class CIEXYZTriple
    {
        /// <summary>
        /// 
        /// </summary>
        public CIEXYZTriple()
            : this(new CIEXYZ(), new CIEXYZ(), new CIEXYZ())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public CIEXYZTriple(CIEXYZ red, CIEXYZ green, CIEXYZ blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Red { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Green { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Blue { get; set; }
    }
}
