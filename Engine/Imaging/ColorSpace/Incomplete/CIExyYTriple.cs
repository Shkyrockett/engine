namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// 
    /// </summary>
    public class CIExyYTriple
    {
        /// <summary>
        /// 
        /// </summary>
        public CIExyYTriple()
            : this(new CIExyY(), new CIExyY(), new CIExyY())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public CIExyYTriple(CIExyY red,CIExyY green,CIExyY blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Red { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Green { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Blue { get; set; }
    }
}
