using FluentValidation;
using FluentValidation.Results;
using ProductService.Bal.DTO;
using ProductService.Bal.ServiceContract;
using ProductService.Bal.Validators;

namespace ProductService.Api.ApiEndPoints;
public static class ProductApiEndPoints
{
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            List<ProductResponse?> products = await productService.FetchProducts();
            //return Results.Ok(products);
            return ((products is not null && products.Count > 0) ? Results.Ok(products) : Results.NotFound("No records in product"));
        });

        //GET /api/products/search/product-id/{ProductId}(0000000-0000-0000-0000-000000000000)
        app.MapGet("/api/products/search/product-id/{ProductId:guid}", async (IProductService productService, Guid ProductId) =>
        {
            ProductResponse? product = await productService.FetchProductByCondition(temp => temp.ProductId == ProductId);
            return product is not null ? Results.Ok(product) : Results.NotFound("No records in product");
        });

        //GET /api/products/search/{SearchString}()
        app.MapGet("/api/products/search/{SearchString}", async (IProductService productService, string SearchString) =>
        {
            List<ProductResponse?> productByProductName = await productService.FetchProductsByCondition
                                                (
                                                    temp => temp.ProductName != null &&
                                                            temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
                                                );

            List<ProductResponse?> productByCategory = await productService.FetchProductsByCondition
                                                (
                                                    temp => temp.Category != null &&
                                                            temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase)
                                                );

            var products = productByProductName.Concat(productByCategory).Distinct().ToList();

            return products is not null ? Results.Ok(products) : Results.NotFound();
        });

        //POST /api/products
        app.MapPost("/api/products", async (IProductService productService, IValidator<ProductAddRequest> productAddRequestValidator, ProductAddRequest productAddRequest) =>
        {
            // Validate the product request using FluentValidation
            ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                     .ToDictionary(group => group.Key, group => group.Select(err => err.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var addedProductResponse = await productService.AddProduct(productAddRequest);
            if (addedProductResponse is null)
                return Results.Problem("Failed to add product. Please try again later.");
            else
                return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductId}", addedProductResponse);
        });

        //PUT /api/products
        app.MapPut("/api/products", async (IProductService productService, IValidator<ProductUpdateRequest> productUpdateRequestValidator, ProductUpdateRequest productUpdateRequest) =>
        {
            // Validate the product request using FluentValidation
            ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(e => e.PropertyName)
                     .ToDictionary(group => group.Key, group => group.Select(err => err.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var updatedProductResponse = await productService.UpdateProduct(productUpdateRequest);
            if (updatedProductResponse is null)
                return Results.Problem("Failed to update product. Please try again later.");
            else
                return Results.Created($"/api/products/search/product-id/{updatedProductResponse.ProductId}", updatedProductResponse);
        });

        //DELETE /api/products/{ProductId}(0000000-0000-0000-0000-000000000000)
        app.MapDelete("/api/products/{ProductId:guid}", async (IProductService productService, Guid ProductId) =>
        {
            bool isDeleted = await productService.DeleteProduct(ProductId);
            return isDeleted ? Results.Ok(true) : Results.Problem("Failed to delete product. Please try again later.");
        });

        return app;
    }
}
