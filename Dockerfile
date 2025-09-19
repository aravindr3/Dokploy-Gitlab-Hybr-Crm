# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

## Copy project files individually (for caching)
COPY LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj LoginApi/Src/Presentation/LoginApi.WebApi/
COPY LoginApi/Src/Core/LoginApi.Application/HyBrCRM.Application.csproj LoginApi/Src/Core/LoginApi.Application/
COPY LoginApi/Src/Core/LoginApi.Domain/HyBrCRM.Domain.csproj LoginApi/Src/Core/LoginApi.Domain/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/HyBrCRM.Infrastructure.Identity.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/HyBrCRM.Infrastructure.Persistence.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/
COPY LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/HyBrCRM.Infrastructure.Resources.csproj LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/
RUN dotnet restore LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj

COPY . .
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port for Dokploy/Traefik
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HyBrCRM.WebApi.dll"]
