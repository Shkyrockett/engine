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
        /// The language spoken.
        /// </summary>
        private Languages language;

        /// <summary>
        /// The nationality.
        /// </summary>
        private Countries country;

        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        public Culture()
        {
            country = 0;
            language = 0;
        }

        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        /// <param name="language">The language spoken.</param>
        /// <param name="country">The country of the culture.</param>
        public Culture(Languages language, Countries country)
        {
            this.country = country;
            this.language = language;
        }

        /// <summary>
        /// Initializes a now instance of the <see cref="Culture"/> class.
        /// </summary>
        /// <param name="code">A string representing a language-country culture code.</param>
        public Culture(string code)
        {
            string[] tokens = code.Split('-');
            Enum.TryParse<Languages>(tokens[0], out language);
            Enum.TryParse<Countries>(tokens[1], out country);
        }

        /// <summary>
        /// Gets or sets the language spoken in the culture.
        /// </summary>
        public Languages Language
        {
            get { return language; }
            set { language = value; }
        }

        /// <summary>
        /// Gets or sets the Country of the culture.
        /// </summary>
        public Countries Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Converts the value of this <see cref="Culture"/> instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}", language.ToString(), country.ToString());
        }
    }
}
