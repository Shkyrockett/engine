// <copyright file="PolynomialDegree.cs" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
        : sbyte
    {
        /// <summary>
        /// The Polynomial or curve is <see cref="Empty"/>.
        /// </summary>
        Empty =-1,

        /// <summary>
        /// The polynomial or curve is a <see cref="Constant"/> expression.
        /// </summary>
        Constant = 0,

        /// <summary>
        /// The polynomial or curve represents a line or <see cref="Linear"/> equation, having a single term.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// The polynomial or curve is <see cref="Quadragintic"/>, having two terms.
        /// </summary>
        Quadratic = 2,

        /// <summary>
        /// The polynomial or curve is <see cref="Cubic"/>, having three terms.
        /// </summary>
        Cubic = 3,

        /// <summary>
        /// The polynomial or curve is <see cref="Quartic"/>, having four terms.
        /// </summary>
        Quartic = 4,

        /// <summary>
        /// The polynomial or curve is <see cref="Quintic"/>, having five terms.
        /// </summary>
        Quintic = 5,

        // The following two have alternates, but these are the most common. 

        /// <summary>
        /// The polynomial or curve is <see cref="Sextic"/>, or Hexic having six terms.
        /// </summary>
        Sextic = 6,

        /// <summary>
        /// The polynomial or curve is <see cref="Septic"/>, or Heptic having seven terms.
        /// </summary>
        Septic = 7,

        /// <summary>
        /// The polynomial or curve is <see cref="Octic"/>, having eight terms.
        /// </summary>
        Octic = 8,

        // The following two are not official. 

        /// <summary>
        /// The polynomial or curve is <see cref="Nonic"/>, having nine terms.
        /// </summary>
        /// <acknowledgment>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </acknowledgment>
        Nonic = 9,

        /// <summary>
        /// The polynomial or curve is <see cref="Decic"/>, having ten terms.
        /// </summary>
        /// <acknowledgment>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </acknowledgment>
        Decic = 10,

        // Note: Degrees beyond here may be wrong.

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Undecic = 11,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duodecic = 12,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Tredecic = 13,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuordecic = 14,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quindecic = 15,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexdecic = 16,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septendecic = 17,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octodecic = 18,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novendecic = 19,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Vigintic = 20,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trigintic = 30,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quadragintic = 40,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinquagintic = 50,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexagintic = 60,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septuagintic = 70,

        /// <summary>
        /// To be documented.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octogintic = 80,

        /// <summary>
        /// The polynomial or curve is <see cref="Hectic"/>, having one hundred terms.
        /// </summary>
        /// <acknowledgment>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </acknowledgment>
        Hectic = 100
    }
}
