# Twitter Assignment

The project is developed using the ASP .NET Core 2.1 framework and uses MySQL as the database. Using this combo makes the project cross platform and lightweight.
The project consists of two APIs the registration and the login API.
The project uses JWT tokens for maintaining session context between API calls.

## Getting Started

### Architectural Design

The project is developed with a scalable and easily understandable architecture.The project is divided into 6 different parts:
 1. Controllers :

 	This namespace contains all the endpoints

 2. Services :

 	Services namespace are like an extension to the endpoints and handle all the business logics

 3. Models:

 	This namespace contains all the models that are used specifically by the endpoints

	3.1. Request Model :
		Contains all the request models (classes) which contain the parameters required to call the end points

	3.2. Response Model :
		Contains all the response models (classes) which contains all the parameters that will be present in the response from the end point

 4. Entities:

 	Entities namespace contains the ORM models of the database

 5. DBFunctions:

 	This namespace contains the functions that are responsible to talk to the database

 6. Helpers:

 	This namespace contains the general functions which are used by many of the services.

### Prerequisites

* .Net Core SDK
* MySQL
* Postman

### Installing

* **Installing .Net core 2.1**
1. Download .net core 2.1 sdk from https://www.microsoft.com/net/download
2. Run the installer
3. Click on install in the bottom right corner
4. Open CMD and type dotnet. You should get something like this.
```
C:\Users\Adarsh>dotnet

Usage: dotnet [options]
Usage: dotnet [path-to-application]

Options:
  -h|--help         Display help.
  --info            Display .NET Core information.
  --list-sdks       Display the installed SDKs.
  --list-runtimes   Display the installed runtimes.

path-to-application:
  The path to an application .dll file to execute.

```

* **Downloading and installing MySQL server and client**
1. Download MySQL from https://dev.mysql.com/downloads/file/?id=480824
2. After downloading, run the installer
3. Agree to the policies
4. Select the custom button on the next page
5. Add MySQL Servers > MySQL Server > MySQL Server (Version) > MySQL Server (Version) - (Architecture)
6. Add Applications > MySQL Shell > MySQL Shell (Version) > MySQL Shell (Version) - (Architecture)
7. Click on Next
8. Verify and click on Execute
9. After installing the MySQL, click on configure
10. Select Standalone MySQL Server
11. Click next, and remember the port (Recommended : 3306)
12. Select use strong password encryption for authentication
13. Create and remember password
14. Click Next > Next > Execute > Finish > Configure > Finish
15. Open MySQL shell installed to check

* **Connecting to MySQL shell**
1. Connect to the Server
```\connect root@localhost:3306```
2. On prompt - enter password

* **Installing and running the Project**
1. Goto https://github.com/APradyut/TwitterAssignmentPOSTMANPublished
2. Download the repository and extract into a directory
		*or*
1. Open git bash and navigate to desired directory
2. Clone the repository
```git clone https://github.com/APradyut/TwitterAssignmentPOSTMANPublished.git```

## Running

1. Open appsettings.production.json file
2. Add the password in the mysqlconnection connectionstring
```server=localhost;port=3306;database=db;uid=root;password=<add-password-here>; ```
3. Save the file and exit
4. Open command prompt at the location (Short-cut: Press Shift + Right Click > Select Open powershell here/Open Command prompt here )
5. Run the project
```dotnet .\TwitterAssignment.dll```
6. Import the postman collection from the collections folder into postman for testing APIs

## Download Links

* [ASP .NET Core 2.1](https://www.microsoft.com/net/download) - The web framework used
* [XAMPP](https://www.apachefriends.org/download.html) - Used as a server for mysql only

## Authors

* **Adarsh Pradyut**
	adarsh.pradyut@gmail.com
	+91 9962256589
