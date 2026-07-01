FROM docker-registry-002.zeuslearning.com/zeuslearning/dotnet/aspnet:10.0-alpine

WORKDIR /app

COPY publish/ .

ENTRYPOINT ["dotnet", "TraineeManagementApi.dll"]