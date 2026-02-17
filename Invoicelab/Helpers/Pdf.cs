namespace InvoiceLib.Helpers;

public static class Pdf
{
    public static async Task<string?> ReadPdfAsync(string filePath)
    {
        if (!File.Exists(filePath)) return null;
        var data = await File.ReadAllBytesAsync(filePath);
        return Convert.ToBase64String(data);
    }
}
