# Estágio de Build utilizando a SDK do .NET 8.0 (ajuste para a versão do seu .NET)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia o arquivo .csproj e restaura as dependências
COPY ["BabySteps.API.csproj", "./"]
RUN dotnet restore "BabySteps.API.csproj"

# Copia todo o código e faz o publish
COPY . .
RUN dotnet publish "BabySteps.API.csproj" -c Release -o /app/publish

# Estágio de Execução utilizando a imagem leve de ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Define a variável de ambiente para ouvir na porta injetada pelo Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BabySteps.API.dll"]