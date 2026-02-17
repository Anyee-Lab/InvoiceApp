using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceLib.Contracts;

namespace InvoiceLib.Services;

public class CsvConfigService:IConfigService
{
    FileInfo ConfigFile { get; } = new("Data/config.txt");

    Dictionary<string, string> Config { get; } = new();

    public async Task LoadConfigAsync()
    {
        if (!ConfigFile.Exists) return;
        using var reader = ConfigFile.OpenText();

        while (await reader.ReadLineAsync() is string line)
        {
            var texts = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (texts.Length != 2) continue;
            Config[texts[0]] = texts[1];
        }
    }

    public async Task SaveConfigAsync()
    {
        if (!(ConfigFile.Directory ?? throw new DirectoryNotFoundException()).Exists)
            ConfigFile.Directory.Create();
        using var writer = ConfigFile.CreateText();
        List<Task> tasks = [];
        foreach (var item in Config)
            tasks.Add(writer.WriteLineAsync($"{item.Key}, {item.Value}"));
        await Task.WhenAll(tasks);
    }

    public void SetConfigItem(string key, string value) => Config[key] = value;

    public string? GetConfigItem(string key) => Config.TryGetValue(key, out string? value) ? value : null;
}
