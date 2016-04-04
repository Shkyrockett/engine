namespace Engine.File
{
    /// <summary>
    /// 
    /// </summary>
    public class FileItem
    {
        /// <summary>
        /// 
        /// </summary>
        private string fileName;

        /// <summary>
        /// 
        /// </summary>
        private object data;

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
