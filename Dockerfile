# Etapa base con runtime ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa build con SDK .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el código fuente
COPY . .

# Restaurar dependencias solo para el proyecto MVC (ajusta la ruta si está en otra carpeta)
RUN dotnet restore Proyecto.Api.MVC/Proyecto.Api.MVC.csproj

# Publicar en modo Release, salida en /app/publish
RUN dotnet publish Proyecto.Api.MVC/Proyecto.Api.MVC.csproj -c Release -o /app/publish

# Etapa final, copiar los archivos publicados y configurar entrypoint
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Proyecto.Api.MVC.dll"]
