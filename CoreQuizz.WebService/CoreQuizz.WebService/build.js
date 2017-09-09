console.log("starting prebuild.js script");
console.log("current dir is " + __dirname);
var sys = require('util');
var exec = require('child_process').exec;

function execShell(command, ifSucess) {
    exec(command, function (error, stdout, stderr) {
        console.log('stdout: ' + stdout);
        console.log('stderr: ' + stderr);
        if (error !== null) {
            console.log('exec error: ' + error);
            throw error;
        }
        else {
            if (ifSucess) ifSucess();
        }
    });
}

//if (process.platform !== "linux") {
    execShell("cd CoreQuizz.UI && npm i && npm run build", function () {
        console.log('copied');
    });
//}
