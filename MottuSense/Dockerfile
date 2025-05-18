# Dockerfile otimizado para .NET 8

# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos necessários para restaurar dependências
COPY ["Motos/Motos.csproj", "Motos/"]
RUN dotnet restore "Motos/Motos.csproj"

# Copiar todo o código fonte
COPY . .

# Publicar aplicação
RUN dotnet publish "Motos/Motos.csproj" -c Release -o /app/publish 

# Estágio final (imagem de runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Configurações de ambiente
ENV ASPNETCORE_URLS=http://+:7050 \
    DOTNET_RUNNING_IN_CONTAINER=true

# Copiar apenas os arquivos publicados
COPY --from=build /app/publish .

# Porta exposta (ajustada para 5000 conforme ENV)
EXPOSE 7050

# Comando de inicialização
ENTRYPOINT ["dotnet", "Motos.dll"]