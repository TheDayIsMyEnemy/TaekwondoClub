#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaekwondoManagementSystem/TaekwondoManagementSystem.csproj", "TaekwondoManagementSystem/"]
RUN dotnet restore "TaekwondoManagementSystem/TaekwondoManagementSystem.csproj"
COPY . .
WORKDIR "/src/TaekwondoManagementSystem"
RUN dotnet build "TaekwondoManagementSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaekwondoManagementSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaekwondoManagementSystem.dll"]