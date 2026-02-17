using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace InvoiceApp.Extensions;

public class ScrollViewExtension
{
    public static readonly DependencyProperty AutoScrollToEndProperty = DependencyProperty.RegisterAttached(
        "AutoScrollToEnd",
        typeof(bool),
        typeof(ScrollViewExtension),
        new PropertyMetadata(false, OnAutoScrollToEndPropertyChanged));

    public static bool GetAutoScrollToEnd(UIElement element) => (bool)element.GetValue(AutoScrollToEndProperty);

    public static void SetAutoScrollToEnd(UIElement element, bool value) => element.SetValue(AutoScrollToEndProperty, value);

    static void OnAutoScrollToEndPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ScrollView view) return;
        var value = (bool)e.NewValue;
        if (value) view.ExtentChanged += View_ExtentChanged;
        else view.ExtentChanged -= View_ExtentChanged;
    }

    static void View_ExtentChanged(ScrollView sender, object args) => 
        sender.ScrollTo(sender.HorizontalOffset, sender.ScrollableHeight);
}
