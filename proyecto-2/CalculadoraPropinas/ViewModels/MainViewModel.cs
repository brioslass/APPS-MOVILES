using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculadoraPropinas.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private decimal totalBoleta;

    [ObservableProperty]
    private int numeroPersonas = 1;

    [ObservableProperty]
    private double porcentajePropina = 10; 

    [ObservableProperty]
    private decimal subtotalPorPersona;

    [ObservableProperty]
    private decimal propinaPorPersona;

    [ObservableProperty]
    private decimal totalPorPersona;

    [ObservableProperty]
    private decimal totalGeneral;

    public MainViewModel()
    {
        CalcularValores();
    }

    partial void OnTotalBoletaChanged(decimal value)
    {
        CalcularValores();
    }

    partial void OnNumeroPersonasChanged(int value)
    {
        CalcularValores();
    }

    partial void OnPorcentajePropinaChanged(double value)
    {
        // Redondear a entero (0 decimales) para que no tenga más de 2 dígitos (ej: 15)
        var rounded = Math.Round(value, 0);
        if (value != rounded)
        {
            PorcentajePropina = rounded;
            return;
        }
        CalcularValores();
    }

    private void CalcularValores()
    {
        if (NumeroPersonas < 1)
            NumeroPersonas = 1;

        SubtotalPorPersona = TotalBoleta / NumeroPersonas;
        decimal propinaTotal = TotalBoleta * ((decimal)PorcentajePropina / 100m);
        PropinaPorPersona = propinaTotal / NumeroPersonas;
        TotalPorPersona = SubtotalPorPersona + PropinaPorPersona;
        
        TotalGeneral = TotalBoleta + propinaTotal;
    }

    [RelayCommand]
    private void ElegirPropina(string porcentajeStr)
    {
        if (double.TryParse(porcentajeStr, out double porcentaje))
        {
            PorcentajePropina = porcentaje;
        }
    }

    [RelayCommand]
    private void DividirMas()
    {
        NumeroPersonas++;
    }

    [RelayCommand]
    private void DividirMenos()
    {
        if (NumeroPersonas > 1)
        {
            NumeroPersonas--;
        }
    }
}
