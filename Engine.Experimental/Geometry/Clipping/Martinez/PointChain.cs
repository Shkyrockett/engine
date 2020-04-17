// <copyright file="PointChain.cs" >
//     Copyright © 2011 Mahir Iqbal. All rights reserved.
// </copyright>
// <author id="akavel">Mahir Iqbal</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/akavel/martinez-src </remarks>

using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// Represents a connected sequence of segments. The sequence can only be extended by connecting
    /// new segments that share an endpoint with the PointChain.
    /// @author Mahir Iqbal
    /// </summary>
    public class PointChain
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PointChain"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public PointChain(LineSegment2D s)
        {
            PointList = new List<Point2D>
            {
                s.A,
                s.B
            };
            Closed = false;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the point list.
        /// </summary>
        public List<Point2D> PointList { get; set; }
        #endregion Properties

        /// <summary>
        /// Links a segment to the pointChain
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool LinkSegment(LineSegment2D s)
        {
            var front = PointList[0];
            var back = PointList[PointList.Count - 1];

            if (s.A.Equals(front))
            {
                if (s.B.Equals(back))
                {
                    Closed = true;
                }
                else
                {
                    PointList.Insert(0, s.B);//unshift
                }

                return true;
            }
            else if (s.B.Equals(back))
            {
                if (s.A.Equals(front))
                {
                    Closed = true;
                }
                else
                {
                    PointList.Add(s.A);
                }

                return true;
            }
            else if (s.B.Equals(front))
            {
                if (s.A.Equals(back))
                {
                    Closed = true;
                }
                else
                {
                    PointList.Insert(0, s.A);//unshift
                }

                return true;
            }
            else if (s.A.Equals(back))
            {
                if (s.B.Equals(front))
                {
                    Closed = true;
                }
                else
                {
                    PointList.Add(s.B);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Links another pointChain onto this point chain.
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public bool LinkPointChain(PointChain chain)
        {
            var firstPoint = PointList[0];
            var lastPoint = PointList[PointList.Count - 1];

            var chainFront = chain.PointList[0];
            var chainBack = chain.PointList[chain.PointList.Count - 1];

            if (chainFront.Equals(lastPoint))
            {
                PointList.RemoveAt(PointList.Count - 1);//pop
                PointList = PointList.Concat(chain.PointList).ToList();
                return true;
            }

            if (chainBack.Equals(firstPoint))
            {
                PointList.RemoveAt(0); //shift: Remove the first element, and join this list to chain.pointList.
                PointList = chain.PointList.Concat(PointList).ToList();
                return true;
            }

            if (chainFront.Equals(firstPoint))
            {
                PointList.RemoveAt(0); //shift Remove the first element, and join to reversed chain.pointList
                var reversedChainList = chain.PointList; // Don't need chain so can ruin it
                reversedChainList.Reverse();
                PointList = reversedChainList.Concat(PointList).ToList();
                return true;
            }

            if (chainBack.Equals(lastPoint))
            {
                PointList.RemoveAt(PointList.Count - 1);//pop
                PointList.Reverse();
                PointList = chain.PointList.Concat(PointList).ToList();
                return true;
            }

            return false;
        }
    }
}
