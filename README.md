# README

This project was created during the Microsoft TED Hack Week in May of 2017.

## Pre-requisites

* [NodeJS](https://nodejs.org/en/)
* [.NET Core](https://www.microsoft.com/net/download/core)
* [Mono](http://www.mono-project.com) (macOS, Linux)
* [.NET Framework](https://www.microsoft.com/net/download/framework) (Windows)

## Building

To build the entire solution, you simply run the build script

### macOS / Linux

```shell
$ ./build.sh
```

### Windows

```shell
c:> build.cmd
```

## Pushing Images

The Build will build a couple of Docker images, these needs to be pushed to the Hub.
You do need to change the name of the images to push them to your account.

### macOS / Linux

```shell
$ ./pushImages.sh
```

### Windows

```shell
c:> pushImages.cmd
```


## Deploying

For deploying the images as Pods and setting up the services for them, you simply run the following:

```shell
$ kubectl create -f k8_bot.yaml
```

> Due to a requirement on the Bot framework of TLS/SSL. At the moment, the configuration will not provide an endpoint that is accessible for registering in the Bot dashboard. You can however run it through the Bot Framework emulator without HTTPS.

### Azure Functions

The Azure Functions project was built using the new [Azure Functions Tools for Visual Studio 2017 Update 3 Preview](https://aka.ms/vs2017functiontools). It uses a C# compiled class library that uses the [Abot](https://github.com/sjdirect/abot) crawler to crawl through each page in the Azure documentation and save the metadata and content of the pages into a CosmosDB collection. Then Azure Search was configured to index the content in CosmosDB to be used by the bot.

## Projects

By opening `DXHelpDeskBot.sln` in [Visual Studio 2017](https://www.visualstudio.com/en-us/news/releasenotes/vs2017-relnotes) or [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/), you'll be able to build and run. Also worth mentioning is that there is a `.vscode` folder configured for Mac to be able to build and run/debug from [Visual Studio Code](https://code.visualstudio.com).

### DXHelpDeskBot

Main ASP.NET MVC/WebApi project for hosting all the conversational APIs used by the Bot Service.

### DXHelpDeskBot.Crawler

The crawler functions that crawl the Azure documentation site and gets all the details, puts it into CosmosDB and indexes it in Azure Search.

### DXHelpDeskBot.Web

A simple frontend for hosting the [Bot Framework WebChat](https://github.com/Microsoft/BotFramework-WebChat).
For details on how to work with this project, read [here](./DXHelpDekBot.Web/README.md)

> It was originally intended to built it from scratch, but due to time constraints it ended up being just a host for the WebChat control. It has been setup for proper Web development - .NET Core, Single Page App, WebAPI and even [SignalR](http://signalr.net).
