##Fase base com ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

##Importante: expor a porta 80, que é a que o Azure usa por padrão
ENV ASPNETCORE_URLS=http://+/
EXPOSE 80

##Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

##Copiar o arquivo .sln e os projetos respeitando a pasta safezone
COPY safezone/safezone.sln safezone/
COPY safezone/safezone.csproj safezone/
COPY safezone.application/safezone.application.csproj safezone.application/
COPY safezone.infrastructure/safezone.infrastructure.csproj safezone.infrastructure/
COPY safezone.domain/safezone.domain.csproj safezone.domain/

##Restaurar as dependências usando o caminho correto para o .sln dentro da pasta safezone
RUN dotnet restore safezone/safezone.sln

##Copiar todo o restante do código para a pasta /src (importante: copie tudo, pois pode ter código fora safezone)
COPY . .

WORKDIR /src/safezone

##Build do projeto, apontando para o csproj dentro de safezone
RUN dotnet build safezone.csproj -c $BUILD_CONFIGURATION -o /app/build

##Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish safezone.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

##Imagem final para rodar
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "safezone.dll"]