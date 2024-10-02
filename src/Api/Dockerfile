FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# ENV ASPNETCORE_URLS=http://+:5000

# USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# ARG configuration=Release
WORKDIR /src

COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Application/Application.csproj", "src/Application/"]

RUN dotnet restore "src/Api/Api.csproj"

COPY . .

WORKDIR "/src/src/Api"

RUN dotnet build "Api.csproj" -c Debug -o /app/build

FROM build AS publish
# ARG configuration=Release

RUN dotnet publish "Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]