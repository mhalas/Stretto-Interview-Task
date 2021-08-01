using RestEase;
using System.Threading.Tasks;

namespace Common.ApiClient
{
    public interface IStrettoApiClient
    {
        [Get("api/flats/csv")]
        public Task<Response<string>> GetCsvData();
        [Get("api/flats/taxes?city={city}")]
        public Task<Response<decimal>> GetTaxForCity([Path("city")]string city);
    }
}
