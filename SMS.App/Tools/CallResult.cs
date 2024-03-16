using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace SMS.App.Tools
{
    public class CallResult<TData>
    {
        public TData? Data { get; set; }
        public bool Processing { get; set; } = true;
        public bool IsFailed { get; set; } = false;

        public Action<object>? OnSuccess { get; set; } = null;
        public Action<object>? OnFail { get; set; } = null;

        public async Task DiagnoseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                Data = await response.Content.ReadFromJsonAsync<TData>();
                OnSuccess?.Invoke(Data!);
            }
            else
            {
                IsFailed = true;
                OnFail?.Invoke(Data!);
            }

            Processing = false;
        }
    }
}
