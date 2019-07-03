// <copyright file="Splittings.cs" >
//    Copyright © 2017 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine
{
    public class Polynomial
    {
        public Polynomial(params double[] numbers)
        {
            this.Numbers = numbers;
        }

        public double[] Numbers { get; set; }

        public double this[int index]
        {
            get { return Numbers[index]; }
            set { Numbers[index] = value; }
        }
    }
}