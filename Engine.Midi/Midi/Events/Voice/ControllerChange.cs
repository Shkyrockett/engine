// <copyright file="ControllerChange.cs" company="Shkyrockett">
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.Midi
{
    /// <summary>
    /// Controller/Mode Change Status.
    /// </summary>
    /// <remarks>
    /// nB 0ccccccc 0vvvvvvv
    /// This message is sent when a controller value changes. 
    /// Controllers include devices such as pedals and levers. 
    /// Controller numbers 120-127 are reserved as "Channel Mode Messages" 
    /// (below). (ccccccc) is the controller number (0-119). 
    /// (vvvvvvv) is the controller value (0-127).
    /// </remarks>
    [ElementName(nameof(ControllerChange))]
    [DisplayName("Controller Change")]
    public class ControllerChange
        : EventStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="value"></param>
        /// <param name="status"></param>
        public ControllerChange(byte controller, byte value, EventStatus status)
            : base(status.DeltaTime, status.Status, status.Channel)
        {
            Controller = controller;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        internal static ControllerChange Read(MidiBinaryReader reader, EventStatus status)
            => new ControllerChange(reader.ReadByte(), reader.ReadByte(), status);
    }
}
