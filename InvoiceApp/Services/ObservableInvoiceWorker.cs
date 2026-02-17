using CommunityToolkit.Mvvm.ComponentModel;
using InvoiceLib.Models;
using InvoiceLib.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace InvoiceApp.Services;

[ObservableObject]
public partial class ObservableInvoiceWorker(InvoiceService invoiceService) : InvoiceWorker(invoiceService)
{
    public string FileName => Path.GetFileName(FilePath) ?? "Invalid file";

    public override string? FilePath
    {
        get => base.FilePath;
        set
        {
            if (value == base.FilePath || !File.Exists(value)) return;
            base.FilePath = value;
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(FileName));
        }
    }

    public override int PageNum
    {
        get => base.PageNum;
        set
        {
            if (value == base.PageNum || value < 1) return;
            base.PageNum = value;
            OnPropertyChanged(nameof(PageNum));
        }
    }

    public override Invoice? Invoice
    {
        get => base.Invoice;
        set
        {
            if (value == base.Invoice) return;
            base.Invoice = value;
            IEnumerable<string> properties =
            [
                nameof(Number),
                nameof(Name),
                nameof(Unit),
                nameof(Price),
                nameof(PriceText),
                nameof(Count),
                nameof(Amount),
                nameof(AmountText),
                nameof(Remarks),
            ];
            foreach (var property in properties)
                OnPropertyChanged(property);
        }
    }

    public string PriceText => $"{Price:f2}";

    public string AmountText => $"{Amount:f2}";

    public string ToSheetLine() =>
        $"{Name}\t{Unit}\t{PriceText}\t{Count}\t{AmountText}\t{Remarks}{Environment.NewLine}";

}
