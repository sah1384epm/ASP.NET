using API.Dtos.Stock;
using API.Interfaces;
using API.Mappers;
using API.Models;
using Newtonsoft.Json;

namespace API.Services   // قبلاً "API.Service" بود (بدون s) که با using API.Services; در Program.cs
                          // تطابق نداشت و کامپایل نمی‌شد. الان با بقیه‌ی پروژه (مثل TokenService) یکی شد.
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var apiKey = _config["FMPKey"];

                var url = $"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={apiKey}";

                var result = await _httpClient.GetAsync(url);
                var content = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"FMP request failed for '{symbol}': {(int)result.StatusCode} {result.StatusCode} - {content}");
                    return null;
                }

                var stocks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                var stock = stocks?.FirstOrDefault();

                if (stock == null)
                {
                    Console.WriteLine($"FMP returned no profile data for '{symbol}'. Raw response: {content}");
                    return null;
                }

                return stock.ToStockFromFMP();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
