using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TestPoint2D
    {
        #region Static Implementations
        /// <summary>
        /// A Unit <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D AUnit = new Point2D(1, 1);

        /// <summary>
        /// An Empty <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Empty = new Point2D();
        #endregion

        #region Private Fields

        /// <summary>
        /// X component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double x;

        /// <summary>
        /// Y component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double y;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPoint2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public TestPoint2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPoint2D"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks></remarks>
        public TestPoint2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Y component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute()]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public double TotalDistance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public int Previous { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return x == 0 && y == 0; }
        }

        #endregion

        /// <summary>
        /// Explicit conversion to Point2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static explicit operator Point2D(TestPoint2D point)
        {
            return new Point2D(point.X, point.Y);
        }

        /// <summary>
        /// Implicit conversion to ItPoint2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static implicit operator TestPoint2D(Point2D point)
        {
            return new TestPoint2D(point.X, point.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Point2D)) return false;
            Point2D comp = (Point2D)obj;
            return comp.X == X && comp.Y == Y && comp.GetType().Equals(GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }

        /// <summary>
        /// Parse - returns an instance converted from the provided string using
        /// the culture "en-US"
        /// <param name="source"> string with Point data </param>
        /// </summary>
        public static Point2D Parse(string source)
        {
            TokenizerHelper th = new TokenizerHelper(source, CultureInfo.InvariantCulture);

            Point2D value;

            String firstToken = th.NextTokenRequired();

            value = new Point2D(
                Convert.ToDouble(firstToken, CultureInfo.InvariantCulture),
                Convert.ToDouble(th.NextTokenRequired(), CultureInfo.InvariantCulture));

            // There should be no more tokens in this string.
            th.LastTokenRequired();

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //if (this == null) return nameof(Point2D);
            return string.Format(CultureInfo.CurrentCulture, "{0}{{{1}={2},{3}={4}}}", nameof(Point2D), nameof(X), x, nameof(Y), y);
        }
    }
}
