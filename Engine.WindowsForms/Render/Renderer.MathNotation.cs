// <copyright file="Renderer.MathNotation.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

//using Engine.MathNotation;
using System.Drawing;
using static System.Math;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Renderer
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Coefficient shape, Graphics g, GraphicItem item, TextStyle style = null)
        //{
        //    var itemStyle = style ?? (TextStyle)item.Style;
        //    if (Abs(shape.Value) > 1 || Abs(shape.Value) < 1)
        //        g.DrawString(shape.Value.ToString(), style.Font, itemStyle.ForeBrush, shape.Bounds.ToRectangleF());
        //    else if (shape.Value == -1)
        //        g.DrawString("-", style.Font, itemStyle.ForeBrush, shape.Bounds.ToRectangleF());
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Expression shape, Graphics g, GraphicItem item, TextStyle style = null)
        //{
        //    var itemStyle = style ?? (TextStyle)item.Style;
        //    foreach (var term in shape.Terms)
        //    {
        //        term.Render(g, item, style);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Logarithm shape, Graphics g, GraphicItem item, TextStyle style = null)
        //{
        //    var itemStyle = style ?? (TextStyle)item.Style;
        //    shape.Base.Render(g, item, style);
        //    shape.Expression.Render(g, item, style);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Radical shape, Graphics g, GraphicItem item, TextStyle style = null)
        //{
        //    var itemStyle = style ?? (TextStyle)item.Style;
        //    shape.Index.Render(g, item, style);
        //    shape.Radicand.Render(g, item, style);
        //    shape.Exponent.Render(g, item, style);
        //    shape.Sequence.Render(g, item, style);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Term shape, Graphics g, GraphicItem item, TextStyle style = null)
        //{
        //    var itemStyle = style ?? (TextStyle)item.Style;
        //    shape.Coefficient.Render(g, item, style);
        //    foreach (var variable in shape.Variables)
        //    {
        //        g.DrawString(variable.Name, style.Font, itemStyle.ForeBrush, shape.Bounds.ToRectangleF());
        //        variable.Exponent.Render(g, item, style);
        //        variable.Sequence.Render(g, item, style);
        //    }
        //}
    }
}
