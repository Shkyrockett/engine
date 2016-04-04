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
        private CIExyY red;

        /// <summary>
        /// 
        /// </summary>
        private CIExyY green;

        /// <summary>
        /// 
        /// </summary>
        private CIExyY blue;

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
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Red
        {
            get { return red; }
            set { red = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Green
        {
            get { return green; }
            set { green = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CIExyY Blue
        {
            get { return blue; }
            set { blue = value; }
        }
    }
}
