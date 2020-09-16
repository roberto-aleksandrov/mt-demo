#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MT.WebApplicationFactory.Demo/MT.WebApplicationFactory.Demo.csproj", "MT.WebApplicationFactory.Demo/"]
RUN dotnet restore "MT.WebApplicationFactory.Demo/MT.WebApplicationFactory.Demo.csproj"
COPY . .
WORKDIR "/src/MT.WebApplicationFactory.Demo"
RUN dotnet build "MT.WebApplicationFactory.Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MT.WebApplicationFactory.Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MT.WebApplicationFactory.Demo.dll"]
