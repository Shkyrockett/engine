// <copyright file="GameElementListTypeConverter.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// The game element list type converter class.
    /// </summary>
    public class GameElementListTypeConverter
        : TypeConverter
    {
        /// <summary>
        /// Convert the to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destinationType">The destinationType.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            var gameElements = value as List<IGameElement>;
            if (gameElements is null)
            {
                return "-";
            }

            return string.Join(", ", gameElements.Select(m => m.Name));
        }

        /// <summary>
        /// Get the properties supported.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// Get the properties.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>The <see cref="PropertyDescriptorCollection"/>.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var list = new List<PropertyDescriptor>();
            if (value is List<IGameElement> gameElements)
            {
                foreach (IGameElement gameElement in gameElements)
                {
                    if (gameElement.Name != null)
                    {
                        list.Add(new GameElementDescriptor(gameElement, list.Count));
                    }
                }
            }

            return new PropertyDescriptorCollection(list.ToArray());
        }

        /// <summary>
        /// The game element descriptor class.
        /// </summary>
        private class GameElementDescriptor
            : SimplePropertyDescriptor
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GameElementDescriptor"/> class.
            /// </summary>
            /// <param name="gameElement">The gameElement.</param>
            /// <param name="index">The index.</param>
            public GameElementDescriptor(IGameElement gameElement, int index)
                : base(gameElement.GetType(), index.ToString(), typeof(string))
            {
                GameElement = gameElement;
            }

            /// <summary>
            /// Gets the game element.
            /// </summary>
            public IGameElement GameElement { get; }

            /// <summary>
            /// Get the value.
            /// </summary>
            /// <param name="component">The component.</param>
            /// <returns>The <see cref="object"/>.</returns>
            public override object GetValue(object component) => GameElement.Name;

            /// <summary>
            /// Set the value.
            /// </summary>
            /// <param name="component">The component.</param>
            /// <param name="value">The value.</param>
            public override void SetValue(object component, object value) => GameElement.Name = (string)value;
        }
    }
}
