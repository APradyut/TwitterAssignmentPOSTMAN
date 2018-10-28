# Twitter Assignment

Submitted By: 

Adarsh Pradyut\n
adarsh.pradyut@gmail.com\n
+91 9962256589
## Architectural Design

The project is developed with a scalable and easily understandable architecture.The project is divided into 6 different parts:
 1. Controllers :
 	This namespace contains all the endpoints
 2. Services :
 	Services namespace are like an extension to the endpoints and handle all the business logics
 3. Models:
 	This namespace contains all the models that are used specifically by the endpoints\n
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
