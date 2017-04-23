# Genie
### Data Access Layer Generator


Genie is a .Net library that can generate a data access layer for a layered system.

## How It Works

* Takes the given configuration file (JSON) and validates the file.
* Reads the database schema and creates internal modal from it.
* Process the model  and generates file content using T4 templates
* Writes the file content to the disk

## Configuration

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

A connection string that points  to the target database. this connection string is only used to read database meta-data.

### projectPath

Path to the project (Data access layer of the target system).
This path should point to a existing directory.

### baseNamespace 

The base namespace of the data access layer (ex :Example.DataAccess)

### projectFile (optional)

The relative path to the project file from project path. this is an optional property. if this is provided the generator will process the project file.


## The Process Output

you can pass an implemented process output to the Generate method or pass null to ignore output .

An example process output for a console application:

```CS
public class ProcessOutput : IProcessOutput
{
    public void WriteInformation(string content)
    {
        Console.WriteLine(":> " + content);
    }

    public void WriteSuccess(string content)
    {
        Console.Write(":> ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(content);
        Console.ResetColor();
    }

    public void WriteWarning(string content)
    {
        Console.Write(":> ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(content);
        Console.ResetColor();
    }
}
```

# Generated DAL

The generated DAL uses [Dapper](https://github.com/StackExchange/Dapper) (micro ORM) to map objects and to access the database.

Implements unit of work pattern and repository pattern.
each table and view has a model object and a repository.

All repositories can be accessed through the unit of work.

the UnitOfWork, DapperContext, FactoryRepository should be singletons and can be implemented using a DI Container in an upper layer.

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
var rusith = userRepository.Get().Where.GivenName.Contains("Rusith").Filter().Top(1).Query().FirstOrDefault(); // Getting an object
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

There are two functions for each procedure (List and Single)
List is for getting list of objects as the result
Single is for getting single object as the result

```CS
// List
IEnumarable<>UnitOfWork.Procedures.SPC_GetOrders<OrderDetailModel>_List();
// Single
int deleted = UnitOfWork.Procedures.SPC_DeleteOrder<bool>_Single(100);
```

## Querying

DAL provides an easy way to filter objects.
this is a Builder like pattern to filter objects.
there is a QueryContext implemented for all repositories.
user can access it through the `Get()` method of any repository.
the `Get` method returns a QueryContext that is specific for the object type of the repository.


### QueryContext

A QueryContext contains some methods to filter, order, page the result

#### Where

There is a FilterContext that can be used to filter the query 
there is a property for each column and these properties can be used to chain the complete filter expression. each two expressions should be connected using And || Or , however if you do not specify a boolean operator between two expressions it will be `and` by default.

```CS
var repo = UnitOfWork.InvoicesViewRepository;
var filter = repo.Get();

if (!string.IsNullOrWhiteSpace(search))
{
    filter.
        Where.ShipTo.Contains(search).
        Or.FactoryInvoiceNo.Contains(search)
        .Or.No.Contains(search).Filter();
}
...
```

there is a filter for every data type and every filter contains methods to filter the property.
the `Filter()` method returns the parent QueryContext.

#### OrderBy

There is a OrderContext that can be used to order the query 
there is a property for each column and these properties can be used to chain the complete order expression. an order expression can be Descending or Ascending.

```CS
var repo = UnitOfWork.InvoicesViewRepository;
        var filter = repo.Get()
            .OrderBy.ModifiedDate.Ascending().Order();
...
```

the `Order()` method returns the parent QueryContext.

### Page

The `Page` function can be used to add paging to the query.


### Top
Add a limit to the query.

### Skip
Skip can be used to skip certain rows from the query result.

### Take
Skip can be used to Take only certain rows from the query result.
Skip and Take can be used together to page the result.

### Query Function
The `Query` function ends the QueryContext and returns the query result.

### Count Function
The `Count` function returns the count of the result.


## Transactions

You can start a transaction by calling `BeginTransaction` method in the UnitOfWork then you should pass the transaction to all methods you use in repositories. you can use the `Commit` method in the UnitOfWork to commit the transaction.

