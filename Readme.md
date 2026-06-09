# Trainee Management System

A Trainee management system API to manage all trainee records by performing CRUD operations through REST APIs. The backend is in .NET framework with Inmemory Database using Entity Core

## Tech Stack

ASP.NET, OpenAPI / Swagger, EF Core, MySQL


## How to Run


Go to the project directory. First install all required packages.
```bash
  dotnet restore
```

Now Initailize MySQL by adding connection string in either the user secret or in appsettings.json(not reccomended)
Create a DB Context and add below in program.cs

```
builder.Services.AddDbContext<ApiContext>( options =>{
    options.UseMySQL(connectionString);         
});
```
Once MySQL is configured we will have to migrate the existing models into the database 

Firstly install the dotnet ef tool
```
dotnet tool install --global dotnet-ef
```
Now run the first migration
```
dotnet ef migrations add InitialCreate
```
Now we can let EF create the database and the table we require using the following
```
dotnet ef database update
```
Now the setup for the databse is completed


To launch the project in development.
```bash
 dotnet run --launch-profile https    
```
To launch in watch mode.
```bash
  dotnet watch --launch-profile https    
```




## API Reference

#### Health check of application

```http
  GET /api/Health
```

#### Interactive Swagger UI for testing of routes
```http
  GET /swagger
```

#### Get all Trainees with optional search query 

```http
  GET /api/trainees
```

| query | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `search` | `string` | **Optional** It check whether first name, last name, texh stack, email contains search string.  |

#### Get Trainee by Id

```http
  GET /api/trainees/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Id`      | `long` | **Required**. Id of trainee to fetch |

#### Add Trainee 
```http
  POST /api/trainees
```
Request Body
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `firstName`      | `string` | **Required**. First name min 3 max 50. |
| `lastName`      | `string` | **Required**. Last name min 3 max 50 |
| `email`      | `string` | **Required**. Valid email. |
| `techStack`      | `string` | **Required**. |
| `status`      | `string` | **Required**. status in 'Active', 'Inactive','Completed' |

#### Update Trainee 
```http
  PUT /api/trainees/${Id}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Id`      | `long` | **Required**. Id of trainee to update |

Request Body
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `firstName`      | `string` | **Required**. First name min 3 max 50. |
| `lastName`      | `string` | **Required**. Last name min 3 max 50 |
| `email`      | `string` | **Required**. Valid email. |
| `techStack`      | `string` | **Required**. |
| `status`      | `string` | **Required**. status in 'Active', 'Inactive','Completed' |

#### Delete Trainee 
```http
  DELETE /api/trainees/${Id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Id`      | `long` | **Required**. Id of trainee to delete |
## Sample Request JSON

```bash
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "techStack": "string",
  "status": "string"
}
```

## Sample Response JSON

```bash
{
  "status": "bool",
  "message": "string",
  "data"?: "T",
  "error"?: "object"
}
```

## Known limitations

The current storage is In-memory which is not efficient as it in its root is designed for small scale testing only. It can't be kept as the primary database.
The application lacks Error handling.