// <copyright file="Occasion.cs" company="Shkyrockett">
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
        private string name;

        /// <summary>
        /// 
        /// </summary>
        private Culture culture;

        /// <summary>
        /// 
        /// </summary>
        private OccasionDateType dateType;

        /// <summary>
        /// 
        /// </summary>
        private EventType eventType;

        /// <summary>
        /// 
        /// </summary>
        private DateTime date;

        /// <summary>
        /// 
        /// </summary>
        private string description;

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
            this.name = name;
            this.culture = culture;
            this.dateType = dateType;
            this.date = date;
            this.description = description;
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
            this.name = name;
            this.culture = culture;
            this.dateType = dateType;
            this.eventType = eventType;
            this.date = date.Value;
            this.description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Culture Culture
        {
            get { return culture; }
            set { culture = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public OccasionDateType DateType
        {
            get { return dateType; }
            set { dateType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public EventType EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
