# README

## PreRequisits

You need to have [NodeJS](https://nodejs.org/en/) and [.NET Core](https://www.microsoft.com/net/download/core) installed.

## Getting Started

In order for all packages to get installed do the following from a shell:

```shell
$ npm install
```

The install will restore [Node packages](https://www.npmjs.com), [JSPM packages](http://jspm.io), [Bower packages](https://bower.io) and [NuGet packages](https://www.nuget.org).

Once this is done you can run the solution with:

```shell
$ npm start
```

This will start a [Gulp](http://gulpjs.com) task that will start transpiling and moving files into the hosting location; `./wwwroot/` and start `dotnet`as well. 
It contains a watcher task that will catch any changes to JavaScript, Less, HTML or other static content and also firing off the `dotnet watch`, enabling you to even change C# code and have it recompile on the fly.

The Gulp tasks can be found in `./gulp/`with the configuration in the `config.js` file.

If you're looking to just compile/transpile everything you can run the build pipeline without watching for changes:

```shell
$ gulp build
```

## Deploy to Docker

In order to create a Docker image you need to publish the application - meaning that we need to gather all the .NET dependencies and put into a folder for deployment alongside all the compiled/transpiled content from the build process.

Perform the following for publishing:

```shell
$ dotnet publish
```

It will make sure all dependencies of packages are restored and then it runs the `gulp build` task.
The result will be put into `./bin/Debug/netcoreapp1.1/publish`.

Then you create the container image:

```shell
$ docker build -t <name of image> .
```
