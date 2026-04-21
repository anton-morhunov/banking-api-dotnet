FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/BankAPI.API/BankAPI.csproj src/BankAPI.API/
COPY src/BankAPI.Application/BankAPI.Application.csproj src/BankAPI.Application/
COPY src/BankAPI.Infrastructure/BankAPI.Infrastructure.csproj src/BankAPI.Infrastructure/
COPY src/BankAPI.Domain/BankAPI.Domain.csproj src/BankAPI.Domain/

RUN dotnet restore src/BankAPI.API/BankAPI.csproj

COPY . .

RUN dotnet publish src/BankAPI.API/BankAPI.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BankAPI.dll"]