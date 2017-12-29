using System;

namespace Engine
{
    /// <summary>
    /// The rotation struct.
    /// </summary>
    public struct Rotation
    {
        /// <summary>
        /// The radiens.
        /// </summary>
        private double radiens;

        /// <summary>
        /// The cos.
        /// </summary>
        private double? cos;

        /// <summary>
        /// The sin.
        /// </summary>
        private double? sin;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rotation"/> class.
        /// </summary>
        /// <param name="radiens">The radiens.</param>
        public Rotation(double radiens)
        {
            this.radiens = radiens;
            cos = null;
            sin = null;
        }

        /// <summary>
        /// Gets or sets the radiens.
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
        /// Gets or sets the degrees.
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
        /// Gets the cosine.
        /// </summary>
        public double Cosine
            => (cos = cos ?? Math.Sin(radiens)).Value;

        /// <summary>
        /// Gets the sine.
        /// </summary>
        public double Sine
            => (sin = sin ?? Math.Sin(radiens)).Value;
    }
}
