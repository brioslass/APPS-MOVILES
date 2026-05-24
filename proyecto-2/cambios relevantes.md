# Cambios Relevantes respecto a la Plantilla Base de .NET MAUI

Este documento detalla todas las modificaciones, configuraciones y patrones arquitectónicos aplicados al proyecto `CalculadoraPropinas` que difieren sustancialmente de la aplicación por defecto ("Hello World" con el botón contador) que entrega `.NET MAUI` al crear un proyecto nuevo.

---

## 1. Patrón Arquitectónico (MVVM)

El cambio más significativo a nivel lógico fue la adopción del patrón **Model-View-ViewModel (MVVM)** en lugar de tener toda la lógica de la aplicación mezclada en el archivo de la vista (`MainPage.xaml.cs` o *Code-Behind*). 

- Requirió instalar e integrar la librería `CommunityToolkit.Mvvm`. En lugar de manipular directamente los elementos visuales (ej. `mylabel.Text = "Hola"`), el patrón exige crear variables "observables" en el ViewModel a las cuales la vista se "suscribe" a través de *Data Bindings* (`{Binding ...}`).
- **Ventaja:** Permite que la interfaz de usuario esté completamente separada de la lógica matemática de la calculadora de propinas.

## 2. Eliminación de la Barra de Navegación (NavBar)

La plantilla de MAUI por defecto usa `AppShell` para proveer navegación básica y una barra de título superior (típicamente blanca y con la palabra "Home"). 

- **El cambio:** Para cumplir con el diseño inmersivo y de tema oscuro (`#212121`), se ocultó globalmente esta barra usando la propiedad `Shell.NavBarIsVisible="False"` dentro de `AppShell.xaml`. 
- **El desafío técnico:** Descubrimos que en Android, al ocultar esta barra en combinación con la Aceleración por Hardware del emulador, se producía un error de renderizado en el primer inicio (pantalla negra). Diagnosticar esto requiere experiencia nativa sobre cómo el sistema operativo Android dibuja los buffers gráficos y su interacción con las vistas previas (Splash Screens).

## 3. Reemplazo del Componente `Frame` por `Border`

En la aplicación original, MAUI proveía `Frame` para hacer contenedores con bordes redondeados. Sin embargo, en versiones recientes de .NET 9 y .NET 10, este componente ha quedado obsoleto.

- **El cambio:** Se reconstruyeron las cajas de la UI que contienen los totales utilizando `<Border>`. Este componente es más moderno y eficiente en consumo de memoria, pero requiere configurar sus bordes de una manera ligeramente distinta, usando la propiedad `StrokeShape="RoundRectangle 10"` y quitando el grosor del borde con `Stroke="Transparent"`.

## 4. Archivos con Mayor Carga de Trabajo

Los dos archivos principales que sostienen casi la totalidad de la complejidad técnica de la aplicación son:

### A. `ViewModels/MainViewModel.cs`
- **Rol:** Es el cerebro de la aplicación.
- **Complejidad:** Contiene toda la lógica matemática reactiva. Se utilizaron decoradores `[ObservableProperty]` que automáticamente generan el código repetitivo en segundo plano. Al cambiar propiedades como `PorcentajePropina` a través del control deslizante (Slider), el método parcial `OnPorcentajePropinaChanged` detecta el cambio instantáneamente y recalcula en cadena la propina, el subtotal por persona y el total general.
- **Detalle:** Maneja validaciones lógicas importantes, como evitar que la cantidad de personas (el divisor) sea menor a 1, previniendo así un error de división por cero.

### B. `MainPage.xaml`
- **Rol:** Es el rostro de la aplicación (La Interfaz Gráfica).
- **Complejidad:** Se eliminó por completo el `StackLayout` y el `ScrollView` originales de la plantilla de MAUI, en favor de un `<Grid>` maestro. 
- **Detalle:** Un `Grid` es mucho más potente pero requiere diseñar la pantalla como una hoja de cálculo invisible, definiendo filas y columnas precisas (`RowDefinitions="Auto, Auto, *, ..."`). Aquí también se empleó el formateo avanzado de Strings dentro del XML (`StringFormat='PROPINA ({0:F0}%)'`) para mostrar los valores con 0 o 2 decimales según los requisitos del PDF, delegando todo el trabajo visual al motor XAML en lugar de calcular cadenas de texto en C#.

