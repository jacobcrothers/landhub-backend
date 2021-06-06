FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
LABEL author="Kevin"


WORKDIR /app

# Copy csproj and restore as distinct layers
COPY LandHubWebService/LandHubWebService/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./LandHubWebService ./
RUN ls -al
RUN dotnet publish PropertyHatchWebApi.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libc6-dev
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 50574

ENTRYPOINT ["dotnet", "PropertyHatch.dll"]