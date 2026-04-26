using Microsoft.Maui.Graphics;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;

namespace ColorGenerator;

public partial class MainPage : ContentPage
{
    private bool isRandomizing = false;

    public MainPage()
    {
        InitializeComponent();
        UpdateColorFromSliders();
    }

    private void OnSliderValueChanged(object? sender, ValueChangedEventArgs e)
    {
        if (isRandomizing) return;
        UpdateColorFromSliders();
    }

    private void UpdateColorFromSliders()
    {
        int r = (int)SliderRed.Value;
        int g = (int)SliderGreen.Value;
        int b = (int)SliderBlue.Value;

        SetColor(r, g, b);
    }

    private void OnRandomClicked(object? sender, EventArgs e)
    {
        isRandomizing = true;
        var rnd = new Random();
        int r = rnd.Next(0, 256);
        int g = rnd.Next(0, 256);
        int b = rnd.Next(0, 256);

        SliderRed.Value = r;
        SliderGreen.Value = g;
        SliderBlue.Value = b;
        isRandomizing = false;

        SetColor(r, g, b);
    }

    private void SetColor(int r, int g, int b)
    {
        // Set the background color
        Color color = Color.FromRgb(r, g, b);
        MainContainer.BackgroundColor = color;

        if (RandomButton != null)
        {
            RandomButton.BackgroundColor = color;
            // Contrast logic for text color to keep the button reading clearly
            double luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;
            RandomButton.TextColor = luminance > 0.5 ? Colors.Black : Colors.White;
        }

        // Format to pure HEX #RRGGBB
        string hex = $"#{r:X2}{g:X2}{b:X2}";
        HexLabel.Text = hex;
    }

    private async void OnCopyClicked(object? sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(HexLabel.Text);
        // Según la advertencia de .NET 10, usamos DisplayAlertAsync
        await DisplayAlertAsync("Copiado", $"Se ha copiado el color {HexLabel.Text} al portapapeles.", "OK");
    }
}
