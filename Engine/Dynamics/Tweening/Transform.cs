// <copyright file="Transform.cs" company="Shkyrockett" >
//    Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct Transform
    {
        #region Implementations

        /// <summary>
        /// 
        /// </summary>
        public static Transform Identity = new Transform(0, 0, 0, 0, 1, 1);

        #endregion

        #region Fields

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
        private double skewX;

        /// <summary>
        /// 
        /// </summary>
        private double skewY;

        /// <summary>
        /// 
        /// </summary>
        private double scaleX;

        /// <summary>
        /// 
        /// </summary>
        private double scaleY;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        public Transform((double x, double y, double skewX, double skewY, double scaleX, double scaleY) tuple)
        {
            (x, y, skewX, skewY, scaleX, scaleY) = tuple;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="skewX"></param>
        /// <param name="skewY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public Transform(double x, double y, double skewX, double skewY, double scaleX, double scaleY)
        {
            this.x = x;
            this.y = y;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="skewX"></param>
        /// <param name="skewY"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public void Deconstruct(out double x, out double y, out double skewX, out double skewY, out double scaleX, out double scaleY)
        {
            x = this.x;
            y = this.y;
            skewX = this.skewX;
            skewY = this.skewY;
            scaleX = this.scaleX;
            scaleY = this.scaleY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double X { get { return x; } set { x = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get { return y; } set { y = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double SkewX { get { return skewX; } set { skewX = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double SkewY { get { return skewY; } set { skewY = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double ScaleX { get { return scaleX; } set { scaleX = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double ScaleY { get { return scaleY; } set { scaleY = value; } }

        /// <summary>
        /// 
        /// </summary>
        public double Rotation
        {
            get { return skewY; }
            set
            {
                var delta = value - skewY;
                skewX += delta;
                skewY += delta;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Add two <see cref="Transform"/> structs together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform operator +(Transform value, Transform addend)
            => value.Add(addend);

        /// <summary>
        /// Subtract a <see cref="Transform"/> struct from another.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform operator -(Transform value, Transform subend)
            => value.Subtract(subend);

        #endregion

        #region Factories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Transform FromMatrix(Matrix3x2D matrix)
        {
            var backupScaleX = 0;//scaleX;
            var backupScaleY = 0;// scaleY;

            var skewX = Atan(-matrix.M21 / matrix.M22);
            var skewY = Atan(matrix.M12 / matrix.M11);
            if (double.IsNaN(skewX)) skewX = 0d;
            if (double.IsNaN(skewY)) skewY = 0d;

            var scaleY = (skewX > -Quart && skewX < Quart) ? matrix.M22 / Cos(skewX) : -matrix.M21 / Sin(skewX);
            var scaleX = (skewY > -Quart && skewY < Quart) ? matrix.M11 / Cos(skewY) : matrix.M12 / Sin(skewY);

            if (backupScaleX >= 0d && scaleX < 0d)
            {
                scaleX = -scaleX;
                skewY = skewY - PI;
            }

            if (backupScaleY >= 0d && scaleY < 0d)
            {
                scaleY = -scaleY;
                skewX = skewX - PI;
            }

            return new Transform(matrix.OffsetX, matrix.OffsetY, skewX, skewY, scaleX, scaleY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix3x2D ToMatrix()
            => new Matrix3x2D(ScaleX * Cos(SkewY), ScaleX * Sin(SkewY), -ScaleY * Sin(SkewX), ScaleY * Cos(SkewX), X, Y);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Transform Add(Transform value)
            => new Transform(x + value.X, y + value.Y, skewX + value.SkewX, skewY + value.SkewY, scaleX * value.ScaleX, scaleY * value.ScaleY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Transform Subtract(Transform value)
            => new Transform(x - value.X, y - value.Y, NormalizeRadian(skewX - value.SkewX), NormalizeRadian(skewY - value.SkewY), ScaleX / value.ScaleX, ScaleY / value.ScaleY);

        #endregion
    }
}
