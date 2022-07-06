FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["nugets", "Buy.Content.Web/nugets"]
COPY ["nuget.config", "Buy.Content.Web/nuget.config"]
COPY ["Buy.Content.Web/Buy.Content.Web.csproj", "Buy.Content.Web/"]
COPY ["Buy.Content.Business/Buy.Content.Business.csproj", "Buy.Content.Business/"]
COPY ["Buy.Content.Search.Core/Buy.Content.Search.Core.csproj", "Buy.Content.Search.Core/"]
COPY ["Buy.Content.DataAccess/Buy.Content.DataAccess.csproj", "Buy.Content.DataAccess/"]
COPY ["Buy.Content.Contract/Buy.Content.Contract.csproj", "Buy.Content.Contract/"]
RUN dotnet restore "Buy.Content.Web/Buy.Content.Web.csproj" --configfile "Buy.Content.Web/nuget.config"
COPY . .
WORKDIR "/src/Buy.Content.Web"
RUN dotnet build "Buy.Content.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Buy.Content.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Buy.Content.Web.dll"]