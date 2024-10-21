using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendSensorDataAsync(string endpoint, object data, string serverAddress)
        {
            var url = $"{serverAddress}/api/{endpoint}/{data}";

            try
            {
                var response = await _httpClient.PutAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Успешно отправлено на {url}. Ответ: {responseBody}");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при отправке на {url}. Код статуса: {response.StatusCode} - {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке данных на {url}: {ex.Message}");
            }
        }
    }
}
