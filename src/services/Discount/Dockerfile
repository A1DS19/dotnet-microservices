FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build the application using the SDK image
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/services/Discount/Discount.GRPC.csproj", "src/services/Discount/"]
RUN dotnet restore "src/services/Discount/Discount.GRPC.csproj"

# Copy the entire source code
COPY . .

# Build the project
WORKDIR "/src/src/services/Discount"
RUN dotnet build "Discount.GRPC.csproj" -c $configuration -o /app/build

# Publish the application
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Discount.GRPC.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# Build the final image
FROM base AS final
WORKDIR /app

# Copy the published application into the final image
COPY --from=publish /app/publish .

# Change ownership of the app directory to the 'app' user
RUN chown -R app:app /app

# Switch to the 'app' user
USER app

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "Discount.GRPC.dll", "--launch-profile", "https"]
