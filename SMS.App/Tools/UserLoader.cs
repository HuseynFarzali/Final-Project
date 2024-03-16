using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using SMS.App.Models;
using System.Net.Http.Headers;

namespace SMS.App.Tools
{
    public class UserLoader
    {
        public HttpClient Api { get; set; } = default!;
        public ISessionStorageService SessionStorage { get; set; } = default!;
        public ILocalStorageService LocalStorage { get; set; } = default!;

        private CallResult<AppUser> _currentUserCall = new();

        public UserLoader(HttpClient api, ISessionStorageService sessionStorage, ILocalStorageService localStorage)
        {
            Api = api;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
        }

        public async Task<AppUser?> LoadCurrent()
        {
            var user = await SessionStorage.GetItemAsync<AppUser>("current-user")
                    ?? await LocalStorage.GetItemAsync<AppUser>("current-user");

            if (user is null)
            {
                var token = await SessionStorage.GetItemAsStringAsync("token")
                         ?? await LocalStorage.GetItemAsStringAsync("token");

                if (token is null)
                {
                    return null;
                }

                Api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await _currentUserCall.DiagnoseAsync(await Api.GetAsync("/api/auth/current-user"));

                if (_currentUserCall.IsFailed is false)
                {
                    user = _currentUserCall.Data!;
                }
            }

            return user;
        }
    }
}
