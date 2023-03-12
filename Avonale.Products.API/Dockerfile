FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Avonale.Product.API/Avonale.Product.API.csproj", "Avonale.Product.API/"]
RUN dotnet restore "Avonale.Product.API/Avonale.Product.API.csproj"
COPY . .
WORKDIR "/src/Avonale.Product.API"
RUN dotnet build "Avonale.Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Avonale.Product.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avonale.Product.API.dll"]
