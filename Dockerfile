FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
LABEL author="Kevin"


WORKDIR /source

# Copy csproj and restore as distinct layers
COPY LandHubWebService/*.sln .
COPY LandHubWebService/LandHubWebService/*.csproj ./LandHubService/
RUN dotnet restore

# Copy everything else and build
COPY ./LandHubWebService ./LandHubService/
RUN ls -al
WORKDIR /source/LandHubService/
RUN dotnet publish -c Release -o /app --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libc6-dev
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app
COPY --from=build /app ./

EXPOSE 50574

ENTRYPOINT ["dotnet", "PropertyHatch.dll"]
