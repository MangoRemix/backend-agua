# Stage 1: Build y Publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar archivos de proyecto y restaurar dependencias
COPY ["backend-agua.csproj", "./"]
RUN dotnet restore "./backend-agua.csproj"

# Copiar todo el código y publicar la app
COPY . .
RUN dotnet publish "./backend-agua.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer puerto 8080 (estándar de .NET 8+)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "backend-agua.dll"]
