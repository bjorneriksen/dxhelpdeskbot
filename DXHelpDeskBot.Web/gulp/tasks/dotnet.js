import gulp from "gulp";
import start from "gulp-start-process";
import ps from "ps-node";
import config from "../config";

var dotnetProcess = null;
var starting = false;

function startDotnet(stream, cb) {

    /*
    if( starting ) return;
    
    starting = true;

    ps.lookup({}, (error, resultList) => {
        if (error) throw new Error(error);

        var processFound = null;

        resultList.forEach(process => {
            for( var argumentIndex=0; argumentIndex<process.arguments.length; argumentIndex++ ) {
                var argument = process.arguments[argumentIndex];
                if (argument.indexOf(config.paths.dotnetProcessString) > 0) {
                    processFound = process;
                    return;
                }
            }
        });

        let dotnetStart = () => {
            dotnetProcess = start("dotnet run", { setsid: true }, cb);
            starting = false;
        };

        if (processFound != null) {
            try {
                ps.kill(processFound.pid, (error) => {
                    dotnetStart();
                });
            } catch( e ) {
                // Do nothing
            }
        } else {clear
            
            dotnetStart();
        }
    });*/

}

export function dotnetPipeline(stream, cb) {
    start("dotnet watch run", {setsid: true}, cb);
    //startDotnet(stream, cb);
}

gulp.task("dotnet", cb => {
    dotnetPipeline(null, cb);
});

gulp.task("dotnetbuild", cb => {
    start("dotnet build", {setsid: true}, cb);
});