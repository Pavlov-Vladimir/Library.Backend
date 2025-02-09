FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Library.WebApi/Library.WebApi.csproj", "Library.WebApi/"]
COPY ["Library.Business/Library.Business.csproj", "Library.Business/"]
COPY ["Library.Domain/Library.Domain.csproj", "Library.Domain/"]
COPY ["Library.DataAccess/Library.DataAccess.csproj", "Library.DataAccess/"]
RUN dotnet restore "Library.WebApi/Library.WebApi.csproj"
COPY . .
WORKDIR "/src/Library.WebApi"
RUN dotnet build "Library.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Library.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.WebApi.dll"]
