using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Services
{
    public class Gravatar : IGravatar
    {
        IHttpClientFactory _httpClientFactory;
        ILogger<Gravatar> _logger;

        public Gravatar(IHttpClientFactory httpClientFactory, ILogger<Gravatar> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public async Task<ProfileInfo> GetInfoAsync(string emailAddress)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var emailHash = GetHash(emailAddress);
                var uri = $"https://gravatar.com/{emailHash}.json";
                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var profileDetails = await response.Content.ReadAsStringAsync();
                    var profile = JsonConvert.DeserializeObject<JObject>(profileDetails);

                    if (profile?["entry"]?.FirstOrDefault() is JObject obj)
                    {
                        var displayName = (string)obj["displayName"];
                        var thumbnailUrl = (string)obj["thumbnailUrl"];
                        return new ProfileInfo(displayName, thumbnailUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get profile info. Exception occured: {ex}");
            }
            finally
            {
                _logger.LogInformation($"Gravatar profile info fetched for user: {emailAddress}");
            }

            return new ProfileInfo(string.Empty, string.Empty);
        }

        public async Task<string> GenerateUserHtmlAsync(string emailAddress)
        {
            try { 
                var userInfo = await GetInfoAsync(emailAddress);
                string thumbnailUrl = $"{userInfo.ThumbnailUrl}?s=30";

                if (userInfo?.DisplayName.Length > 0 && userInfo?.ThumbnailUrl?.Length > 0)
                {
                    return $@"
                    <img src=""{thumbnailUrl}"" />
                    <span>{userInfo.DisplayName}</span>";
                }
            } catch (Exception ex)
            {
                _logger.LogError($"Unable to generate user html. Exception occured: {ex}");
            }
            return "";
        }
    }
}
