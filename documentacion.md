# Documentación del Proyecto: Generador de Colores (.NET MAUI)

## 1. Diseño de la Interfaz (`MainPage.xaml`)

El diseño se construyó asegurando que los controles descansen sobre una capa translúcida para que el color de fondo generado siempre sea visible. Se utilizó el componente `Grid` como un contenedor principal que abarca toda la pantalla y que será coloreado dinámicamente.

### Color Dinámico como Fondo
```xml
<Grid x:Name="MainContainer" BackgroundColor="#FFFFFF">
```
El `x:Name="MainContainer"` nos permite referenciar el elemento raíz en el código (C#) para cambiar dinámicamente la propiedad `BackgroundColor`.

### Componente de Mostrado y Copiado Hexadecimal
```xml
<Label x:Name="HexLabel" Text="#FFFFFF" TextColor="White" />
<Button Text="Copiar Hexadecimal" Clicked="OnCopyClicked" />
```
El `Label` se encargará de mostrar constantemente el valor hexadecimal que está en memoria, mientras que el `Button` lanza el evento `OnCopyClicked` para realizar la lógica dentro del backend y enviar el String al portapapeles.

### Sliders Manuales de Color
```xml
<Slider x:Name="SliderRed" Minimum="0" Maximum="255" ValueChanged="OnSliderValueChanged" MinimumTrackColor="#FF5252" />
```
Existen 3 `Slider` (rojo, verde y azul). Están configurados de 0 a 255. Poseen un evento `ValueChanged` para que el color se actualice de forma continua e instantánea mientras el usuario arrastra la barra.

## 2. Lógica y Comportamiento (`MainPage.xaml.cs`)

En el "code-behind" se implementan las funciones que responden a las acciones del usuario de la interfaz.

### Prevención de Loops en Randomización
```csharp
private bool isRandomizing = false;
```
Al modificar un slider que pertenece al generador de Random mediante código, el evento `OnSliderValueChanged` es ejecutado automáticamente una y otra vez (debido al framework). Usando esta bandera (flag) `isRandomizing`, evitamos que se recalculen incorrectamente los colores una vez que presionamos el botón de randomizar.

### Transformación de Color y Actualización
```csharp
private void SetColor(int r, int g, int b)
{
    Color color = Color.FromRgb(r, g, b);
    MainContainer.BackgroundColor = color;

    // Forma de forzar un formato estricto Hexadecimal en modo RGB
    string hex = $"#{r:X2}{g:X2}{b:X2}";
    HexLabel.Text = hex;
}
```
Esta función centraliza la asignación del color. Toma valores enteros correspondientes a RGB, construye una instancia `Color` del propio framework y lo inyecta como `BackgroundColor` del layout. Luego genera su formato Hexadecimal y lo plasma en la pantalla.

### Acción del Botón Random
```csharp
private void OnRandomClicked(object sender, EventArgs e)
{
    isRandomizing = true;
    var rnd = new Random();
    int r = rnd.Next(0, 256);
    int g = rnd.Next(0, 256);
    int b = rnd.Next(0, 256);

    // Ajustar los sliders físicamente en la pantalla.
    SliderRed.Value = r;
    SliderGreen.Value = g;
    SliderBlue.Value = b;
    isRandomizing = false;

    SetColor(r, g, b);
}
```
Para asegurar que todo quede sincronizado y coordinado, al calcular colores al azar debemos mover "físicamente" las posiciones de los tres Sliders en base a los números escogidos de manera que el estado de los controles no se disocie con el color mostrado.

### Copiado al Portapapeles (Clipboard)
```csharp
private async void OnCopyClicked(object sender, EventArgs e)
{
    await Clipboard.Default.SetTextAsync(HexLabel.Text);
    await DisplayAlert("Copiado", $"Se ha copiado el color {HexLabel.Text} al portapapeles.", "OK");
}
```
Se hace uso del API nativo abstraído de MAUI `Clipboard` para setear el texto actual que figure en la variable mostrada en pantalla. Posteriormente, levanta una alerta para indicarle al usuario la confirmación a través del SO.
