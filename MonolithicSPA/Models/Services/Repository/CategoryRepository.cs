using Microsoft.EntityFrameworkCore;
using MonolithicSPA.Models.DomainModels.ProductType;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.Models.Services.Repository
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly ProjectDbContext _projectDbContext;
        public CategoryRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await _projectDbContext.Category.ToListAsync();
        }
    }
}
