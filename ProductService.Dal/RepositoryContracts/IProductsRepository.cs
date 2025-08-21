using ProductService.Dal.Entities;
using System.Linq.Expressions;

namespace ProductService.Dal.RepositoryContracts;
/// <summary>
/// Represents a repository for managing products.
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>A list of all products.</returns>
    Task<IEnumerable<Products>> FetchProducts();

    /// <summary>
    /// Retrieves all products based on the specified condition expression.
    /// </summary>
    /// <param name="conditionExpression">The condition expression of the products to retrieve.</param>
    /// <returns>A list of all condition expression matching products.</returns>
    Task<IEnumerable<Products?>> FetchProductsByCondition(Expression<Func<Products,bool>>conditionExpression);

    /// <summary>
    /// Retrieves product based on the specified condition expression.
    /// </summary>
    /// <param name="conditionExpression">The condition expression of the product to retrieve.</param>
    /// <returns>A single product based on condition expression.</returns>
    Task<Products?> FetchProductByCondition(Expression<Func<Products, bool>> conditionExpression);

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="product">The product to add.</param>
    /// <returns>The added product.</returns>
    Task<Products?> AddProduct(Products product);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="product">The product to update.</param>
    /// <returns>The updated product.</returns>
    Task<Products?> UpdateProduct(Products product);

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="productId">The ID of the product to delete.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteProduct(Guid productId);
}
