namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public struct IntersectionParams
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="params"></param>
        public IntersectionParams(string name, string @params)
        {
            this.name = name;
            this.@params = @params;
        }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string @params { get; set; }
    }
}
