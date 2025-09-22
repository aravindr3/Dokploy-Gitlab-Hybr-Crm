# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj", "LoginApi/Src/Presentation/LoginApi.WebApi/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/HyBrCRM.Infrastructure.Identity.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/"]
COPY ["LoginApi/Src/Core/LoginApi.Application/HyBrCRM.Application.csproj", "LoginApi/Src/Core/LoginApi.Application/"]
COPY ["LoginApi/Src/Core/LoginApi.Domain/HyBrCRM.Domain.csproj", "LoginApi/Src/Core/LoginApi.Domain/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/HyBrCRM.Infrastructure.Resources.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/HyBrCRM.Infrastructure.Persistence.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/"]
RUN dotnet restore "./LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj"
COPY . .
WORKDIR "/src/LoginApi/Src/Presentation/LoginApi.WebApi"
RUN dotnet build "./HyBrCRM.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HyBrCRM.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HyBrCRM.WebApi.dll"]