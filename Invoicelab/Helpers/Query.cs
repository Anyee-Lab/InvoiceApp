using System.Text;

namespace InvoiceLib.Helpers;

public static class Query
{
    public static string Create(IEnumerable<KeyValuePair<string, string>> queries)
    {
        var sb = new StringBuilder("?");

        foreach (var pair in queries)
            sb.Append($"{pair.Key}={pair.Value}&");
        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();   
    }
}
