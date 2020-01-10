#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NovaCash.SportsbookServices/NovaCash.SportsbookServices.csproj", "NovaCash.SportsbookServices/"]
RUN dotnet restore "NovaCash.SportsbookServices/NovaCash.SportsbookServices.csproj"
COPY . .
WORKDIR "/src/NovaCash.SportsbookServices"
RUN dotnet build "NovaCash.SportsbookServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NovaCash.SportsbookServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NovaCash.SportsbookServices.dll"]