// <copyright file="IStroke.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The IStroke interface.
    /// </summary>
    public interface IStroke
        : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        [NotifyParentProperty(true)]
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the miter limit.
        /// </summary>
        /// <value>The <see cref="double"/>.</value>
        [NotifyParentProperty(true)]
        double MiterLimit { get; set; }

        /// <summary>
        /// Gets or sets the start cap.
        /// </summary>
        /// <value>The <see cref="LineCapStyle"/>.</value>
        [NotifyParentProperty(true)]
        LineCapStyle StartCap { get; set; }

        /// <summary>
        /// Gets or sets the dash cap.
        /// </summary>
        /// <value>The <see cref="LineCapStyle"/>.</value>
        [NotifyParentProperty(true)]
        LineCapStyle DashCap { get; set; }

        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        /// <value>The <see cref="LineCapStyle"/>.</value>
        [NotifyParentProperty(true)]
        LineCapStyle EndCap { get; set; }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The <see cref="LineDashStyle"/>.</value>
        [NotifyParentProperty(true)]
        LineDashStyle DashStyle { get; set; }

        /// <summary>
        /// Gets or sets the fill.
        /// </summary>
        /// <value>The <see cref="IFillable"/>.</value>
        [NotifyParentProperty(true)]
        IFillable Fill { get; set; }
    }
}
