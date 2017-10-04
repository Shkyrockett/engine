﻿// <copyright file="IPen.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public interface IStroke
        : INotifyPropertyChanging, INotifyPropertyChanged
    {
        [NotifyParentProperty(true)]
        double Width { get; set; }

        [NotifyParentProperty(true)]
        double MiterLimit { get; set; }

        [NotifyParentProperty(true)]
        LineCapStyle StartCap { get; set; }

        [NotifyParentProperty(true)]
        LineCapStyle DashCap { get; set; }

        [NotifyParentProperty(true)]
        LineCapStyle EndCap { get; set; }

        [NotifyParentProperty(true)]
        LineDashStyle DashStyle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotifyParentProperty(true)]
        IFill Fill { get; set; }

    }
}