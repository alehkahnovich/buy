FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["nugets", "Buy.Upload.Web/nugets"]
COPY ["nuget.config", "Buy.Upload.Web/nuget.config"]
COPY ["Buy.Upload.Web/Buy.Upload.Web.csproj", "Buy.Upload.Web/"]
COPY ["Buy.Upload.Business/Buy.Upload.Business.csproj", "Buy.Upload.Business/"]
COPY ["Buy.Upload.IO/Buy.Upload.IO.csproj", "Buy.Upload.IO/"]
COPY ["Buy.Upload.DataAccess/Buy.Upload.DataAccess.csproj", "Buy.Upload.DataAccess/"]
COPY ["Buy.Upload.Contracts/Buy.Upload.Contracts.csproj", "Buy.Upload.Contracts/"]
RUN dotnet restore "Buy.Upload.Web/Buy.Upload.Web.csproj" --configfile "Buy.Upload.Web/nuget.config"
COPY . .
WORKDIR "/src/Buy.Upload.Web"
RUN dotnet build "Buy.Upload.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Buy.Upload.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Buy.Upload.Web.dll"]