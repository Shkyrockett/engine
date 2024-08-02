// <copyright file="NodeType.cs" company="angusj" version="6.4.0" date="2 July 2015" >
// Copyright (c) 2010 - 2015 Angus Johnson. All rights reserved.
// </copyright>
// <license href="http://www.boost.org/LICENSE_1_0.txt">
//    Use, modification & distribution is subject to Boost Software License Version 1.
// </license>
// <author id="angusj" href="http://www.angusj.com">Angus Johnson</author>
// <attribution href="http://portal.acm.org/citation.cfm?id=129906 ">
//    The code in this library is an extension of Bala Vatti's clipping algorithm: "A generic solution to polygon clipping"
//    Communications of the ACM, Vol 35, Issue 7 (July 1992) pp 56-63.
// </attribution>
// <attribution href="http://books.google.com/books?q=vatti+clipping+agoston">
//    Computer graphics and geometric modeling: implementation and algorithms By Max K. Agoston
//    Springer; 1 edition (January 4, 2005)
// </attribution>
// <seealso href="http://www.me.berkeley.edu/~mcmains/pubs/DAC05OffsetPolygon.pdf">
//     "Polygon Offsetting by Computing Winding Numbers"
//     Paper no. DETC2005-85513 pp. 565-575
//     ASME 2005 International Design Engineering Technical Conferences
//     and Computers and Information in Engineering Conference (IDETC/CIE2005)
//     September 24-28, 2005 , Long Beach, California, USA
// </seealso>
// <summary></summary>

namespace Engine;

/// <summary>
/// The node type enum.
/// </summary>
public enum NodeType
    : byte
{
    /// <summary>
    /// The Any.
    /// </summary>
    Any,

    /// <summary>
    /// The Open.
    /// </summary>
    Open,

    /// <summary>
    /// The Closed.
    /// </summary>
    Closed
}
