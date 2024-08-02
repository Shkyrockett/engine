// <copyright file="FileItem.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.File;

/// <summary>
/// The file item class.
/// </summary>
public class FileItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileItem" /> class.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="data">The data.</param>
    public FileItem(string fileName, object data)
    {
        FileName = fileName;
        Data = data ?? throw new System.ArgumentNullException(nameof(data));
    }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public object Data { get; set; }
}
