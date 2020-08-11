#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SmartHub.Api/SmartHub.Api.csproj", "SmartHub.Api/"]
COPY ["SmartHub.Infrastructure/SmartHub.Infrastructure.csproj", "SmartHub.Infrastructure/"]
COPY ["SmartHub.Application/SmartHub.Application.csproj", "SmartHub.Application/"]
COPY ["SmartHub.BasePlugin/SmartHub.BasePlugin.csproj", "SmartHub.BasePlugin/"]
COPY ["SmartHub.Domain/SmartHub.Domain.csproj", "SmartHub.Domain/"]
RUN dotnet restore "SmartHub.Api/SmartHub.Api.csproj"
COPY . .
WORKDIR "/src/SmartHub.Api"
RUN dotnet build "SmartHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartHub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartHub.Api.dll"]