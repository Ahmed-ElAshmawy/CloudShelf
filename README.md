# CloudShelf

This is ASP.NET Core WebAPI project using the latest .NET 8 SDK, it exposed 3 end REST endpoints

## TL;DR

To get started right away:

* clone the project locally
* start the development server with `http profile` selected
* this will start the project and launch it on the browser showing `swagger` on `http://localhost:5290/swagger/index.html`

## What You're Getting
```bash
├── README.md - This file.
├── .gitignore # specifies intentionally untracked files that Git should ignore.
└── CloudShift.Api
│    ├── Controllers # Controllers folder.
│    ├── logs # logs folder contains errors logged into file.
│    └── appsettings.json # Used to hold configurations.
└── CloudShift.Application
│    ├── Services # Services folder contains Services that wrap the business logic.
│    └── Repositories # Repositories folder contains BaseRepository which is generic Repository to retrieve data from data source.
└── CloudShift.Domain
│    ├── IEntity.cs # interface contains Id will be implemented by entity models.
│    ├── Model # folder contains dmoain entity models.
│    ├── CloudShiftDbContext.cs # DbContext responsible for operations on DbSet.
│    └── SeedData.cs # static class for seeding initial data.
└── CloudShift.Kernel
│    ├── Application # Used for shared functionality related to application layer.
│    ├── Controllers # Used for shared functionality related to controllers layer.
│    └── Middleware  # Used for shared functionality related to middlewares like ExceptionHandlingMiddleware.
└── CloudShift.Application.Tests
    └── Services # Used for test services related to application layer.
```

## Endpoints

* [`api/GuidEntities/single`](#singleGuid)
* [`api/OrderItems/invoice`](#orderItemsInvoice)
* [`api/Todos`](#todos)

### `GuidEntities/single`

Method Signature:

```js
GetGuid()
```

* `HttpGet` endpoint.
* Returns a dto contains `guidValue` with value of `guid`, retrieved form `InMemoryDatabase`.
* Endpoint allow anonymous calls.

Response sample
```
{
  "guidValue": "25a4c3ad-8bd8-4781-9e1e-1e32f62c1409"
}
```

### `OrderItems/invoice`

Method Signature:

```js
OrderItemsInvoice(List<OrderItemDto> orderItems)
```

* `HttpPost` endpoint.
* endpoint accepts list of `orderItems` from request body, each `orderItem` has `ItemId`, `UnitPrice`, `Quantity`.
* returns `OrderTaxDetailsDto` contains `TotalOrderPrice`, `CalculatedTax`, after calculating the sum of each order `UnitPrice *  Quantity`, and `15% tax` value.
* Endpoint allow anonymous calls.

Request sample
```
[
  {
    "itemId": 1,
    "unitPrice": 10,
    "quantity": 5
  },
  {
    "itemId": 2,
    "unitPrice": 10,
    "quantity": 5
  }
]
```

Response sample
```
{
  "totalOrderPrice": 100,
  "calculatedTax": 15
}
```

### `Todos`

Method Signature:

```js
GetTodos()
```

* `HttpGet` endpoint.
* endpoint calls an external API `https://jsonplaceholder.typicode.com/todos` to get the result as JSON.
* returns list of `TodoDto`, each `Todo` contains `UserId`, `Id`, `Title`, `Completed`.
* Endpoint requires Authorization to be called.

Response sample
```
[
  {
    "userId": 1,
    "id": 1,
    "title": "delectus aut autem",
    "completed": false
  },
  {
    "userId": 1,
    "id": 2,
    "title": "quis ut nam facilis et officia qui",
    "completed": false
  }
]
```

### Note
The database used in the application is `InMemoryDatabase`, that will be seeded when application runs with 2 records of `GuidEntity` which has the following class structure
```
{
    public int Id { get; set; }
    public Guid GuidValue { get; set; } = Guid.NewGuid();
}
```





