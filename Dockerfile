# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos los archivos del proyecto
COPY ComederoPetWeb.csproj ./
RUN dotnet restore ComederoPetWeb.csproj

# Copiamos todo el resto del código
COPY . ./
RUN dotnet publish ComederoPetWeb.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render usa el puerto 10000 por defecto
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "ComederoPetWeb.dll"]
