// <copyright file="Vector3.cs" >
//     Copyright (c) 2013 Jacob Albano. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="jacobalbano">Jacob Albano</author>
// <summary></summary>
// <remarks>Based on: https://bitbucket.org/jacobalbano/glide </remarks>

namespace Engine.Tweening
{
    /// <summary>
    /// 
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// 
        /// </summary>
        private double x;

        /// <summary>
        /// 
        /// </summary>
        private double y;

        /// <summary>
        /// 
        /// </summary>
        private double z;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// 
        /// </summary>
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        #region Equals and GetHashCode implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector3) && Equals((Vector3)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                hashCode += 1000000007 * X.GetHashCode();
                hashCode += 1000000009 * Y.GetHashCode();
                hashCode += 1000000021 * Z.GetHashCode();
            }

            return hashCode;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{{{1}={2},{3}={4},{5}={6}}}", nameof(Vector3), nameof(X), x, nameof(Y), y, nameof(Z), z);
        }
    }
}
