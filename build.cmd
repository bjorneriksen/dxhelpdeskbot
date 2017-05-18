msbuild DXHelpDeskBot/DXHelpDeskBot.csproj
docker build -t dxhelpdeskbot/bot DXHelpDeskBot
dotnet build DXHelpDeskBot.Web
docker build -t dxhelpdeskbot/web DXHelpDeskBot.Web
