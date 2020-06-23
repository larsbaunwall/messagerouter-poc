namespace Messaging.Support
{
    public static class NamingConventions
    {
        public static string Topic(string boundedContextName, string publishedLanguageEntity)
        {
            return $"Ecosystem.{boundedContextName}.{publishedLanguageEntity}";
        }
    }
}