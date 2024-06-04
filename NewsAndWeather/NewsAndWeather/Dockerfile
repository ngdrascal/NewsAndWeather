#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-arm64v8 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy-arm64v8 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewsAndWeather/NewsAndWeather/NewsAndWeather.csproj", "NewsAndWeather/NewsAndWeather/"]
COPY ["ResilientHttpClient/ResilientHttpClient.csproj", "ResilientHttpClient/"]
COPY ["WeatherDataClients/WeatherDataClients.csproj", "WeatherDataClients/"]
COPY ["NewsAndWeather/NewsAndWeather.Client/NewsAndWeather.Client.csproj", "NewsAndWeather/NewsAndWeather.Client/"]
RUN dotnet restore "./NewsAndWeather/NewsAndWeather/NewsAndWeather.csproj"
COPY . .
WORKDIR "/src/NewsAndWeather/NewsAndWeather"
RUN dotnet build "./NewsAndWeather.csproj" -c $BUILD_CONFIGURATION -a arm64 -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewsAndWeather.csproj" -c $BUILD_CONFIGURATION -a arm64 -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAndWeather.dll"]