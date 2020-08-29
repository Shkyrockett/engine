// <copyright file="AngleUnits.cs" company="Shkyrockett" >
//     Copyright © 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The planar angle units class. https://en.wikipedia.org/wiki/Conversion_of_units
    /// </summary>
    public static class AngleUnits
    {
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
        public const double RadiansInDegree = Radian;

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
        public const double DegreesInRadian = Degree;

        /// <summary>
        /// The degrees in degree (const). Value: 1d.
        /// </summary>
        public const double DegreesInDegree = 1d;
        #endregion Degrees Conversions
    }
}
