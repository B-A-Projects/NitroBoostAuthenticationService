#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NitroBoostAuthenticationService.Web/NitroBoostAuthenticationService.Web.csproj", "NitroBoostAuthenticationService.Web/"]
COPY ["NitroBoostAuthenticationService.Core/NitroBoostAuthenticationService.Core.csproj", "NitroBoostAuthenticationService.Core/"]
COPY ["NitroBoostAuthenticationService.Data/NitroBoostAuthenticationService.Data.csproj", "NitroBoostAuthenticationService.Data/"]
COPY ["NitroBoostAuthenticationService.Shared/NitroBoostAuthenticationService.Shared.csproj", "NitroBoostAuthenticationService.Shared/"]
RUN dotnet restore "NitroBoostAuthenticationService.Web/NitroBoostAuthenticationService.Web.csproj"
COPY . .
WORKDIR "/src/NitroBoostAuthenticationService.Web"
RUN dotnet build "NitroBoostAuthenticationService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NitroBoostAuthenticationService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NitroBoostAuthenticationService.Web.dll"]