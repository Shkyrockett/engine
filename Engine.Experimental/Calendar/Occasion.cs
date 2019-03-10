// <copyright file="Occasion.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.Localization;
using System;

namespace Engine.Chrono
{
    /// <summary>
    /// The occasion class.
    /// </summary>
    public partial class Occasion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Occasion"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="dateType">The dateType.</param>
        /// <param name="date">The date.</param>
        /// <param name="description">The description.</param>
        public Occasion(string name, Culture culture, OccasionDateType dateType, DateTime date, string description)
        {
            Name = name;
            Culture = culture;
            DateType = dateType;
            Date = date;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Occasion"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="dateType">The dateType.</param>
        /// <param name="eventType">The eventType.</param>
        /// <param name="date">The date.</param>
        /// <param name="description">The description.</param>
        public Occasion(string name, Culture culture, OccasionDateType dateType, EventType eventType, DateTime? date, string description)
        {
            Name = name;
            Culture = culture;
            DateType = dateType;
            EventType = eventType;
            Date = date.Value;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        public Culture Culture { get; set; }

        /// <summary>
        /// Gets or sets the date type.
        /// </summary>
        public OccasionDateType DateType { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
