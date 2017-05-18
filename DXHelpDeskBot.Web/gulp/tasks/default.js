import gulp from "gulp";
import config from "../config";
import runSequence from "run-sequence";
import "./html";
import "./javascript";
import "./less";
import "./staticContent";
import "./dotnet";
import "./watch";

gulp.task("build", ["html", "javascript", "less", "staticContent", "dotnetbuild"]);

gulp.task("printConfig", () => {
    console.log("**** Configuration ****")
    console.log(`Root folder : ${config.paths.rootDir}`)
    console.log(`Source folder : ${config.paths.sourceDir}`)
    console.log(`Outputting all files to : ${config.paths.outputDir}`)
    console.log("**** Configuration ****\n")
});

gulp.task("default", () => {
    runSequence("printConfig", "build", ["watch", "dotnet"]);
});

export default {
    get config() {
        return config;
    }
}
