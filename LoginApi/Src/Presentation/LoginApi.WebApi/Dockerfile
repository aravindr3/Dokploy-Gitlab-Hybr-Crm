# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8082"
EXPOSE 8082


# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files individually (for caching)
COPY LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj LoginApi/Src/Presentation/LoginApi.WebApi/
COPY LoginApi/Src/Core/LoginApi.Application/HyBrCRM.Application.csproj LoginApi/Src/Core/LoginApi.Application/
COPY LoginApi/Src/Core/LoginApi.Domain/HyBrCRM.Domain.csproj LoginApi/Src/Core/LoginApi.Domain/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/HyBrCRM.Infrastructure.Identity.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/HyBrCRM.Infrastructure.Persistence.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/HyBrCRM.Infrastructure.Resources.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/

# Restore dependencies
RUN dotnet restore LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj

# Copy the rest of the source
COPY . .

# Build and publish
WORKDIR /src/LoginApi/Src/Presentation/LoginApi.WebApi
RUN dotnet publish HyBrCRM.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# =========================
# 2. Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8082

ENTRYPOINT ["dotnet", "HyBrCRM.WebApi.dll"]
