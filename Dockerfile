# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ .

RUN dotnet restore UserManagementService.sln

RUN dotnet publish ./UserManagementService/UserManagementService.app.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output
COPY --from=build /app/out ./

# Set the ASP.NET Core URL binding to listen on port 80
ENV ASPNETCORE_URLS=http://+:80

# Expose port 80 to the host
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "UserManagementService.app.dll"]
