FROM mcr.microsoft.com/dotnet/core/runtime:2.2-alpine  AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /src
COPY  ./AspNetCourseProject/FanFictionApp/FanFictionApp.csproj FanFictionApp/
RUN dotnet restore FanFictionApp/FanFictionApp.csproj
COPY ./AspNetCourseProject .
WORKDIR /src/FanFictionApp
RUN dotnet build FanFictionApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FanFictionApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FanFictionApp.dll"]