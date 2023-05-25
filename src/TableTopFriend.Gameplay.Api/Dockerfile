#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TableTopFriend.Gameplay.Api/TableTopFriend.Gameplay.Api.csproj", "src/TableTopFriend.Gameplay.Api/"]
COPY ["src/TableTopFriend.Application/TableTopFriend.Application.csproj", "src/TableTopFriend.Application/"]
COPY ["src/TableTopFriend.Domain/TableTopFriend.Domain.csproj", "src/TableTopFriend.Domain/"]
COPY ["src/TableTopFriend.Contracts/TableTopFriend.Contracts.csproj", "src/TableTopFriend.Contracts/"]
COPY ["src/TableTopFriend.Infrastructure/TableTopFriend.Infrastructure.csproj", "src/TableTopFriend.Infrastructure/"]
RUN dotnet restore "src/TableTopFriend.Gameplay.Api/TableTopFriend.Gameplay.Api.csproj"
COPY . .
WORKDIR "/src/src/TableTopFriend.Gameplay.Api"
RUN dotnet build "TableTopFriend.Gameplay.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TableTopFriend.Gameplay.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableTopFriend.Gameplay.Api.dll"]