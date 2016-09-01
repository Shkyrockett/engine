using System.Collections.Generic;

namespace Engine.MathNotation
{
    /// <summary>
    /// 
    /// </summary>
    public class Constant
        :Term
    {
        /// <summary>
        /// 
        /// </summary>
        public Constant()
        {
            Variables = new List<Variable>();
        }

        /// <summary>
        /// 
        /// </summary>
        private new List<Variable> Variables { get; set; }
    }
}
