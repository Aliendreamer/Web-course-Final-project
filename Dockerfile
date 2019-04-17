#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2 aspnetcore-runtime-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 3000

FROM microsoft/dotnet:2.2-sdk-nanoserver-1809 AS build
WORKDIR /src
COPY ["AspNetCourseProject/FanFictionApp/FanFictionApp.csproj", "AspNetCourseProject/FanFictionApp/"]

RUN dotnet restore "AspNetCourseProject/FanFictionApp/FanFictionApp.csproj"
COPY . .

WORKDIR "/src/AspNetCourseProject/FanFictionApp"
RUN dotnet build "FanFictionApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FanFictionApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FanFictionApp.dll"]
