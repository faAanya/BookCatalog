FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src


COPY ["bookCatalog.Presentation/bookCatalog.Presentation.csproj", "bookCatalog.Presentation/"]
COPY ["bookCatalog.Application/bookCatalog.Application.csproj", "bookCatalog.Application/"]
COPY ["bookCatalog.Domain/bookCatalog.Domain.csproj", "bookCatalog.Domain/"]
COPY ["bookCatalog.Infrastructure/bookCatalog.Infrastructure.csproj", "bookCatalog.Infrastructure/"]

RUN dotnet restore "bookCatalog.Presentation/bookCatalog.Presentation.csproj"


COPY . .

WORKDIR "/src/bookCatalog.Presentation"
RUN dotnet build "bookCatalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "bookCatalog.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bookCatalog.Presentation.dll"]
