# Technical Test - Full-Stack

## Setup
1. Download and install .NET Core.

>https://dotnet.microsoft.com/download

2. Download VSCode or an IDE of your choice.

3. Clone the repo locally.

```shell
git clone https://github.com/clinia/technical-test-full-stack.git
```

4. Open the root folder (or the solution file depending on your IDE).

5. Run the project.
```shell
dotnet run -p src/TechnicalTest.Project
```

You can make HTTP calls using curl
```shell
curl http://localhost:5000/home
```

Or using an HTTP client of your choice.

6. Code away!
---
## What to do

### 1. Create the domain
Based on the database files contained in `/src/TechnicalTest/Infrastructure/Database`, create model classes and configure their relationships inside the `TestDbContext` in order to be able to query the database.

The domain should contain at least :
- A `HealthFacility` entity.
- A `Practitioner` entity.
- A `Service` entity.

The relationships between those entities are :
- A `Practitioner` can only work at 1 `HealthFacility`.
- Multiple `Practitioner` can work at the same `HealthFacility`.
- A `Practitioner` offers multiple services.
- A `HealthFacility` offers multiple services.
- A `Service` can be offered in multiples `HealthFacility` and by multiple `Practitioner`.

### 2. Accessing the data
Based on the domain, you must now create at least one repository in order to access the data. Repositories should implement the `IRepository<T>` interface from `/src/TechnicalTest/Infrastructure/Repositories`.

### 3. Use LINQ and LINQ extension methods
Now that you have repositories, you must add a new methods to the `IRepository<T>` interface.

The new methods should allow :
- To get a specific entity while including all its relationships and nested relationships.
- To list multiple entities using paging parameters
- To get a count of entities respecting a certain condition

### 4. Polymorphism
In addition to our domain entities, our service should be able to manipulate `Modality`. Based on the `modalities.json` file in `/src/TechnicalTest/Stores/Data`, create a polymorphic class structure where a `Modality` can be either a `PaymentModality` or a `TreatmentModality`. 

You also need to add configuration in order for our service to be ablo to parse the content of `modalities.json` using the `GenericStore`.

### 5. Make it an API
Now that the basics are down, you need to expose all of this through an API that should allow to :

- Get, List, Create, Update, Delete all types of entities in the domain.
- List all `Modality`

This step is purposely vague. Show us how you would go about this!



