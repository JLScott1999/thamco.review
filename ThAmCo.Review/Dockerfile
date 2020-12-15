FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=”Production”
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ThAmCo.Review.csproj", "ThAmCo.Review/"]
RUN dotnet restore "ThAmCo.Review/ThAmCo.Review.csproj"
COPY . ThAmCo.Review/
WORKDIR "/src/ThAmCo.Review"
RUN dotnet build "ThAmCo.Review.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThAmCo.Review.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThAmCo.Review.dll"]
