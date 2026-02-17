namespace InvoiceLib.Contracts
{
    public interface IConfigService
    {
        string? GetConfigItem(string key);
        Task LoadConfigAsync();
        Task SaveConfigAsync();
        void SetConfigItem(string key, string value);
    }
}