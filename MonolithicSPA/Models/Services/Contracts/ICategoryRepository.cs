using MonolithicSPA.Models.DomainModels.ProductType;

namespace MonolithicSPA.Models.Services.Contracts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
    }
}
