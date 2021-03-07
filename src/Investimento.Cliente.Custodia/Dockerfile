#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Investimento.Cliente.Custodia/Investimento.Cliente.Custodia.csproj", "Investimento.Cliente.Custodia/"]
COPY ["Investimento.Cliente.Custodia.Domain/Investimento.Cliente.Custodia.Domain.csproj", "Investimento.Cliente.Custodia.Domain/"]
COPY ["Investimento.Cliente.Custodia.Infrastructure/Investimento.Cliente.Custodia.Infrastructure.csproj", "Investimento.Cliente.Custodia.Infrastructure/"]
RUN dotnet restore "Investimento.Cliente.Custodia/Investimento.Cliente.Custodia.csproj"
COPY . .
WORKDIR "/src/Investimento.Cliente.Custodia"
RUN dotnet build "Investimento.Cliente.Custodia.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investimento.Cliente.Custodia.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investimento.Cliente.Custodia.dll"]