// <copyright file="AngleUnits.cs" company="Shkyrockett" >
//     Copyright © 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using static System.Math;

namespace NewEngine
{
    /// <summary>
    /// The planiar angle units class. https://en.wikipedia.org/wiki/Conversion_of_units
    /// </summary>
    public static class AngleUnits
    {
        /// <summary>
        /// Represents the inverse of Pi, or the quotient of one over pi.
        /// </summary>
        public const double InversePi = 1d / PI; // 0.31830988618379067153776752674503d;

        /// <summary>
        /// Represents the inverse of Tau, or the quotient of one over 2 pi.
        /// </summary>
        public const double InverseTau = 1d / Tau; // 0.15915494309189533576888376337251d;

        /// <summary>
        /// Represents the value of the double inverse of Pi, or the quotient of two over pi.
        /// </summary>
        public const double Inverse2OverPi = 2d / PI; // 0.63661977236758134307553505349006d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first sixteenth of that circle.
        /// One sixteenth Tau or a eighth Pi.
        /// </summary>
        /// <remarks><para>PI / 8</para></remarks>
        public const double EighthPi = 0.125d * PI; // 0.39269908169872415480783042290994d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first eighth of that circle.
        /// One eighth Tau or a quarter Pi. A 45 degree angle.
        /// </summary>
        /// <remarks><para>PI / 4</para></remarks>
        public const double Quart = 0.25d * PI; // 0.78539816339744830961566084581988d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the first quarter of that circle.
        /// One quarter Tau or half Pi. A right angle in mathematics.
        /// </summary>
        /// <remarks><para>PI / 2</para></remarks>
        public const double HalfPi = 0.5d * PI; // 1.5707963267948966192313216916398d;

        ///// <summary>
        ///// Represents the ratio of the circumference of a circle to its diameter, specified
        ///// by the constant, π (Pi).
        ///// One half Tau or One Pi.
        ///// </summary>
        ///// <value>≈3.1415926535897931...</value>
        //public const double Pi = Math.PI; // 3.1415926535897932384626433832795d;

        /// <summary>
        /// Represents the ratio of the radius of a circle to the third quarter of that circle.
        /// Three quarter tau, or one and a half pi.
        /// </summary>
        /// <remarks>
        /// <para>Three quarter tau, or one and a half pi are just too long and awkward.
        /// Randal Munro's joke "compromise" works well enough for a name: http://xkcd.com/1292/</para>
        /// </remarks>
        /// <acknowledgment>
        /// Randal Munro http://xkcd.com/1292/ 
        /// </acknowledgment>
        public const double Pau = 1.5d * PI; // 4.7123889803846898576939650749193d;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its radius, specified
        /// by the proposed constant, τ (Tau).
        /// One Tau or two Pi.
        /// </summary>
        /// <value>≈6.28318...</value>
        public const double Tau = 2d * PI; // 6.283185307179586476925286766559d;

        #region Radians Conversions
        /// <summary>
        /// The radians in angular mil (const). Value: 2d * Math.PI / 6400d.
        /// </summary>
        public const double RadiansInAngularMil = Tau / 6400d;

        /// <summary>
        /// The radians in arcminute (const). Value: RadiansInDegree / 60d.
        /// </summary>
        public const double RadiansInArcminute = RadiansInDegree / 60d;

        /// <summary>
        /// The radians in arc second (const). Value: RadiansInDegree / 3600d.
        /// </summary>
        public const double RadiansInArcSecond = RadiansInDegree / 3600d;

        /// <summary>
        /// The radians in centesimal arc minute (const). Value: RadiansInGradian * (.
        /// </summary>
        public const double RadiansInCentesimalArcMinute = RadiansInGradian * 0.01d;

        /// <summary>
        /// The radians in centesimal arc second (const). Value: RadiansInGradian * (1d / 10000d).
        /// </summary>
        public const double RadiansInCentesimalArcSecond = RadiansInGradian * 0.0001d;

        /// <summary>
        /// The radians in degree (const). Value: Math.PI / 180d.
        /// </summary>
        public const double RadiansInDegree = PI / 180d;

        /// <summary>
        /// The radians in gradian (const). Value: Math.PI / 200d.
        /// </summary>
        public const double RadiansInGradian = PI / 200d;

        /// <summary>
        /// The radians in octant (const). Value: RadiansInDegree * 45d.
        /// </summary>
        public const double RadiansInOctant = RadiansInDegree * 45d;

        /// <summary>
        /// The radians in quadrant (const). Value: RadiansInDegree * 90d.
        /// </summary>
        public const double RadiansInQuadrant = RadiansInDegree * 90d;

        /// <summary>
        /// The radians in radian (const). Value: 1.
        /// </summary>
        public const double RadiansInRadian = 1;

        /// <summary>
        /// The radians in sextant (const). Value: RadiansInDegree * 60d.
        /// </summary>
        public const double RadiansInSextant = RadiansInDegree * 60d;

        /// <summary>
        /// The radians in sign (const). Value: RadiansInDegree * 30d.
        /// </summary>
        public const double RadiansInSign = RadiansInDegree * 30d;
        #endregion Radians Conversions

        #region Degrees Conversions
        /// <summary>
        /// The degrees in radian (const). Value: 180d / Math.PI.
        /// </summary>
        public const double DegreesInRadian = 180d / PI;

        /// <summary>
        /// The degrees in degree (const). Value: 1d.
        /// </summary>
        public const double DegreesInDegree = 1d;
        #endregion Degrees Conversions
    }
}
