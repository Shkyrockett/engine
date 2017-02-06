// <copyright file="PolynomialDegree.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public enum PolynomialDegree
        : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Constant = 0,

        /// <summary>
        /// 
        /// </summary>
        Linear = 1,

        /// <summary>
        /// 
        /// </summary>
        Quadratic = 2,

        /// <summary>
        /// 
        /// </summary>
        Cubic = 3,

        /// <summary>
        /// 
        /// </summary>
        Quartic = 4,

        /// <summary>
        /// 
        /// </summary>
        Quintic = 5,

        // The following two have alternates, but these are the most common. 

        /// <summary>
        /// 
        /// </summary>
        Sextic = 6,

        /// <summary>
        /// 
        /// </summary>
        Septic = 7,

        /// <summary>
        /// 
        /// </summary>
        Octic = 8,

        // The following two are not official. 

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </remarks>
        Nonic = 9,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </remarks>
        Decic = 10,

        // Note: Degrees beyond here may be wrong.

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Undecic = 11,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Duodecic = 12,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Tredecic = 13,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Quattuordecic = 14,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Quindecic = 15,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Sexdecic = 16,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Septendecic = 17,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Octodecic = 18,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Novendecic = 19,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Vigintic = 20,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Trigintic = 30,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Quadragintic = 40,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Quinquagintic = 50,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Sexagintic = 60,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Septuagintic = 70,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Numeral_prefix
        /// </remarks>
        Octogintic = 80,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </remarks>
        Hectic = 100
    }
}
