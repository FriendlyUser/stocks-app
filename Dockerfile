FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
# Copy everything and build
COPY . ./
RUN dotnet restore "./stock-notifications.csproj"
RUN dotnet publish "./stock-notifications.csproj" -c Release
RUN dotnet publish -c Release
ENV SHELL /bin/bash
