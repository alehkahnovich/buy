FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Buy.Idp.Web/Buy.Idp.Web.csproj", "Buy.Idp.Web/"]
COPY ["Buy.Idp.Business/Buy.Idp.Business.csproj", "Buy.Idp.Business/"]
COPY ["Buy.Idp.DataAccess/Buy.Idp.DataAccess.csproj", "Buy.Idp.DataAccess/"]
COPY ["Buy.Idp.Infrastructure/Buy.Idp.Infrastructure.csproj", "Buy.Idp.Infrastructure/"]
RUN dotnet restore "Buy.Idp.Web/Buy.Idp.Web.csproj"
COPY . .
WORKDIR "/src/Buy.Idp.Web"
RUN dotnet build "Buy.Idp.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Buy.Idp.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Buy.Idp.Web.dll"]