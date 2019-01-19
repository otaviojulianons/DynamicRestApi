using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Interfaces.Infrastructure
{
    public interface ISwaggerRepository
    {
         object GetSwaggerObject();
         void Update(string json);
    }
}