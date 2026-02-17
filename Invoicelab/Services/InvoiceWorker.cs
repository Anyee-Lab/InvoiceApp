using InvoiceLib.Helpers;
using InvoiceLib.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace InvoiceLib.Services;

public class InvoiceWorker(InvoiceService invoiceService)
{
    InvoiceService InvoiceService { get; } = invoiceService;

    public virtual string? FilePath { get; set; }

    public virtual int PageNum { get; set; } = 1;

    public virtual Invoice? Invoice { get; set; }

    public virtual string Number =>
        Invoice?.words_result?.InvoiceNum is string num
            ? num
            : "未解析";

    public virtual string Name
    {
        get
        {
            if (Invoice?.words_result?.CommodityName is null) return "未解析";
            var names = Invoice.words_result.CommodityName;
            if (names.Length < 1) return "未解析";
            var texts = names[0].word?.Split('*', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return (texts is null || texts.Length < 1)
                ? "未解析"
                : texts[^1];
        }
    }

    public virtual string Unit
    {
        get
        {
            if (Invoice?.words_result?.CommodityUnit is null) return "未解析";
            var units = Invoice.words_result.CommodityUnit;
            if (units.Length < 1) return "个";
            if (units.Length > 1) return "套";
            return units[0].word ?? "个";
        }
    }

    public virtual float Price => Amount / Count;

    public virtual int Count
    {
        get
        {
            if (Invoice?.words_result?.CommodityNum is null) return 0;
            var counts = Invoice.words_result.CommodityNum;
            if (counts.Length != 1) return 1;
            var success = int.TryParse(counts[0].word, out var ret);
            return success ? ret : 1;
        }
    }

    public virtual float Amount
    {
        get
        {
            if (Invoice?.words_result is null) return float.NaN;
            var success = float.TryParse(Invoice.words_result.AmountInFiguers, out var ret);
            return success ? ret : float.NaN;
        }
    }

    public virtual string Remarks => Invoice?.words_result?.Remarks ?? "未解析";

    public virtual async Task ResolveInvoiceAsync()
    {
        if (FilePath is null) return;
        var pdf = await Pdf.ReadPdfAsync(FilePath);
        if (pdf is null) return;
        Invoice = await InvoiceService.PostInvoiceAsync(pdf, PageNum);
    }

    public virtual void AutoRename(bool isName = true, bool isAmount = true, bool isNum = false)
    {
        if ((isName, isAmount, isNum) == (false, false, false)) isNum = true;
        if (FilePath is null || !File.Exists(FilePath) || Invoice is null) return;
        var dir = Path.GetDirectoryName(FilePath);
        if (dir is null) return;
        StringBuilder nameBuilder = new();
        if (isName) nameBuilder.Append(Regex.Replace(Name, "[\\/:*?\"<>|]", string.Empty));
        if (isAmount) nameBuilder.Append($"_{Amount:f2}");
        if (isNum) nameBuilder.Append($"_{Number}");
        nameBuilder.Append(".pdf");
        var newFilePath = Path.Combine([dir, nameBuilder.ToString()]);
        while (File.Exists(newFilePath)) newFilePath = nameBuilder.Append('+').ToString();
        File.Move(FilePath, newFilePath);
        FilePath = newFilePath;
    }
}
