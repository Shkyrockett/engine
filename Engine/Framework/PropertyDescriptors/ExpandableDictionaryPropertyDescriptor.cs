// <copyright file="ExpandableDictionaryPropertyDescriptor.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <acknowledgment>
    /// https://stackoverflow.com/a/1928595/7004229
    /// </acknowledgment>
    public class ExpandableDictionaryPropertyDescriptor
        : PropertyDescriptor
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private IDictionary dictionary;

        /// <summary>
        /// 
        /// </summary>
        private object key;

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        internal event EventHandler RefreshRequired;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="key"></param>
        public ExpandableDictionaryPropertyDescriptor(IDictionary d, object key)
            : base(key.ToString(), null)
        {
            dictionary = d;
            this.key = key;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override string Name
            => key.ToString();

        /// <summary>
        /// 
        /// </summary>
        public override bool IsReadOnly
            => false;

        /// <summary>
        /// 
        /// </summary>
        public override bool SupportsChangeEvents
            => true;

        /// <summary>
        /// 
        /// </summary>
        public override Type ComponentType
            => dictionary.GetType();

        /// <summary>
        /// 
        /// </summary>
        public override Type PropertyType
            => dictionary[key].GetType();

        /// <summary>
        /// 
        /// </summary>
        public override AttributeCollection Attributes
            => new AttributeCollection(null);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
            => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public override void ResetValue(object component)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override object GetValue(object component)
            => dictionary[key];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
            => dictionary[key] = value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
            => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        protected virtual void OnRefreshRequired()
            => RefreshRequired?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}
