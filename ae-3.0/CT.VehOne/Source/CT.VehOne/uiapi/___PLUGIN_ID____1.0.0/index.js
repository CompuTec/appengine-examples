const express = require('express')
const fs = require('fs');
const cors = require('cors');
var morgan = require('morgan');
require("colors");

let app = express();
app.use(cors({ origin: '*' }));
app.use(morgan('tiny'));

const appsContent = JSON.parse(fs.readFileSync(__dirname + "/app.json", 'utf-8'));
console.log("SAP Business One Web Client Extensions".bold);
console.log("Start serving....".green);
appsContent.applications.forEach(function (obj) {
  //obj.path=obj.path.replace("/webapp","/");
  let module = obj.path.split("/");
  module = obj.path.replace(module[0] + "/", "")
  app.use(`/${obj.path}`, (req, res, next) => {
    req.accepts('application/json');
    var result = req.url.match(/\/gulpfile.js$/)
    if (result) {
      return res.status(404).end('Cannot GET ' + req.url)
    }
    return next();
  })


  app.use(`/${obj.path}`, express.static(`${module}`));     //serve webapp folder
  obj.path = obj.path.replace("/webapp", "/src");
  module = module.replace("/webapp", "/src")

  app.use(`/${obj.path}`, express.static(`${module}`));    //serve src folder
  console.log(`Module source file path : ${module}`);
});
app.use('/app.json', express.static('app.json'));


let port = process.argv[2] || 8081;
app.listen(port, 'localhost');
console.log("Serving modules on port : ".green + `${port}`.yellow);
