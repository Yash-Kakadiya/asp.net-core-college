# Lab 19 – API Versioning in ASP.NET Core Web API

## 1. What is API Versioning?

**API Versioning** is the practice of managing and maintaining multiple versions of an API simultaneously. It allows you to introduce breaking changes, new features, or improvements to your API without disrupting existing clients that rely on the older version.

Think of it like software releases — just as a mobile app has version 1.0, 2.0, etc., your API can expose `v1`, `v2`, and so on, each behaving differently.

### Key Concepts

- **Current Version** → The latest, actively developed version of the API.
- **Deprecated Version** → An older version still supported but scheduled for removal.
- **Sunset Version** → A version that is no longer available.

---

## 2. Why is API Versioning Needed?

| Problem Without Versioning | How Versioning Solves It |
| -------------------------- | ------------------------ |
| Changes break existing clients | Old clients continue using `v1` while new clients use `v2` |
| Cannot safely refactor response models | Change the model in `v2` without touching `v1` |
| Cannot remove deprecated fields | Mark them deprecated in `v2`, remove in `v3` |
| Rolling out features gradually is hard | Ship new endpoints only in the new version |
| Mobile apps cannot be force-updated instantly | They keep calling `v1` until users update |

### Real-World Scenario

You have a `GET /api/users` endpoint in production returning:

```json
{ "name": "John", "age": 25 }
```

Now you want to return:

```json
{ "firstName": "John", "lastName": "Doe", "dateOfBirth": "1999-01-15" }
```

Changing the existing endpoint would **break all existing clients**. API versioning lets you expose the new response shape under `v2` while `v1` continues to work.

---

## 3. Types of API Versioning

There are four widely used strategies to version an API:

### 3.1 URL Path Versioning

The version number is embedded directly in the URL path.

```
GET /api/v1/products
GET /api/v2/products
```

**Pros:** Simple, visible, and easy to test in a browser.  
**Cons:** Pollutes the URL structure; violates the idea that a URI should identify a resource, not a version.

---

### 3.2 Query String Versioning

The version is passed as a query parameter.

```
GET /api/products?api-version=1.0
GET /api/products?api-version=2.0
```

**Pros:** Simple to implement; does not change the URL structure.  
**Cons:** Easy to forget; can conflict with other query parameters.

---

### 3.3 HTTP Header Versioning

The version is passed in a custom HTTP request header.

```
GET /api/products
Headers:
  X-API-Version: 1.0
```

**Pros:** Keeps URLs clean; headers are the semantically correct place for metadata.  
**Cons:** Not visible in the browser address bar; harder to test without tools like Postman.

---

### 3.4 Media Type Versioning (Content Negotiation)

The version is embedded in the `Accept` header using a custom media type.

```
GET /api/products
Headers:
  Accept: application/json;v=2.0
```

**Pros:** Most REST-pure approach; version is part of content negotiation.  
**Cons:** Complex to implement and consume; poor tooling support.

---

### Comparison Table

| Strategy | Example | Visibility | Ease of Testing | REST Purity |
| -------- | ------- | ---------- | --------------- | ----------- |
| URL Path | `/api/v1/products` | High | Easy (browser) | Low |
| Query String | `?api-version=1.0` | Medium | Easy | Medium |
| HTTP Header | `X-API-Version: 1.0` | Low | Requires tooling | High |
| Media Type | `Accept: application/json;v=1.0` | Low | Complex | Highest |

> **Recommendation:** **URL Path Versioning** is the most commonly used approach in real-world ASP.NET Core projects due to its clarity and simplicity.

---

## 4. Code Example – URL Path Versioning in ASP.NET Core Web API with EF Core

We will implement URL path versioning (`/api/v1/products` and `/api/v2/products`) backed by a real SQL Server database using EF Core. Both versions read from the **same `Products` table** — V1 exposes only basic fields while V2 exposes the full schema and an extra endpoint.

---

### Step 1: Create ASP.NET Core Web API Project

```bash
dotnet new webapi -n ApiVersioningDemo
cd ApiVersioningDemo
```

---

### Step 2: Install Required NuGet Packages

```bash
# API Versioning
dotnet add package Asp.Versioning.Mvc
dotnet add package Asp.Versioning.Mvc.ApiExplorer

# EF Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

> **Note:** The older `Microsoft.AspNetCore.Mvc.Versioning` package is **deprecated**. Always use `Asp.Versioning.Mvc` for .NET 6+.

---

### Step 3: Configure `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ApiVersioningDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

### Step 4: Configure `Program.cs`

```csharp
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);            // Default: v1.0
    options.AssumeDefaultVersionWhenUnspecified = true;           // Use default if no version specified
    options.ReportApiVersions = true;                             // Add api-supported-versions to response headers
    options.ApiVersionReader = new UrlSegmentApiVersionReader();  // Read version from URL path
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### Explanation

| Registration | Purpose |
| ------------ | ------- |
| `AddDbContext<AppDbContext>` | Registers EF Core context with DI container |
| `DefaultApiVersion` | Fallback version when none is specified |
| `AssumeDefaultVersionWhenUnspecified` | Prevents 400 errors for clients not sending a version |
| `ReportApiVersions` | Adds `api-supported-versions` header to every response |
| `UrlSegmentApiVersionReader` | Tells the framework to read the version from the URL segment |

---

### Step 5: Create the Product Entity

This is the **single EF Core entity** mapped to the `Products` table. Both API versions read from the same table.

```csharp
// Models/Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }    // Used in v2
    public bool IsAvailable { get; set; }   // Used in v2
}
```

---

### Step 6: Create `AppDbContext`

```csharp
// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed initial data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop",   Price = 75000, Category = "Electronics", IsAvailable = true  },
            new Product { Id = 2, Name = "Mouse",    Price = 1200,  Category = "Accessories", IsAvailable = true  },
            new Product { Id = 3, Name = "Keyboard", Price = 2500,  Category = "Accessories", IsAvailable = false }
        );
    }
}
```

---

### Step 7: Create Migration and Update Database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### Step 8: Create V1 Controller

V1 returns only `Id`, `Name`, and `Price` — the basic fields.

```csharp
// Controllers/V1/ProductsController.cs
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVersioningDemo.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/v1/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Select(p => new { p.Id, p.Name, p.Price })
                .ToListAsync();

            return Ok(products);
        }

        // GET api/v1/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new { p.Id, p.Name, p.Price })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound(new { message = $"Product with id {id} not found." });

            return Ok(product);
        }
    }
}
```

---

### Step 9: Create V2 Controller

V2 returns all fields and adds a new `/available` endpoint.

```csharp
// Controllers/V2/ProductsController.cs
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVersioningDemo.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/v2/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // GET api/v2/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound(new { message = $"Product with id {id} not found." });

            return Ok(product);
        }

        // GET api/v2/products/available  ← New endpoint only in v2
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var available = await _context.Products
                .Where(p => p.IsAvailable)
                .ToListAsync();

            return Ok(available);
        }
    }
}
```

---

### Step 10: Final Project Structure

```
ApiVersioningDemo/
├── Controllers/
│   ├── V1/
│   │   └── ProductsController.cs   ← Returns Id, Name, Price only
│   └── V2/
│       └── ProductsController.cs   ← Returns all fields + /available endpoint
├── Data/
│   └── AppDbContext.cs             ← EF Core DbContext with seed data
├── Models/
│   └── Product.cs                  ← Single entity mapped to Products table
├── Migrations/
│   └── ...                         ← Auto-generated by EF Core
├── Program.cs
└── appsettings.json
```

---

## 5. Testing the API

Run the application:

```bash
dotnet run
```

### Test V1 Endpoints

| Request | Expected Response |
| ------- | ----------------- |
| `GET /api/v1/products` | List with `Id`, `Name`, `Price` |
| `GET /api/v1/products/1` | Single product `{ id, name, price }` |

**V1 Response Sample:**

```json
[
  { "id": 1, "name": "Laptop",   "price": 75000 },
  { "id": 2, "name": "Mouse",    "price": 1200  },
  { "id": 3, "name": "Keyboard", "price": 2500  }
]
```

---

### Test V2 Endpoints

| Request | Expected Response |
| ------- | ----------------- |
| `GET /api/v2/products` | List with `Id`, `Name`, `Price`, `Category`, `IsAvailable` |
| `GET /api/v2/products/1` | Single product with all fields |
| `GET /api/v2/products/available` | Only products where `IsAvailable = true` |

**V2 Response Sample:**

```json
[
  { "id": 1, "name": "Laptop",   "price": 75000, "category": "Electronics", "isAvailable": true  },
  { "id": 2, "name": "Mouse",    "price": 1200,  "category": "Accessories", "isAvailable": true  },
  { "id": 3, "name": "Keyboard", "price": 2500,  "category": "Accessories", "isAvailable": false }
]
```

---

### Verify Response Headers

When `ReportApiVersions = true`, every response will include:

```
api-supported-versions: 1.0, 2.0
```

---

## 6. Deprecating an Old Version

To mark `v1` as deprecated (supported but discouraged):

```csharp
[ApiVersion("1.0", Deprecated = true)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    // ...
}
```

This adds a response header informing clients:

```
api-deprecated-versions: 1.0
api-supported-versions: 2.0
```

Clients still calling `v1` will receive the response but are notified to upgrade.

---

## 7. Summary

| Concept | Detail |
| ------- | ------ |
| **API Versioning** | Managing multiple versions of an API simultaneously |
| **URL Path (used here)** | Version in URL: `/api/v1/`, `/api/v2/` |
| **Query String** | Version as param: `?api-version=1.0` |
| **HTTP Header** | Version in header: `X-API-Version: 1.0` |
| **Media Type** | Version in Accept header: `application/json;v=1.0` |
| **Versioning Package** | `Asp.Versioning.Mvc` (for .NET 6+) |
| **EF Core Package** | `Microsoft.EntityFrameworkCore.SqlServer` |
| **Reader** | `UrlSegmentApiVersionReader` for URL path strategy |
| **Single Entity** | One `Product` table; both versions query the same DB |
| **V1 vs V2 Difference** | V1 projects only basic fields; V2 returns all fields + new endpoint |
| **Deprecation** | `Deprecated = true` on `[ApiVersion]` attribute |
