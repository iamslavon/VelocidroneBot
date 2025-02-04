#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Veloci.Web/Veloci.Web.csproj", "Veloci.Web/"]
RUN dotnet restore "./Veloci.Web/./Veloci.Web.csproj"
COPY . .
WORKDIR "/src/Veloci.Web"
RUN dotnet build "./Veloci.Web.csproj" -c $BUILD_CONFIGURATION /p:OpenApiGenerateDocuments=false -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Veloci.Web.csproj" -c $BUILD_CONFIGURATION /p:OpenApiGenerateDocuments=false -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Veloci.Web.dll"]
