
FROM mcr.microsoft.com/dotnet/sdk:2.1 AS aspwebsite
WORKDIR /App
COPY ./bin/Release/netcoreapp2.1/publish/ /App/
EXPOSE 5000
ENTRYPOINT ["dotnet", "ParkingAPI.dll"]
