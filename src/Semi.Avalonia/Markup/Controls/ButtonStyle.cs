using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Semi.Avalonia.Markups;

namespace Semi.Avalonia.Markup.Controls;

public class ButtonStyle
{
    static ButtonStyle()
    {
        ColorProperty.Changed.Subscribe(new PropertyChangeHandler<Button, Color>(SetColor));
        SizeProperty.Changed.Subscribe(new PropertyChangeHandler<Button, Size>(SetSize));
        VariantProperty.Changed.Subscribe(new PropertyChangeHandler<Button, Variant>(SetVariant));
    }

    public static readonly AttachedProperty<Color> ColorProperty =
        AvaloniaProperty.RegisterAttached<ButtonStyle, Button, Color>(
            nameof(Color)
        );

    public static readonly AttachedProperty<Size> SizeProperty =
        AvaloniaProperty.RegisterAttached<ButtonStyle, Button, Size>(
            nameof(Size),
            Size.Medium
        );

    public static readonly AttachedProperty<Variant> VariantProperty =
        AvaloniaProperty.RegisterAttached<ButtonStyle, Button, Variant>(
            nameof(Variant)
        );

    private static readonly string[] _colorClassNames =
    [
        nameof(Color.Primary),
        nameof(Color.Secondary),
        nameof(Color.Tertiary),
        nameof(Color.Success),
        nameof(Color.Warning),
        nameof(Color.Danger)
    ];

    private static readonly string[] _sizeClassNames =
    [
        nameof(Size.Small),
        nameof(Size.Medium),
        nameof(Size.Large)
    ];

    private static readonly string[] _variantKeys =
    [
        string.Empty, // Variant.Light
        nameof(Variant.Solid),
        nameof(Variant.Outline),
        nameof(Variant.Borderless),
        nameof(Variant.InnerIcon)
    ];

    public static Color GetColor(Button button)
    {
        throw new NotSupportedException();
    }

    public static Size GetSize(Button button)
    {
        throw new NotSupportedException();
    }

    public static Variant GetVariant(Button button)
    {
        throw new NotSupportedException();
    }

    public static void SetColor(Button button, Color color)
    {
        button.Classes.RemoveAll(_colorClassNames);

        var setClassName = _colorClassNames[(int)color];
        button.Classes.Add(setClassName);
    }

    public static void SetSize(Button button, Size size)
    {
        button.Classes.RemoveAll(_sizeClassNames);

        var setClassName = _sizeClassNames[(int)size];
        button.Classes.Add(setClassName);
    }

    public static void SetVariant(Button button, Variant variant)
    {
        var type = button.GetType();
        var buttonType = type.Name;
        var variantKey = _variantKeys[(int)variant];

        object resourceKey = variant switch
        {
            Variant.Light => type,
            _ => $"{variantKey}{buttonType}"
        };

        var dynamicResourceBinding = new DynamicResourceExtension(resourceKey);

        button.Bind(StyledElement.ThemeProperty, dynamicResourceBinding);
    }

    public enum Color
    {
        Primary,

        Secondary,

        Tertiary,

        Success,

        Warning,

        Danger
    }

    public enum Size
    {
        Small,

        Medium,

        Large
    }

    public enum Variant
    {
        Light,

        Solid,

        Outline,

        Borderless,

        InnerIcon
    }
}