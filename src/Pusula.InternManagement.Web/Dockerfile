#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Pusula.InternManagement.Web/Pusula.InternManagement.Web.csproj", "src/Pusula.InternManagement.Web/"]
COPY ["src/Pusula.InternManagement.Application/Pusula.InternManagement.Application.csproj", "src/Pusula.InternManagement.Application/"]
COPY ["src/Pusula.InternManagement.Domain/Pusula.InternManagement.Domain.csproj", "src/Pusula.InternManagement.Domain/"]
COPY ["src/Pusula.InternManagement.Application.Contracts/Pusula.InternManagement.Application.Contracts.csproj", "src/Pusula.InternManagement.Application.Contracts/"]
COPY ["src/Pusula.InternManagement.Domain.Shared/Pusula.InternManagement.Domain.Shared.csproj", "src/Pusula.InternManagement.Domain.Shared/"]
COPY ["src/Pusula.InternManagement.HttpApi/Pusula.InternManagement.HttpApi.csproj", "src/Pusula.InternManagement.HttpApi/"]
COPY ["src/Pusula.InternManagement.EntityFrameworkCore/Pusula.InternManagement.EntityFrameworkCore.csproj", "src/Pusula.InternManagement.EntityFrameworkCore/"]
RUN dotnet restore "src/Pusula.InternManagement.Web/Pusula.InternManagement.Web.csproj"
COPY . .
WORKDIR "/src/src/Pusula.InternManagement.Web"
RUN dotnet build "Pusula.InternManagement.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pusula.InternManagement.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pusula.InternManagement.Web.dll"]