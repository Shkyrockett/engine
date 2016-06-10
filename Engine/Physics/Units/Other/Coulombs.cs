namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// Unit of Charge
    /// </summary>
    public struct Coulombs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Coulombs(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Coulombs";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Coulombs(double value) => new Coulombs(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} C", Value);
    }
}
