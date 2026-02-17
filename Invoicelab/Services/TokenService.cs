using InvoiceLib.Contracts;
using InvoiceLib.Helpers;
using InvoiceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceLib.Services
{
    public class TokenService
    {
        HttpClient TokenClient { get; } = new()
        {
            BaseAddress = new Uri("https://aip.baidubce.com/oauth/2.0/token")
        };

        IConfigService ConfigService { get; }

        public string? ID => ConfigService.GetConfigItem(nameof(ID));

        public string? Secret => ConfigService.GetConfigItem(nameof(Secret));

        public string? Token => ConfigService.GetConfigItem(nameof(Token));

        public async Task UpdateTokenAsync()
        {
            if (string.IsNullOrEmpty(ID))
            {
                Console.WriteLine("Invalid ID");
                return;
            }

            if (string.IsNullOrEmpty(Secret))
            {
                Console.WriteLine("Invalid secret");
                return;
            }

            IEnumerable<KeyValuePair<string, string>> queries =
            [
                new("grant_type","client_credentials"),
                new("client_id",ID),
                new("client_secret",Secret),
            ];

            using HttpResponseMessage response = await TokenClient.PostAsync(Query.Create(queries), null);
            try
            {
                var token = (await response.Content.ReadFromJsonAsync<Token>())?.access_token;
                if (string.IsNullOrEmpty(token)) return;
                ConfigService.SetConfigItem(nameof(Token), token);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        public TokenService(IConfigService configService)
        {
            ConfigService = configService;
        }

    }
}
