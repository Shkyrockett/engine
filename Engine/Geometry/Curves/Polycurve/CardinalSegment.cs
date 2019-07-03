// <copyright file="CardinalSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The cardinal segment class.
    /// </summary>
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public class CardinalSegment
         : CurveSegment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CardinalSegment"/> class.
        /// </summary>
        public CardinalSegment()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardinalSegment"/> class.
        /// </summary>
        /// <param name="previous">The previous.</param>
        /// <param name="points">The points.</param>
        public CardinalSegment(CurveSegment previous, List<Point2D> points)
        {
            Previous = previous;
            previous.Next = this;
            CentralPoints = points;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? Start
        {
            get { return Previous?.End; }
            set
            {
                if (Previous is null)
                {
                    Previous = new PointSegment(value);
                }
                else
                {
                    Previous.End = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the central points.
        /// </summary>
        [XmlArray]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> CentralPoints { get; set; }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Nodes
        {
            get
            {
                var nodes = new List<Point2D> { Start.Value };
                nodes.AddRange(CentralPoints);
                return nodes;
            }
        }

        /// <summary>
        /// Gets or sets the next to end.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Point2D? NextToEnd { get { return Nodes[Nodes.Count - 1]; } set { Nodes[Nodes.Count - 1] = value.Value; } }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public override Point2D? End
        {
            get { return CentralPoints?[CentralPoints.Count - 1]; }
            set
            {
                if (CentralPoints is null)
                {
                    CentralPoints = new List<Point2D> { value.Value };
                }
                else
                {
                    CentralPoints[CentralPoints.Count - 1] = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets the grips.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public override List<Point2D> Grips
        {
            get
            {
                var result = new List<Point2D> { Start.Value };
                result.AddRange(CentralPoints);
                result.Add(End.Value);
                return result;
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.PolygonBounds(Nodes));

        /// <summary>
        /// ToDo: Add length calculation for Cardinal curves.
        /// </summary>
        public override double Length => 0;
        #endregion Properties

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
            => throw new NotImplementedException();
    }
}
