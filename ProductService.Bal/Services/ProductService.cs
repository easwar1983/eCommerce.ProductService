using AutoMapper;
using FluentValidation;
using ProductService.Bal.DTO;
using ProductService.Bal.ServiceContract;
using ProductService.Dal.Entities;
using ProductService.Dal.RepositoryContracts;
using System.Linq.Expressions;

namespace ProductService.Bal.Services;
public class ProductService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductService
    (
        IValidator<ProductAddRequest> productAddRequestValidator,
        IValidator<ProductUpdateRequest> productUpdateRequestValidator,
        IMapper mapper,
        IProductsRepository productsRepository
    )
    {
        _productAddRequestValidator = productAddRequestValidator ?? throw new ArgumentNullException(nameof(productAddRequestValidator));
        _productUpdateRequestValidator = productUpdateRequestValidator ?? throw new ArgumentNullException(nameof(productUpdateRequestValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository));
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        var validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException(errors);
        }

        //Attempt to add product
        Products productInputs = _mapper.Map<Products>(productAddRequest);
        Products addedProduct = await _productsRepository.AddProduct(productInputs);

        if (addedProduct == null)
        {
            throw new Exception("Failed to add product. Please try again later.");
        }

        //Map the added product to ProductResponse DTO
        ProductResponse productResponse = _mapper.Map<ProductResponse>(addedProduct);

        return productResponse;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        //Find Product is existing
        Products? productIsExists = await _productsRepository.FetchProductByCondition(temp => temp.ProductId == productId);
        if (productIsExists == null)
        {
            return false;
        }

        // Attempt to delete product
        bool isDeleted = await _productsRepository.DeleteProduct(productId);
        return isDeleted;
    }

    public async Task<ProductResponse?> FetchProductByCondition(Expression<Func<Products, bool>> conditionExpression)
    {
        Products? productIsExists = await _productsRepository.FetchProductByCondition(conditionExpression);
        if (productIsExists == null)
        {
            return null;
        }

        //Map the added product to ProductResponse DTO
        ProductResponse productResponse = _mapper.Map<ProductResponse>(productIsExists);
        return productResponse;
    }

    public async Task<List<ProductResponse?>> FetchProducts()
    {
        IEnumerable<Products?> products = await _productsRepository.FetchProducts();
        if (products == null)
        {
            return null;
        }

        //Map the added product to ProductResponse DTO
        IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return productResponse.ToList();
    }

    public async Task<List<ProductResponse?>> FetchProductsByCondition(Expression<Func<Products, bool>> conditionExpression)
    {
        IEnumerable<Products?> products = await _productsRepository.FetchProductsByCondition(conditionExpression);
        if (products == null)
        {
            return null;
        }

        //Map the added product to ProductResponse DTO
        IEnumerable<ProductResponse?> productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return productResponse.ToList();
    }

    public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        //Find Product is existing
        Products? productIsExists = await _productsRepository.FetchProductByCondition(temp => temp.ProductId == productUpdateRequest.ProductId);
        if (productIsExists == null)
        {
            throw new ArgumentException("Invalid Product Id");
        }

        var validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ArgumentException(errors);
        }

        //Attempt to add product
        Products productInputs = _mapper.Map<Products>(productUpdateRequest);
        Products? updatedProduct = await _productsRepository.UpdateProduct(productInputs);

        if (updatedProduct == null)
        {
            throw new Exception("Failed to update product. Please try again later.");
        }

        //Map the added product to ProductResponse DTO
        ProductResponse productResponse = _mapper.Map<ProductResponse>(updatedProduct);

        return productResponse;
    }
}
