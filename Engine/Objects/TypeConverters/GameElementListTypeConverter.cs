// <copyright file="GameElementListTypeConverter.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class GameElementListTypeConverter
        : TypeConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
                return base.ConvertTo(context, culture, value, destinationType);

            var gameElements = value as List<IGameElement>;
            if (gameElements == null)
                return "-";

            return string.Join(", ", gameElements.Select(m => m.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var list = new List<PropertyDescriptor>();
            if (value is List<IGameElement> gameElements)
            {
                foreach (IGameElement gameElement in gameElements)
                {
                    if (gameElement.Name != null)
                        list.Add(new GameElementDescriptor(gameElement, list.Count));
                }
            }

            return new PropertyDescriptorCollection(list.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        private class GameElementDescriptor
            : SimplePropertyDescriptor
        {
            public GameElementDescriptor(IGameElement gameElement, int index)
                : base(gameElement.GetType(), index.ToString(), typeof(string))
            {
                GameElement = gameElement;
            }

            public IGameElement GameElement { get; }

            public override object GetValue(object component) => GameElement.Name;

            public override void SetValue(object component, object value)
            {
                GameElement.Name = (string)value;
            }
        }
    }
}
