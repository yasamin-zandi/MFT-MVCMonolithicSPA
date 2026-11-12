using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Dtos;
using MonolithicSPA.Models.DomainModels.ProductAggregate;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.ApplicationService.Services
{
    public class ProductApplicationService : IProductApplicationService
    {



        #region ctor


        private readonly IProductRepository _productRepository;
        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Post
        public async Task PostProduct(PostProductDto postProductDto)
        {
            try
            {
                var product = new Product()
                {
                    Title = postProductDto.Title,
                    CategoryId = postProductDto.CategoryId,
                    Count = postProductDto.Count
                };
                await _productRepository.Insert(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Edit
        public async Task EditProduct(EditProductDto editProductDto)
        {
            try
            {
                var product = await _productRepository.SelectById(editProductDto.Id);
                if (product == null)
                    throw new KeyNotFoundException("شخص پیدا نشد!");

                product.Title = editProductDto.Title;
                product.Count = editProductDto.Count;
                product.CategoryId = editProductDto.CategoryId;

                await _productRepository.Update(product);
            }
            catch
            {

            }
        }
        #endregion

        #region Delete
        public async Task DeleteProduct(int id)
        {
            try
            {
                await _productRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }


        public async Task DeleteProduct(EditProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    Id = productDto.Id,
                    Title = productDto.Title,
                    Count = productDto.Count
                };
                await _productRepository.Delete(product);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region GetAll
        public async Task<List<GetProductDto>> GetAllProducts()

        {
            try
            {
                var products = await _productRepository.SelectAll();
                var getProductsDtos = new List<GetProductDto>();

                foreach (var product in products)
                {

                    var productDto = new GetProductDto()
                    {
                        Id = product.Id,
                        Title = product.Title,
                        CategoryId = product.CategoryId,
                        Count = product.Count,
                        CategoryTitle = product.Category.Title,
                    };
                    getProductsDtos.Add(productDto);
                }
                return getProductsDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetById
        public async Task<GetProductDto?> GetProductbyId(int id)
        {

            var product = await _productRepository.SelectById(id);
            if (product == null) return null;
            return new GetProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Count = product.Count,
                CategoryId = product.CategoryId,
                CategoryTitle = product.Category.Title
            };
        }
        #endregion


    }

}
