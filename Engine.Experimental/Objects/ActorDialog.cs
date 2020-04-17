// <copyright file="ActorDialog.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
    /// The actor dialog class.
    /// </summary>
    public class ActorDialog
        : IGameElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActorDialog"/> class.
        /// </summary>
        public ActorDialog()
        {
            DisplayName = nameof(ActorDialog);
            Name = nameof(ActorDialog);
            Text = new List<string>();
            TimeSyncPoints = new Dictionary<int, DateTimeOffset>();
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the time sync points.
        /// </summary>
        public Dictionary<int, DateTimeOffset> TimeSyncPoints { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public List<string> Text { get; set; }
    }
}
