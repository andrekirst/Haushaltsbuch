FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Haushaltsbuch.WebApi/Haushaltsbuch.WebApi.csproj", "Haushaltsbuch.WebApi/"]
RUN dotnet restore "Haushaltsbuch.WebApi/Haushaltsbuch.WebApi.csproj"
COPY . .
WORKDIR "/src/Haushaltsbuch.WebApi"
RUN dotnet build "Haushaltsbuch.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Haushaltsbuch.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Haushaltsbuch.WebApi.dll"]