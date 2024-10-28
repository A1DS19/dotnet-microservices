FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/services/Catalog/Catalog.API.csproj", "src/services/Catalog/"]
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "src/services/Catalog/Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/services/Catalog"
RUN dotnet build "Catalog.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Catalog.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
