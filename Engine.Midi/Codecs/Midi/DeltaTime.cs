﻿// <copyright file="DeltaTime.cs" company="Shkyrockett">
//     Copyright © 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <notes></notes>
// <references>
// </references>

namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    public class DeltaTime
    {
        /// <summary>
        /// 
        /// </summary>
        public short Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => Value.ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static DeltaTime Read(BinaryReaderExtended reader)
            => new DeltaTime()
            {
                Value = reader.ReadNetworkInt16()
            };
    }
}
