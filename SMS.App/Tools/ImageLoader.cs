using Microsoft.AspNetCore.Components;

namespace SMS.App.Tools
{
    public class ImageLoader
    {
        public ImageLoader(HttpClient api)
        {
            Api = api;
        }

        public HttpClient Api { get; set; }

        public async Task<string> TryLoadFrom(string uri, bool background = false)
        {
            var response = await Api.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var imageData = await response.Content.ReadAsByteArrayAsync();
                return $"data:{response.Content.Headers.ContentType};base64,{Convert.ToBase64String(imageData)}";
            }
            else
            {
                if (!background)
                    return "assets/img/photos/no-user.webp";
                else
                    return "assets/img/photos/no-background.png";
            }
        }
    }
}
