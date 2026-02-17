using InvoiceLib.Contracts;
using InvoiceLib.Helpers;
using InvoiceLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace InvoiceLib.Services;

public class InvoiceService
{
    IConfigService ConfigService { get; }

    HttpClient InvoiceClient { get; } = new()
    {
        BaseAddress = new Uri("https://aip.baidubce.com/rest/2.0/ocr/v1/vat_invoice")
    };

    public InvoiceService(IConfigService configService)
    {
        ConfigService = configService;
    }

    public async Task<Invoice?> PostInvoiceAsync(string pdfData, int pageNum = 1)
    {
        IEnumerable<KeyValuePair<string, string>> queries =
            [new("access_token", ConfigService.GetConfigItem("Token") ?? throw new NullReferenceException())];

        FormUrlEncodedContent content = new(
        [
            new("pdf_file", pdfData),
            new("pdf_file_num", pageNum.ToString()),
        ]);

        using HttpResponseMessage response = await InvoiceClient.PostAsync(Query.Create(queries), content);
        try { return await response.Content.ReadFromJsonAsync<Invoice>(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        return null;
    }
}