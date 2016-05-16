// <copyright file="GraphicItem.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphicItem
    {
        #region Private properties

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        private Dictionary<string, object> propertyCache = new Dictionary<string, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="style"></param>
        /// <param name="metadata"></param>
        public GraphicItem(GraphicsObject item, IStyle style, Metadata metadata = null)
        {
            Item = item;
            Style = style;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public GraphicsObject Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IStyle Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Metadata Metadata { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Rectangle2D Bounds => Item.Bounds;
        //public Rectangle2D Bounds => CachingProperty(ref Item.Bounds);

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool HitTest(Point2D point) => Item.HitTest(point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool VisibleTest(Rectangle2D bounds) => (Item.Bounds.IntersectsWith(bounds) || Item.Bounds.Contains(bounds));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{nameof(GraphicItem)}{{{Item}}}";

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal object CachingProperty(ref object property, [CallerMemberName]string name = "")
        {
            // This is a failed attempt to build auto caching properties. Leaving in as there are rumors on what c# 7 can do. 
            if (!propertyCache.Keys.Contains(name)) propertyCache.Add(name, property);
            return propertyCache[name];
        }

        #endregion
    }
}
