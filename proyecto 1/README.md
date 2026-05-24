# ColorGenerator
Proyecto de Apps Móviles realizado por:
   - Jaime Guzmán
   - Martín Pardo
   - Bastián Ríos
## Requisitos Previos

Antes de ejecutar este proyecto, asegúrate de tener instalado lo siguiente en tu dispositivo:

1. [SDK de .NET](https://dotnet.microsoft.com/download) (Compatible con net10 y net9 hasta donde se tiene conocimiento)
2. **Workload de .NET MAUI**: Se puede instalar abriendo una terminal como administrador y ejecutando:
   ```bash
   dotnet workload install maui
   ```
3. **Entorno de Desarrollo (IDE)**:
   - **Visual Studio Code** con las siguientes extensiones instaladas:
     - C# Dev Kit
     - Extension de .NET MAUI
4. Un **emulador de Android** configurado y en ejecución, o un dispositivo físico conectado con el **Modo Desarrollador** y **Depuración USB** activados.

## Cómo ejecutar el proyecto
**MÉTODO RECOMENDADO**

Se debe tener el emulador abierto desde Android Studio y posteriormente desde la consola utilizando el CLI de dotnet:

1. Abrir terminal y navegar hasta la carpeta raíz del proyecto /ColorGenerator.
2. Usar el siguiente comando para ejecutarlo:
   ```bash
   dotnet build -t:Run -f net10.0-android
   ```
   *(Nota:reemplazar con `net9.0-android` en caso de fallo en la build).*
