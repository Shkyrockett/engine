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
        private CIEXYZ red;

        /// <summary>
        /// 
        /// </summary>
        private CIEXYZ green;

        /// <summary>
        /// 
        /// </summary>
        private CIEXYZ blue;

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
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Red
        {
            get { return red; }
            set { red = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Green
        {
            get { return green; }
            set { green = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public CIEXYZ Blue
        {
            get { return blue; }
            set { blue = value; }
        }
    }
}
