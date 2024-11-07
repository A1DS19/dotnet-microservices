FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/services/Ordering/Ordering.API/Ordering.API.csproj", "src/services/Ordering/Ordering.API/"]
RUN dotnet restore "src/services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/src/services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ordering.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
