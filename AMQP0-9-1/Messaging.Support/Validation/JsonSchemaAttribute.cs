using System;

namespace Messaging.Support.Validation
{
    public class JsonSchemaAttribute : Attribute
    {
        public string SchemaUri { get; }

        public JsonSchemaAttribute(string schemaUri)
        {
            SchemaUri = schemaUri;
        }
    }
}