# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8082

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj", "LoginApi/Src/Presentation/LoginApi.WebApi/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/HyBrCRM.Infrastructure.Identity.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Identity/"]
COPY ["LoginApi/Src/Core/LoginApi.Application/HyBrCRM.Application.csproj", "LoginApi/Src/Core/LoginApi.Application/"]
COPY ["LoginApi/Src/Core/LoginApi.Domain/HyBrCRM.Domain.csproj", "LoginApi/Src/Core/LoginApi.Domain/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/HyBrCRM.Infrastructure.Resources.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Resources/"]
COPY ["LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/HyBrCRM.Infrastructure.Persistence.csproj", "LoginApi/Src/Infrastructure/LoginApi.Infrastructure.Persistence/"]

# Restore dependencies
RUN dotnet restore "LoginApi/Src/Presentation/LoginApi.WebApi/HyBrCRM.WebApi.csproj"

# Copy all source code and build
COPY . .
WORKDIR "/src/LoginApi/Src/Presentation/LoginApi.WebApi"
RUN dotnet build "HyBrCRM.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "HyBrCRM.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Ensure the app listens on all interfaces
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_HOST=0.0.0.0
ENTRYPOINT ["dotnet", "HyBrCRM.WebApi.dll"]
