using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Engine.Objects
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

            List<IGameElement> gameElements = value as List<IGameElement>;
            if (gameElements == null)
                return "-";

            return string.Join(", ", gameElements.Select(m => m.Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            List<IGameElement> gameElements = value as List<IGameElement>;
            if (gameElements != null)
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

            public IGameElement GameElement { get; private set; }

            public override object GetValue(object component)
            {
                return GameElement.Name;
            }

            public override void SetValue(object component, object value)
            {
                GameElement.Name = (string)value;
            }
        }
    }
}
