// <copyright file="Culture.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">shkyrockett</author>
// <summary></summary>

using System;

namespace Engine.Localization
{
    /// <summary>
    /// Cultural Country Language class.
    /// </summary>
    public class Culture
    {
        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        public Culture()
        {
            Country = 0;
            Language = 0;
        }

        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        /// <param name="language">The language spoken.</param>
        /// <param name="country">The country of the culture.</param>
        public Culture(Languages language, Countries country)
        {
            Country = country;
            Language = language;
        }

        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        /// <param name="code">A string representing a language-country culture code.</param>
        public Culture(string code)
        {
            string[] tokens = code.Split('-');
            Languages language;
            Countries country;
            Enum.TryParse(tokens[0], out language);
            Enum.TryParse(tokens[1], out country);
            Language = language;
            Country = country;
        }

        /// <summary>
        /// Gets or sets the language spoken in the culture.
        /// </summary>
        public Languages Language { get; set; }

        /// <summary>
        /// Gets or sets the Country of the culture.
        /// </summary>
        public Countries Country { get; set; }

        /// <summary>
        /// Converts the value of this <see cref="Culture"/> instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString() => $"{Language}-{Country}";
    }
}
