FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./AmecysAPI/AmecysAPI.csproj ./AmecysAPI/
COPY ./AmecysAPI.DataAccessLayer/AmecysAPI.DataAccessLayer.csproj ./AmecysAPI.DataAccessLayer/
COPY ./AmecysAPI.Domain/AmecysAPI.Domain.csproj ./AmecysAPI.Domain/

RUN dotnet restore "AmecysAPI/AmecysAPI.csproj"

COPY . .

WORKDIR "/src/AmecysAPI"
RUN dotnet build "AmecysAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AmecysAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmecysAPI.dll"]






