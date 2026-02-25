
const fs = require('fs');
const path = require('path');
const chokidar = require('chokidar');

const appsContent = JSON.parse(fs.readFileSync(__dirname + "/app.json", 'utf-8'));
function handleFileChange(fpath) {
    // Use the path.sep to build a platform-independent path
	let srcFolderRegExp = new RegExp(`\\${path.sep}src\\${path.sep}`, 'g');

    const changedFilePath =fpath.replace(srcFolderRegExp, `${path.sep}webapp${path.sep}`);
    if (fs.existsSync(changedFilePath)) {
        const fileContent = fs.readFileSync(fpath, 'utf8');
        fs.writeFileSync(changedFilePath, fileContent);
        console.log(`Content mirrored to ${changedFilePath}`);

    }
}
const modules = [];
appsContent.applications.forEach(function (obj) {
    obj.path = obj.path.replace("/webapp", "/src");
    let module = obj.path.split("/");
    module = obj.path.replace(module[0] + "/", "")

    modules.push(path.join(__dirname, module));

});
console.log(`module detected ${modules}`);
const watcher = chokidar.watch(modules, {
    ignored: /(^|[\/\\])\..*|\.ts$/,
    persistent: true
});

watcher
    .on('add', path => { console.log(`File ${path} has been added`); handleFileChange(path) })
    .on('change', path => { console.log(`File ${path} has been changed`); handleFileChange(path) })
    .on('unlink', path => { console.log(`File ${path} has been removed`); handleFileChange(path) });


