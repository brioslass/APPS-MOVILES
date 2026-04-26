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

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
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

    private void OnRandomClicked(object sender, EventArgs e)
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

        // Format to pure HEX #RRGGBB
        string hex = $"#{r:X2}{g:X2}{b:X2}";
        HexLabel.Text = hex;
    }

    private async void OnCopyClicked(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(HexLabel.Text);
        await DisplayAlert("Copiado", $"Se ha copiado el color {HexLabel.Text} al portapapeles.", "OK");
    }
}
