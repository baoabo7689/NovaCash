#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NovaCash.SportsbookWebServices/NovaCash.SportsbookWebServices.csproj", "NovaCash.SportsbookWebServices/"]
COPY ["NovaCash.Sportsbook.Clients/NovaCash.Sportsbook.Clients.csproj", "NovaCash.Sportsbook.Clients/"]
RUN dotnet restore "NovaCash.SportsbookWebServices/NovaCash.SportsbookWebServices.csproj" --source http://nuget.nexdev.net/api/v2/ --source https://api.nuget.org/v3/index.json

COPY . .
WORKDIR "/src/NovaCash.SportsbookWebServices"
RUN dotnet build "NovaCash.SportsbookWebServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NovaCash.SportsbookWebServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "run", "https://localhost:5001/"]