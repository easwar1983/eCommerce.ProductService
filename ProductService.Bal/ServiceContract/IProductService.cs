using ProductService.Bal.DTO;
using ProductService.Dal.Entities;
using System.Linq.Expressions;

namespace ProductService.Bal.ServiceContract;
public interface IProductService
{
    /// <summary>
    /// Retrieves the list of products from the products repository.
    /// </summary>
    /// <returns>Returns list of Products</returns>
    Task<List<ProductResponse?>> FetchProducts();

    /// <summary>
    /// Retrieves list of products matching with given expression.
    /// </summary>
    /// <param name="conditionExpression">Expression thant represents condition to check</param>
    /// <returns>Returns list of matching Products</returns>
    Task<List<ProductResponse?>> FetchProductsByCondition(Expression<Func<Products, bool>> conditionExpression);

    /// <summary>
    /// Retriev a single product matching with given expression.
    /// </summary>
    /// <param name="conditionExpression">Expression thant represents condition to check</param>
    /// <returns>Returns a single matching Products</returns>
    Task<ProductResponse?> FetchProductByCondition(Expression<Func<Products, bool>> conditionExpression);

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="productAddRequest">The product details to add.</param>
    /// <returns>The added product details.</returns>
    Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="request">The updated product details.</param>
    /// <returns>The updated product details.</returns>
    Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="productId">The ID of the product to delete.</param>
    /// <returns>Returns true if the deletion is successful other false</returns>
    Task<bool> DeleteProduct(Guid productId);
}