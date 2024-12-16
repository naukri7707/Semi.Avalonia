using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Semi.Avalonia.Markup.Controls;

namespace Semi.Avalonia.Demo.Pages;

public partial class MarkupDemoViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BindingText))]
    private ButtonStyle.Color _btnColor = ButtonStyle.Color.Primary;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BindingText))]
    private ButtonStyle.Size _btnSize = ButtonStyle.Size.Medium;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BindingText))]
    private ButtonStyle.Variant _btnVariant = ButtonStyle.Variant.Light;

    private int cnt;

    public string BindingText => $"{BtnColor}, {BtnSize}, {BtnVariant}";

    [RelayCommand]
    private void Next(object sender)
    {
        cnt++;

        BtnColor = (ButtonStyle.Color)(cnt % 6);
        BtnSize = (ButtonStyle.Size)(cnt % 3);
        BtnVariant = (ButtonStyle.Variant)(cnt % 4);
    }
}

public partial class MarkupDemo : UserControl
{
    public MarkupDemo()
    {
        InitializeComponent();
        var csMarkup = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 20,
            Children =
            {
                new Button
                {
                    [ButtonStyle.ColorProperty] = ButtonStyle.Color.Secondary,
                    Content = "Normal"
                },
                new Button
                {
                    [!ButtonStyle.ColorProperty] = new Binding(nameof(MarkupDemoViewModel.BtnColor)),
                    [!ButtonStyle.SizeProperty] = new Binding(nameof(MarkupDemoViewModel.BtnSize)),
                    [!ButtonStyle.VariantProperty] = new Binding(nameof(MarkupDemoViewModel.BtnVariant)),
                    [!Button.CommandProperty] = new Binding(nameof(MarkupDemoViewModel.NextCommand)),
                    [!ContentProperty] = new Binding(nameof(MarkupDemoViewModel.BindingText))
                }
            }
        };

        demoContainer.Children.Add(csMarkup);
    }
}