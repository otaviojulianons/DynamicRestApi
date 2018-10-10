using Domain.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class JsonRepository : IJsonRepository
    {
        private string _url;

        public JsonRepository(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("Comunication:JsonRepository:Url");
        }

        public async Task Update(string json)
        {
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await httpClient.PutAsync(_url,content);
        }

        public async Task<string> Get()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetAsync(_url);
            return await json.Content.ReadAsStringAsync();
        }
    }
}
