# ColorGenerator

Una aplicación móvil desarrollada con .NET MAUI.

## Requisitos Previos

Antes de ejecutar este proyecto, asegúrate de tener instalado lo siguiente en tu dispositivo:

1. [SDK de .NET](https://dotnet.microsoft.com/download) (versión correspondiente a la que usa el proyecto, por ejemplo .NET 8 o .NET 9).
2. **Workload de .NET MAUI**: Puedes instalarlo abriendo una terminal como administrador y ejecutando:
   ```bash
   dotnet workload install maui
   ```
3. **Entorno de Desarrollo (IDE)**:
   - **Visual Studio 2022**: Asegúrate de incluir la carga de trabajo de "Desarrollo de IU de aplicaciones multiplataforma de .NET" (.NET MAUI).
   - O **Visual Studio Code** con las siguientes extensiones instaladas:
     - C# Dev Kit
     - Extension de .NET MAUI
4. Un **emulador de Android** configurado y en ejecución (el cual ya tienes), o un dispositivo físico conectado con el **Modo Desarrollador** y **Depuración USB** activados.

## Cómo ejecutar el proyecto

### Opción 1: Usando Visual Studio 2022 (Recomendado)

1. Abre el archivo de solución (`.sln`) o el archivo del proyecto (`ColorGenerator.csproj`) en Visual Studio.
2. Espera unos segundos a que se restauren y descarguen las dependencias necesarias.
3. En la barra de herramientas superior central, asegúrate de que tu emulador esté seleccionado en el menú desplegable (al lado del botón de "Play").
4. Haz clic en el botón de reproducción verde (o presiona la tecla `F5`). Visual Studio compilará la aplicación y la abrirá automáticamente en tu emulador.

### Opción 2: Usando Visual Studio Code

1. Abre la carpeta del proyecto (`ColorGenerator`) en Visual Studio Code.
2. Abre la paleta de comandos (`Ctrl+Shift+P`).
3. Escribe y selecciona `.NET MAUI: Select Debug Target` (o "Seleccionar destino de depuración") y elige tu emulador activo en la lista.
4. Presiona la tecla `F5` o ve al panel izquierdo de "Ejecución y depuración" y haz clic en el botón verde "Play" para iniciar. 

### Opción 3: Usando la Terminal (CLI de dotnet)

Si prefieres usar la consola y ya tienes tu emulador abierto:

1. Abre tu terminal y navega hasta la carpeta raíz del proyecto (donde está el archivo `ColorGenerator.csproj`).
2. Dependiendo de la versión de .NET de tu proyecto, usa el siguiente comando para ejecutarlo:
   ```bash
   dotnet build -t:Run -f net9.0-android
   ```
   *(Nota: si tu proyecto utiliza otra versión como .NET 8, reemplaza `net9.0-android` por `net8.0-android`).*

## 🛠 Solución de problemas comunes

- **El emulador no es reconocido:** Cierra el emulador y ábrelo nuevamente. Si usas Android Studio, inicia el emulador desde el `Device Manager`.
- **Faltan cargas de trabajo (Workloads):** Si te da errores de compilación relacionados a MAUI, ejecuta `dotnet workload restore` en la carpeta del proyecto.
- **Advertencias de línea de finalización en Git:** Si ves advertencias de tipo `LF will be replaced by CRLF`, no te preocupes, están relacionadas a los archivos autogenerados en la carpeta `obj`.
