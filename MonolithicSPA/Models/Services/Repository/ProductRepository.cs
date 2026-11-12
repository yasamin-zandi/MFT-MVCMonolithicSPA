using Microsoft.EntityFrameworkCore;
using MonolithicSPA.Models.DomainModels.ProductAggregate;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.Models.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProjectDbContext _projectDbContext;

        #region Ctor
        public ProductRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            try
            {
                var product = await _projectDbContext.Product.FindAsync(id);

                if (product != null)
                {
                    _projectDbContext.Product.Remove(product);
                    await _projectDbContext.SaveChangesAsync();
                }

            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(Product product)
        {
            try
            {
                if (product != null)
                {
                    _projectDbContext.Product.Remove(product);
                    await _projectDbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Insert

        public async Task Insert(Product product)
        {
            try
            {
                _projectDbContext.Product.Add(product);
                await _projectDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Update
        public async Task Update(Product product)
        {
            try
            {
                _projectDbContext.Product.Update(product);
                await _projectDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region selectAll
        public async Task<List<Product>> SelectAll()
        {
            try
            {

                return await _projectDbContext.Product.Include(p => p.Category).ToListAsync();

            }

            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Select-by-Id
        public async Task<Product?> SelectById(int id)
        {
            try
            {
                return await _projectDbContext.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
