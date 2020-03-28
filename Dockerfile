FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
# Copy everything and build
COPY . ./
RUN dotnet restore "./stock-notifications.csproj"
RUN dotnet publish "./stock-notifications.csproj" -c Release
RUN dotnet publish -c Release
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN cp -R /app/bin/Release/netcoreapp3.1/ *
RUN ls
ENTRYPOINT ["dotnet", "stock-notifications.dll"]
