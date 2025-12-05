# Multi-stage build for .NET 8 web app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore first (improves cache)
COPY ["PMS.Api/PMS.Api.csproj", "PMS.Api/"]
RUN dotnet restore "PMS.Api/PMS.Api.csproj"

# copy everything and publish
COPY . .
WORKDIR "/src/PMS.Api"
RUN dotnet publish -c Release -o /app --no-restore /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Let the platform set PORT env var; fallback to 5000
ENV ASPNETCORE_URLS=http://+:${PORT:-5000}
EXPOSE 5000

ENTRYPOINT ["dotnet", "PMS.Api.dll"]