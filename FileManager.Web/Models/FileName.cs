namespace FileManager.Web.Models
{
    public struct FileName : IEquatable<FileName>
    {
        public string Name;

        public FileName(string name)
        {
            Name = name;
        }

        public bool Equals(FileName fileName)
        {
            return fileName.Name == Name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
