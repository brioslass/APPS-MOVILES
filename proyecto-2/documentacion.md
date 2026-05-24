# Documentación de CalculadoraPropinas

Este documento describe la estructura y el comportamiento de las funciones implementadas en la aplicación `.NET MAUI` para el cálculo de propinas, siguiendo el patrón MVVM a través del uso de `CommunityToolkit.Mvvm`.

## Estructura MVVM

La lógica de la aplicación se encuentra separada de la vista (`MainPage.xaml`) dentro de la clase `MainViewModel.cs`. El `MainViewModel` hereda de `ObservableObject`, lo cual permite notificar a la interfaz de usuario automáticamente cuando cambian los valores de las propiedades.

Para conectar el ViewModel a la Vista, se utiliza el patrón de **Inyección de Dependencias** (Dependency Injection).
1. En `MauiProgram.cs` se registran ambos componentes (`builder.Services.AddTransient<MainViewModel>();` y `builder.Services.AddTransient<MainPage>();`).
2. La vista `MainPage.xaml.cs` recibe el ViewModel por su constructor (`public MainPage(ViewModels.MainViewModel viewModel)`) y lo asigna a su contexto (`BindingContext = viewModel;`).
3. Esto garantiza un bajo acoplamiento y facilita enormemente las pruebas unitarias.

## Propiedades y su Comportamiento

Se utilizaron atributos `[ObservableProperty]` para generar propiedades reactivas. Cuando estas propiedades cambian, la interfaz de usuario que está vinculada (Bindeada) a ellas se actualiza automáticamente.

- **`TotalBoleta` (decimal):** Representa el monto total de la cuenta ingresado por el usuario. Cuando cambia, se ejecuta automáticamente el método `OnTotalBoletaChanged`.
- **`NumeroPersonas` (int):** Indica entre cuántas personas se dividirá la cuenta. Su valor mínimo es 1. Al cambiar, dispara `OnNumeroPersonasChanged`.
- **`PorcentajePropina` (int):** Representa el porcentaje de propina seleccionado (de 0 a 50). Se encuentra enlazado bidireccionalmente al `Slider`. Al cambiar, dispara `OnPorcentajePropinaChanged`.
- **`SubtotalPorPersona` (decimal):** Almacena el valor resultante del total dividido por el número de personas (sin incluir propina).
- **`PropinaPorPersona` (decimal):** Almacena la cantidad específica de dinero de propina que debe pagar cada persona.
- **`TotalPorPersona` (decimal):** La suma de `SubtotalPorPersona` y `PropinaPorPersona`. Representa el total final que paga cada persona.

## Métodos y Cálculos

### `CalcularValores()`
Este es el método central encargado de la lógica de negocio. Se ejecuta cada vez que el usuario modifica el valor de la boleta, la propina o la cantidad de personas.

**Comportamiento:**
1. Valida que `NumeroPersonas` nunca sea menor a 1.
2. Calcula el subtotal dividiendo `TotalBoleta` entre `NumeroPersonas`.
3. Calcula la cantidad de propina basándose en el porcentaje actual (`TotalBoleta * (PorcentajePropina / 100)`) y luego lo divide por la cantidad de personas.
4. Calcula el total por persona sumando el subtotal y la propina.

### Métodos Parciales de Cambio
Gracias al Toolkit, se generaron estos métodos que se ejecutan automáticamente cuando cambia la propiedad asociada:
- `OnTotalBoletaChanged(decimal value)`
- `OnNumeroPersonasChanged(int value)`
- `OnPorcentajePropinaChanged(int value)`

Todos estos métodos llaman internamente a `CalcularValores()` para asegurar que la vista siempre refleje datos consistentes.

## Comandos (Interacción del Usuario)

Los botones en la interfaz de usuario (`MainPage.xaml`) no tienen eventos `Click` tradicionales (como en code-behind), sino que utilizan el atributo `Command` que los enlaza a los siguientes métodos marcados con `[RelayCommand]`:

### `ElegirPropina(string porcentajeStr)`
Se dispara al presionar los botones de "10%", "15%" y "20%".
**Interacción:** Recibe el parámetro del botón como un texto (string), lo convierte a `double` y actualiza la propiedad `PorcentajePropina`. Debido al Binding bidireccional, esto hace que el `Slider` (que maneja valores double) se mueva automáticamente a la posición correcta y dispara el recálculo de los totales.

### `DividirMas()`
Se dispara al presionar el botón `+`.
**Interacción:** Incrementa en 1 la propiedad `NumeroPersonas`, lo que a su vez dispara un recálculo automático.

### `DividirMenos()`
Se dispara al presionar el botón `-`.
**Interacción:** Comprueba que `NumeroPersonas` sea estrictamente mayor que 1 antes de restar. De este modo evita errores matemáticos por dividir por cero y previene valores ilógicos. Al modificar la propiedad, se recalcula todo.
