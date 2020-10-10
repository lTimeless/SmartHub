#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://*:5000

# Build .Net core App
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SmartHub.Api/SmartHub.Api.csproj", "SmartHub.Api/"]
COPY ["SmartHub.Infrastructure.Shared/SmartHub.Infrastructure.Shared.csproj", "SmartHub.Infrastructure.Shared/"]
COPY ["SmartHub.Application/SmartHub.Application.csproj", "SmartHub.Application/"]
COPY ["SmartHub.BasePlugin/SmartHub.BasePlugin.csproj", "SmartHub.BasePlugin/"]
COPY ["SmartHub.Domain/SmartHub.Domain.csproj", "SmartHub.Domain/"]
COPY ["SmartHub.Infrastructure.Persistence/SmartHub.Infrastructure.Persistence.csproj", "SmartHub.Infrastructure.Persistence/"]
RUN dotnet restore "SmartHub.Api/SmartHub.Api.csproj"
COPY . .
WORKDIR "/src/SmartHub.Api"
RUN dotnet build "SmartHub.Api.csproj" -c Release -o /app/build

# Vue app:
FROM node:alpine as buildvue
WORKDIR /src
# Npm install
COPY SmartHub.Ui/package.json .
RUN npm install
# Vue build
COPY SmartHub.Ui .
RUN npm run build

# Publish .Net core App
FROM build AS publish
RUN dotnet publish "SmartHub.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
# copy .net content
COPY --from=publish /app/publish .

# copy vue content into .net's static files folder:
COPY --from=buildvue /src/dist /app/wwwroot

ENTRYPOINT ["dotnet", "SmartHub.Api.dll"]