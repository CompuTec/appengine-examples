const fs = require('fs');
const concurrently = require('concurrently');
const path = require('path');
const { exec } = require('child_process');
// Get modules from app.json
const appsContent = JSON.parse(fs.readFileSync(__dirname + "/app.json", 'utf-8'));
const modules = [];
appsContent.applications.forEach(function (obj) {
    let module = obj.path.split("/");
    modules.push(path.join(__dirname, module[1]));
});
// Function to run gulp tasks for each module dynamically
function runGulpTasks() {
    return Promise.all(modules.map(module => {
        return new Promise((resolve, reject) => {
            exec(`node ${path.join(__dirname, 'node_modules/gulp/bin/gulp')} --gulpfile ${path.join(__dirname, 'gulpfile.js')} --cwd ${module} debug`, (error, stdout, stderr) => {
                if (error) {
                    reject(error);
                    return;
                }
                console.log(stdout);
                resolve();
            });
        });
    }));
}

function start() {
    runGulpTasks().then(() => {
        let commands = [
            `node ${path.join(__dirname, 'node_modules/nodemon/bin/nodemon')} ${path.join(__dirname, 'index.js')}`,
            `node ${path.join(__dirname, 'watch.js')}`
        ];

        // Add TypeScript Watch commands for each module
        modules.forEach(module => {
            commands.push(`node ${path.join(__dirname, 'node_modules/typescript/bin/tsc')} -p ./${module} -watch`);
        });

        concurrently(commands
        );
    });
}

start();