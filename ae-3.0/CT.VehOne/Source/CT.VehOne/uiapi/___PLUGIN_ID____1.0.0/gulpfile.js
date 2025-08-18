var gulp = require('gulp');
var uglify = require('gulp-uglify');
var logger = require('gulplog');
var watch = require('gulp-watch');
var clean = require('gulp-rimraf');
var sourcemaps = require('gulp-sourcemaps');
var fs = require('fs')
var requirejs = require('requirejs');
var ts = require('gulp-typescript');
var tsProject = ts.createProject('tsconfig.json');
const path = require('path');
const ignore = require('gulp-ignore');

function getManifestContent(){
  let cwd = process.cwd();
  let manifestPath = cwd + "/webapp/manifest.json";
  let manifestContent = JSON.parse(fs.readFileSync(manifestPath, 'utf-8'));
  return manifestContent;
}

function getJavaScriptFiles(dir) {
  let files = [];
  if(fs.existsSync(dir)){
    const entries = fs.readdirSync(dir, { withFileTypes: true });
    for (let entry of entries) {
        const fullPath = path.join(dir, entry.name);
        if (entry.isDirectory()) {
            const nestedFiles = getJavaScriptFiles(fullPath);
            files = files.concat(nestedFiles);
        } else if (entry.isFile() && path.extname(entry.name) === '.js') {
            files.push(fullPath);
        }
    }
  }
  return files;
}

function generateRequireJSConfig(){
  let manifestContent = getManifestContent();
  let name = manifestContent["name"];
  let moduleName = name.split('.').splice(-1)[0];
  let slashName = name.replace(/\./g, "/");

  let config = {};
  config["baseUrl"] = "./webapp";
  config["paths"] = {};
  let paths = ["/controller", "/model"];
  for(let path of paths){
    let key = slashName + path;
    let val = "." + path;
    config["paths"][key] = val;
  }
  config["include"] = [];
  for(let path of paths){
    let dir = "webapp" + path;
    let files = getJavaScriptFiles(dir);
    for(let file of files){
      file = file.replaceAll("\\", "/").replace(dir, slashName + path).replace(/\.js$/, "");
      config["include"].push(file);
    }
  }
  config["optimize"] = "none";
  config["out"] = `./dist/controller/${moduleName}.bundle.js`;
  logger.info(config);
  return config;
}

gulp.task('clean', function () {
  logger.info("Clean dist and webapp folder");
  return gulp.src(["dist", "webapp"], { read: false, allowEmpty: true }).pipe(clean());
});

gulp.task('createWebApp', function () {
  logger.info("Copy src files to webapp folder");
  return gulp.src(['src/**/*', '!src/**/*.ts', '!src/lib/**'])
    .pipe(gulp.dest('webapp'));
});

gulp.task('tsc', function () {
  logger.info("Compile typescript to javascript");
  return tsResult = tsProject.src()
    .pipe(sourcemaps.init())
    .pipe(tsProject())
    .pipe(sourcemaps.write('.', { includeContent: false, sourceRoot: './' }))
    .pipe(gulp.dest('webapp'));
});

gulp.task('copyToDist', function () {
  return gulp.src('./webapp/**/*')
      .pipe(ignore(function(file) {
          let relativePath = path.relative(file.cwd, file.path);
          let ret = relativePath.startsWith(`webapp${path.sep}controller`) || relativePath.startsWith(`webapp${path.sep}model`);
          return ret;
      }))
      .pipe(gulp.dest('./dist'));
});

gulp.task('minify', function () {
  logger.info("Minify js files");
  return gulp.src('dist/**/*.js')
    .pipe(gulp.dest('dist'))
    .pipe(uglify())
    .pipe(gulp.dest('dist'));
});

gulp.task('manifest', function (cb) {
  logger.info("Create manifest files");
  const cwd = process.cwd();
  let manifestContent = getManifestContent();
  const moduleName = manifestContent["name"].split('.').splice(-1)[0];
  manifestContent.mainBundle = `controller/${moduleName}.bundle`;
  const destManifestPath = cwd + "/dist/manifest.json";
  fs.writeFileSync(destManifestPath, JSON.stringify(manifestContent, " ", 2));
  cb();
});

gulp.task('bundle', function (cb) {
  // Bundle multiple js files into one, especially in the case of one module having multiple views.
  let rjsConfig = generateRequireJSConfig();
  requirejs.optimize(rjsConfig, function (buildResponse) {
    logger.info(`bundling files...${buildResponse}`);
    cb();
  }, function (err) {
    logger.error(err);
    cb();
  });
});

gulp.task('tsc-watch', function () {
  logger.info("Compile typescript to javascript");
  return tsResult = watch('src/**/*.ts', { ignoreInitial: false })
    .pipe(sourcemaps.init())
    .pipe(tsProject())
    .pipe(sourcemaps.write('.', { includeContent: false, sourceRoot: './' }))
    .pipe(gulp.dest('webapp'));
});

gulp.task('default', gulp.series('clean', 'createWebApp', 'tsc', 'copyToDist', 'bundle', 'minify', 'manifest'));
gulp.task('debug', gulp.series('clean', 'createWebApp', 'tsc'));
