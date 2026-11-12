using MonolithicSPA.ApplicationService.Contracts;
using MonolithicSPA.ApplicationService.Dtos;
using MonolithicSPA.Models.Services.Contracts;

namespace MonolithicSPA.ApplicationService.Services
{
    public class CategoryApplicationService : ICategoryApplicationService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryApplicationService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #region Select All category
        public async Task<List<GetCategoryDto>> GetCategoriesSelectList()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return categories.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Title = c.Title
            }).ToList();
        }
        #endregion
    }
}
