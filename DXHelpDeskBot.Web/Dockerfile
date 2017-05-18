FROM microsoft/dotnet:runtime

ADD bin/Debug/netcoreapp1.1/publish/. /app
ADD jspm_packages/ /app/wwwroot/jspm_packages
ADD bower_components/ /app/wwwroot/bower_components

WORKDIR /app
EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "DXHelpDeskBot.Web.dll", "--server.urls", "http://*:5000"]


