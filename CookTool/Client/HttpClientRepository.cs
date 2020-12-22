using Blazored.LocalStorage;
using CookTool.Shared.Authentication;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace CookTool.Client
{
    public class HttpClientRepository : AbstractHttpClientRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private User _currentUser;

        public HttpClientRepository(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(client)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        // --- AUTHENTICATION --- //

        public override async Task<LoginResult> Login(LoginModel loginModel)
        {
         
            var response = await _client.PostAsJsonAsync("auth/login", loginModel);
            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email, loginResult.Nickname, loginResult.Image);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public override async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public override async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _client.PostAsJsonAsync("auth/register", registerModel);
            var response = await result.Content.ReadFromJsonAsync<RegisterResult>();

            return response;
        }

        // --- USER --- //

        public override async Task<User> GetCurrentUser()
        {
            if (_currentUser != null) return _currentUser;

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

            var userAuth = authState.User;
            if (userAuth.Identity.IsAuthenticated)
            {
                var userData = await _client.PostAsJsonAsync($"users/current", userAuth.Identity.Name);
                _currentUser = await userData.Content.ReadFromJsonAsync<User>();
            }
            
            return _currentUser;
        }
    }
}
