# Calculadora de Propinas

Proyecto de Apps Móviles realizado por:
   - Jaime Guzmán
   - Martín Pardo
   - Bastián Ríos

Esta es una aplicación móvil multiplataforma desarrollada en **.NET MAUI** utilizando el patrón de diseño **MVVM** mediante el paquete **CommunityToolkit.Mvvm**. Permite a los usuarios calcular propinas, dividir cuentas entre múltiples personas y conocer tanto el subtotal como el total a pagar de forma rápida.

## Requisitos Previos

Antes de ejecutar la aplicación, asegúrate de tener instalados los siguientes componentes en tu sistema:

1. **.NET 8.0 SDK** (o superior).
2. La carga de trabajo (workload) de **.NET MAUI**.
   - Para instalarla desde la terminal, ejecuta: `dotnet workload install maui`
3. Un IDE compatible, como:
   - **Visual Studio 2022** (con la carga de trabajo "Desarrollo de la interfaz de usuario de aplicaciones multiplataforma de .NET" seleccionada).
   - **Visual Studio Code** (con la extensión de C# Dev Kit y .NET MAUI instaladas).

## Instrucciones de Ejecución

Sigue estos pasos para compilar y ejecutar la aplicación en tu entorno local.

### 1. Desde la Terminal (CLI)

Abre una terminal, sitúate en el directorio donde se encuentra el archivo `CalculadoraPropinas.csproj` y ejecuta el siguiente comando según la plataforma que desees probar:

- **Para Windows:**
  ```bash
  dotnet build -t:Run -f net10.0-windows10.0.19041.0
  ```
  *(Nota: La versión de `net10.0-windows10...` puede variar dependiendo de la configuración de tu `.csproj`. Si usas .NET 8, utiliza `net8.0-windows10.0.19041.0`)*

- **Para Android (requiere emulador activo o dispositivo físico conectado):**
  ```bash
  dotnet build -t:Run -f net10.0-android
  ```

### 2. Desde Visual Studio 2022

1. Abre el archivo de solución (`CalculadoraPropinas.sln`) o la carpeta del proyecto en Visual Studio.
2. En la barra superior, selecciona tu dispositivo de destino (Ej: *Windows Machine*, *Android Emulator*, etc.).
3. Presiona el botón verde de "Reproducir" (o presiona **F5** para depurar).

### 3. Desde Visual Studio Code

1. Abre la carpeta del proyecto (`CalculadoraPropinas`).
2. Ve a la pestaña **Ejecutar y depurar** (Ctrl+Shift+D).
3. Asegúrate de tener una configuración de lanzamiento lista para MAUI y selecciona tu dispositivo destino en el menú emergente que la extensión te provee, luego presiona **F5**.

## Documentación Adicional

Para más detalles sobre cómo funciona el código, la lógica matemática, y la manera en la que los componentes visuales interactúan mediante MVVM, puedes consultar el archivo [documentacion.md](./documentacion.md) incluido en este repositorio.
