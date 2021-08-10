# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /app/nu_authorizations

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:2.1
WORKDIR /app/nu_authorizations
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "nu_authorizations.dll"]