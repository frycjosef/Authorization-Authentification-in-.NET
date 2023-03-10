FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App1/App1.csproj", "App1/"]
RUN dotnet restore "App1/App1.csproj"
COPY . .
WORKDIR "/src/App1"
RUN dotnet build "App1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App1.dll"]
