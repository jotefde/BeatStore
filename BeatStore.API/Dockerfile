FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BeatStore.API/BeatStore.API.csproj", "BeatStore.API/"]
RUN dotnet restore "BeatStore.API/BeatStore.API.csproj"
COPY . .
WORKDIR "/src/BeatStore.API"
RUN dotnet build "BeatStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeatStore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeatStore.API.dll"]