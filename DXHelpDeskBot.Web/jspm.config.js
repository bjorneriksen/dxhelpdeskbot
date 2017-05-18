SystemJS.config({
    map: {
        "signalr-jquery": "Scripts/jquery.signalr-2.2.0.js",
        "signalr": "signalr"
    },
    meta: {
        "signalr-jquery": {
            "format": "global",
            "deps": [
                "jquery"
            ]
        }
    },
    browserConfig: {
        "paths": {
            "npm:": "/jspm_packages/npm/",
            "github:": "/jspm_packages/github/",
            "web/": "/src/"
        }
    },
    nodeConfig: {
        "paths": {
            "npm:": "jspm_packages/npm/",
            "github:": "jspm_packages/github/",
            "web/": "src/"
        }
    },
    devConfig: {
        "map": {
            "plugin-babel": "npm:systemjs-plugin-babel@0.0.15",
            "babel-runtime": "npm:babel-runtime@5.8.38",
            "core-js": "npm:core-js@1.2.7",
            "babel": "npm:babel-core@5.8.38",
            "path": "npm:jspm-nodelibs-path@0.2.0",
            "process": "npm:jspm-nodelibs-process@0.2.0",
            "fs": "npm:jspm-nodelibs-fs@0.2.0"
        }
    },
    transpiler: "plugin-babel",
    packages: {
        "web": {
            "main": "web.js",
            "meta": {
                "*.js": {
                    "loader": "plugin-babel"
                }
            }
        },
        "signalr": {
            "format": "global",
            "defaultExtension": false,
            "meta": {
                "js": {
                    "format": "global",
                    "deps": [
                        "signalr-jquery"
                    ]
                }
            }
        }
    }
});

SystemJS.config({
    packageConfigPaths: [
        "npm:@*/*.json",
        "npm:*.json",
        "github:*/*.json"
    ],
    map: {
        "jquery": "Scripts/jquery-2.2.4.min.js",
        "html": "github:Hypercubed/systemjs-plugin-html@0.0.8",
        "knockout": "github:knockout/knockout@3.4.0"
    },
    packages: {
        "/": {
            defaultExtension: "js"
        },
        
        "github:Hypercubed/systemjs-plugin-html@0.0.8": {
            "map": {
                "webcomponentsjs": "github:webcomponents/webcomponentsjs@0.7.22"
            }
        }
    }
});
