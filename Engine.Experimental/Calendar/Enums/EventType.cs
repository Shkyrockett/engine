// <copyright file="EventType.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Chrono
{
    /// <summary>
    /// Calendar item event types.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Unknown event.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Someone's Birthday.
        /// </summary>
        Birthday,

        /// <summary>
        /// A Genaric Event.
        /// </summary>
        Event,

        /// <summary>
        /// A Holiday.
        /// </summary>
        Holiday,

        /// <summary>
        /// A General Notification.
        /// </summary>
        Notification
    }
}
