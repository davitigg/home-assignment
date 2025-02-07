namespace Infrastructure.Configuration
{
    public class DbSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string SqlServer { get; set; } = null;
        public string InMemoryDb { get; set; } = null;
    }
}
