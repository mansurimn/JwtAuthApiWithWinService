using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WindowsServiceClient
{
    public class WindowsService
    {
        private readonly HttpClient _httpClient;

        public WindowsService()
        {
            _httpClient = new HttpClient();
        }

        // Method to authenticate and get a JWT token
        public async Task<string> AuthenticateAndGetTokenAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };

            try
            {
                // Send authentication request to AuthApi
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7124/api/authentication/generate-token", loginRequest);

                // Check for success status and parse token from the response
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<dynamic>();
                    return result.token;
                }

                Console.WriteLine("Authentication failed. Status: " + response.StatusCode);
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during authentication: " + ex.Message);
                return string.Empty;
            }
        }

        // Method to retrieve product information using the JWT token
        public async Task<string> GetProductInfoAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Call the protected product info endpoint
                var response = await _httpClient.GetAsync("https://localhost:7124/api/product/info");

                // Check response for access status
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    Console.WriteLine("Access Forbidden: Unauthorized access to product info.");
                    return "403 Forbidden";
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving product info: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
