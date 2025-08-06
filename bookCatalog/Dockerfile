FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 3000
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["bookCatalog.csproj", "./"]

RUN dotnet restore "bookCatalog.csproj"

COPY . . 

WORKDIR /src
RUN dotnet build "bookCatalog.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "bookCatalog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost

FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet","bookCatalog.dll" ]