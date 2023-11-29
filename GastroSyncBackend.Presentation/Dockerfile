# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copie os arquivos do projeto
COPY ["GastroSyncBackend.Presentation/GastroSyncBackend.Presentation.csproj", "GastroSyncBackend.Presentation/"]
COPY ["GastroSyncBackend.Common/GastroSyncBackend.Common.csproj", "GastroSyncBackend.Common/"]
COPY ["GastroSyncBackend.Services/GastroSyncBackend.Services.csproj", "GastroSyncBackend.Services/"]
COPY ["GastroSyncBackend.Repository/GastroSyncBackend.Repository.csproj", "GastroSyncBackend.Repository/"]

# Restaure as dependências
RUN dotnet restore "GastroSyncBackend.Presentation/GastroSyncBackend.Presentation.csproj"

# Copie o resto dos arquivos e construa o projeto
COPY . .
WORKDIR "/src/GastroSyncBackend.Presentation"
RUN dotnet build "GastroSyncBackend.Presentation.csproj" -c Release -o /app/build

# Publicar o aplicativo
FROM build AS publish
RUN dotnet publish "GastroSyncBackend.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final para configurar o aplicativo
FROM base AS final
WORKDIR /app

# Copiar os certificados gerados e o aplicativo publicado
COPY --from=publish /app/publish .

# Criar usuário não-root
RUN useradd -u 1000 appuser
USER appuser

ENTRYPOINT ["dotnet", "GastroSyncBackend.Presentation.dll"]
