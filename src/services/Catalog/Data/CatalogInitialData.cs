using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.OpenSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                ImageFile = "product-1.png",
                Categories = new List<string> { "Electronics" },
                Name = "Keyboard",
                Price = 20,
                Description = "Ergonomic keyboard",
            },
            new Product
            {
                Id = new Guid("d2a9e1f8-1b1c-4b0d-8f1d-3f0b6e1d6b9e"),
                ImageFile = "product-2.png",
                Categories = new List<string> { "Electronics" },
                Name = "Mouse",
                Price = 12,
                Description = "Wireless mouse",
            },
            new Product
            {
                Id = new Guid("d2a9e1f8-1b1c-4b0d-8f1d-3f0b6e2d6b9e"),
                ImageFile = "product-3.png",
                Categories = new List<string> { "Clothing" },
                Name = "T-Shirt",
                Price = 8,
            },
            new Product
            {
                Id = new Guid("d3a9e1f8-1b1c-4b0d-8f1d-3f0b6e1d6b9e"),
                ImageFile = "product-4.png",
                Categories = new List<string> { "Clothing" },
                Name = "Dress",
                Price = 18,
            },
            new Product
            {
                Id = new Guid("d2a9e1f8-1b1c-4b0d-9f1d-3f0b6e1d6b9e"),
                ImageFile = "product-5.png",
                Categories = new List<string> { "Stationery" },
                Name = "Pencil",
                Price = 3,
            },
            new Product
            {
                Id = new Guid("d2a9e1f8-2b2c-4b0d-8f1d-3f0b6e1d6b9e"),
                ImageFile = "product-6.png",
                Categories = new List<string> { "Stationery" },
                Name = "Notebook",
                Price = 6,
            },
        };
    }
}
