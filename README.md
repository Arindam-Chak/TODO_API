# API with In Built JWT Token Genrations asl well as Refresh and endpoints to support TODO API which are only accesscible using JWT Tokens

This example API shows how to implement JSON Web Token authentication and authorization with ASP.NET Core 3.1, built from scratch.HAve taken reference from few Online tutorials or materials

### Features
 - User registration;
 - Role-based authorization;
 - Login via access token creation;
 - Refresh tokens, to create new access tokens when access tokens expire;

  
### Frameworks and Libraries

The API uses the following libraries and frameworks to deliver the functionalities described above:
 - [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore) (for data access)
 - [AutoMapper](https://github.com/AutoMapper/AutoMapper) (for mapping between domain entities and resource classes)
 
### How to test

In the last update, I have added [Swagger](https://swagger.io/) to document the API routes, as well as to simplify the way of testing the API. You can run the application and navigate to `/swagger` to see the API documentation:


You can also test the API using a tool such as [Postman](https://www.getpostman.com/). I describe how to use Postman to test the API below.

First of all, clone this repository and open it in a terminal. Then restore all the dependencies and run the project. Since it is configured to use [Entity Framework InMemory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) provider, the project will run without any problems.

As well as it uses Entity Framework SQl Server Integration for Storing the Data captured for TODO Items
Please use the DB scripts as placed in the src folder to create the DB and table for storing ToDo items accordingly.

The Endpoints exposed for TODO API are mentioned below:


$ git clone https://github.com/evgomes/jwt-api.git
$ cd jwt-api/src
$ dotnet restore
$ dotnet run
```

#### Creating users

To create a user, make a `POST` request to `http://localhost:44341/api/users` specifying a test e-mail and password. The result will be a new user with common users permission.

```
{
	"email": "mytest@mytest.com",
	"password": "123456"
}
```


#### Requesting access tokens

To request the access tokens, make a `POST` request to `http://localhost:44341/api/login` sending a JSON object with user credentials. The response will be a JSON object with:

 - An access token which can be used to access protected API endpoints;
 - A request token, necessary to get a new access token when an access token expires;
 - A long value that represents the expiration date of the token.
 
 Access tokens expire after 2 min, and refresh tokens after 2 mins (you can change this in the `appsetings.json`).



#### Accessing protected data


 
With a valid access token in hands, make a `GET` request to one of the endpoints mentioned above with the following header added to your request:

`Authorization: Bearer your_valid_access_token_here`

If you get a token as a common user (a user that has the `Common` role) and make a request to the endpoint for all users, you will get a response as follows:



But if you try to pass this token to the endpoint that requires admin permission, you will get a `403 - forbidden` response:



If you sign in as an admin and make a `GET` request to the admin endpoint, you will receive the following content as response:



If you pass an invalid token to any of the endpoints (a expired one or a token that was changed by hand, for example), you will get a `401 unauthorized` response.



#### Refreshing tokens

Imagine you have a single page application or a mobile app and you do not want the users to have to log in again every time an access token expires. To deal with this, you can get a new token with a valid refresh token. This way, you can keep users logged in without explicitly asking them to sign in again.

To refresh a token, make a `POST` request to `http://localhost:44341/api/token/refresh` passing a valid refresh token and the user's e-mail in the body of the request.

```
{
	"token": "your_valid_refresh_token",
	"userEmail": "user@email.com"
}
```

You will receive a new token if the specified refresh token and e-mail are valid:



If the request token is invalid, you will receive a 400 response:

There are 5 API endpoints that you can test which supports TODO API as mentioned below:

 Get >>>> `http://localhost:44341/api/fetch_all/todoitem: Fetch all saved TODO Item Details from DB
 Get >>>`http://localhost:44341/api/fetch_by_id/todoitem`: Fetch saved TODO Item Details by ID from DB
  PUT >>>`http://localhost:44341/api/update/todoitem: Update the TODO Item by ID to DB 
 POST >> `http://localhost:44341/api/add/todoitem`: Add the TODO Item  deatils to DB
  DELETE>> `http://localhost:44341/api/delete/todoitem: Delete teh TODO Item by ID from DB
 


### Considerations

This API is not matured enough if we consider refactoring of Code as well as having the Design principles in mind ,This tool still requires enhnacements on the case od storing DB Coonection string in a more secured manner as well as teh Logging has to be implemented .The Exceptions were made available but not tested .I would appreciate your comments as well as , and soon will update the code accordingly when I have some available time