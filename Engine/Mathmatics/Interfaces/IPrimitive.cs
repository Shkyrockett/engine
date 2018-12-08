// <copyright file="IPrimitive.cs" company="Shkyrockett" >
//     Copyright © 2018 Shkyrockett. All rights reserved.
// </copyright> 
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine
{
    /// <summary>
    /// The base interface for all Primitive types used in shapes.
    /// </summary>
    public interface IPrimitive
        : IFormattable//, IComparable
    {
        // Uncomment the following once default interface methods become available.

        ///// <summary>
        ///// Creates a <see cref="string"/> representation of this <see cref="IPrimitive"/> struct based on the current culture.
        ///// </summary>
        ///// <returns>A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object.</returns>
        //string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        ///// <summary>
        ///// Creates a <see cref="string"/> representation of this <see cref="FormatableObject"/> struct based on the <see cref="IFormatProvider"/>
        ///// passed in.
        ///// If the provider is null, the CurrentCulture is used.
        ///// </summary>
        ///// <param name="provider">An object that supplies culture-specific formatting information.</param>
        ///// <returns>
        ///// A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object as specified by provider.
        ///// </returns>
        //string ToString(IFormatProvider provider) => ConvertToString(string.Empty /* format string */, provider /* format provider */);

        ///// <summary>
        ///// Creates a <see cref="string"/> representation of this <see cref="FormatableObject"/> struct based on the format <see cref="string"/>
        ///// and <see cref="IFormatProvider"/> passed in.
        ///// If the provider is null, the CurrentCulture is used.
        ///// </summary>
        ///// <param name="format">A numeric formating <see cref="string"/>.</param>
        ///// <param name="provider">An object that supplies culture-specific formatting information.</param>
        ///// <returns>
        ///// A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object as specified by format and provider.
        ///// </returns>
        //string ToString(string format, IFormatProvider provider) => ConvertToString(format /* format string */, provider /* format provider */);

        ///// <summary>
        ///// Creates a <see cref="string"/> representation of this <see cref="IPrimitive"/> object based on the format <see cref="string"/>
        ///// and <see cref="IFormatProvider"/> passed in.
        ///// If the provider is null, the CurrentCulture is used.
        ///// See the documentation for <see cref="IFormattable"/> for more information.
        ///// </summary>
        ///// <param name="format">A numeric formating <see cref="string"/>.</param>
        ///// <param name="provider">An object that supplies culture-specific formatting information.</param>
        ///// <returns>
        ///// A <see cref="string"/> representation of this instance of the <see cref="IPrimitive"/> object as specified by format and provider.
        ///// </returns>
        //string ConvertToString(string format, IFormatProvider provider) => base.ToString(format /* format string */, provider /* format provider */);
    }
}
