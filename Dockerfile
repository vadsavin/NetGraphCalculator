FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Set working directory
WORKDIR /app

# Copy the application code from the current context
COPY . .

# Restore dependencies using NuGet
RUN dotnet restore WebApp/WebApp.csproj

# Publish the application for deployment
RUN dotnet publish WebApp/WebApp.csproj -c Release -o out

# Expose port for the web application (adjust if needed)
EXPOSE 8080

# Run the published application
CMD ["dotnet", "out/WebApp.dll"]
