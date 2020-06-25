using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Messaging.Support.Validation
{
    public class JsonSchemaValidator : ISchemaValidator
    {
        private static readonly ConcurrentDictionary<string, JSchema> SchemaCache = new ConcurrentDictionary<string, JSchema>();
        
        public async Task<ValidationResult> IsValid<TEvent>(string msg)
        {
            var schemaUri = typeof(TEvent).GetAttribute<JsonSchemaAttribute>()?.SchemaUri;
            
            if(schemaUri == null)
                return new ValidationResult(true, null);

            if (!SchemaCache.TryGetValue(schemaUri, out var schema))
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(schemaUri);
                var schemaJson = await response.Content.ReadAsStringAsync();
                schema = JSchema.Parse(schemaJson, new JSchemaUrlResolver());
                SchemaCache.Add(schemaUri, schema);
            }
            
            var valid = JObject.Parse(msg).IsValid(schema, out IList<string> errors);
            return new ValidationResult(valid, errors);
        }
    }

    public class ValidationResult
    {
        public ValidationResult(bool isValid, IList<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public bool IsValid { get; }
        public IList<string> Errors { get; }
    }
}