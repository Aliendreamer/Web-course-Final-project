FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine  AS build-env


WORKDIR /app
COPY  ./AspNetCourseProject/FanFiction.Data  ./FanFiction.Data/
COPY ./AspNetCourseProject/FanFiction.Models ./FanFiction.Models/
COPY ./AspNetCourseProject/FanFiction.Services ./FanFiction.Services/
COPY ./AspNetCourseProject/FanFiction.ViewModels ./FanFiction.ViewModels/
COPY ./AspNetCourseProject/FanFictionApp.Selenium.Tests ./FanFiction.Selenium.Tests/
COPY ./AspNetCourseProject/FanFiction.Tests ./FanFiction.Tests/
COPY ./AspNetCourseProject/FanFictionApp ./FanFictionApp/



WORKDIR /app/FanFictionApp
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine  
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FanFictionApp.dll"]