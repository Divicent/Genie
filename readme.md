<h1>
<img src="https://raw.githubusercontent.com/rusith/genie/develop/icon/genie.png" alt="Icon" style="width: 200px;"/> Genie
</h1>

[![Build Status](https://travis-ci.org/rusith/Genie.svg?branch=core)](https://travis-ci.org/rusith/Genie)

### Data Access Layer Generator

Genie is a cross platform .Net library  that can generate a data access layer for any .Net or .Net Core system.


## Getting Started

Download latest release or build the project
run GenieCLI from the genieSetting file's location 

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
    "ProjectFile": "...",
    "noDapper": false,
    "core": false,
    "enums": [
        {
            "table": "",
            "valueColumn": "",
            "nameColumn": "",
            "type": ""
        },
    ]
}
```

### connectionString

A connection string that points  to the target database. this connection string is only used to read database meta-data.

### projectPath

Path to the project (Data access layer of the target system).
This path should point to a existing directory.

### baseNamespace 

The base namespace of the data access layer (ex :Example.DataAccess)

### noDapper

Include the dapper source code in the generated code (/Dapper)
Dapper will not be included if set to true . will need to reference dapper externally.

### core

If this is set to true , the generated code will target .net core framework.
This uses .Net cores inbuilt DI. the DapperContext should be injected to use connection string.

### projectFile (optional)

The relative path to the project file from project path. this is an optional property. if this is provided the generator will process the project file.


### enums (optional)

List of table details that can be used as enums. if a table has predefined set of rows which need to be  accessed often,this table can be used as an enum table.

example :

the OrderType table (ID, Name) has 3 rows and these rows do not change. we can use this table as an enum table and can be defined in the configuration file as 
```
{
    "table": "OrderType",
    "valueColumn": "ID",
    "nameColumn": "Name",
    "type": "int"
}
```
* type can be string / int / bool or double

this will result a class which has static members for each row and and those members can be implicitly converted to the specified type.

the resulting class will be  something like this.

```CS
public sealed class OrderTypeEnum
{
    private readonly int _value;
    private OrderTypeEnum(int value)
    {
        _value = value;
    }

    public static implicit operator int(OrderTypeEnum @enum)
    {
        return @enum._value;
    }

    private static OrderTypeEnum _shipped;
    private static OrderTypeEnum _preShipped;
    private static OrderTypeEnum _pending;

    public static OrderTypeEnum Shipped => _shipped ?? ( _shipped = new OrderTypeEnum(1));
    
    public static OrderTypeEnum PreShipped => _preShipped ?? ( _preShipped = new OrderTypeEnum(2));

    public static OrderTypeEnum Pending => _pending ?? ( _pending = new OrderTypeEnum(3));
}
```

# Generated DAL

The generated DAL uses [Dapper](https://github.com/StackExchange/Dapper) (micro ORM) to map objects and to access the database.

Implements unit of work pattern and repository pattern.
each table and view has a model object and a repository.

All repositories can be accessed through a unit of work.

the DapperContext,should be singleton.
UnitOfWork, DapperContext can be implemented using a DI Container in an upper layer.

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
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IDapperContext>().To<DapperContext>().InSingletonScope();
        }
    }
}
```

## Repositories

UnitOfWork can be used to access the repositories.
Unit() function in the DapperContext can be used to create new Unit
A repository contains methods to get, add, update, remove database entities
all unit of work objects must be used in a using block.

a unit has the Commit method that can be used to commit changes to the database
```CS
using(var unit = Context.Unit())
{
    var userRepository = unit.UserRepository; // getting the repository
    var users = userRepository.Get().Where.GivenName.Contains("John").Filter().Query().ToList(); // Getting objects
    if (users.Count < 1)
        return null;
    var user = users.First();
    user.GivenName = "Doe"; // Updating the object .will update when committing changes 
    userRepository.Update(user); 
    var rusith = userRepository.Get().Where.GivenName.Contains("Rusith").Filter().Top(1).Query().FirstOrDefault(); // Getting an object
    userRepository.Remove(rusith); // deleting an object

    var newUser = new User {FamilyName = "Example", Username = "e@e.com", GivenName = "User"};
    userRepository.Add(newUser); // adding an object

    unit.Commit();
}

```

## Procedures

All Stored procedures of the the database can be accessed through the Procedures object of any Unit Of Work object, all the parameters of the methods are nullable and null by default.
A generic parameter should be provided in order to execute a procedure and the result will be mapped to the given class type.

```CS
// Executing a procedure
unit.Procedures.SPC_GetOrders<OrderDetailModel>_List(100);
```

There are three functions for each procedure (List, Single and Void)
List is for getting list of objects as the result
Single is for getting single object as the result
Void is for no result

```CS
// List
IEnumarable<>unit.Procedures.SPC_GetOrders_List<OrderDetailModel>();
// Single
int deleted = unit.Procedures.SPC_DeleteOrder_Single<bool>(100);
// Void
unit.Procedures.SPC_DeleteOrder_Void(100);
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

### Filter

This function needs an `IEnumerable<IPropertyFilter>` which is a collection of property  names, operations and values(if necessary) . this method can be used to filter the result in a customizable way 
    How the operations are filtered and what are the operations : 
```C#
  switch (type.ToLower())
    {
        case "equals":
        case "eq":
            return QueryMaker.EqualsTo(propName, value, quoted);
        case "notequals":
        case "neq":
        case "ne":
            return QueryMaker.NotEquals(propName, value, quoted);
        case "contains":
        case "c":
            return QueryMaker.Contains(propName, value);
        case "notcontains":
        case "nc":
            return QueryMaker.NotContains(propName, value);
        case "startswith":
        case "sw":
            return QueryMaker.StartsWith(propName, value);
        case "notstartswith":
        case "nsw":
            return QueryMaker.NotStartsWith(propName, value);
        case "endswith":
        case "ew":
            return QueryMaker.EndsWith(propName, value);
        case "notendswith":
        case "new":
            return QueryMaker.NotEndsWith(propName, value);
        case "isempty":
        case "ie":
            return QueryMaker.IsEmpty(propName);
        case "isnotempty":
        case "ino":
            return QueryMaker.IsNotEmpty(propName);
        case "isnull":
        case "in":
            return QueryMaker.IsNull(propName);
        case "isnotnull":
        case "inn":
            return QueryMaker.IsNotNull(propName);
        case "greaterthan":
        case "gt":
            return QueryMaker.GreaterThan(propName, value, quoted);
        case "lessthan":
        case "lt":
            return QueryMaker.LessThan(propName, value, quoted);
        case "greaterthanorequals":
        case "gtoe":
        case "gte":
            return QueryMaker.GreaterThanOrEquals(propName, value, quoted);
        case "lessthanorequals":
        case "ltoe":
        case "lte":
            return QueryMaker.LessThanOrEquals(propName, value, quoted);
        case "istrue":
        case "it":
            return QueryMaker.IsTrue(propName);
        case "isfalse":
        case "if":
            return QueryMaker.IsFalse(propName);
        default:
            return "";
    }
```

## Transactions

You can start a transaction by calling `BeginTransaction` method in the UnitOfWork then you should pass the transaction to all methods you use in repositories. you can use the `Commit` method in the UnitOfWork to commit the transaction.

