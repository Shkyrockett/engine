// <copyright file="Group.cs" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine
{
    [GraphicsObject]
    [DataContract, Serializable]
    public struct Group
        : IShape, IEquatable<Group>
    {
        public Group(List<IShape> shapes)
        {
            Shapes = shapes;
        }

        public List<IShape> Shapes { get; set; }

        public static bool operator ==(Group left, Group right) => left.Equals(right);
        public static bool operator !=(Group left, Group right) => !(left == right);

        public override bool Equals(object obj) => obj is Group d && Equals(d);
        public bool Equals(Group other) => EqualityComparer<List<IShape>>.Default.Equals(Shapes, other.Shapes);
        public override int GetHashCode() => HashCode.Combine(Shapes);
        public override string ToString() => base.ToString();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
