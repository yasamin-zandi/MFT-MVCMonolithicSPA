using MonolithicSPA.Models.DomainModels.ProductAggregate;

namespace MonolithicSPA.Models.Services.Contracts
{
    public interface IProductRepository
    {

        #region Delete
        Task Delete(int id);
        Task Delete(Product product);
        #endregion


        #region Insert
        Task Insert(Product product);
        #endregion

        #region Update
        Task Update(Product product);
        #endregion

        #region selectAll
        Task<List<Product>> SelectAll();
        #endregion


        #region select-by-id

        Task<Product?> SelectById(int id);
        #endregion



    }
}
