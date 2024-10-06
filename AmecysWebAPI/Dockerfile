# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the solution file and project files for restoring dependencies
COPY *.sln ./
COPY AmecysAPI/AmecysAPI.csproj AmecysAPI/
COPY AmecysAPI.DataAccessLayer/AmecysAPI.DataAccessLayer.csproj AmecysAPI.DataAccessLayer/
COPY AmecysAPI.Domain/AmecysAPI.Domain.csproj AmecysAPI.Domain/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /src/AmecysAPI
RUN dotnet build AmecysAPI.csproj -c Release -o /app

# Publish the application
RUN dotnet publish AmecysAPI.csproj -c Release -o /app

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Expose the port the app will run on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "AmecysAPI.dll"]





