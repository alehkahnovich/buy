FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
# install System.Drawing native dependencies
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["nugets", "Buy.Rasterization.Processor/nugets"]
COPY ["nuget.config", "Buy.Rasterization.Processor/nuget.config"]
COPY ["Buy.Rasterization.Processor/Buy.Rasterization.Processor.csproj", "Buy.Rasterization.Processor/"]
COPY ["Buy.Rasterization.Contract/Buy.Rasterization.Contract.csproj", "Buy.Rasterization.Contract/"]
COPY ["Buy.Rasterization.DataAccess/Buy.Rasterization.DataAccess.csproj", "Buy.Rasterization.DataAccess/"]
COPY ["Buy.Rasterization.IO/Buy.Rasterization.IO.csproj", "Buy.Rasterization.IO/"]
COPY ["Buy.Rasterization.Business/Buy.Rasterization.Business.csproj", "Buy.Rasterization.Business/"]
RUN dotnet restore "Buy.Rasterization.Processor/Buy.Rasterization.Processor.csproj" --configfile "Buy.Rasterization.Processor/nuget.config"
COPY . .
WORKDIR "/src/Buy.Rasterization.Processor"
RUN dotnet build "Buy.Rasterization.Processor.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Buy.Rasterization.Processor.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Buy.Rasterization.Processor.dll"]