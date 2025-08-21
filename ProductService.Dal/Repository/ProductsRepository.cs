using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Context;
using ProductService.Dal.Entities;
using ProductService.Dal.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductService.Dal.Repository;
public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Products?> AddProduct(Products product)
    {
        try
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        try
        {
            Products? productIsExists = await _dbContext.Products.FirstOrDefaultAsync(temp=>temp.ProductId== productId);
            if (productIsExists == null)
            {
                return false; // Product not found
            }

            _dbContext.Products.Remove(productIsExists);
            int iAffectedRows = await _dbContext.SaveChangesAsync();
            return (iAffectedRows > 0 ? true : false);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<IEnumerable<Products>> FetchProducts()
    {
        try
        {
            return await _dbContext.Products.ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Products?> FetchProductByCondition(Expression<Func<Products, bool>> conditionExpression)
    {
        try
        {
            return await _dbContext.Products.FirstOrDefaultAsync(conditionExpression);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Products?>> FetchProductsByCondition(Expression<Func<Products, bool>> conditionExpression)
    {
        try
        {
            return await _dbContext.Products.Where(conditionExpression).ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Products?> UpdateProduct(Products product)
    {
        try
        {
            Products? productIsExists = await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductId == product.ProductId);
            if (productIsExists == null)
            {
                return null;
            }

            _dbContext.Products.Remove(productIsExists);

            productIsExists.ProductName = product.ProductName;
            productIsExists.Category = product.Category;
            productIsExists.UnitPrice = product.UnitPrice;
            productIsExists.QuantityInStock = product.QuantityInStock;

            int iAffectedRows = await _dbContext.SaveChangesAsync();

            return (iAffectedRows>0? productIsExists : null);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
