// <copyright file="FactorizedPolynomial.cs" >
//     Copyright (c) 2007 hanzzoid. All rights reserved.
// </copyright>
// <license>
//     Licensed under the Code Project Open License (CPOL). See http://www.codeproject.com/info/cpol10.aspx for full license information.
// </license>
// <author id="hanzzoid">hanzzoid</author>
// <summary></summary>

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class FactorizedPolynomial
    {
        /// <summary>
        /// Set of factors the polynomial  consists of.
        /// </summary>
        public Polynomial[] Factor;

        /// <summary>
        /// Set of powers, where Factor[i] is lifted
        /// to Power[i].
        /// </summary>
        public int[] Power;
    }
}
