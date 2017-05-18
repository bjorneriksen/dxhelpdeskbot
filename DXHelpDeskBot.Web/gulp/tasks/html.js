import gulp from "gulp";
import config from "../config";


export function htmlPipeline(stream) {
    stream.pipe(gulp.dest(config.paths.outputDir));
    return stream;
}

gulp.task("html", () => {
    var stream = gulp.src(config.paths.html,{base:config.paths.sourceDir});
    htmlPipeline(stream);
    return stream;
});