FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["SecretSanta.sln", "."]
COPY ["src/SecretSanta.WebAPI/SecretSanta.WebAPI.csproj", "src/SecretSanta.WebAPI/"]
COPY ["src/SecretSanta.Application/SecretSanta.Application.csproj", "src/SecretSanta.Application/"]
COPY ["src/SecretSanta.Domain/SecretSanta.Domain.csproj", "src/SecretSanta.Domain/"]
COPY ["src/SecretSanta.Infrastructure/SecretSanta.Infrastructure.csproj", "src/SecretSanta.Infrastructure/"]
COPY ["tests/Application.IntegrationTests/Application.IntegrationTests.csproj", "tests/Application.IntegrationTests/"]
RUN dotnet restore

COPY . .
WORKDIR /src/tests/Application.IntegrationTests
ENTRYPOINT ["dotnet", "test", "/p:CollectCoverage=true", "/p:ExcludeByFile=\"**/Persistence/**\""]
