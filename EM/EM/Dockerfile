FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID:$APP_UID
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

 # Instalar dependencias de sistema
     RUN apt update && apt install -y curl libgdiplus libc6-dev && \
         apt clean && rm -rf /var/lib/apt/lists/*
    # Instalar herramientas adicionales
    RUN apt update && apt install -y git vim nano && \
        apt clean && rm -rf /var/lib/apt/lists/* 

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EM/EM.csproj", "EM/"]
RUN dotnet restore "EM/EM.csproj"
COPY . .
WORKDIR "/src/EM"
RUN dotnet build "EM.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EM.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false && \
    rm -rf /root/.nuget/packages && \
    rm -rf /src

FROM base AS final
WORKDIR /app
COPY --chown=$APP_UID:$APP_UID --from=publish /app/publish .
RUN mkdir -p /app/data
RUN chmod -R 755 /app/data
RUN chmod -R 755 /app/Rotativa
RUN chmod +x /app/bin/Debug/net9.0/Rotativa/wkhtmltopdf.exe
RUN chmod +x /app/Rotativa/wkhtmltopdf.exe
VOLUME /app/data
VOLUME /app/Rotativa
VOLUME /app/.aspnet/DataProtection-Keys
VOLUME /app/wwwroot
ENV docker=true

    # For Rotativa SDK
RUN apt update && \
    apt install -y fontconfig libfreetype6 libpng16-16 libx11-6 libxcb1 libxext6 libxrender1 xfonts-75dpi xfonts-base libgdiplus libc6-dev wkhtmltopdf && \
    apt clean && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "EM.dll"]