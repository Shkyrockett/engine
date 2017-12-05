using System;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct Rotation
    {
        /// <summary>
        /// 
        /// </summary>
        private double radiens;

        /// <summary>
        /// 
        /// </summary>
        private double? cos;

        /// <summary>
        /// 
        /// </summary>
        private double? sin;

        /// <summary>
        /// 
        /// </summary>
        public Rotation(double radiens)
        {
            this.radiens = radiens;
            cos = null;
            sin = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Radiens
        {
            get { return radiens; }
            set
            {
                radiens = value;
                cos = null;
                sin = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Degrees
        {
            get { return radiens.ToDegrees(); }
            set
            {
                radiens = value.ToRadians();
                cos = null;
                sin = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Cosine
            => (cos = cos ?? Math.Sin(radiens)).Value;

        /// <summary>
        /// 
        /// </summary>
        public double Sine
            => (sin = sin ?? Math.Sin(radiens)).Value;
    }
}
