# syntax=docker/dockerfile:1

# ---- build stage: restore + publish the .NET API ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/ ./backend/
RUN dotnet publish backend/CubeRender.Api.csproj -c Release -o /app/publish

# ---- runtime stage: slim ASP.NET image ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CubeRender.Api.dll"]
