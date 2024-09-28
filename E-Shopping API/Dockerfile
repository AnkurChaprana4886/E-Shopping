#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["E-Shopping API/E-Shopping API.csproj", "E-Shopping API/"]
COPY ["E-Shopping BAL/E-Shopping BAL.csproj", "E-Shopping BAL/"]
COPY ["E-Shopping Common/E-Shopping Common.csproj", "E-Shopping Common/"]
COPY ["E-Shopping DAL/E-Shopping DAL.csproj", "E-Shopping DAL/"]
RUN dotnet restore "./E-Shopping API/E-Shopping API.csproj"
COPY . .
WORKDIR "/src/E-Shopping API"
RUN dotnet build "./E-Shopping API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-Shopping API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Shopping API.dll"]