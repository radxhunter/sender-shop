﻿using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Product product)
        => await _context.Products.InsertOneAsync(product);

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
                                            .Products
                                            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
        => await _context.Products.Find(p => p.Id == id).SingleOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetProductByCategory(string category)
    {
        return await _context.Products.Find(p => p.Category.Contains(category)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _context.Products.Find(p => p.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
        => await _context.Products.Find(p => true).ToListAsync();

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context
                                      .Products
                                      .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

        return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
    }
}
