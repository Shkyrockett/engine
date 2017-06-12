// <copyright file="Occasion.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public partial class Occasion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <param name="dateType"></param>
        /// <param name="date"></param>
        /// <param name="description"></param>
        public Occasion(string name, Culture culture, OccasionDateType dateType, DateTime date, string description)
        {
            Name = name;
            Culture = culture;
            DateType = dateType;
            Date = date;
            Description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <param name="dateType"></param>
        /// <param name="eventType"></param>
        /// <param name="date"></param>
        /// <param name="description"></param>
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
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Culture Culture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OccasionDateType DateType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventType EventType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
    }
}
