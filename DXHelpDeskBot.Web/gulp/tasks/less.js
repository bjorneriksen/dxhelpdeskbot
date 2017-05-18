import gulp from "gulp";
import config from "../config";
import util from "gulp-util";
import less from "gulp-less";


export function lessPipeline(stream) {
    stream
        .pipe(less())
        .on("error", util.log)
        .pipe(gulp.dest(config.paths.outputDir));
    return stream;
}

gulp.task("less", () => {
    var stream = gulp.src(config.paths.less,{base:config.paths.sourceDir});
    lessPipeline(stream);
    return stream;
});
