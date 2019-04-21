#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine  AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build

WORKDIR /src
COPY ["AspNetCourseProject/FanFictionApp/FanFictionApp.csproj", "AspNetCourseProject/FanFictionApp/"]
RUN dotnet restore "AspNetCourseProject/FanFictionApp/FanFictionApp.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.Models/FanFiction.Models.csproj", "AspNetCourseProject/FanFiction.Models/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.Models/FanFiction.Models.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.Services/FanFiction.Services.csproj", "AspNetCourseProject/FanFiction.Services/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.Services/FanFiction.Services.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.Services/FanFiction.Services.csproj", "AspNetCourseProject/FanFiction.Services/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.Services/FanFiction.Services.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.ViewModels/FanFiction.ViewModels.csproj", "AspNetCourseProject/FanFiction.ViewModels/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.ViewModels/FanFiction.ViewModels.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.Data/FanFiction.Data.csproj", "AspNetCourseProject/FanFiction.Data/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.Data/FanFiction.Data.csproj"
COPY . .

COPY ["AspNetCourseProject/FanFiction.Tests/FanFiction.Tests.csproj", "AspNetCourseProject/FanFiction.Tests/"]
RUN dotnet restore "AspNetCourseProject/FanFiction.Tests/FanFiction.Tests.csproj"
COPY . .

WORKDIR "/src/AspNetCourseProject/FanFictionApp"
RUN dotnet build "FanFictionApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FanFictionApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FanFictionApp.dll"]
