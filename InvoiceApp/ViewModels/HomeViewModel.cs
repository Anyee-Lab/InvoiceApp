using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InvoiceApp.Helpers;
using InvoiceApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace InvoiceApp.ViewModels;

public enum ExportType
{
    ToConsole,
    ToFile,
}

public partial class HomeViewModel : ObservableObject
{
    #region Console
    public string OutString => outStringBuilder.ToString();
    readonly StringBuilder outStringBuilder = new();

    [RelayCommand]
    void ClearConsole()
    {
        outStringBuilder.Clear();
        OnPropertyChanged(nameof(OutString));
    }
    #endregion

    #region Grid
    public ObservableCollection<ObservableInvoiceWorker> Workers { get; } = [];

    [RelayCommand]
    async Task AddWorkersAsync()
    {
        try
        {
            // Create a files picker
            FileOpenPicker picker = new()
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };
            picker.FileTypeFilter.Add(".pdf");

            // Initialize the files picker with the window handle (HWnd).
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindow));

            // Open the picker for the user to pick a files
            var files = await picker.PickMultipleFilesAsync();
            if (files.Count < 1)
            {
                Console.WriteLine("加载已取消");
                return;
            }

            // Get data from files
            foreach (var file in files)
            {
                var worker = App.GetService<ObservableInvoiceWorker>();
                if (worker is null) return;
                Console.WriteLine($"已添加\"{file.Path}\"");
                worker.FilePath = file.Path;
                Workers.Add(worker);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("加载失败：");
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    void ClearWorkers() => Workers.Clear();

    [RelayCommand]
    async Task ResolveInvoicesAsync()
    {
        foreach (var worker in Workers)
        {
            await worker.ResolveInvoiceAsync();
            Console.WriteLine($"文件\"{worker.FileName}\"已解析");
            Thread.Sleep(200);
        }
    }
    #endregion

    #region Rename
    [ObservableProperty]
    bool _isName = true;

    [ObservableProperty]
    bool _isAmount = true;

    [ObservableProperty]
    bool _isNum = false;

    [RelayCommand]
    void AutoRename()
    {
        foreach (var worker in Workers)
        {
            Console.Write($"文件\"{worker.FileName}\"重命名为");
            worker.AutoRename(IsName, IsAmount, IsNum);
            Console.WriteLine($"\"{worker.FileName}\"");
        }
    }
    #endregion

    #region Export
    public int ExportTypeSelectedIndex
    {
        get => (int)ExportType;
        set
        {
            if (value == (int)ExportType) return;
            ExportType = (ExportType)value;
            OnPropertyChanged(nameof(ExportTypeSelectedIndex));
        }
    }
    ExportType ExportType { get; set; } = ExportType.ToConsole;

    [RelayCommand]
    async Task ExportLinesAsync()
    {
        StringBuilder lines = new();
        foreach (var worker in Workers)
            lines.Append(worker.ToSheetLine());
        if (ExportType == ExportType.ToConsole)
        {
            Console.Write(lines.ToString());
            return;
        }
        // Save as txt
        try
        {
            // Create a file picker
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "出入库信息"
            };
            // Initialize the file picker with the window handle (HWnd).
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindow));
            // Dropdown of file types the user can save the file as
            picker.FileTypeChoices.Add("文本文件", [".txt"]);
            // Open the picker for the user to pick a file
            StorageFile file = await picker.PickSaveFileAsync();
            if (file == null)
            {
                Console.WriteLine("保存已取消");
                return;
            };
            await FileIO.WriteTextAsync(file, lines.ToString());
            Console.WriteLine($"出入库信息已保存至\"{file.Path}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine("保存失败：");
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    async Task ExportNamesAsync()
    {
        StringBuilder names = new();
        foreach (var worker in Workers)
            names.AppendLine($"{worker.Name}{Environment.NewLine}");
        if (ExportType == ExportType.ToConsole)
        {
            Console.Write(names.ToString());
            return;
        }
        // Save as txt
        try
        {
            // Create a file picker
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "商品名称"
            };
            // Initialize the file picker with the window handle (HWnd).
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindow));
            // Dropdown of file types the user can save the file as
            picker.FileTypeChoices.Add("文本文件", [".txt"]);
            // Open the picker for the user to pick a file
            StorageFile file = await picker.PickSaveFileAsync();
            if (file == null)
            {
                Console.WriteLine("保存已取消");
                return;
            };
            await FileIO.WriteTextAsync(file, names.ToString());
            Console.WriteLine($"商品名称已保存至\"{file.Path}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine("保存失败：");
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    async Task ExportNumbersAsync()
    {
        StringBuilder numbers = new();
        foreach (var worker in Workers)
            numbers.Append($"{worker.Number}, ");
        if (numbers.Length > 2) numbers.Remove(numbers.Length - 2, 2);
        if (ExportType == ExportType.ToConsole)
        {
            Console.WriteLine(numbers.ToString());
            return;
        }
        // Save as txt
        try
        {
            // Create a file picker
            FileSavePicker picker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "发票号码"
            };
            // Initialize the file picker with the window handle (HWnd).
            InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(App.MainWindow));
            // Dropdown of file types the user can save the file as
            picker.FileTypeChoices.Add("文本文件", [".txt"]);
            // Open the picker for the user to pick a file
            StorageFile file = await picker.PickSaveFileAsync();
            if (file == null)
            {
                Console.WriteLine("保存已取消");
                return;
            };
            await FileIO.WriteTextAsync(file, numbers.ToString());
            Console.WriteLine($"发票号码已保存至\"{file.Path}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine("保存失败：");
            Console.WriteLine(ex.Message);
        }
    }
    #endregion

    public HomeViewModel()
    {
        void writeChar(char c)
        {
            outStringBuilder.Append(c);
            OnPropertyChanged(nameof(OutString));
        }

        void writeString(string? s)
        {
            if (string.IsNullOrEmpty(s)) return;
            outStringBuilder.Append(s);
            OnPropertyChanged(nameof(OutString));
        }

        Console.SetOut(new OutWriter(writeChar, writeString));
    }

}
