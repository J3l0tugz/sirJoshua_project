using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mamilots_POS.Services
{
    public interface ILoginService
    {
        Task<AuthenticationResult?> Authenticate(string username, string password);
        Task<DummyUser[]> Users();
    }

    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthenticationResult?> Authenticate(string username, string password)
        {
            var response = await _httpClient.PostAsync("auth/login", JsonContent.Create(new
            {
                username,
                password
            }));
           
            var content = await response.Content.ReadAsStringAsync();
            
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<AuthenticationResult>(content, JsonOptions)
                : null;
        }

        public async Task<DummyUser[]> Users()
        {
            var response = await _httpClient.GetAsync("users");
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserResponse>(content, JsonOptions)?.Users ?? Array.Empty<DummyUser>();
        }
    }

    public record AuthenticationResult(
        int Id,
        string Username,
        string Email,
        string FirstName,
        string LastName,
        string Gender,
        string Image,
        string AccessToken);

    public record UserResponse(DummyUser[] Users);

    public record DummyUser(string Username, string Password, string FirstName, string LastName)
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
