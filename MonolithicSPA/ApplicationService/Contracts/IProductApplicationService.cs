using MonolithicSPA.ApplicationService.Dtos;

namespace MonolithicSPA.ApplicationService.Contracts
{
    public interface IProductApplicationService
    {

        #region Post

        Task PostProduct(PostProductDto postProductDto);


        #endregion

        #region Edit
        Task EditProduct(EditProductDto editProductDto);
        #endregion

        #region Delete

        Task DeleteProduct(int id);
        Task DeleteProduct(EditProductDto productDto);
        #endregion

        #region SelectAll

        Task<List<GetProductDto>> GetAllProducts();
        #endregion

        #region get-by-id
        Task<GetProductDto?> GetProductbyId(int id);
        #endregion


    }
}
