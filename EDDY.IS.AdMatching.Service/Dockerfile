#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EDDY.IS.AdMatching.Service/EDDY.IS.AdMatching.Service.csproj", "EDDY.IS.AdMatching.Service/"]
RUN dotnet restore "EDDY.IS.AdMatching.Service/EDDY.IS.AdMatching.Service.csproj"
COPY . .
WORKDIR "/src/EDDY.IS.AdMatching.Service"
RUN dotnet build "EDDY.IS.AdMatching.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EDDY.IS.AdMatching.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EDDY.IS.AdMatching.Service.dll"]