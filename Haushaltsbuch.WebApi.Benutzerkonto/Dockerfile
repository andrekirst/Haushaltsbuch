FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Haushaltsbuch.WebApi.Benutzerkonto/Haushaltsbuch.WebApi.Benutzerkonto.csproj", "Haushaltsbuch.WebApi.Benutzerkonto/"]
RUN dotnet restore "Haushaltsbuch.WebApi.Benutzerkonto/Haushaltsbuch.WebApi.Benutzerkonto.csproj"
COPY . .
WORKDIR "/src/Haushaltsbuch.WebApi.Benutzerkonto"
RUN dotnet build "Haushaltsbuch.WebApi.Benutzerkonto.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Haushaltsbuch.WebApi.Benutzerkonto.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Haushaltsbuch.WebApi.Benutzerkonto.dll"]