# Usar la imagen de SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos .csproj y restaurar dependencias
COPY ["AmecysAPI/AmecysAPI.csproj", "AmecysAPI/"]
COPY ["AmecysAPI.DataAccessLayer/AmecysAPI.DataAccessLayer.csproj", "AmecysAPI.DataAccessLayer/"]
COPY ["AmecysAPI.Domain/AmecysAPI.Domain.csproj", "AmecysAPI.Domain/"]

RUN dotnet restore "AmecysAPI/AmecysAPI.csproj"

# Copiar el resto de los archivos y compilar
COPY . .

# Cambiar al directorio del proyecto principal
WORKDIR "/src/AmecysAPI"
RUN dotnet build "AmecysAPI.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "AmecysAPI.csproj" -c Release -o /app/publish

# Generar la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmecysAPI.dll"]






