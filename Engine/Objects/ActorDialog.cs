// <copyright file="ActorDialog.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Dialog
    /// </summary>
    public class ActorDialog
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public ActorDialog()
        {
            DisplayName = "ActorDialog";
            Name = "ActorDialog";
            Text = new List<string>();
            TimeSyncPoints = new Dictionary<int, DateTimeOffset>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Text { get; set; }
    }
}
