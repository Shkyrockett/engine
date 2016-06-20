using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct AtomicMassUnit
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public AtomicMassUnit(double value)
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
        public string Name => "Atomic Mass Unit";
        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "amu";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator AtomicMassUnit(double value) => new AtomicMassUnit(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} amu";
    }
}
