# .NET Framework Example Project of Process Force API Consumption
## General

> [!IMPORTANT]
 .NET Framework codebase is obsolete for CompuTec solution and will be removed in future releases
## Environment Setup
1) ProcessForce must be in the same version on the company database as the ProcessForce API installed on the machine where the code is executed.
2) CompuTec.Core2 libraries in this application must be in the same version as the one in the ProcessForce API minimum version. you can download them from nuget.org or appengine. `https://{{AE_Address}}:{{AE_Port}}/api/Core/GetCorePackage/net48`. You can just replace them in the application directory.   
## Run The Code
1) Please note that the CompuTec.ProcessForce libraries are loaded at runtime from the API installation directory.
So please add below command as PostBuild Action in the project properties:
```
del /q "$(TargetDir)CompuTec.ProcessForce*.*"
```
2) Add x64 platform target to the project.
 
 