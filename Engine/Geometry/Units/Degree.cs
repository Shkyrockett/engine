using System.Diagnostics;

namespace Engine.Geometry
{
    struct Degree
    {
        private double value;

        public Degree(Radian radian)
        {
            value = radian.Value.ToDegrees();
        }

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public static implicit operator Degree(double value)
        {
            return new Degree(value);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else if (obj is Radian)
            {
                return value.Equals(((Radian)obj).ToDegrees().Value);
            }

            return value.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public Radian ToRadian()
        {
            return value.ToRadians();
        }
    }
}
