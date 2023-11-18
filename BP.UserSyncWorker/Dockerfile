FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KeycloakUserSyncWorker.csproj", "./"]
RUN dotnet restore "KeycloakUserSyncWorker.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "KeycloakUserSyncWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeycloakUserSyncWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeycloakUserSyncWorker.dll"]
