# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the csproj and restore dependencies
COPY src/StudentManagement.Api/StudentManagement.Api.csproj ./src/StudentManagement.Api/
RUN dotnet restore src/StudentManagement.Api/StudentManagement.Api.csproj

# Copy the rest of the code and build
COPY src/StudentManagement.Api/ ./src/StudentManagement.Api/
WORKDIR /app/src/StudentManagement.Api
RUN dotnet publish -c Release -o /app/out

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port the app runs on
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "StudentManagement.Api.dll"]
