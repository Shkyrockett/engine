// <copyright file="Occasions.cs" company="Shkyrockett">
//     Copyright © Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">shkyrockett</author>
// <summary></summary>

using Engine.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Chrono
{
    /// <summary>
    /// Holidays and special occasions class.
    /// </summary>
    public class Occasions
    {
        /// <summary>
        /// The list of holidays.
        /// </summary>
        public List<Occasion> HolidayList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <remarks>
        /// http://www.kenhamady.com/form26.shtml
        /// http://www.kenhamady.com/form25b.shtml
        /// </remarks>
        public Occasions(int year)
        {
            HolidayList = new List<Occasion>
            {
                // - Floating holidays -
                Occasion.MartinLutherKingDay(year, Cultures.en_US),
                Occasion.PresidentsDay(year, Cultures.en_US),
                Occasion.MothersDay(year, Cultures.en_US),
                Occasion.MemorialDay(year, Cultures.en_US),
                Occasion.FathersDay(year, Cultures.en_US),
                Occasion.LaborDay(year, Cultures.en_US),
                Occasion.ColumbusDay(year),
                Occasion.ElectionDay(year, Cultures.en_US),
                Occasion.ThanksgivingDay(year, Cultures.en_US),
                Occasion.ThanksgivingDay(year, Cultures.en_CA),
                Occasion.ThanksgivingDay(year, Cultures.en_AU),
                Occasion.ThanksgivingDay(year, Cultures.en_GD),
                Occasion.ThanksgivingDay(year, Cultures.en_LR),
                Occasion.GoodFriday(year, Cultures.en_US),
                Occasion.EasterSunday(year, Cultures.en_US),
                Occasion.AscensionDay(year, Cultures.en_US),
                Occasion.FirstSundayOfAdvent(year, Cultures.en_US),
                Occasion.OrthodoxEaster(year, Cultures.en_US),
                Occasion.WhitSunday(year, Cultures.en_US),

                Occasion.DalightSavingsTimeBegins(year, Cultures.en_US),
                Occasion.DalightSavingsTimeEnds(year, Cultures.en_US),
                Occasion.TaxDay(year, Cultures.en_US),
                Occasion.FlagDay(year, Cultures.en_US),
                Occasion.IndependenceDay(year, Cultures.en_US),

                // - Local occasions -
                Occasion.BellevueStrawberryDaysStart(year, Cultures.en_US),
                Occasion.BellevueStrawberryDaysEnd(year, Cultures.en_US),
                Occasion.BrighamCityPeachDaysStart(year, Cultures.en_US),
                Occasion.BrighamCityPeachDaysEnd(year, Cultures.en_US),
                Occasion.PioneerDay(year, Cultures.en_US),

                // - Static holidays -
                Occasion.NewYearsDay(year, Cultures.en_US),
                Occasion.GroundhogDay(year, Cultures.en_US),
                Occasion.ValentinesDay(year, Cultures.en_US),
                Occasion.SaintPatricksDay(year, Cultures.en_US),
                Occasion.EuropeanLaborDay(year, Cultures.en_US),
                Occasion.CincoDeMayo(year, Cultures.en_US),
                Occasion.PatriotDay(year, Cultures.en_US),
                Occasion.Halloween(year, Cultures.en_US),
                Occasion.GuyFawkesDay(year, Cultures.en_US),
                Occasion.AllSaintsDay(year, Cultures.en_US),
                Occasion.VeteransDay(year, Cultures.en_US),
                Occasion.ChristmasEve(year, Cultures.en_US),
                Occasion.ChristmasDay(year, Cultures.en_US),
                Occasion.BoxingDay(year, Cultures.en_US),
                Occasion.NewYearsEve(year, Cultures.en_US),
                Occasion.ElvisPresleysBirthday(year),

                new Occasion(
                    "Pi Day",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Event,
                    new DateTime(year, (int)Months.March, 14),
                    "Day to celebrate the irrational number Pi on 3/14."),

                new Occasion(
                    "Programmer's Day",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Event,
                    new DateTime(year, (int)Months.January, 1).AddDays(255),
                    "Day to celebrate Programmers."),

                //Holiday.VernalEquinoxNorthernHemisphere(year, Cultures.en_US),
                //Holiday.SummerSolsticeNorthernHemisphere(year, Cultures.en_US),
                //Holiday.AutumnalEquinoxNorthernHemisphere(year, Cultures.en_US),
                //Holiday.WinterSolsticeNorthernHemisphere(year, Cultures.en_US),

                // - Birthdays -

                Occasion.ElvisPresleysBirthday(year),
                (year <=  1706)? null : new Occasion(
                    "Benjamin Franklin's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.January, 17),
                    "The great early American scientist, thinker and statesman. Benjamin Franklin was Born in Boston MA on January 17th, 1706."),
                (year <=  1882)? null : new Occasion(
                    "Franklin Delano Roosevelt's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.January, 30),
                    "The 32nd president of the United States. Franklin Delano Roosevelt was born in New York on January 30th, 1882."),
                (year <=  1847)? null : new Occasion(
                    "Thomas Alva Edison's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.February, 11),
                    "The inventor and entrepreneur credited with the invention of the electric light bulb and the phonograph. Thomas Alva Edison was born in Milan Ohio on February 11th, 1847."),
                Occasion.AbrahamLincolnsBirthday(year),
                Occasion.GeorgeWashingtonsBirthday(year),
                (year <=  1743)? null : new Occasion(
                    "Thomas Jefferson's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.April, 13),
                    "Author of the Declaration of Independence, and a two term president (1801-09). Thomas Jefferson was born at the family home in a one and a half story farmhouse not far from the Virginia wilderness, on April 13, 1743 (April 2, 1743 OS)."),
                (year <=  1955)? null : new Occasion(
                    "Albert Einstein's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.March, 14),
                    "The great German-born theoretical physicist.Albert Einstein was Born in Ulm, in the Kingdom of Württemberg in the German Empire, on March 14th 1879."),
                (year <=  1884)? null : new Occasion(
                    "Harry S Truman's Birthday",
                    Cultures.en_US,
                    OccasionDateType.AnnualDate,
                    EventType.Birthday,
                    new DateTime(year, (int)Months.May, 8),
                    "The 33rd president of the U.S. (1945-1953). Harry S Truman presided during the Cold War and Korean War. Known for the New Deal economic policies. He was born in Lamar MO on May 8tth, 1884."),

                // - Microsoft Update dates -

                new Occasion(
                    "January Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.January, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of January."),
                new Occasion(
                    "February Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.February, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of February."),
                new Occasion(
                    "March Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.March, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of March."),
                new Occasion(
                    "April Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.April, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of April."),
                new Occasion(
                    "May Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.May, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "Second Tuesday of May."),
                new Occasion(
                    "June Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.June, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of the Month."),
                new Occasion(
                    "July Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.July, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of June."),
                new Occasion(
                    "August Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.August, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of August."),
                new Occasion(
                    "September Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.September, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of September."),
                new Occasion(
                    "October Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.October, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of October."),
                new Occasion(
                    "November Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of November."),
                new Occasion(
                    "December Patch Tuesday",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.December, 1).FirstInstanceWeekdayOfMonth(2,DayOfWeek.Tuesday),
                    "The Second Tuesday of December."),

                // - Algorithm tests -

                new Occasion(
                    "Test 1",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(1, DayOfWeek.Sunday),
                    "The First Sunday of November"),
                new Occasion(
                    "Test 2",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(2, DayOfWeek.Monday),
                    "The Second Monday of November"),
                new Occasion(
                    "Test 3",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth(3, DayOfWeek.Tuesday),
                    "The Third Tuesday of November"),
                new Occasion(
                    "Test 4",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).FirstInstanceWeekdayOfMonth02(4, DayOfWeek.Wednesday),
                    "The Fourth Wednesday of November"),

                new Occasion(
                    "Last Test 1",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).LastInstanceWeekdayOfMonth(1, DayOfWeek.Sunday),
                    "The last Sunday of November"),
                new Occasion(
                    "Last Test 2",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).LastInstanceWeekdayOfMonth(2, DayOfWeek.Monday),
                    "The Second from last Monday of November"),
                new Occasion(
                    "Last Test 3",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).LastInstanceWeekdayOfMonth(3, DayOfWeek.Tuesday),
                    "The Third from last Tuesday of November"),
                new Occasion(
                    "Last Test 4",
                    Cultures.en_US,
                    OccasionDateType.DynamicAnnualDate,
                    EventType.Notification,
                    new DateTime(year, (int)Months.November, 1).LastInstanceWeekdayOfMonth(4, DayOfWeek.Wednesday),
                    "The Fourth from last Wednesday of November")
            };
            HolidayList.RemoveAll(a => a == null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        /// <remarks>http://solidcoding.blogspot.com/2007/11/c30-extension-method-to-check-holidays.html</remarks>
        public (bool, List<Occasion>) IsHoliday(DateTime currentDate)
        {
            var occasions = new List<Occasion>(
                from h in HolidayList
                where h.Date.Month == currentDate.Month
                && h.Date.Day == currentDate.Day
                select h
                );

            if (occasions.Count > 0) return (true, occasions);

            return (false, null);
        }
    }
}
