FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BO.TestTask.Api/BO.TestTask.Api.csproj", "BO.TestTask.Api/"]
RUN dotnet restore ./BO.TestTask.Api

COPY . .
RUN dotnet build "./BO.TestTask.Api/BO.TestTask.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./BO.TestTask.Api/BO.TestTask.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BO.TestTask.Api.dll"]