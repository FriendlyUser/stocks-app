FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . .
RUN dotnet publish -c Release
ENTRYPOINT ["dotnet", "bin/stock-notifications.dll"]