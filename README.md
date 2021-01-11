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

### 1. Accessing the data
Based on the domain, you must create at least one repository in order to access the data. Repository(ies) should implement the `IRepository<T>` interface from `/src/TechnicalTest/Infrastructure/Repositories`.

### 2. Use LINQ and LINQ extension methods
Now that you have repositories, you must add a new methods to the `IRepository<T>` interface and implement them.

The new methods should allow :
- To get a specific entity while including all its relationships and nested relationships.
- To list multiple entities using paging parameters
- To get a count of entities respecting a certain condition

### 3. Polymorphism
In addition to our domain entities, our API should be able to manipulate `Modality`. Based on the `modalities.json` file in `/src/TechnicalTest/Stores/Data`, create a polymorphic class structure where a `Modality` can be either a `PaymentModality` or a `TreatmentModality`. 

Modalities should be accessible using the already implemented generic `ReadAll` method in the `GenericStore`.

### 4. Make it an API
Now that the basics are down, you need to expose all of this through an API that should allow to :

- Get, List, Create, Update, Delete all types of entities in the domain.
- List all `Modality`

This step is purposely vague. Show us how you would go about this!



