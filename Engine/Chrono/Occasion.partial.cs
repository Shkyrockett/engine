using Engine.Localization;
using System;
using static System.Math;

namespace Engine.Chrono
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Occasion
    {
        // - Floating holidays -

        /// <summary>
        /// Daylight Savings Time Begins
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Tax Day.</returns>
        /// <remarks>
        /// Daylight Savings Time begins at 2:00 a.m. on the second Sunday of March. http://www.nist.gov/pml/div688/dst.cfm
        /// </remarks>
        public static Occasion DalightSavingsTimeBegins(int year, Culture culture)
        {
            switch (culture.Country)
            {
                case Countries.CA:
                case Countries.US:
                    if (year >= 1918 && year <= 1920)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.March, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the last Sunday of March.");
                    }
                    else if (year >= 1921 && year <= 1941)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.April, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the last Sunday of April.");
                    }
                    else if (year == 1942)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.February, 9, 2, 0, 0),
                                "Daylight Savings Time begins at 2:00 a.m. on February 9th.");
                    }
                    else if (year >= 1943 && year <= 1945)
                    {
                        return null;
                    }
                    else if (year > 1945 && year <= 1973)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.April, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the last Sunday of April.");
                    }
                    else if (year == 1974)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.January, 6, 2, 0, 0),
                                "Daylight Savings Time begins at 2:00 a.m. on January 6th.");
                    }
                    else if (year == 1975)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.February, 23, 2, 0, 0),
                                "Daylight Savings Time begins at 2:00 a.m. on February 23rd.");
                    }
                    else if (year >= 1976 && year <= 1986)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.April, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the last Sunday of April.");
                    }
                    else if (year >= 1987 && year < 2007)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.April, 1, 2, 0, 0).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the 1st Sunday of April.");
                    }
                    else if (year >= 2007)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.March, 1, 2, 0, 0).FirstInstanceWeekdayOfMonth(2, DayOfWeek.Sunday),
                                "Daylight Savings Time begins at 2:00 a.m. on the second Sunday of March.");
                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Daylight Savings Time Ends
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Tax Day.</returns>
        /// <remarks>
        /// Daylight Savings Time ends at 2:00 a.m. on the first Sunday of November. http://www.nist.gov/pml/div688/dst.cfm
        /// By act of Congress, civil clocks in most areas of the United States are adjusted ahead one hour in the summer months (known as daylight time) and returned back one hour in the winter months (known as standard time). The dates marking the beginning and end of daylight time have changed as Congress has passed new statutes. As of 2007, daylight time begins in the United States on the second Sunday in March and ends on the first Sunday in November. On the second Sunday in March, clocks are set ahead one hour at 2:00 a.m. local standard time (which becomes 3:00 a.m. local daylight time). On the first Sunday in November, clocks are set back one hour at 2:00 a.m. local daylight time (which becomes 1:00 a.m. local standard time). These dates were established by Congress in the Energy Policy Act of 2005, Pub. L. no. 109-58, 119 Stat 594 (2005).
        /// Not all places in the U.S.observe daylight time. In particular, Hawaii and most of Arizona do not use it.The most recent change to local daylight time policy was in 2006, when Indiana adopted the use of daylight time state-wide.
        /// </remarks>
        public static Occasion DalightSavingsTimeEnds(int year, Culture culture)
        {
            switch (culture.Country)
            {
                case Countries.CA:
                case Countries.US:
                    if (year >= 1918 && year <= 1921)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.October, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time ends at 2:00 a.m. on the last Sunday of October");
                    }
                    else if (year >= 1921 && year <= 1941)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.September, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time ends at 2:00 a.m. on the last Sunday of September");
                    }
                    else if (year >= 1942 && year < 1945)
                    {
                        return null;
                    }
                    else if (year >= 1945 && year <= 1945)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.September, 1, 2, 0, 0).LastWeekdayOfMonth(DayOfWeek.Sunday),
                                "Daylight Savings Time ends at 2:00 a.m. on the last Sunday of September");
                    }
                    else if (year >= 1955 && year <= 2007)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.November, 1, 2, 0, 0).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Sunday),
                                "Daylight Savings Time ends at 2:00 a.m. on the Last Sunday of October.");
                    }
                    else if (year >= 2007)
                    {
                        return new Occasion(
                                "Daylight Savings Time Begins",
                                culture,
                                OccasionDateType.DynamicAnnualDate,
                                EventType.Notification,
                                new DateTime(year, (int)Months.November, 1, 2, 0, 0).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Sunday),
                                "Daylight Savings Time ends at 2:00 a.m. on the first Sunday of November");
                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Martin Luther King Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Martin Luther King Day.</returns>
        /// <remarks>
        /// The Third Monday of January.
        /// </remarks>
        public static Occasion MartinLutherKingDay(int year, Culture culture) => new Occasion(
        "Martin Luther King Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.January, 1).FirstInstanceWeekdayOfMonth(3, DayOfWeek.Monday),
        "The Third Monday of January.");

        /// <summary>
        /// Presidents Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Presidents Day.</returns>
        /// <remarks>
        /// The Third Monday of February.
        /// </remarks>
        public static Occasion PresidentsDay(int year, Culture culture) => new Occasion(
        "Presidents Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.February, 1).FirstInstanceWeekdayOfMonth(3, DayOfWeek.Monday),
        "The Third Monday of February.");

        /// <summary>
        /// Mother's Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Mother's Day.</returns>
        /// <remarks>
        /// The Second Sunday of May.
        /// </remarks>
        public static Occasion MothersDay(int year, Culture culture) => new Occasion(
        "Mother's Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.May, 1).FirstInstanceWeekdayOfMonth(2, DayOfWeek.Sunday),
        "The Second Sunday of May.");

        /// <summary>
        /// Memorial Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Memorial Day.</returns>
        /// <remarks>
        /// The last Monday of May. http://en.wikipedia.org/wiki/Memorial_Day.
        /// </remarks>
        public static Occasion MemorialDay(int year, Culture culture) => new Occasion(
        "Memorial Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.May, 1).LastInstanceWeekdayOfMonth(1, DayOfWeek.Monday),
        "The last Monday of May.");

        /// <summary>
        /// Father's Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Father's Day.</returns>
        /// <remarks>
        /// The Third Sunday of June. http://en.wikipedia.org/wiki/Father%27s_Day.
        /// </remarks>
        public static Occasion FathersDay(int year, Culture culture) => new Occasion(
        "Father's Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.June, 1).FirstInstanceWeekdayOfMonth(3, DayOfWeek.Sunday),
        "The Third Sunday of June.");

        /// <summary>
        /// Labor Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Labor Day.</returns>
        /// <remarks>
        /// The 1st Monday of September.
        /// </remarks>
        public static Occasion LaborDay(int year, Culture culture) => new Occasion(
        "Labor Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.September, 1).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Monday),
        "The 1st Monday of September.");

        /// <summary>
        /// Columbus Day
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Columbus Day.</returns>
        public static Occasion ColumbusDay(int year) => new Occasion(
        "Columbus Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.October, 1).FirstInstanceWeekdayOfMonth(2, DayOfWeek.Monday),
        "The 2nd Monday of Oct."
        );

        /// <summary>
        /// Election Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Election Day.</returns>
        /// <remarks>
        /// The Tuesday after First Monday of Nov.
        /// </remarks>
        public static Occasion ElectionDay(int year, Culture culture) => new Occasion(
        "Election Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
        EventType.Holiday,
        new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Monday).AddDays(1),
        "The Tuesday after First Monday of Nov.");

        /// <summary>
        /// Thanksgiving Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Thanksgiving Day.</returns>
        /// <remarks>
        /// http://en.wikipedia.org/wiki/Thanksgiving
        /// http://en.wikipedia.org/wiki/Thanksgiving_(Canada)
        /// </remarks>
        public static Occasion ThanksgivingDay(int year, Culture culture)
        {
            switch (culture.Country)
            {
                case Countries.AU:
                    return new Occasion(
                        "Thanksgiving Day (Norfolk Island Australia)",
                        culture,
                        OccasionDateType.DynamicAnnualDate,
                        EventType.Holiday,
                        new DateTime(year, (int)Months.November, 1).LastInstanceWeekdayOfMonth(1, DayOfWeek.Wednesday),
                        "The Last Wednesday in November.");
                case Countries.CA:
                    return new Occasion(
                        "Thanksgiving Day (Canada)",
                        culture,
                        OccasionDateType.DynamicAnnualDate,
                        EventType.Holiday,
                        new DateTime(year, (int)Months.October, 1).FirstInstanceWeekdayOfMonth(2, DayOfWeek.Thursday),
                        "The Second Monday in October.");
                case Countries.GD:
                    return new Occasion(
                        "Thanksgiving Day (Grenada)",
                        culture,
                        OccasionDateType.AnnualDate,
                        EventType.Holiday,
                        new DateTime(year, (int)Months.October, 25),
                        "October Twenty Fifth.");
                case Countries.LR:
                    return new Occasion(
                        "Thanksgiving Day (Liberia)",
                        culture,
                        OccasionDateType.DynamicAnnualDate,
                        EventType.Holiday,
                        new DateTime(year, (int)Months.October, 1).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Thursday),
                        "The First Thursday in November.");
                case Countries.US:
                default:
                    return new Occasion(
                       "Thanksgiving Day (USA)",
                       culture,
                       OccasionDateType.DynamicAnnualDate,
                       EventType.Holiday,
                       new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(4, DayOfWeek.Thursday),
                       "The Fourth Thursday in November.");
            }
        }

        // - Christian Floating holidays -

        /// <summary>
        /// Good Friday
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Good Friday.</returns>
        /// <remarks>
        /// The Friday before Easter. http://en.wikipedia.org/wiki/Good_Friday
        /// </remarks>
        public static Occasion GoodFriday(int year, Culture culture) => new Occasion(
    "Good Friday",
    culture,
    OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
    GoodFriday(year),
    "The Friday before Easter.");

        /// <summary>
        /// Easter Sunday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Easter Sunday.</returns>
        public static Occasion EasterSunday(int year, Culture culture) => new Occasion(
    "Easter Sunday",
    culture,
    OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
    EasterSunday(year),
    "Easter Sunday...");

        /// <summary>
        /// Incorrect implementation of Orthodox Easter Sunday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Orthodox Easter Sunday.</returns>
        //new Holiday("Orthodox Easter Day",
        //    "en-US",
        //    new DateTime(year, 0, 0),
        //    "http://en.wikipedia.org/wiki/Computus"),

        public static Occasion OrthodoxEaster(int year, Culture culture) => new Occasion(
            "Orthodox Easter Sunday",
            culture,
            OccasionDateType.DynamicAnnualDate,
            EventType.Holiday,
            EasterSunday(year),
            "Orthodox Easter Sunday...");

        /// <summary>
        /// Ascension Day is 10 days before Whit Sunday, or 39 days after Easter.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Ascension Day.</returns>
        public static Occasion AscensionDay(int year, Culture culture) => new Occasion(
    "Ascension Day",
    culture,
    OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
    AscensionDay(year),
    "Ascension Day is 10 days before Whit Sunday, or 39 days after Easter.");

        /// <summary>
        /// Whit Sunday or the festival of Pentecost is 7 weeks after Easter Sunday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Whit Sunday or the festival of Pentecost.</returns>
        public static Occasion WhitSunday(int year, Culture culture) => new Occasion(
    "Whit Sunday",
    culture,
    OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
    WhitSunday(year),
    "Whit Sunday or the festival of Pentecost is 7 weeks after Easter Sunday.");

        /// <summary>
        /// First Sunday of Advent.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for First Sunday of Advent.</returns>
        public static Occasion FirstSundayOfAdvent(int year, Culture culture) => new Occasion(
    "First Sunday of Advent",
    culture,
    OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
    FirstSundayOfAdvent(year),
    "The first Sunday of Advent is the 4th Sunday before Christmas.");

        // - Semi-Static holidays -

        /// <summary>
        /// Tax Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Tax Day.</returns>
        /// <remarks>
        /// Nearest business day to April Fifteenth. http://en.wikipedia.org/wiki/Tax_Day.
        /// </remarks>
        public static Occasion TaxDay(int year, Culture culture) => new Occasion(
        "Tax Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Notification,
        new DateTime(year, (int)Months.April, 15).EnsureWeekday(),
        "Nearest business day to April Fifteenth.");

        /// <summary>
        /// Flag Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Flag Day.</returns>
        /// <remarks>
        /// Nearest business day to June Fourteenth. http://en.wikipedia.org/wiki/Flag_Day_%28United_States%29.
        /// </remarks>
        public static Occasion FlagDay(int year, Culture culture) => new Occasion(
        "Flag Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.June, 14).EnsureWeekday(),
        "Nearest business day to June Fourteenth.");

        /// <summary>
        /// Independence Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Independence Day.</returns>
        /// <remarks>
        /// Nearest business day to The Fourth of July. http://en.wikipedia.org/wiki/Independence_Day_(United_States).
        /// </remarks>
        public static Occasion IndependenceDay(int year, Culture culture) => new Occasion(
        "Independence Day",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.July, 4),
        "Nearest business day to The Fourth of July.");

        // - Birthdays -

        /// <summary>
        /// Abraham Lincoln's Birthday
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Abraham Lincoln's Birthday.</returns>
        /// <remarks>
        /// The Twelfth of February. http://en.wikipedia.org/wiki/Lincoln%27s_Birthday
        /// </remarks>
        public static Occasion AbrahamLincolnsBirthday(int year, Culture culture = null) => (year <= 1732) ? null :
    new Occasion(
            "Abraham Lincoln's Birthday",
            new Culture(Languages.en, Countries.US),
            OccasionDateType.AnnualDate,
    EventType.Birthday,
            new DateTime(year, (int)Months.February, 12),
            "The 16th president of the United states who presided during the Civil War. Abraham Lincoln was born in Kentucky on February 12th, 1809.");

        /// <summary>
        /// George Washington's Birthday
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Abraham Lincoln's Birthday.</returns>
        /// <remarks>
        /// The Twenty second of February. http://en.wikipedia.org/wiki/George_Washington%27s_Birthday
        /// </remarks>
        public static Occasion GeorgeWashingtonsBirthday(int year, Culture culture = null) => (year <= 1732) ? null :
    new Occasion(
            "George Washington's Birthday",
            new Culture(Languages.en, Countries.US),
            OccasionDateType.AnnualDate,
    EventType.Birthday,
            new DateTime(year, (int)Months.February, 22),
            "The first president of the United States and commander in chief of the Continental army during the American Revolution. George Washington was born on the Pope's Creek Estate near present-day Colonial Beach in Westmore land County, Virginia; on February 22nd, 1732.");

        /// <summary>
        /// Elvis Presley's Birthday
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Abraham Lincoln's Birthday.</returns>
        /// <remarks></remarks>
        public static Occasion ElvisPresleysBirthday(int year, Culture culture = null) => (year <= 1935) ? null :
    new Occasion(
        "Elvis Presley's Birthday",
        Cultures.en_US,
        OccasionDateType.AnnualDate,
    EventType.Birthday,
        new DateTime(year, (int)Months.January, 8),
        "Often referred to as the king of rock and roll. Elvis Aaron Presley was born in Tupelo MS on January 8th, 1935.");

        // - Static holidays -

        /// <summary>
        /// New Year's Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for New Year's Day.</returns>
        /// <remarks>
        /// The First day of January. http://en.wikipedia.org/wiki/New_Year%27s_Day
        /// </remarks>
        public static Occasion NewYearsDay(int year, Culture culture) => new Occasion(
        "New Year's Day",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.January, 1),
        "The first day of January.");

        /// <summary>
        /// Groundhog Day
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Groundhog Day.</returns>
        /// <remarks>
        /// The Second day of February. http://en.wikipedia.org/wiki/Groundhog_Day
        /// </remarks>
        public static Occasion GroundhogDay(int year, Culture culture = null) => new Occasion(
        "Groundhog Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.February, 2),
        "The Second day of February.");

        /// <summary>
        /// Saint Valentine's Day
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Valentine's Day.</returns>
        /// <remarks>
        /// February Fourteenth. http://en.wikipedia.org/wiki/Valentine%27s_Day
        /// </remarks>
        public static Occasion ValentinesDay(int year, Culture culture = null) => new Occasion(
        "Saint Valentine's Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.February, 14),
        "February Fourteenth.");

        /// <summary>
        /// Saint Patrick's Day
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Patrick's Day.</returns>
        /// <remarks>
        /// The Seventeenth of March. http://en.wikipedia.org/wiki/Saint_Patrick%27s_Day
        /// </remarks>
        public static Occasion SaintPatricksDay(int year, Culture culture = null) => new Occasion(
        "Saint Patrick's Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.March, 17),
        "The Seventeenth of March.");

        /// <summary>
        /// European Labor Day, also known as May day, is equivalent to U.S.Labor day.Day to honor workers. Particularly observed by communist nations.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Patrick's Day.</returns>
        /// <remarks>
        /// </remarks>
        public static Occasion EuropeanLaborDay(int year, Culture culture = null) => new Occasion(
        "European Labor Day (May day)",
        new Culture(Languages.en, Countries.GB),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.May, 1),
        "The First of May.");

        /// <summary>
        /// Cinco De Mayo.
        /// This day commemorates a major victory by the outnumbered and underarmed Mexican army, against the occupying French, on May 5th of 1862. "Cinco de Mayo" is Spanish for May 5th.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Patrick's Day.</returns>
        /// <remarks>
        /// </remarks>
        public static Occasion CincoDeMayo(int year, Culture culture = null) => new Occasion(
        "Cinco De Mayo",
        new Culture(Languages.es, Countries.MX),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.May, 5),
        "The Fifth of May.");

        /// <summary>
        /// Patriot Day.
        /// Day to honor those who were killed in the terrorist attacks on the U.S. on this date in 2001. The attack using four hijacked airliners destroyed the twin World Trade Center buildings in New York city (each over 100 stories tall) and severely damaged the Pentagon in Washington DC. Over three thousand people in the buildings as well as hundreds of rescue workers and airliner passengers died early that Tuesday morning. This holiday was signed into public law (#107-89) on 2001.12.18 and is observed by flying flags at half-staff.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Patrick's Day.</returns>
        /// <remarks>
        /// </remarks>
        public static Occasion PatriotDay(int year, Culture culture = null) => new Occasion(
        "Patriot Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.September, 11),
        "September 11th.");

        /// <summary>
        /// Guy Fawkes Day.
        /// Anniversary of the Gunpowder Plot of 1605 to blow up the English king and parliament.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Saint Patrick's Day.</returns>
        /// <remarks>
        /// </remarks>
        public static Occasion GuyFawkesDay(int year, Culture culture = null) => new Occasion(
        "Guy Fawkes Day",
        new Culture(Languages.en, Countries.GB),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.November, 5),
        "Remember, remember the Fifth of November.");

        /// <summary>
        /// Halloween Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Halloween Day.</returns>
        /// <remarks>
        /// October Thirty First. http://en.wikipedia.org/wiki/Halloween.
        /// </remarks>
        public static Occasion Halloween(int year, Culture culture = null) => new Occasion(
        "Halloween",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.October, 31),
        "October Thirty First.");

        /// <summary>
        /// All Saints' Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for All Saints' Day.</returns>
        /// <remarks>
        /// November First. http://en.wikipedia.org/wiki/All_Saints%27_Day.
        /// </remarks>
        public static Occasion AllSaintsDay(int year, Culture culture) => new Occasion(
        "All Saints' Day",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.November, 1),
        "November First.");

        /// <summary>
        /// Veterans Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Veterans Day.</returns>
        /// <remarks>
        /// November Eleventh. http://en.wikipedia.org/wiki/Veterans_Day.
        /// </remarks>
        public static Occasion VeteransDay(int year, Culture culture) => new Occasion(
        "Veterans Day",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.November, 11),
        "November Eleventh.");

        /// <summary>
        /// Christmas Eve.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Christmas Eve.</returns>
        /// <remarks>
        /// The Twenty Fourth of December, or Day Before Christmas. http://en.wikipedia.org/wiki/Christmas_Eve.
        /// </remarks>
        public static Occasion ChristmasEve(int year, Culture culture) => new Occasion(
        "Christmas Eve",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.December, 24),
        "The Twenty Fourth of December, or Day Before Christmas.");

        /// <summary>
        /// Christmas Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Christmas Day.</returns>
        /// <remarks>
        /// The Twenty Fifth of December. http://en.wikipedia.org/wiki/Christmas
        /// </remarks>
        public static Occasion ChristmasDay(int year, Culture culture) => new Occasion(
        "Christmas Day",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.December, 25),
        "The Twenty Fifth of December.");

        /// <summary>
        /// Boxing Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Boxing Day.</returns>
        /// <remarks>
        /// The Twenty Sixth of December, or The Day after Christmas. http://en.wikipedia.org/wiki/Boxing_Day
        /// </remarks>
        public static Occasion BoxingDay(int year, Culture culture) => new Occasion(
        "Boxing Day",
        culture,
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.December, 26),
        "The Twenty Sixth of December, or The Day after Christmas.");

        /// <summary>
        /// New Year's Eve.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for New Year's Eve.</returns>
        /// <remarks>
        /// The last day of the Year. http://en.wikipedia.org/wiki/New_Year%27s_Eve
        /// </remarks>
        public static Occasion NewYearsEve(int year, Culture culture = null) => new Occasion(
        "New Year's Eve",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.December, 31),
        "The last day of the Year.");

        // - Regional Floating holidays -

        /// <summary>
        /// Strawberry Days Starts.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Strawberry Days Starts.</returns>
        /// <remarks>
        /// The First Thursday of September.
        /// </remarks>
        public static Occasion BellevueStrawberryDaysStart(int year, Culture culture = null) => new Occasion(
        "Strawberry Days Starts",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.June, 1).LastWeekdayOfMonth(DayOfWeek.Sunday).BeforeSunday(),
        "The Saturday before the last Sunday of June.");

        /// <summary>
        /// Strawberry Days Ends.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Strawberry Days Ends.</returns>
        /// <remarks>
        /// The Saturday following the first Thursday of September.
        /// </remarks>
        public static Occasion BellevueStrawberryDaysEnd(int year, Culture culture = null) => new Occasion(
        "Strawberry Days Ends",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.June, 1).LastWeekdayOfMonth(DayOfWeek.Sunday),
        "The last Sunday of June.");

        /// <summary>
        /// Peach Days Starts.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Peach Days Starts.</returns>
        /// <remarks>
        /// The First Thursday of September.
        /// </remarks>
        public static Occasion BrighamCityPeachDaysStart(int year, Culture culture = null) => new Occasion(
        "Peach Days Starts",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.September, 1).NextDayOfWeek(DayOfWeek.Thursday),
        "The First Thursday of September.");

        /// <summary>
        /// Peach Days Ends.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Peach Days Ends.</returns>
        /// <remarks>
        /// The Saturday following the first Thursday of September.
        /// </remarks>
        public static Occasion BrighamCityPeachDaysEnd(int year, Culture culture = null) => new Occasion(
        "Peach Days Ends",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.September, 1).NextDayOfWeek(DayOfWeek.Thursday).AddDays(2),
        "The Saturday following the first Thursday of September.");

        /// <summary>
        /// Pioneer Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns>Returns a <see cref="Occasion"/> class representing the date and other information for Pioneer Day.</returns>
        /// <remarks>
        /// The Twenty Fourth of July.
        /// </remarks>
        public static Occasion PioneerDay(int year, Culture culture = null) => new Occasion(
        "Pioneer Day",
        new Culture(Languages.en, Countries.US),
        OccasionDateType.AnnualDate,
    EventType.Holiday,
        new DateTime(year, (int)Months.July, 24),
        "The Twenty Fourth of July.");

        // - Season Dates - 

        /// <summary>
        /// Vernal Equinox Northern Hemisphere
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns></returns>
        public static Occasion VernalEquinoxNorthernHemisphere(int year, Culture culture) => new Occasion(
        "Vernal Equinox",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        CalculateEquinoxSolsticeDate(year, Season.Spring, culture),
        "Vernal Equinox for the Northern Hemisphere.");

        /// <summary>
        /// Summer Solstice Northern Hemisphere
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns></returns>
        public static Occasion SummerSolsticeNorthernHemisphere(int year, Culture culture) => new Occasion(
        "Summer Solstice",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        CalculateEquinoxSolsticeDate(year, Season.Summer, culture),
        "Summer Solstice for the Northern Hemisphere.");

        /// <summary>
        /// Autumnal Equinox Northern Hemisphere
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns></returns>
        public static Occasion AutumnalEquinoxNorthernHemisphere(int year, Culture culture) => new Occasion(
        "Autumnal Equinox",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        CalculateEquinoxSolsticeDate(year, Season.Autumn, culture),
        "Autumnal Equinox for the Northern Hemisphere.");

        /// <summary>
        /// Winter Solstice Northern Hemisphere
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <param name="culture">The Language and Country culture codes for the occasion region.</param>
        /// <returns></returns>
        public static Occasion WinterSolsticeNorthernHemisphere(int year, Culture culture) => new Occasion(
        "Winter Solstice",
        culture,
        OccasionDateType.DynamicAnnualDate,
    EventType.Holiday,
        CalculateEquinoxSolsticeDate(year, Season.Winter, culture),
        "Winter Solstice for the Northern Hemisphere.");

        // - Season Helpers -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="season"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private static DateTime CalculateEquinoxSolsticeDate(int year, Season season, Culture culture)
        {
            float p = (year - 2000f) / 1000f;
            float val = 0f;

            switch (season)
            {
                case Season.Spring:
                    val = (float)(2451623.80984 + 365242.37404 * p + 0.05169 * p * p - 0.00411 * p * p * p - 0.00057 * p * p * p * p);
                    break;
                case Season.Summer:
                    val = (float)(2451716.56767 + 365241.62603 * p + 0.00325 * p * p + 0.00888 * p * p * p - 0.00030 * p * p * p * p);
                    break;
                case Season.Autumn:
                    val = (float)(2451810.21715 + 365242.01767 * p - 0.11575 * p * p + 0.00337 * p * p * p + 0.00078 * p * p * p * p);
                    break;
                case Season.Winter:
                    val = (float)(2451900.05952 + 365242.74049 * p - 0.06223 * p * p - 0.00823 * p * p * p + 0.00032 * p * p * p * p);
                    break;
            }
            //  Convert astronomical JDN to chronological
            val += 0.5f;

            //// Pope Gregory XIII's decree
            //// last day to use Julian calendar
            //float LASTJULDATE = 15821004f;

            // jdn of same
            float LASTJULJDN = 2299160f;

            // British-American usage
            if (culture.Language == Languages.en && (culture.Country == Countries.US || culture.Country == Countries.GB))
            {
                //// last day to use Julian calendar
                //LASTJULDATE = 17520902f;

                // jdn of same
                LASTJULJDN = 2361221f;
            }

            float daysPer400Years = 146097f;
            float fudgedDaysPer4000Years = 1460970f + 31f;

            var jdn = (float)Floor(val);
            float ut = val - jdn;
            bool julian = (jdn <= LASTJULJDN);
            float x = jdn + 68569f;

            if (julian)
            {
                x += 38f;
                daysPer400Years = 146100f;
                fudgedDaysPer4000Years = 1461000f + 1;
            }

            float z = 4f * x / daysPer400Years;
            x -= (daysPer400Years * z + 3f) / 4f;
            double y = 4000f * (x + 1f) / fudgedDaysPer4000Years;
            x = (float)(x - 1461f * y / 4f + 31f);
            float month = 80f * x / 2447f;
            float day = x - 2447f * month / 80f;
            x = month / 11f;
            month = month + 2f - 12f * x;
            y = 100f * (z - 49f) + y + x;

            // adjust BC years
            if ((int)y <= 0)
                y--;

            var hour = (int)(ut * 24);
            var minute = (int)((ut * 24 - hour) * 60);  //  Accurate to about 15 minutes c. 2000 CE.
            var second = (int)((((ut * 24 - hour) * 60) - minute) * 60);

            return new DateTime((int)y, (int)month, (int)day, hour, minute, second);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ofSouthernHemisphere"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/1579587/how-can-i-get-the-current-season-using-net-summer-winter-etc</remarks>
        private Season getSeason(DateTime date, bool ofSouthernHemisphere)
        {
            int hemisphereConst = (ofSouthernHemisphere ? 2 : 0);
            Func<Season, Season> getReturn = (northern)
                => (Season)(((int)northern + hemisphereConst) % 4);

            float value = date.Month + date.Day / 100f;  // <month>.<day(2 digit)>
            if (value < 3.21 || value >= 12.22) return getReturn(Season.Winter);    // 3: Winter
            if (value < 6.21) return getReturn(Season.Spring);  // 0: Spring
            if (value < 9.23) return getReturn(Season.Summer);  // 1: Summer
            return getReturn(Season.Autumn);    // 2: Autumn
        }

        // - Date Helpers -

        /// <summary>
        /// Good Friday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Good Friday.</returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year
        /// </remarks>
        private static DateTime GoodFriday(int year) => EasterSunday(year).AddDays(-2);

        /// <summary>
        /// Good Friday.
        /// </summary>
        /// <param name="year">The year for which to determine the date of Good Friday.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Good Friday.</returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year
        /// </remarks>
        private static DateTime GoodFriday01(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int i = c / 4;
            int k = c % 4;
            int g = (8 * b + 13) / 25;
            int h = ((19 * a) + b - d - g + 15) % 30;
            int l = ((2 * e) + (2 * i) - k + 32 - h) % 7;
            int m = (a + (11 * h) + (19 * l)) / 433;

            int days_to_good_friday = h + l - (7 * m) - 2;

            int month = (days_to_good_friday + 90) / 25;
            int day = (days_to_good_friday + (33 * month) + 19) % 32;
            return new DateTime(year, month, day);
        }

        /// <summary>
        ///  Easter Sunday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Easter Sunday.</returns>
        /// <remarks>
        /// http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        /// http://aa.usno.navy.mil/faq/docs/easter.php
        /// http://aa.usno.navy.mil/data/docs/easter.php
        /// http://aa.usno.navy.mil/faq/docs/easter.php#compute
        /// http://aa.usno.navy.mil/faq/docs/calendars.php#Gregorian
        /// http://aa.usno.navy.mil/faq/docs/calendars.php
        /// http://en.wikipedia.org/wiki/Good_Friday#Calculating_the_date
        /// http://www.kenhamady.com/form25.shtml
        /// http://www.assa.org.au/edm#Calculator
        /// http://mb-soft.com/believe/txx/easter01.htm
        /// </remarks>
        private static DateTime EasterSunday(int year)
        {
            int month = 0;
            int day = 0;
            int g = year % 19;
            int c = year / 100;
            int h = h = (c - c / 4 - (8 * c + 13) / 25
                                                + 19 * g + 15) % 30;
            int i = h - h / 28 * (1 - h / 28 *
                        (29 / (h + 1)) * ((21 - g) / 11));

            day = i - ((year + year / 4 +
                          i + 2 - c + c / 4) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

        /// <summary>
        ///  Easter Sunday.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Easter Sunday.</returns>
        /// <remarks>
        /// http://www.codeproject.com/Articles/1595/Calculating-Easter-Sunday
        /// </remarks>
        private static DateTime EasterSunday01(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
            int i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));

            day = i - ((year + year / 4 + i + 2 - c + c / 4) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(month, day, year);
        }

        /// <summary>
        /// Easter Sunday.
        /// </summary>
        /// <param name="year">The year for which to determine the date of Easter Sunday.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Easter Sunday.</returns>
        /// <remarks>
        /// http://www.geekpedia.com/code68_Find-Easter-Sunday-of-any-year.html
        /// </remarks>
        private static DateTime EasterSunday02(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int L = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * L) / 451;

            int month = (h + L - 7 * m + 114) / 31;
            int day = ((h + L - 7 * m + 114) % 31) + 1;
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Ascension Day.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Ascension Day.</returns>
        /// <remarks>
        /// http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        /// </remarks>
        private static DateTime AscensionDay(int year) => EasterSunday(year).AddDays(39);

        /// <summary>
        /// WhitSunday or the festival of Pentecost.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the date of Whit Sunday.</returns>
        /// <remarks>
        /// http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        /// </remarks>
        private static DateTime WhitSunday(int year) => EasterSunday(year).AddDays(49);

        /// <summary>
        /// First Sunday of Advent.
        /// </summary>
        /// <param name="year">The year to look up.</param>
        /// <returns>Returns a <see cref="DateTime"/> struct representing the First Sunday of Advent.</returns>
        /// <remarks>
        /// http://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        /// </remarks>
        private static DateTime FirstSundayOfAdvent(int year)
        {
            int weeks = 4;
            int correction = 0;
            var christmas = new DateTime(year, 12, 25);

            if (christmas.DayOfWeek != DayOfWeek.Sunday)
            {
                weeks--;
                correction = ((int)christmas.DayOfWeek - (int)DayOfWeek.Sunday);
            }

            return christmas.AddDays(-1 * ((weeks * 7) + correction));
        }
    }
}
