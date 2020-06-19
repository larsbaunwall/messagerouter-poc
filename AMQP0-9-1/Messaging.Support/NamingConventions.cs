namespace Messaging.Support
{
    public static class NamingConventions
    {
        public static string ExchangeNamingConvention(string boundedContextName, string publishedLanguageEntity)
        {
            return $"Ecosystem.{boundedContextName}.{publishedLanguageEntity}";
        }
        
        public static string QueueNamingConvention(string boundedContextName, string sourceBoundedContextName, string publishedLanguageEntity, string subscriberId)
        {
            return $"{sourceBoundedContextName}.{publishedLanguageEntity}:{boundedContextName}-{subscriberId}";
        }
    }
}