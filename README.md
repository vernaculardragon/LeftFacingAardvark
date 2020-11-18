# LeftFacingAardvark
A sample API Project


How to install and configure:

Ensure that you have the runtime for .Net Core 3.1 installed on the target machine. 
You may run this solution directly from visual studio or push it into a folder

If published to a folder is can be run with the dotnet command: dotnet LeftFacingAardvark.dll
this should start a listener on the default port for dotnet (5000) whcih you can use to access this project via localhost

If you are installing this to IIS, you can copy the files into a directory that IIS has access to and configure IIS accordingly. 
Assuming you have teh correct runtimes installed IIS shoudl serve the site as expected. 

This project is configured to use a SQLite DB that should automatically generate itself.


How to Browse Endpoints: 
Please go to {projectURL}/swagger once the project is running to access a list of Services with a description of what they do. 
You may also use swagger to test and access this API since it currently has no security checks