# EM Academy

Este proyecto es una aplicación web desarrollada con ASP.NET Core y Entity Framework Core.

## Requisitos

- .NET SDK 9.0
- Docker

## Configuración del Proyecto

1. Clona el repositorio:
    ```sh
    git clone https://github.com/tu-usuario/EM_academy.git
    cd EM_academy
    ```

2. Restaura las dependencias de NuGet:
    ```sh
    dotnet restore
    ```

3. Configura las variables de entorno necesarias. Puedes usar el archivo `appsettings.Development.json` para configuración local.

## Despliegue en Docker

### Construcción y Ejecución Local

1. Construye la imagen de Docker:
    ```sh
    docker build -t em_academy:latest -f EM/EM/Dockerfile .
    ```

2. Ejecuta el contenedor:
    ```sh
    docker run -d -p 8080:80 --name em_academy_container em_academy:latest
    ```

3. La aplicación estará disponible en `http://localhost:8080`.