#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KsiazeczkaPttk/KsiazeczkaPttk.API.csproj", "KsiazeczkaPttk/"]
COPY ["KsiazeczkaPttk.DAL/KsiazeczkaPttk.DAL.csproj", "KsiazeczkaPttk.DAL/"]
COPY ["KsiazeczkaPttk.Domain/KsiazeczkaPttk.Domain.csproj", "KsiazeczkaPttk.Domain/"]
RUN dotnet restore "KsiazeczkaPttk/KsiazeczkaPttk.API.csproj"
COPY . .
WORKDIR "/src/KsiazeczkaPttk"
RUN dotnet build "KsiazeczkaPttk.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KsiazeczkaPttk.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KsiazeczkaPttk.API.dll"]