// <copyright file="DateTimeExtentions.cs" company="Shyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Chrono
{
    /// <summary>
    /// Extensions for Date and Time operations.
    /// </summary>
    public static class DateTimeExtentions
    {
        /// <summary>
        /// The same day, at midnight
        /// </summary>
        /// <example>
        /// DateTime startOfDay = DateTime.Now.AtMidnight();
        /// </example>
        /// <param name="date">Start date</param>
        /// <returns>Returns the same day, at midnight</returns>
        public static DateTime AtMidnight(this DateTime date)
            => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

        /// <summary>
        /// The same day, at midday
        /// </summary>
        /// <example>
        /// DateTime startOfAfternoon = DateTime.Now.AtMidday();
        /// </example>
        /// <param name="date">Start date</param>
        /// <returns>Returns the same day, at midday</returns>
        public static DateTime AtMidday(this DateTime date)
            => new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);

        /// <summary>
        /// If the date falls on a weekend, adjust to the nearest business day.
        /// </summary>
        /// <param name="date">The date to find the Weekday from.</param>
        /// <returns></returns>
        public static DateTime EnsureWeekday(this DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Saturday => date.AddDays(-1),
                DayOfWeek.Sunday => date.AddDays(1),
                _ => date,
            };
        }

        /// <summary>
        /// If the date falls on a Sunday, adjust to the nearest Saturday day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime BeforeSunday(this DateTime date)
        {
            return date.DayOfWeek switch
            {
                DayOfWeek.Sunday => date.AddDays(-1),
                _ => date,
            };
        }

        /// <summary>
        /// Returns the next date which falls on the given day of the week
        /// </summary>
        /// <example>
        /// DateTime nextTuesday = DateTime.Now.NextDayOfWeek(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="date">Start date</param>
        /// <param name="weekday">The required day of week</param>
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek weekday)
        {
            var offsetDays = weekday - date.DayOfWeek;
            return date.AddDays(offsetDays > 0 ? offsetDays : offsetDays + 7);
        }

        /// <summary>
        /// Returns the next date which falls on the given day of the week
        /// </summary>
        /// <example>
        /// DateTime nextTuesday = DateTime.Now.NextDayOfWeek(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="date">Start date</param>
        /// <param name="weekday">The required day of week</param>
        public static DateTime NextDayOfWeek02(this DateTime date, DayOfWeek weekday)
        {
            var offsetDays = weekday - date.DayOfWeek;

            // return offsetDays > 0 ? date.AddDays(offsetDays) : date;
            return date.AddDays(offsetDays);
        }

        /// <summary>
        /// Returns the first day of the month
        /// </summary>
        /// <param name="date">Start date</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, 1).AtMidnight();

        /// <summary>
        /// Returns the first day of the month
        /// </summary>
        /// <example>
        /// DateTime firstOfThisMonth = DateTime.Now.FirstOfMonth;
        /// </example>
        /// <param name="date">Start date</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth02(this DateTime date)
            => date.AddDays(1 - date.Day).AtMidnight();

        /// <summary>
        /// Returns the first specified day of the week in the current month
        /// </summary>
        /// <example>
        /// DateTime firstTuesday = DateTime.Now.FirstDayOfMonth(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="date">Start date</param>
        /// <param name="weekday">The required day of week</param>
        /// <returns></returns>
        public static DateTime FirstWeekdayOfMonth(this DateTime date, DayOfWeek weekday)
        {
            var firstDayOfMonth = date.FirstDayOfMonth02();
            return (firstDayOfMonth.DayOfWeek == weekday ? firstDayOfMonth :
                    firstDayOfMonth.NextDayOfWeek(weekday)).AtMidnight();
        }

        /// <summary>
        /// The last day of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        public static DateTime LastDayOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).AtMidnight();

        /// <summary>
        /// Returns the last day in the current month
        /// </summary>
        /// <example>
        /// DateTime endOfMonth = DateTime.Now.LastDayOfMonth();
        /// </example>
        /// <param name="date" />Start date
        /// <returns />
        public static DateTime LastDayOfMonth02(this DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return date.FirstDayOfMonth02().AddDays(daysInMonth - 1).AtMidnight();
        }

        /// <summary>
        /// The weekday count in month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="weekday">The weekday.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int WeekdayCountInMonth(this DateTime date, DayOfWeek weekday)
            => ((LastInstanceWeekdayOfMonth(date, weekday).Day - FirstInstanceWeekdayOfMonth(date, weekday).Day) / 7) + 1;

        /// <summary>
        /// Returns the last specified day of the week in the current month
        /// </summary>
        /// <example>
        /// DateTime finalTuesday = DateTime.Now.LastDayOfMonth(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="date" />Start date
        /// <param name="weekday" />The required day of week
        /// <returns />
        public static DateTime LastWeekdayOfMonth(this DateTime date, DayOfWeek weekday)
        {
            var lastDayOfMonth = date.LastDayOfMonth();
            return lastDayOfMonth.AddDays(lastDayOfMonth.DayOfWeek < weekday ?
                    weekday - lastDayOfMonth.DayOfWeek - 7 :
                    weekday - lastDayOfMonth.DayOfWeek);
        }

        /// <summary>
        /// The first instance weekday of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="weekday">The weekday.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        /// <exception cref="ArgumentException">Instance count must be greater than zero</exception>
        /// <exception cref="ArgumentException">Instance range exceeded, max: </exception>
        public static DateTime FirstInstanceWeekdayOfMonth(this DateTime date, int instance, DayOfWeek weekday)
        {
            if (instance <= 0)
            {
                throw new ArgumentException("Instance count must be greater than zero", nameof(instance));
            }

            DateTime returnDate;
            var firstDay = FirstInstanceWeekdayOfMonth(date, weekday);
            var instancesInMonth = WeekdayCountInMonth(date, weekday);
            if (instance <= instancesInMonth)
            {
                var padDays = 7 * (instance - 1);
                returnDate = new DateTime(date.Year, date.Month, firstDay.Day + padDays);
            }
            else
            {
                throw new ArgumentException("Instance range exceeded, max: " + instancesInMonth, nameof(instance));
            }

            return returnDate;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/6140018/how-to-calculate-2nd-friday-of-month-in-c-sharp
        /// </summary>
        /// <param name="date"></param>
        /// <param name="instance"></param>
        /// <param name="weekday"></param>
        /// <returns></returns>
        public static DateTime FirstInstanceWeekdayOfMonth01(this DateTime date, int instance, DayOfWeek weekday)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var fOc = firstDay.DayOfWeek == weekday ? firstDay : firstDay.AddDays(weekday - firstDay.DayOfWeek);
            // CurDate = 2011.10.1 Occurrence = 1, Day = Friday >> 2011.09.30 FIX.
            if (fOc.Month < date.Month)
            {
                instance += 1;
            }

            return fOc.AddDays(7 * (instance - 1));
        }

        /// <summary>
        /// Find the Nth occurrence of a weekday.
        /// </summary>
        /// <param name="date">The date to find the nth day of the week for.</param>
        /// <param name="instance">The number of x weekdays to find</param>
        /// <param name="weekday">The day of the week for look for.s</param>
        /// <returns>A nullable <see cref="DateTime"/> representing the date requested.</returns>
        /// <remarks><para>Always check for null first, then use the null coalescing operator "??" to retrieve the value.</para></remarks>
        /// <example>
        /// <see cref="DateTime"/> test =  NthWeekDayOfMonth(Now, 2, )
        /// </example>
        public static DateTime FirstInstanceWeekdayOfMonth02(this DateTime date, int instance, DayOfWeek weekday)
        {
            var test = FirstDayOfMonth(date);
            test = test.NextDayOfWeek02(weekday);
            test = test.AddDays(7 * (instance - 1));
            return test;
        }

        /// <summary>
        /// The first instance weekday of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="weekday">The weekday.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        public static DateTime FirstInstanceWeekdayOfMonth(this DateTime date, DayOfWeek weekday)
        {
            var dtFirstDay = new DateTime(date.Year, date.Month, 1);
            dtFirstDay = weekday < dtFirstDay.DayOfWeek ? dtFirstDay.AddDays(weekday - dtFirstDay.DayOfWeek + 7) : dtFirstDay.AddDays(weekday - dtFirstDay.DayOfWeek);

            return dtFirstDay;
        }

        /// <summary>
        /// The last instance weekday of month.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="weekday">The weekday.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        public static DateTime LastInstanceWeekdayOfMonth(this DateTime date, DayOfWeek weekday)
        {
            var dtLastDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            dtLastDay = weekday > dtLastDay.DayOfWeek ? dtLastDay.AddDays(weekday - dtLastDay.DayOfWeek - 7) : dtLastDay.AddDays(weekday - dtLastDay.DayOfWeek);

            return dtLastDay;
        }

        /// <summary>
        /// Find the Nth occurrence of a weekday from the end of the month.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="occurance"></param>
        /// <param name="weekday"></param>
        /// <returns></returns>
        public static DateTime LastInstanceWeekdayOfMonth(this DateTime date, int occurance, DayOfWeek weekday)
            => LastDayOfMonth(date).NextDayOfWeek(weekday).AddDays(-(7 * occurance));
    }
}
