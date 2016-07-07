﻿// <copyright file="Occasion.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">shkyrockett</author>
// <summary></summary>

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