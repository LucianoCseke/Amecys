FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto
COPY ["AmecysAPI/AmecysAPI.csproj", "AmecysAPI/"]
COPY ["AmecysAPI.DataAccessLayer/AmecysAPI.DataAccessLayer.csproj", "AmecysAPI.DataAccessLayer/"]
COPY ["AmecysAPI.Domain/AmecysAPI.Domain.csproj", "AmecysAPI.Domain/"]

# Restaurar dependencias
RUN dotnet restore "AmecysAPI/AmecysAPI.csproj"

# Copiar el resto del código
COPY . .

# Compilar la aplicación
RUN dotnet build "AmecysAPI/AmecysAPI.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "AmecysAPI/AmecysAPI.csproj" -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Comando de inicio
ENTRYPOINT ["dotnet", "AmecysAPI.dll"]





