using System.Diagnostics;

namespace Engine.Geometry
{
    struct Radian
    {
        private double value;

        public Radian(double value)
        {
            this.value = value;
        }

        public Radian(Degree radian)
        {
            value = radian.Value.ToRadians();
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
        public static implicit operator Radian(double value)
        {
            return new Radian(value);
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
            else if (obj is Degree)
            {
                return value.Equals(((Degree)obj).ToRadian().Value);
            }

            return value.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public Radian ToDegrees()
        {
            return value.ToDegrees();
        }
    }
}
