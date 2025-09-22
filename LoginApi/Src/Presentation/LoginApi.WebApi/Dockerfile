# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Mountable folder for Data Protection keys
VOLUME /keys

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj", "LoginApi/Src/Presentation/LoginApi.WebApi/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/HyBrCRM.Infrastructure.Identity.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/"]
COPY ["LoginApi/Src/Core/LoginApi.Application/HyBrCRM.Application.csproj", "LoginApi/Src/Core/LoginApi.Application/"]
COPY ["LoginApi/Src/Core/LoginApi.Domain/HyBrCRM.Domain.csproj", "LoginApi/Src/Core/LoginApi.Domain/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/HyBrCRM.Infrastructure.Resources.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/HyBrCRM.Infrastructure.Persistence.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/"]

RUN dotnet restore "./LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/LoginApi/Src/Presentation/LoginApi.WebApi"
RUN dotnet build "./HyBrCRM.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HyBrCRM.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Optional: copy wwwroot if your Web API serves static files (Swagger UI, SPA, etc.)
COPY LoginApi/Src/Presentation/LoginApi.WebApi/wwwroot /app/wwwroot

ENTRYPOINT ["dotnet", "HyBrCRM.WebApi.dll"]
