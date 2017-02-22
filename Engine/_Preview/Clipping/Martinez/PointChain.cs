// <copyright file="PointChain.cs" >
//     Copyright (c) 2011 Mahir Iqbal. All rights reserved.
// </copyright>
// <author id="akavel">Mahir Iqbal</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/akavel/martinez-src </remarks>

using System.Collections.Generic;
using System.Linq;

namespace Engine._Preview
{
     /// <summary>
     /// Represents a connected sequence of segments. The sequence can only be extended by connecting
     /// new segments that share an endpoint with the PointChain.
     /// @author Mahir Iqbal
     /// </summary>
    public class PointChain
    {
        /// <summary>
        /// 
        /// </summary>
        public bool closed;

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> pointList;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public PointChain(LineSegment s)
        {
            pointList = new List<Point2D>
            {
                s.A,
                s.B
            };
            closed = false;
        }

        /// <summary>
        /// Links a segment to the pointChain
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool LinkSegment(LineSegment s)
        {
            var front = pointList[0];
            var back = pointList[pointList.Count - 1];

            if (s.A.Equals(front))
            {
                if (s.B.Equals(back))
                    closed = true;
                else
                    pointList.Insert(0, s.B);//unshift

                return true;
            }
            else if (s.B.Equals(back))
            {
                if (s.A.Equals(front))
                    closed = true;
                else
                    pointList.Add(s.A);

                return true;
            }
            else if (s.B.Equals(front))
            {
                if (s.A.Equals(back))
                    closed = true;
                else
                    pointList.Insert(0, s.A);//unshift

                return true;
            }
            else if (s.A.Equals(back))
            {
                if (s.B.Equals(front))
                    closed = true;
                else
                    pointList.Add(s.B);

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
            var firstPoint = pointList[0];
            var lastPoint = pointList[pointList.Count - 1];

            var chainFront = chain.pointList[0];
            var chainBack = chain.pointList[chain.pointList.Count - 1];

            if (chainFront.Equals(lastPoint))
            {
                pointList.RemoveAt(pointList.Count - 1);//pop
                pointList = pointList.Concat(chain.pointList).ToList();
                return true;
            }

            if (chainBack.Equals(firstPoint))
            {
                pointList.RemoveAt(0); //shift: Remove the first element, and join this list to chain.pointList.
                pointList = chain.pointList.Concat(pointList).ToList();
                return true;
            }

            if (chainFront.Equals(firstPoint))
            {
                pointList.RemoveAt(0); //shift Remove the first element, and join to reversed chain.pointList
                var reversedChainList = chain.pointList; // Don't need chain so can ruin it
                reversedChainList.Reverse();
                pointList = reversedChainList.Concat(pointList).ToList();
                return true;
            }

            if (chainBack.Equals(lastPoint))
            {
                pointList.RemoveAt(pointList.Count - 1);//pop
                pointList.Reverse();
                pointList = chain.pointList.Concat(pointList).ToList();
                return true;
            }

            return false;
        }
    }
}
