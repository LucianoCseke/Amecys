# Usa la imagen oficial de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de solución y restaura las dependencias
COPY *.sln ./
COPY AmecysAPI/*.csproj ./AmecysAPI/
COPY AmecysAPI.DataAccessLayer/*.csproj ./AmecysAPI.DataAccessLayer/
COPY AmecysAPI.Domain/*.csproj ./AmecysAPI.Domain/

# Restaura las dependencias
RUN dotnet restore

# Copia el proyecto completo y compila la aplicación
COPY . .
WORKDIR /src/AmecysAPI
RUN dotnet publish -c Release -o /app/publish

# Usa la imagen oficial de .NET runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expone los puertos (ajusta si tu aplicación corre en otro puerto)
EXPOSE 80
EXPOSE 443

# Establece el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "AmecysAPI.dll"]
