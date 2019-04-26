FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["FanFictionApp/FanFictionApp.csproj", "FanFictionApp/"]
COPY ["FanFiction.Services/FanFiction.Services.csproj", "FanFiction.Services/"]
COPY ["FanFiction.ViewModels/FanFiction.ViewModels.csproj", "FanFiction.ViewModels/"]
COPY ["FanFiction.Data/FanFiction.Data.csproj", "FanFiction.Data/"]
COPY ["FanFiction.Models/FanFiction.Models.csproj", "FanFiction.Models/"]

RUN dotnet restore "FanFictionApp/FanFictionApp.csproj"
COPY . .
WORKDIR "/src/FanFictionApp"
RUN dotnet build "FanFictionApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FanFictionApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FanFictionApp.dll"]