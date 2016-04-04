namespace Engine.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class Variable
        : IGameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
    }
}
