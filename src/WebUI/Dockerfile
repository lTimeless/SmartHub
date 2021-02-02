#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# Build .Net core App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt install -y nodejs
WORKDIR /src
COPY ["src/WebUI.csproj", "src/WebUI/"]
COPY ["src/Infrastructure.Shared.csproj", "src/Infrastructured/"]
COPY ["src/Application.csproj", "src/Application/"]
COPY ["shared/BasePlugin.csproj", "shared/BasePlugin/"]
COPY ["src/Domain.csproj", "src/Domain/"]
RUN dotnet restore "src/WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

# Publish .Net core App
FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartHub.Api.dll"]