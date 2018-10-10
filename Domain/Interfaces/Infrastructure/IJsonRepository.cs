using System.Threading.Tasks;

namespace Domain.Interfaces.Infrastructure
{
    public interface IJsonRepository
    {
        Task Update(string json);

        Task<string> Get();
    }
}
