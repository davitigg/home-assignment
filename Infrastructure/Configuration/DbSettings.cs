namespace Infrastructure.Configuration
{
    public class DbSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string ConnectionDbDefault { get; set; } = null;
        public string InMemoryDb { get; set; } = null;
    }
}
