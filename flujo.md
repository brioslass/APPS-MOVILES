# Flujo de Interacción y Arquitectura (ColorGenerator)

Este documento explica cómo interactúan los distintos componentes de la aplicación cuando el usuario la utiliza. La arquitectura principal se basa en el patrón estándar de .NET MAUI, separando la interfaz de usuario (XAML) de la lógica de negocio (C#).

## 1. Archivos Principales

* **`MainPage.xaml`** Es el archivo que define la interfaz visual. Contiene el contenedor principal (`MainContainer`), los textos (`HexLabel`), los botones y los tres sliders (Rojo, Verde y Azul).
* **`MainPage.xaml.cs`** Contiene la lógica en segundo plano. Escucha las interacciones que el usuario hace en la pantalla y ejecuta los cálculos matemáticos y las llamadas al sistema.

## 2. Inicialización de la App (Lo que ocurre al abrir la app)

1.  El motor de .NET MAUI (`MauiProgram.cs` y `App.xaml`) levanta la aplicación y carga `MainPage` como la pantalla principal de inicio.
2.  El constructor de `MainPage` ejecuta `InitializeComponent()` para dibujar la interfaz y luego llama inmediatamente a `UpdateColorFromSliders()`.
3.  Esto asegura que la app no inicie en blanco, sino que lea la posición inicial por defecto de los sliders y pinte el fondo desde el primer segundo.

## 3. Flujo 1: El usuario mueve un Slider (Interacción Manual)

Cuando el usuario arrastra la barra de cualquiera de los tres colores (Rojo, Verde o Azul):

1.  **Disparador:** La vista (`MainPage.xaml`) detecta el movimiento y dispara el evento `OnSliderValueChanged` en el controlador.
2.  **Validación de Seguridad:** El código verifica la variable `isRandomizing`. Si es `true` (significa que la app está moviendo los sliders automáticamente), ignora el evento para evitar un bucle infinito. Si es `false`, continúa.
3.  **Recolección de Datos:** Se extraen los valores actuales (de 0 a 255) de los tres sliders mediante `UpdateColorFromSliders()`.
4.  **Actualización Visual (`SetColor`):**
    * Se crea un nuevo objeto `Color` de MAUI y se aplica al fondo del contenedor principal.
    * Se calcula la **luminancia** del nuevo color para determinar si el texto del botón debe ser blanco o negro (para garantizar que siempre se pueda leer).
    * Se transforma el valor RGB a código Hexadecimal y se actualiza el texto en la pantalla (`HexLabel`).

## 4. Flujo 2: El usuario presiona el botón "Random" (Generación Automática)

Cuando el usuario decide generar un color al azar:

1.  **Disparador:** Se activa el evento `OnRandomClicked`.
2.  **Bloqueo de UI:** La variable `isRandomizing` pasa a `true`. Esto "silencia" los sliders temporalmente.
3.  **Cálculo:** Se generan tres números aleatorios entre 0 y 255 para R, G y B.
4.  **Sincronización:** El código "mueve" físicamente los tres sliders en la pantalla a las nuevas posiciones calculadas. (Al moverse, intentarán disparar el evento del Flujo 1, pero como `isRandomizing` es `true`, el ciclo se corta).
5.  **Desbloqueo y Pintado:** Se vuelve `isRandomizing` a `false` y se invoca manualmente a `SetColor()` para pintar la pantalla y actualizar los textos con la misma lógica del Flujo 1.

## 5. Flujo 3: El usuario presiona "Copiar Hexadecimal" (Interacción con el Sistema)

1.  **Disparador:** Se ejecuta `OnCopyClicked`.
2.  **Comunicación con el SO:** La app utiliza la API nativa `Clipboard.Default.SetTextAsync` de MAUI para inyectar el código Hexadecimal actual (que está en `HexLabel`) directamente en el portapapeles del dispositivo (Android, iOS o Windows).
3.  **Feedback:** Finalmente, se dispara una alerta en pantalla (`DisplayAlertAsync`) que le notifica al usuario que el texto fue copiado exitosamente.
