#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["BeatStore.Web/BeatStore.Web.csproj", "BeatStore.Web/"]
RUN dotnet restore "BeatStore.Web/BeatStore.Web.csproj"
COPY . .
WORKDIR "/src/BeatStore.Web"
RUN dotnet build "BeatStore.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeatStore.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeatStore.Web.dll"]