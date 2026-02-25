1. Create extension using SAP Business One Web Client Extensions
   3. Set application id to: ___PLUGIN_ID___
   4. Set provider to: ___PROVIDER___
   4. For module name please use prefix: ___MODULE___<ModuleName>. For example: `___MODULE___SalesQuotation`
    
![img.png](webup_img.png)
![img_1.png](webup_img_1.png)

2. Build using SAP Business One Web Client Extensions
   3. Right click on `mta.yaml` and select `Build MTA (Web Client)`

3. When adding new modules please use prefix: ___MODULE___.
4. You can add modules to AppEngine plugin in manifest:
   5. moduleId should be module without prefix. For example: `SalesQuotation`
   6. buildPath should be path to module build folder. For example: `webup\___PLUGIN_ID____1.0.1\___MODULE___SalesQuotation\dist`

```json
{
   "Version": "1.9",
   "id": "CT.VehOne",
   "name": "CT.VehOne",
   //"...",
   "ui": {
      //"...",
      "webupModules": [
         {
            "moduleId": "VehOne",
            "buildPath": "webup\\___PLUGIN_ID____1.0.1\\___MODULE___VehOne\\dist"
         },
         {
            "moduleId": "SalesQuotation",
            "buildPath": "webup\\___PLUGIN_ID____1.0.1\\___MODULE___SalesQuotation\\dist"
         }
      ]
   }
}
```

