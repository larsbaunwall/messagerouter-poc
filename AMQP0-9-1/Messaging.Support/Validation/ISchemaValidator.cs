using System.Threading.Tasks;

namespace Messaging.Support.Validation
{
    public interface ISchemaValidator
    {
        public Task<ValidationResult> IsValid<TEvent>(string msg);
    }
}