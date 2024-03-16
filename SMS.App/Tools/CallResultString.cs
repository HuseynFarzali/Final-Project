using System.Net.Http.Json;
using System.Xml.Linq;

namespace SMS.App.Tools
{
    public class CallResultString
    {
        public string? StringData { get; set; }
        public bool Processing { get; set; } = true;
        public bool IsFailed { get; set; } = false;

        public Action<string>? OnSuccess { get; set; } = null;
        public Action<string>? OnFail { get; set; } = null;

        public async Task DiagnoseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                StringData = await response.Content.ReadAsStringAsync();
                OnSuccess?.Invoke(StringData!);
            }
            else
            {
                IsFailed = true;
                OnFail?.Invoke(StringData!);
            }

            Processing = false;
        }
    }
}
