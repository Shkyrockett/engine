﻿// <copyright file="EnvelopeDistort.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    /// <summary>
    /// The Envelope distort class.
    /// </summary>
    public class EnvelopeDistort
        : DestructiveFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvelopeDistort"/> class.
        /// </summary>
        /// <param name="envelope">The envelope.</param>
        /// <param name="boundingBox">The bounding box.</param>
        public EnvelopeDistort(Envelope envelope, Rectangle2D boundingBox)
        {
            Envelope = envelope;
            BoundingBox = boundingBox;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the envelope to use for warping.
        /// </summary>
        public Envelope Envelope { get; set; }

        /// <summary>
        /// Gets or sets the bounding box of the shape.
        /// </summary>
        public Rectangle2D BoundingBox { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Process.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Process(Point2D point)
            => Process(point, Envelope, BoundingBox);

        /// <summary>
        /// Get the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="envelope">The envelope.</param>
        /// <param name="boundingBox"></param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public Point2D Process(Point2D point, Envelope envelope, Rectangle2D boundingBox)
            => Distortions.Envelope(point, boundingBox,
                envelope.ControlPointTopLeft.Point, envelope.ControlPointTopLeft.AnchorAGlobal, envelope.ControlPointTopLeft.AnchorBGlobal,
                envelope.ControlPointTopRight.Point, envelope.ControlPointTopRight.AnchorAGlobal, envelope.ControlPointTopRight.AnchorBGlobal,
                envelope.ControlPointBottomRight.Point, envelope.ControlPointBottomRight.AnchorAGlobal, envelope.ControlPointBottomRight.AnchorBGlobal,
                envelope.ControlPointBottomLeft.Point, envelope.ControlPointBottomLeft.AnchorAGlobal, envelope.ControlPointBottomLeft.AnchorBGlobal);

        #endregion
    }
}