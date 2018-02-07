// <copyright file="ExpandableDictionaryPropertyDescriptor.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The expandable dictionary property descriptor class.
    /// </summary>
    /// <acknowledgment>
    /// https://stackoverflow.com/a/1928595/7004229
    /// </acknowledgment>
    public class ExpandableDictionaryPropertyDescriptor
        : PropertyDescriptor
    {
        #region Fields
        /// <summary>
        /// The dictionary.
        /// </summary>
        private IDictionary dictionary;

        /// <summary>
        /// The key.
        /// </summary>
        private object key;
        #endregion Fields

        #region Events
        /// <summary>
        /// The refresh required event of the <see cref="EventHandler"/>.
        /// </summary>
        internal event EventHandler RefreshRequired;
        #endregion Events

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandableDictionaryPropertyDescriptor"/> class.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="key">The key.</param>
        public ExpandableDictionaryPropertyDescriptor(IDictionary d, object key)
            : base(key.ToString(), null)
        {
            dictionary = d;
            this.key = key;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
            => key.ToString();

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public override bool IsReadOnly
            => false;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        public override bool SupportsChangeEvents
            => true;

        /// <summary>
        /// Gets the component type.
        /// </summary>
        public override Type ComponentType
            => dictionary.GetType();

        /// <summary>
        /// Gets the property type.
        /// </summary>
        public override Type PropertyType
            => dictionary[key].GetType();

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public override AttributeCollection Attributes
            => new AttributeCollection(null);
        #endregion Properties

        #region Methods
        /// <summary>
        /// The can reset value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool CanResetValue(object component)
            => false;

        /// <summary>
        /// Reset the value.
        /// </summary>
        /// <param name="component">The component.</param>
        public override void ResetValue(object component)
        { }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object GetValue(object component)
            => dictionary[key];

        /// <summary>
        /// Set the value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="value">The value.</param>
        public override void SetValue(object component, object value)
            => dictionary[key] = value;

        /// <summary>
        /// The should serialize value.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool ShouldSerializeValue(object component)
            => false;

        /// <summary>
        /// Get the display name.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetDisplayName(IDictionary dictionary, object key)
            //=> $"{CSharpName(dictionary[key].GetType())} [{key,4}]";
            => $"[{key}]";

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //private static string CSharpName(Type type)
        //{
        //    var name = type.Name;
        //    if (!type.IsGenericType)
        //        return name;
        //    return $"{name.Substring(0, name.IndexOf('`'))}<{string.Join(", ", type.GetGenericArguments().Select(CSharpName))}>";
        //}

        /// <summary>
        /// Raises the refresh required event.
        /// </summary>
        protected virtual void OnRefreshRequired()
            => RefreshRequired?.Invoke(this, EventArgs.Empty);
        #endregion Methods
    }
}
