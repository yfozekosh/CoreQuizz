console.log("starting prebuild.js script");
console.log("current dir is " + __dirname);
var sys = require('util');
var exec = require('child_process').exec;
var spawn = require('child_process').spawn;


function execShell(command, ifSucess) {
    var comm = exec(command, function (error, stdout, stderr) {
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

    comm.stdout.on('data', function (data) {
        console.log('stdout: ' + data.toString());
    });

    comm.stderr.on('data', function (data) {
        console.log('stderr: ' + data.toString());
    });

    comm.on('exit', function (code) {
        console.log('child process exited with code ' + code.toString());
    });
}

//if (process.platform !== "linux") {
    execShell("cd ../CoreQuizz.UI && npm i && npm run build", function () {
        console.log('builded frontend');
		execShell("rm -rf wwwroot/", function(){
			execShell("cd ../CoreQuizz.UI && mv wwwroot/ ../CoreQuizz.WebService/", function() {
				console.log('copied frontend');
			});
		});
    });
//}
