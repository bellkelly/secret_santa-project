FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Copy over all files required to install dependencies
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SecretSanta.WebAPI/SecretSanta.WebAPI.csproj", "src/SecretSanta.WebAPI/"]
COPY ["src/SecretSanta.Application/SecretSanta.Application.csproj", "src/SecretSanta.Application/"]
COPY ["src/SecretSanta.Domain/SecretSanta.Domain.csproj", "src/SecretSanta.Domain/"]
COPY ["src/SecretSanta.Infrastructure/SecretSanta.Infrastructure.csproj", "src/SecretSanta.Infrastructure/"]
RUN dotnet restore "src/SecretSanta.WebAPI/SecretSanta.WebAPI.csproj"

# Copy over all source code and create release build
COPY . .
WORKDIR /src/src/SecretSanta.WebAPI
RUN dotnet build "SecretSanta.WebAPI.csproj" --nologo -c Release -o /app/build

# Publish build
FROM build AS publish
RUN dotnet publish "SecretSanta.WebAPI.csproj" --nologo -c Release -o /app/publish

# Run WebAPI
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecretSanta.WebAPI.dll"]
