// <copyright file="PolynomialDegree.cs" >
//     Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
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
    /// The degree of a polynomial or curve.
    /// </summary>
    public enum PolynomialDegree
        : sbyte
    {
        /// <summary>
        /// The Polynomial or curve is <see cref="Empty"/>.
        /// </summary>
        Empty = -1,

        /// <summary>
        /// The polynomial or curve is a <see cref="Constant"/> expression.
        /// </summary>
        Constant = 0,

        /// <summary>
        /// The polynomial or curve represents a line or <see cref="Linear"/> equation, having a single term.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// The polynomial or curve is <see cref="Quadratic"/>, having two terms.
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

        // The following two values of degree have alternates, but these are the most common. 

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

        // The following two values of degree are not official but have been proposed. 

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

        // Note: Degrees beyond here may be completely wrong.

        /// <summary>
        /// The polynomial or curve is of 11th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Undecic = 11,

        /// <summary>
        /// The polynomial or curve is of 12th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duodecic = 12,

        /// <summary>
        /// The polynomial or curve is of 13th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Tredecic = 13,

        /// <summary>
        /// The polynomial or curve is of 14th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuordecic = 14,

        /// <summary>
        /// The polynomial or curve is of 15th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quindecic = 15,

        /// <summary>
        /// The polynomial or curve is of 16th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexdecic = 16,

        /// <summary>
        /// The polynomial or curve is of 17th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septendecic = 17,

        /// <summary>
        /// The polynomial or curve is of 18th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octodecic = 18,

        /// <summary>
        /// The polynomial or curve is of 19th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novendecic = 19,

        /// <summary>
        /// The polynomial or curve is of 20th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Vigintic = 20,

        /// <summary>
        /// The polynomial or curve is of 21st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unvigintic = 21,

        /// <summary>
        /// The polynomial or curve is of 22nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duovigintic = 22,

        /// <summary>
        /// The polynomial or curve is of 23rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trevigintic = 23,

        /// <summary>
        /// The polynomial or curve is of 24th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuorvigintic = 24,

        /// <summary>
        /// The polynomial or curve is of 25th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinvigintic = 25,

        /// <summary>
        /// The polynomial or curve is of 26th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexvigintic = 26,

        /// <summary>
        /// The polynomial or curve is of 27th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septenvigintic = 27,

        /// <summary>
        /// The polynomial or curve is of 28th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octovigintic = 28,

        /// <summary>
        /// The polynomial or curve is of 29th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novenvigintic = 29,

        /// <summary>
        /// The polynomial or curve is of 30th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trigintic = 30,

        /// <summary>
        /// The polynomial or curve is of 31st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Untrigintic = 31,

        /// <summary>
        /// The polynomial or curve is of 32nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duotrigintic = 32,

        /// <summary>
        /// The polynomial or curve is of 33rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Tretrigintic = 33,

        /// <summary>
        /// The polynomial or curve is of 34th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuortrigintic = 34,

        /// <summary>
        /// The polynomial or curve is of 35th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quintrigintic = 35,

        /// <summary>
        /// The polynomial or curve is of 36th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sextrigintic = 36,

        /// <summary>
        /// The polynomial or curve is of 37th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septentrigintic = 37,

        /// <summary>
        /// The polynomial or curve is of 38th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octotrigintic = 38,

        /// <summary>
        /// The polynomial or curve is of 39th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Noventrigintic = 39,

        /// <summary>
        /// The polynomial or curve is of 40th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quadragintic = 40,

        /// <summary>
        /// The polynomial or curve is of 41st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unquadragintic = 41,

        /// <summary>
        /// The polynomial or curve is of 42nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duoquadragintic = 42,

        /// <summary>
        /// The polynomial or curve is of 43rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trequadragintic = 43,

        /// <summary>
        /// The polynomial or curve is of 44th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuorquadragintic = 44,

        /// <summary>
        /// The polynomial or curve is of 45th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinquadragintic = 45,

        /// <summary>
        /// The polynomial or curve is of 46th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexquadragintic = 46,

        /// <summary>
        /// The polynomial or curve is of 47th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septenquadragintic = 47,

        /// <summary>
        /// The polynomial or curve is of 48th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octoquadragintic = 48,

        /// <summary>
        /// The polynomial or curve is of 49th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novenquadragintic = 49,

        /// <summary>
        /// The polynomial or curve is of 50th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinquagintic = 50,

        /// <summary>
        /// The polynomial or curve is of 51st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unquinquagintic = 51,

        /// <summary>
        /// The polynomial or curve is of 52nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duoquinquagintic = 52,

        /// <summary>
        /// The polynomial or curve is of 53rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trequinquagintic = 53,

        /// <summary>
        /// The polynomial or curve is of 54th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuorquinquagintic = 54,

        /// <summary>
        /// The polynomial or curve is of 55th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinquinquagintic = 55,

        /// <summary>
        /// The polynomial or curve is of 56th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexquinquagintic = 56,

        /// <summary>
        /// The polynomial or curve is of 57th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septenquinquagintic = 57,

        /// <summary>
        /// The polynomial or curve is of 58th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octoquinquagintic = 58,

        /// <summary>
        /// The polynomial or curve is of 59th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novenquinquagintic = 59,

        /// <summary>
        /// The polynomial or curve is of 60th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexagintic = 60,

        /// <summary>
        /// The polynomial or curve is of 61st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unsexagintic = 61,

        /// <summary>
        /// The polynomial or curve is of 62nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duosexagintic = 62,

        /// <summary>
        /// The polynomial or curve is of 63rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Tresexagintic = 63,

        /// <summary>
        /// The polynomial or curve is of 64th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuorsexagintic = 64,

        /// <summary>
        /// The polynomial or curve is of 65th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinsexagintic = 65,

        /// <summary>
        /// The polynomial or curve is of 66th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexsexagintic = 66,

        /// <summary>
        /// The polynomial or curve is of 67th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septensexagintic = 67,

        /// <summary>
        /// The polynomial or curve is of 68th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octosexagintic = 68,

        /// <summary>
        /// The polynomial or curve is of 69th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novensexagintic = 69,

        /// <summary>
        /// The polynomial or curve is of 70th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septuagintic = 70,

        /// <summary>
        /// The polynomial or curve is of 71st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unseptuagintic = 71,

        /// <summary>
        /// The polynomial or curve is of 72nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duoseptuagintic = 72,

        /// <summary>
        /// The polynomial or curve is of 73rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Treseptuagintic = 73,

        /// <summary>
        /// The polynomial or curve is of 74th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuorseptuagintic = 74,

        /// <summary>
        /// The polynomial or curve is of 75th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinseptuagintic = 75,

        /// <summary>
        /// The polynomial or curve is of 76th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexseptuagintic = 76,

        /// <summary>
        /// The polynomial or curve is of 77th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septenseptuagintic = 77,

        /// <summary>
        /// The polynomial or curve is of 78th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octoseptuagintic = 78,

        /// <summary>
        /// The polynomial or curve is of 79th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novenseptuagintic = 79,

        /// <summary>
        /// The polynomial or curve is of 80th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octogintic = 80,

        /// <summary>
        /// The polynomial or curve is of 81st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unoctogintic = 81,

        /// <summary>
        /// The polynomial or curve is of 82nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duooctogintic = 82,

        /// <summary>
        /// The polynomial or curve is of 83rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Treoctogintic = 83,

        /// <summary>
        /// The polynomial or curve is of 84th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuoroctogintic = 84,

        /// <summary>
        /// The polynomial or curve is of 85th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinoctogintic = 85,

        /// <summary>
        /// The polynomial or curve is of 86th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexoctogintic = 86,

        /// <summary>
        /// The polynomial or curve is of 87th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septenoctogintic = 87,

        /// <summary>
        /// The polynomial or curve is of 88th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octooctogintic = 88,

        /// <summary>
        /// The polynomial or curve is of 89th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novenoctogintic = 89,

        /// <summary>
        /// The polynomial or curve is of 90th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Nonagintic = 90,

        /// <summary>
        /// The polynomial or curve is of 91st degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Unnonagintic = 91,

        /// <summary>
        /// The polynomial or curve is of 92nd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Duononagintic = 92,

        /// <summary>
        /// The polynomial or curve is of 93rd degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Trenonagintic = 93,

        /// <summary>
        /// The polynomial or curve is of 94th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quattuornonagintic = 94,

        /// <summary>
        /// The polynomial or curve is of 95th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Quinnonagintic = 95,

        /// <summary>
        /// The polynomial or curve is of 96th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Sexnonagintic = 96,

        /// <summary>
        /// The polynomial or curve is of 97th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Septennonagintic = 97,

        /// <summary>
        /// The polynomial or curve is of 98th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Octononagintic = 98,

        /// <summary>
        /// The polynomial or curve is of 99th degree.
        /// </summary>
        /// <acknowledgment>
        /// May be incorrect. See: https://en.wikipedia.org/wiki/Numeral_prefix
        /// </acknowledgment>
        Novennonagintic = 99,

        /// <summary>
        /// The polynomial or curve is <see cref="Hectic"/>, having one hundred terms.
        /// </summary>
        /// <acknowledgment>
        /// http://mathforum.org/library/drmath/view/56413.html
        /// </acknowledgment>
        Hectic = 100
    }
}
