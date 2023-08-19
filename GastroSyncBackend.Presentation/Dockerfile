#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GastroSyncBackend.Presentation/GastroSyncBackend.Presentation.csproj", "GastroSyncBackend.Presentation/"]
COPY ["GastroSyncBackend.Common/GastroSyncBackend.Common.csproj", "GastroSyncBackend.Common/"]
COPY ["GastroSyncBackend.Services/GastroSyncBackend.Services.csproj", "GastroSyncBackend.Services/"]
COPY ["GastroSyncBackend.Repository/GastroSyncBackend.Repository.csproj", "GastroSyncBackend.Repository/"]
RUN dotnet restore "GastroSyncBackend.Presentation/GastroSyncBackend.Presentation.csproj"
COPY . .
WORKDIR "/src/GastroSyncBackend.Presentation"
RUN dotnet build "GastroSyncBackend.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GastroSyncBackend.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GastroSyncBackend.Presentation.dll"]