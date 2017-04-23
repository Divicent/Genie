# Genie
### Data Access Layer Generator


Genie is a .Net library that can generate a data access layer for a layered system. using a provided configuration.

## How It Works

* Takes the given configuration file (JSON) and validates the file.
* Read the database schema and creates internal modal from it.
* Process the model and crete and generates file content using T4 templates
* Write the file content to the disk

## Confguration

It should be something like this 
```JSON
{
    "connectionString": "...",
    "projectPath": "...",
    "baseNamespace": "...",
    "ProjectFile": "..."
}
```

### connectionString

Connection string is a connection string that points  to the database target database. this connection string is only used to read database meta-data.

### projectPath

Path to the project (Data access layer of the target system).
This path should point to a existing directory.

### baseNamespace 

The base namespace of the data access layer (ex :Example.DataAccess)

### projectFile (optional)

The relative path to the project file from project path. this is a optional property. if this is provided the generator will process the project file.

# Generated DAL

The generated DAL uses [Dapper](https://github.com/StackExchange/Dapper) (micro ORM) to map objects and access the database.

Implements unit of work pattern and repository pattern.
each table and view has a model object and a repository.

All repositoris can be accessed through the unit of work.

the UnitOfWork, DapperContext, FactoryRepository should be sigletons and can be implemented using a DI Container in an upper layer.

```CS
// An example using Ninject
using Example.DataAccess.Infrastructure;
using Example.DataAccess.Infrastructure.Interfaces;
using Ninject.Modules;

namespace Example.Business.Base.Concrete.Modules
{
    internal class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<IDapperContext>().To<DapperContext>().InSingletonScope();
            Bind<IRepositoryFactory>().To<FactoryRepository>().InSingletonScope();
        }
    }
}
```

## Repositories

UnitOfWork can be used to access the repositories.
A repository contains methods to get, add, update, remove database entities

```CS
var userRepository = UnitOfWork.UserRepository; // getting the repository
var users = userRepository.Get().Where.GivenName.Contains("John").Filter().Query().ToList(); // Getting objects
if (users.Count < 1)
    return null;
var user = users.First();
user.GivenName = "Doe";
userRepository.Update(user); // Updating the object . this will only updated properties of the object. (for this  the GivenName property only)
var rusith = userRepository.Get().Where.GivenName.Contains("Rusith").Filter().Top(1).Query().FirstOrDefault(); // Getting the object
userRepository.Remove(rusith); // deleting an object

var newUser = new User {FamilyName = "Example", Username = "e@e.com", GivenName = "User"};
userRepository.Add(newUser); // adding an object
```

## Procedures

All Stored procedures of the the database can be accessed through the UnitOfWork, all the parameters of the methods are nullable and null by default.
A generic parameter should be provided in order to execute a procedure and the result will be mapped to the given class type.

```CS
// Executing a procedure
UnitOfWork.Procedures.SPC_GetOrders<OrderDetailModel>_List(100);
```

There are two functions for each procurdure (List and Single)
List is for getting list of objects as the result
Single is for getting single object as the result

```CS
// List
IEnumarable<>UnitOfWork.Procedures.SPC_GetOrders<OrderDetailModel>_List();
int deleted = UnitOfWork.Procedures.SPC_DeleteOrder<int>_Single(100);
```

A complete code generator that can generate a data access layer for enterprise applications 

- Uses dapper to <-> 
- Unit of work
- Dependency from interfaces
- Transaction management
- SP Support 
- View support
- Implements repository pattern
- Automatic project file management
- Detailed log output
- Fast
- Free , Open source
