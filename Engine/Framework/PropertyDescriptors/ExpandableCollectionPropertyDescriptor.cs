// <copyright file="ExpandableCollectionPropertyDescriptor.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/32582504/propertygrid-expandable-collection
    /// </acknowledgment>
    public class ExpandableCollectionPropertyDescriptor
        : PropertyDescriptor
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private IList collection;

        /// <summary>
        /// 
        /// </summary>
        private readonly int index = -1;

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
        /// <param name="coll"></param>
        /// <param name="idx"></param>
        public ExpandableCollectionPropertyDescriptor(IList coll, int idx)
            : base(GetDisplayName(coll, idx), null)
        {
            collection = coll;
            index = idx;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override string Name
            => index.ToString(CultureInfo.InvariantCulture);

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
            => collection.GetType();

        /// <summary>
        /// 
        /// </summary>
        public override Type PropertyType
            => collection[index].GetType();

        ///// <summary>
        ///// 
        ///// </summary>
        //public override AttributeCollection Attributes
        //    => new AttributeCollection(null);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool CanResetValue(object component)
            => true;

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
        {
            OnRefreshRequired();
            return collection[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public override void SetValue(object component, object value)
            => collection[index] = value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override bool ShouldSerializeValue(object component)
            => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetDisplayName(IList list, int index)
            //=> $"{CSharpName(list[index].GetType())} [{index,4}]";
            => $"[{index}]";

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
