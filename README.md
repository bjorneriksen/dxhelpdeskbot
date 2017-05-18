# README

## Pre-requisites

* NodeJS
* .NET Core
* Mono (macOS, Linux)
* .NET Framework (Windows)

## Building

To build the entire solution, you simply run the build script

### macOS / Linux:

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

### Azure Functions

The Azure Functions project was built using the new [Azure Functions Tools for Visual Studio 2017 Update 3 Preview](https://aka.ms/vs2017functiontools). It uses a C# compiled class library that uses the [Abot](https://github.com/sjdirect/abot) crawler to crawl through each page in the Azure documentation and save the metadata and content of the pages into a CosmosDB collection. Then Azure Search was configured to index the content in CosmosDB to be used by the bot.