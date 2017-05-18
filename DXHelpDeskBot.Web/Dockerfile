FROM microsoft/aspnetcore:1.1.2
COPY bin/Debug/netcoreapp1.1/publish/. /app
WORKDIR /app
EXPOSE 5000/tcp
CMD ["dotnet", "Web.dll", "--server.urls", "http://*:5000"]

