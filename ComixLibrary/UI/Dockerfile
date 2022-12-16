FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["./api/HandbookActivity.Contracts/HandbookActivity.Contracts.csproj", "HandbookActivity.Contracts/"]
COPY ["./api/HandbookActivity.Business/HandbookActivity.Business.csproj", "HandbookActivity.Business/"]
COPY ["./api/HandbookActivity.Data/HandbookActivity.Data.csproj", "HandbookActivity.Data/"]
COPY ["./api/HandbookActivity.Core/HandbookActivity.Core.csproj", "HandbookActivity.Core/"]
COPY . .
RUN dotnet restore "./api/HandbookActivity.Api/HandbookActivity.Api.csproj"
RUN dotnet build "./api/HandbookActivity.Api/HandbookActivity.Api.csproj" -c Release -o /app
FROM build AS publish
RUN dotnet publish "./api/HandbookActivity.Api/HandbookActivity.Api.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandbookActivity.Api.dll"]
