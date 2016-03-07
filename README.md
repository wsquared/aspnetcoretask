## Getting started
Please note that this guide is for running the solution on the latest version of Visual Studio 2015

### Running on windows
You will need the latest version of Visual Studio 2015.

### DNVM or running without Visual Studio 2015
Please go to https://github.com/aspnet/Home for instructions.

### Visual Studio 2015
Build the solution

Double click on the 'Task.SqlServer.publish.xml' file and publish to your local development database - needs
(localdb)\v11 to work in the dev environment. Otherwise, you can set your own connection strings in the 
appSettings.json file.

Run the application with F5 - should default to development environment.

### Tests
To run the tests you must use Test Explorer by Microsoft - Resharper does not currently work - 
there are currently 4 skipped tests and 5 passing tests until I integrate an in memory database to run
integration tests on the dapper queries.

### Production
When you want to go to production, you will have to configure the appSettings.production.json file and set
your production connection strings.
