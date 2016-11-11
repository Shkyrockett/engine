// <copyright file="Figure.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author>Shkyrockett</author>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Figure
        : Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public Figure()
        {
            Items = new List<FigureItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Figure(Point2D start)
        {
            Items.Add(new FigurePoint(start));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FigureItem this[int index]
                => Items[index];

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        //[XmlArray]
        [TypeConverter(typeof(ListConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<FigureItem> Items { get; set; } = new List<FigureItem>();

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PathCommandString { get { return ToPathCommandString(); } set { Items = ParsePathCommandString(value).Item1; } }

        /// <summary>
        /// Gets a listing of all end nodes from the Figure.
        /// </summary>
        [XmlIgnore]
        public List<Point2D> Nodes
            => Items.Select(item => item.End).ToList();

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool Closed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override Rectangle2D Bounds
            => Boundings.Figure(this);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Containings.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddLineSegment(Point2D end)
        {
            var segment = new FigureLineSegment(Items[Items.Count - 1], end);
            Items.Add(segment);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="angle"></param>
        /// <param name="largeArc"></param>
        /// <param name="sweep"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddArc(double r1, double r2, double angle, bool largeArc, bool sweep, Point2D end)
        {
            var arc = new FigureArc(Items[Items.Count - 1], r1, r2, angle, largeArc, sweep, end);
            Items.Add(arc);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddQuadraticBezier(Point2D handle, Point2D end)
        {
            var quad = new FigureQuadraticBezier(Items[Items.Count - 1], handle, end);
            Items.Add(quad);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle1"></param>
        /// <param name="handle2"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Figure AddCubicBezier(Point2D handle1, Point2D handle2, Point2D end)
        {
            var cubic = new FigureCubicBezier(Items[Items.Count - 1], handle1, handle2, end);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public Figure AddCardinalCurve(List<Point2D> nodes)
        {
            var cubic = new FigureCardinal(Items[Items.Count - 1], nodes);
            Items.Add(cubic);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathString"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/5115388/parsing-svg-path-elements-with-c-sharp-are-there-libraries-out-there-to-do-t
        /// </remarks>
        public (List<FigureItem>, bool) ParsePathCommandString(string pathString)
        {
            List<FigureItem> figure = new List<FigureItem>();
            bool closed = false;
            bool relitive = false;
            Point2D startPoint = null;
            FigureItem item = null;

            // these letters are valid SVG commands. Whenever we find one, a new command is starting. Let's split the string there.
            string separators = @"(?=[MZLHVCSQTAmzlhvcsqta])";

            // discard whitespace and comma but keep the -
            string argSeparators = @"[\s,]|(?=-)";

            var tokens = Regex.Split(pathString, separators).Where(t => !string.IsNullOrWhiteSpace(t));
            foreach (var token in tokens)
            {
                var cmd = token.Take(1).Single();
                string remainingargs = token.Substring(1);

                var splitArgs = Regex.Split(remainingargs, argSeparators).Where(t => !string.IsNullOrEmpty(t));
                double[] args = splitArgs.Select(arg => double.Parse(arg)).ToArray();

                switch (cmd)
                {
                    case 'm':
                        relitive = true;
                        goto case 'M';
                    case 'M': // Svg moveto
                        item = new FigurePoint(item, relitive, args);
                        startPoint = item.Start;
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 'z':
                        relitive = true;
                        goto case 'Z';
                    case 'Z': // Svg closepath
                        item = new FigurePoint(item, relitive, startPoint);
                        closed = true;
                        relitive = false;
                        break;
                    case 'l':
                        relitive = true;
                        goto case 'L';
                    case 'L': // Svg lineto
                        item = new FigureLineSegment(item, relitive, args);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 'h':
                        relitive = true;
                        goto case 'H';
                    case 'H': // Svg horizontal-lineto
                        item = new FigureLineSegment(item, relitive, item.End.X, args[0]);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 'v':
                        relitive = true;
                        goto case 'V';
                    case 'V': // Svg vertical-lineto
                        item = new FigureLineSegment(item, relitive, args[0], item.End.Y);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 'c':
                        relitive = true;
                        goto case 'C';
                    case 'C': // Svg curveto
                        item = new FigureCubicBezier(item, relitive, args);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 's':
                        relitive = true;
                        goto case 'S';
                    case 'S': // Svg smooth-curveto
                        item = new FigureCubicBezier(item, relitive, args);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 'q':
                        relitive = true;
                        goto case 'Q';
                    case 'Q': // Svg quadratic-bezier-curveto
                        item = new FigureQuadraticBezier(item, relitive, args);
                        figure.Add(item);
                        relitive = false;
                        break;
                    case 't':
                        relitive = true;
                        goto case 'T';
                    case 'T': // Svg smooth-quadratic-bezier-curveto
                        item = new FigureQuadraticBezier(item, relitive, args);
                        relitive = false;
                        break;
                    case 'a':
                        relitive = true;
                        goto case 'A';
                    case 'A': // Svg elliptical-arc
                        item = new FigureArc(item, relitive, args);
                        figure.Add(item);
                        relitive = false;
                        break;
                    default: // Unknown element.
                        break;
                }
            }

            return (figure, closed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private String ToPathCommandString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var item in Items)
            {
                switch (item)
                {
                    case FigurePoint t:
                        output.Append($"M{t.Start.X},{t.Start.Y} ");
                        break;
                    case FigureLineSegment t:
                        output.Append($"L{t.End.X},{t.End.Y} ");
                        break;
                    case FigureCubicBezier t:
                        output.Append($"C{t.Handle1.X},{t.Handle1.Y},{t.Handle2.X},{t.Handle2.Y},{t.End.X},{t.End.Y} ");
                        break;
                    case FigureQuadraticBezier t:
                        output.Append($"Q{t.Handle.X},{t.Handle.X},{t.End.X},{t.End.Y} ");
                        break;
                    case FigureArc t:
                        int largearc = t.LargeArc ? 1 : 0;
                        int sweep = t.Sweep ? 1 : 0;
                        output.Append($"A{t.RX},{t.RY},{t.Angle},{largearc},{sweep},{t.End.X},{t.End.Y} ");
                        break;
                    default:
                        break;
                }
            }

            return output.ToString().TrimEnd();
        }
    }
}
