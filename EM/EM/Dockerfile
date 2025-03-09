FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID:$APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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
ENV docker=true
ENTRYPOINT ["dotnet", "EM.dll"]