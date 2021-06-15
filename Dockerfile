FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
LABEL author="Kevin"


WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY LandHubWebService/LandHubWebService/*.csproj ./
#RUN dotnet restore

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY LandHubWebService/LandHubWebService/*.csproj ./LandHubWebService/
RUN dotnet restore

# Copy everything else and build
#COPY ./LandHubWebService ./
#RUN ls -al
#RUN dotnet publish PropertyHatchWebService.sln -c Release -o out

# Copy everything else and build website
COPY ./LandHubWebService/. ./LandHubWebService
WORKDIR /app/LandHubWebService
RUN dotnet publish -c release -o /DockerOutput/Website --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libc6-dev
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /DockerOutput/Website
COPY --from=build /DockerOutput/Website ./

EXPOSE 50574

ENTRYPOINT ["dotnet", "LandHubWebService.dll"]