FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Haushaltsbuch.UI.Web/Haushaltsbuch.UI.Web.csproj", "Haushaltsbuch.UI.Web/"]
RUN dotnet restore "Haushaltsbuch.UI.Web/Haushaltsbuch.UI.Web.csproj"
COPY . .
WORKDIR "/src/Haushaltsbuch.UI.Web"
RUN dotnet build "Haushaltsbuch.UI.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Haushaltsbuch.UI.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Haushaltsbuch.UI.Web.dll"]