FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Avonale.Payment.API/Avonale.Payment.API.csproj", "Avonale.Payment.API/"]
RUN dotnet restore "Avonale.Payment.API/Avonale.Payment.API.csproj"
COPY . .
WORKDIR "/src/Avonale.Payment.API"
RUN dotnet build "Avonale.Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Avonale.Payment.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Avonale.Payment.API.dll"]
